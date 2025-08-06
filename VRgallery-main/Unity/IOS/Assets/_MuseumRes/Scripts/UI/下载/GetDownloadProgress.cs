using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using yoyohan;

public class GetDownloadProgress : MonoBehaviour
{
    //public enum DownloadEnum
    //{
    //    App,
    //    Scene,
    //    PM,
    //    Checkout
    //}

    //public DownloadEnum downloadEnum;

    //public Text downloadState;
    //public Text downloadProgressText;
    //public Image downloadProgress;
    public Text CurrentVersionText;

    //public float currentDownload;
    //public float totalDownload;
    //public float alreadyDownload;

    //public TextLanguageReplace[] textReplaces;

    //public bool isDownload = false;

    public void Awake()
    {
        //downloadState.text = "App Update";
        DownloadProgressIntital();

        //textReplaces = this.GetComponentsInChildren<TextLanguageReplace>();
    }

    public void DownloadProgressIntital()
    {
        //downloadProgressText.text = "0%";
        //downloadProgress.fillAmount = 0;
    }

    public void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        CurrentVersionText.text = PaintingModuleDataManager.Instance.appCurrentWinVersions;
#elif UNITY_IOS
        CurrentVersionText.text = PaintingModuleDataManager.Instance.appCurrentIosVersions;
#elif UNITY_ANDROID
        CurrentVersionText.text = PaintingModuleDataManager.Instance.appCurrentAndroidVersions;
#endif


    }

    //public void DownloadApp()
    //{
    //    downloadState.text = "App Update";
    //    DownloadProgressIntital();
    //    downloadEnum = DownloadEnum.App;
    //}

    //public void DownloadRoom()
    //{
    //    downloadState.text = "Scene Download";
    //    DownloadProgressIntital();
    //    downloadEnum = DownloadEnum.Scene;
    //}

    //public void DownloadPM()
    //{
    //    downloadState.text = "Data Download";
    //    DownloadProgressIntital();
    //    downloadEnum = DownloadEnum.PM;
    //}

    //public void DownloadCheckout()
    //{
    //    downloadState.text = "Data Checkout";
    //    DownloadProgressIntital();
    //    downloadEnum = DownloadEnum.Checkout;
    //}

    //public void DownloadDataConvert(string length)
    //{
    //    //float sta1 = 0;
    //    //float.TryParse(length.Substring(0, length.Length - 1), out sta1);
    //    //totalDownload += sta1;
    //}

    ///// <summary>
    ///// 语言切换
    ///// </summary>
    //public void SwitchLanguageEvent(Language language)
    //{
    //    if (textReplaces != null)
    //    {
    //        for (int i = 0; i < textReplaces.Length; i++)
    //        {
    //            textReplaces[i].ReplaceLanguageText(language);
    //        }
    //    }
    //}

    //public void Update()
    //{
    //if (isDownload)
    //{
    //    for (int i = 0; i < Downloader.Instance.requestDownloadObj.Count; i++)
    //    {
    //        currentDownload += ByteConversion(Downloader.Instance.requestDownloadObj[i].sDownloadFileResult.downloadedLength);
    //    }

    //    downloadProgressText.text = (currentDownload + alreadyDownload) + "/" + totalDownload + "m";
    //    downloadProgress.fillAmount = (currentDownload + alreadyDownload) / totalDownload;

    //    currentDownload = 0;
    //}

    //}

    //public void UpdateDownloadProgress()
    //{
    //    Debug.Log("当前进度为：" + (float)Downloader.Instance.currentDownload / Downloader.Instance.totalDownload);
    //    Debug.Log("当前进度为：" + (((float)Downloader.Instance.currentDownload / Downloader.Instance.totalDownload) * 100).ToString("f1") + "%");
    //    downloadProgressText.text = (((float)Downloader.Instance.currentDownload / Downloader.Instance.totalDownload) * 100).ToString("f1") + "%";
    //    downloadProgress.fillAmount = (float)Downloader.Instance.currentDownload / Downloader.Instance.totalDownload;
    //}

    //public void DownloadSucceed()
    //{
    //    Downloader.Instance.totalDownload = 0;
    //    Downloader.Instance.currentDownload = 0;
    //    downloadProgressText.text = "100%";
    //    downloadProgress.fillAmount = 1;
    //}

    public float ByteConversion(int length)
    {
        float temp = 0;

        temp = (float)length / 1048576;

        Debug.Log("转换的大小为：" + temp);

        return temp;
    }

    //public bool isFake = false;
    //public float fakeTimer = 0;
    ///// <summary>
    ///// 场景下载假进度
    ///// </summary>
    //public void FakeProgressScene()
    //{
    //    isFake = true;
    //    fakeTimer = 0;
    //    downloadProgress.fillAmount = 0;
    //    downloadProgressText.text = "0%";
    //}

    //public void FakeProgressSceneEvent()
    //{
    //    HttpManager.Instance.DetectionRoomPMData();
    //}
    ///// <summary>
    ///// 画作下载假进度
    ///// </summary>
    //public void FakeProgressPMEvent()
    //{
    //    UnityDownloadMgr.instance.CheckoutAllData();
    //}
    ///// <summary>
    ///// 校验下载假进度
    ///// </summary>
    //public void FakeProgressCheckoutEvent()
    //{
    //    SceneManager.LoadScene("Main");
    //}

    //public void Update()
    //{
        //if (isFake)
        //{
        //    downloadProgress.fillAmount = fakeTimer;
        //    if (fakeTimer >= 1)
        //    {
        //        downloadProgressText.text = "100%";
        //    }
        //    else
        //        downloadProgressText.text = (fakeTimer * 100).ToString("f1") + "%";
        //    if (fakeTimer >= 1)
        //    {
        //        isFake = false;
        //        fakeTimer = 0;
        //        switch (downloadEnum)
        //        {
        //            case DownloadEnum.App:
        //                //跳转安装界面
        //            case DownloadEnum.Scene:
        //                FakeProgressSceneEvent();
        //                break;
        //            case DownloadEnum.PM:
        //                FakeProgressPMEvent();
        //                break;
        //            case DownloadEnum.Checkout:
        //                FakeProgressCheckoutEvent();
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        fakeTimer += Time.deltaTime;
        //    }
        //}
    //}
}
