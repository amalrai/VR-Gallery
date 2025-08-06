using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using yoyohan;
using static DataJsonClass;

public class PopupWindowCanvasManager : MonoBehaviour
{
    private static PopupWindowCanvasManager _instance;
    public static PopupWindowCanvasManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PopupWindowCanvasManager)) as PopupWindowCanvasManager;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = (PopupWindowCanvasManager)obj.AddComponent(typeof(PopupWindowCanvasManager));
                }
            }
            return _instance;
        }
    }

    public PopupWindow3DState popupState;

    public TextLanguageReplace[] textReplaces;

    public CanvasGroup PopupWindow3D;
    public Button PopupWindow3DMaskBG;
    public Text PopupWindow3DTextYifasong;
    public Text PopupWindow3DTextYouxianggeshi;

    public CanvasGroup UpdateCanvasGroup;
    public Button UpdateCanvasGroupButton;

    public CanvasGroup DownloadFileCanvasGroup;
    public Button DownloadFileCanvasGroupButton;
    public Text DownloadFileCanvasGroupText;

    public Button DownloadFileCanvasGroupExitButton;

    public CanvasGroup DownloadingExitAccountCanvasGroup;
    public Button DownloadingExitAccountCanvasGroupButton;
    public Button DownloadingExitAccountCanvasGroupExitButton;

    public CanvasGroup ExitAccountCanvasGroup;
    public Button ExitAccountCanvasGroupButton;
    public Button ExitAccountCanvasGroupExitButton;

    public CanvasGroup DownloadSuccessCanvasGroup;
    public Button DownloadSuccessCanvasGroupButton;
    public Button DownloadSuccessCanvasGroupExitButton;



    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PopupWindow3D = GameObject.Find("PopupWindow3D").GetComponent<CanvasGroup>();
        PopupWindow3D.alpha = 0;
        PopupWindow3D.blocksRaycasts = false;
        PopupWindow3D.interactable = false;

        UpdateCanvasGroup = GameObject.Find("UpdateCanvasGroup").GetComponent<CanvasGroup>();
        UpdateCanvasGroup.alpha = 0;
        UpdateCanvasGroup.blocksRaycasts = false;
        UpdateCanvasGroup.interactable = false;

        DownloadFileCanvasGroup = GameObject.Find("DownloadFileCanvasGroup").GetComponent<CanvasGroup>();
        DownloadFileCanvasGroup.alpha = 0;
        DownloadFileCanvasGroup.blocksRaycasts = false;
        DownloadFileCanvasGroup.interactable = false;

        DownloadingExitAccountCanvasGroup = GameObject.Find("DownloadingExitAccountCanvasGroup").GetComponent<CanvasGroup>();
        DownloadingExitAccountCanvasGroup.alpha = 0;
        DownloadingExitAccountCanvasGroup.blocksRaycasts = false;
        DownloadingExitAccountCanvasGroup.interactable = false;

        ExitAccountCanvasGroup = GameObject.Find("ExitAccountCanvasGroup").GetComponent<CanvasGroup>();
        ExitAccountCanvasGroup.alpha = 0;
        ExitAccountCanvasGroup.blocksRaycasts = false;
        ExitAccountCanvasGroup.interactable = false;

        DownloadSuccessCanvasGroup = GameObject.Find("DownloadSuccessCanvasGroup").GetComponent<CanvasGroup>();
        DownloadSuccessCanvasGroup.alpha = 0;
        DownloadSuccessCanvasGroup.blocksRaycasts = false;
        DownloadSuccessCanvasGroup.interactable = false;

        PopupWindow3DTextYifasong = GameObject.Find("PopupWindow3DTextServerError").GetComponent<Text>();
        PopupWindow3DTextYouxianggeshi = GameObject.Find("PopupWindow3DTextHttpError").GetComponent<Text>();

        PopupWindow3DTextYifasong.enabled = false;
        PopupWindow3DTextYouxianggeshi.enabled = false;

        PopupWindow3DMaskBG = GameObject.Find("PopupWindow3DMaskBG").GetComponent<Button>();
        PopupWindow3DMaskBG.onClick.AddListener(PopupWindow3DCloseEvent);

        UpdateCanvasGroupButton = GameObject.Find("UpdateCanvasGroupButton").GetComponent<Button>();
        UpdateCanvasGroupButton.onClick.AddListener(UpdateCanvasGroupButtonEvent);

        DownloadFileCanvasGroupButton = GameObject.Find("DownloadFileCanvasGroupButton").GetComponent<Button>();
        DownloadFileCanvasGroupButton.onClick.AddListener(DownloadFileCanvasGroupButtonEvent);

        DownloadFileCanvasGroupExitButton = GameObject.Find("DownloadFileCanvasGroupExitButton").GetComponent<Button>();
        DownloadFileCanvasGroupExitButton.onClick.AddListener(DownloadFileCanvasGroupExitButtonEvent);

        DownloadFileCanvasGroupText = GameObject.Find("DownloadFileCanvasGroupText").GetComponent<Text>();

        DownloadingExitAccountCanvasGroupButton = GameObject.Find("DownloadingExitAccountCanvasGroupButton").GetComponent<Button>();
        DownloadingExitAccountCanvasGroupButton.onClick.AddListener(DownloadingExitAccountCanvasGroupButtonEvent);

        DownloadingExitAccountCanvasGroupExitButton = GameObject.Find("DownloadingExitAccountCanvasGroupExitButton").GetComponent<Button>();
        DownloadingExitAccountCanvasGroupExitButton.onClick.AddListener(DownloadingExitAccountCanvasGroupExitButtonEvent);

        ExitAccountCanvasGroupButton = GameObject.Find("ExitAccountCanvasGroupButton").GetComponent<Button>();
        ExitAccountCanvasGroupButton.onClick.AddListener(ExitAccountCanvasGroupButtonEvent);

        ExitAccountCanvasGroupExitButton = GameObject.Find("ExitAccountCanvasGroupExitButton").GetComponent<Button>();
        ExitAccountCanvasGroupExitButton.onClick.AddListener(ExitAccountCanvasGroupExitButtonEvent);

        DownloadSuccessCanvasGroupButton = GameObject.Find("DownloadSuccessCanvasGroupButton").GetComponent<Button>();
        DownloadSuccessCanvasGroupButton.onClick.AddListener(DownloadSuccessCanvasGroupButtonEvent);

        DownloadSuccessCanvasGroupExitButton = GameObject.Find("DownloadSuccessCanvasGroupExitButton").GetComponent<Button>();
        DownloadSuccessCanvasGroupExitButton.onClick.AddListener(DownloadSuccessCanvasGroupExitButtonEvent);

        textReplaces = this.GetComponentsInChildren<TextLanguageReplace>();
    }

    public void Start()
    {
        SwitchLanguageEvent(PaintingModuleDataManager.Instance.language);
    }

    /// <summary>
    /// 弹窗打开事件
    /// </summary>
    public void PopupWindow3DOpenEvent(PopupWindow3DState state)
    {
        if (PopupWindow3D.alpha == 1)
            return;

        PopupWindow3D.alpha = 1;
        PopupWindow3D.blocksRaycasts = true;
        PopupWindow3D.interactable = true;

        popupState = state;

        switch (state)
        {
            case PopupWindow3DState.None:
                break;
            case PopupWindow3DState.ServerError:
                PopupWindow3DTextYifasong.enabled = true;
                break;
            case PopupWindow3DState.HttpError:
                PopupWindow3DTextYouxianggeshi.enabled = true;
                break;
        }
    }
    /// <summary>
    /// 弹窗关闭事件
    /// </summary>
    public void PopupWindow3DCloseEvent()
    {
        PopupWindow3D.alpha = 0;
        PopupWindow3D.blocksRaycasts = false;
        PopupWindow3D.interactable = false;

        PopupWindow3DState state = popupState;

        switch (state)
        {
            case PopupWindow3DState.None:
                break;
            case PopupWindow3DState.ServerError:
                PopupWindow3DTextYifasong.enabled = false;
                break;
            case PopupWindow3DState.HttpError:
                PopupWindow3DTextYouxianggeshi.enabled = false;
                break;
        }

        PopupWindow3DOnClickEvent(HttpManager.Instance.httpState);

        popupState = PopupWindow3DState.None;
    }

    /// <summary>
    /// 语言切换
    /// </summary>
    public void SwitchLanguageEvent(Language language)
    {
        if (textReplaces != null)
        {
            for (int i = 0; i < textReplaces.Length; i++)
            {
                textReplaces[i].ReplaceLanguageText(language);
            }
        }
    }
    /// <summary>
    /// 弹窗点击事件
    /// </summary>
    public void PopupWindow3DOnClickEvent(HttpState state)
    {
        switch (state)
        {
            case HttpState.GetAppVersionsWebRequest:
                HttpManager.Instance.GetAppVersions();
                break;
            case HttpState.GetAppVersionsWeb:
                HttpManager.Instance.NetWorkUpdate(NetTypeListen.AppIDGetAppDataURL, null);
                break;
            case HttpState.GetDataListWebRequest:
                HttpManager.Instance.GetDataList();
                break;
            case HttpState.GetDataList:
                HttpManager.Instance.NetWorkUpdate(NetTypeListen.AppIDGetDataListURL, null);
                break;
            case HttpState.GetGalleryListWebRequest:
                HttpManager.Instance.GetGalleryList();
                break;
            case HttpState.GetGalleryListWeb:
                HttpManager.Instance.NetWorkUpdate(NetTypeListen.AppIDGetGalleryListURL, null);
                break;
            case HttpState.GetRoomDataWebRequest:
                HttpManager.Instance.GetHttpRoomData();
                break;
            case HttpState.GetRoomDataWeb:
                HttpManager.Instance.NetWorkUpdate(NetTypeListen.GalleryIDGetAllRoomURL, null);
                break;
            case HttpState.DownloadRoomData:
                UnityDownloadMgr.instance.JudgeDownloader();
                break;
            case HttpState.GetRoomPMDataWebRequest:
                HttpManager.Instance.GetHttpRoomPMData();
                break;
            case HttpState.GetRoomPMDataWeb:
                HttpManager.Instance.NetWorkUpdate(NetTypeListen.GetRoomAllExhibitsData, null);
                break;
            case HttpState.DownloadRoomPMData:
                UnityDownloadMgr.instance.JudgeDownloader();
                break;
            case HttpState.DownloadCheckoutData:
                UnityDownloadMgr.instance.JudgeDownloader();
                break;
        }
    }


    /// <summary>
    /// 更新弹窗打开事件
    /// </summary>
    public void UpdateCanvasGroupOpenEvent()
    {

        UpdateCanvasGroup.alpha = 1;
        UpdateCanvasGroup.blocksRaycasts = true;
        UpdateCanvasGroup.interactable = true;

    }

    /// <summary>
    /// 下载提示弹窗打开事件
    /// </summary>
    public void DownloadFileCanvasGroupOpenEvent()
    {
        SwitchLanguageEvent(PaintingModuleDataManager.Instance.language);

        DownloadFileCanvasGroup.alpha = 1;
        DownloadFileCanvasGroup.blocksRaycasts = true;
        DownloadFileCanvasGroup.interactable = true;

        switch (PaintingModuleDataManager.Instance.language)
        {
            case Language.中文:
                DownloadFileCanvasGroupText.text = "有" + PaintingModuleDataManager.Instance.fileSize.ToString("f2") + "MB的文件需要下载，请点击下载";
                break;
            case Language.英语:
                DownloadFileCanvasGroupText.text = "Download File Size ：" + PaintingModuleDataManager.Instance.fileSize.ToString("f2") + "MB";
                break;
            case Language.日文:
                DownloadFileCanvasGroupText.text = "ダウンロードをお願いします。 ファイル容量：" + PaintingModuleDataManager.Instance.fileSize.ToString("f2") + "MB";
                break;
        }

    }

    public void UpdateCanvasGroupButtonEvent()
    {
        CustomUrlOpener.Open(PaintingModuleDataManager.Instance.appIosStoreUrl);
    }

    /// <summary>
    /// 版本下载文件按钮确定事件
    /// </summary>
    public void DownloadFileCanvasGroupButtonEvent()
    {
        DownloadFileCanvasGroup.alpha = 0;
        DownloadFileCanvasGroup.blocksRaycasts = false;
        DownloadFileCanvasGroup.interactable = false;
        //HttpManager.Instance.DetectionRoomData();

        HttpManager.Instance.currentDownloadState = currentDownloadState.DownloadRoomData;

        Downloader.Instance.isDownloadCheck = true;

        HttpManager.Instance.SetHttpState(HttpState.DownloadRoomData);

        PaintingModuleDataManager.Instance.SceneSelectionLayoutGroupButtonDownloadEvent();

        UnityDownloadMgr.instance.BatchDownloadPMData();
    }

    public void DownloadFileCanvasGroupExitButtonEvent()
    {
        //Application.Quit();

        DownloadFileCanvasGroup.alpha = 0;
        DownloadFileCanvasGroup.blocksRaycasts = false;
        DownloadFileCanvasGroup.interactable = false;
    }
    /// <summary>
    /// 下载中退出账户按钮事件
    /// </summary>
    public void DownloadingExitAccountCanvasGroupButtonEvent()
    {
        Debug.Log("下载中退出账户按钮事件");
        DownloadingExitAccountCanvasGroup.alpha = 0;
        DownloadingExitAccountCanvasGroup.blocksRaycasts = false;
        DownloadingExitAccountCanvasGroup.interactable = false;

        UnityDownloadMgr.instance.StopDownloadEvent();

        HttpManager.Instance.currentDownloadState = currentDownloadState.None;
        HttpManager.Instance.httpState = HttpState.None;

        AccountManager.Instance.SceneSelectionButtonReturnCanvasGroupEvent();

        Downloader.Instance.isDownloadCheck = false;
    }
    /// <summary>
    /// 下载中退出账户按钮取消事件
    /// </summary>
    public void DownloadingExitAccountCanvasGroupExitButtonEvent()
    {
        DownloadingExitAccountCanvasGroup.alpha = 0;
        DownloadingExitAccountCanvasGroup.blocksRaycasts = false;
        DownloadingExitAccountCanvasGroup.interactable = false;
    }
    /// <summary>
    /// 下载中退出账户UI事件
    /// </summary>
    public void DownloadingExitAccountCanvasGroupEvent()
    {
        DownloadingExitAccountCanvasGroup.alpha = 1;
        DownloadingExitAccountCanvasGroup.blocksRaycasts = true;
        DownloadingExitAccountCanvasGroup.interactable = true;
    }
    /// <summary>
    /// 退出账户按钮事件
    /// </summary>
    public void ExitAccountCanvasGroupButtonEvent()
    {
        Debug.Log("退出账户按钮事件");

        ExitAccountCanvasGroup.alpha = 0;
        ExitAccountCanvasGroup.blocksRaycasts = false;
        ExitAccountCanvasGroup.interactable = false;

        HttpManager.Instance.currentDownloadState = currentDownloadState.None;

        PaintingModuleDataManager.Instance.currentSelectSceneData = null;

        AccountManager.Instance.SceneSelectionButtonReturnCanvasGroupEvent();
    }

    /// <summary>
    /// 退出账户按钮取消事件
    /// </summary>
    public void ExitAccountCanvasGroupExitButtonEvent()
    {
        ExitAccountCanvasGroup.alpha = 0;
        ExitAccountCanvasGroup.blocksRaycasts = false;
        ExitAccountCanvasGroup.interactable = false;
    }
    /// <summary>
    /// 退出账户UI事件
    /// </summary>
    public void ExitAccountCanvasGroupEvent()
    {
        ExitAccountCanvasGroup.alpha = 1;
        ExitAccountCanvasGroup.blocksRaycasts = true;
        ExitAccountCanvasGroup.interactable = true;
    }

    /// <summary>
    /// 下载成功按钮事件
    /// </summary>
    public void DownloadSuccessCanvasGroupButtonEvent()
    {
        Debug.Log("下载成功按钮事件");

        DownloadSuccessCanvasGroup.alpha = 0;
        DownloadSuccessCanvasGroup.blocksRaycasts = false;
        DownloadSuccessCanvasGroup.interactable = false;

        if (PaintingModuleDataManager.Instance.isYouke)
        {
            HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.YoukeLoginURL, null);
        }

        SceneManager.LoadScene("Player");

    }

    /// <summary>
    /// 下载成功按钮取消事件
    /// </summary>
    public void DownloadSuccessCanvasGroupExitButtonEvent()
    {
        DownloadSuccessCanvasGroup.alpha = 0;
        DownloadSuccessCanvasGroup.blocksRaycasts = false;
        DownloadSuccessCanvasGroup.interactable = false;
    }
    /// <summary>
    /// 下载成功UI事件
    /// </summary>
    public void DownloadSuccessCanvasGroupEvent()
    {
        DownloadSuccessCanvasGroup.alpha = 1;
        DownloadSuccessCanvasGroup.blocksRaycasts = true;
        DownloadSuccessCanvasGroup.interactable = true;

        ExitAccountCanvasGroup.alpha = 0;
        ExitAccountCanvasGroup.blocksRaycasts = false;
        ExitAccountCanvasGroup.interactable = false;

        DownloadingExitAccountCanvasGroup.alpha = 0;
        DownloadingExitAccountCanvasGroup.blocksRaycasts = false;
        DownloadingExitAccountCanvasGroup.interactable = false;
    }
}

public enum PopupWindow3DState
{
    None,
    ServerError,
    HttpError
}

