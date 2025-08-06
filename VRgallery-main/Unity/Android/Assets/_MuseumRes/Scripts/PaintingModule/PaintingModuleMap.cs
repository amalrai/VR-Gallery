using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static DataJsonClass;

public class PaintingModuleMap : UnitySingleton<PaintingModuleMap>
{
    /// <summary>
    /// 是否smart进去帧动画
    /// </summary>
    public bool _SmartToFrame;
    /// <summary>
    /// 画作地图UICanvas
    /// </summary>
    public GameObject paintingModuleMapUICanvas;   
    /// <summary>
    /// 画作地图UICanvas
    /// </summary>
    public GameObject paintingModuleHelpUICanvas;

    /// <summary>
    /// 画作地图Canvas
    /// </summary>
    public GameObject paintingModuleMapCanvas;
    /// <summary>
    /// 画作地图背景bg
    /// </summary>
    public Image paintingModuleMapCanvas_bg;
    /// <summary>
    /// 画作地图进入按钮
    /// </summary>
    public Button paintingModuleMapUICanvasButton;
    /// <summary>
    /// 画作地图进入按钮事件
    /// </summary>
    public UnityAction paintingModuleMapUICanvasButton_UnityAction;
    /// <summary>
    /// 画作地图退出按钮
    /// </summary>
    public Button paintingModuleMapCanvas_Exit;
    /// <summary>
    /// 画作地图退出按钮事件
    /// </summary>
    public UnityAction paintingModuleMapCanvas_Exit_UnityAction;

    /// <summary>
    /// 画作地图选择Normal按钮
    /// </summary>
    /// <returns></returns>
    public Sprite[] PaintingModuleMapCanvas_ButtonModeSprites;
    /// <summary>
    /// 画作地图选择Normal按钮
    /// </summary>
    public Button PaintingModuleMapCanvas_Button_Normal;
    /// <summary>
    /// 画作地图选择Normal按钮
    /// </summary>
    public Button PaintingModuleMapCanvas_Button_Samrt; 
    /// <summary>
    /// 画作地图选择Setting按钮
    /// </summary>
    public Button PaintingModuleMapCanvas_Button_Setting;
    /// <summary>
    /// 画作地图选择Normal按钮Image
    /// </summary>
    public Image PaintingModuleMapCanvas_Button_Normal_Image;
    /// <summary>
    /// 画作地图选择Normal按钮Image
    /// </summary>
    public Image PaintingModuleMapCanvas_Button_Samrt_Image;
    /// <summary>
    /// 画作地图选择Normal按钮Text
    /// </summary>
    public Image PaintingModuleMapCanvas_Button_Normal_Text;
    /// <summary>
    /// 画作地图选择Normal按钮Text
    /// </summary>
    public Image PaintingModuleMapCanvas_Button_Samrt_Text;
    /// <summary>
    /// 画作地图选择Normal模块
    /// </summary>
    public GameObject PaintingModuleMapCanvas_Normal;
    /// <summary>
    /// 画作地图选择SamrtMode模块
    /// </summary>
    public GameObject PaintingModuleMapCanvas_SamrtMode;
    /// <summary>
    /// 画作地图Normal模式背景bg
    /// </summary>
    public Image PaintingModuleMapCanvas_Map_bg;

    /// <summary>
    /// 画作地图UI传送点parent
    /// </summary>
    public Transform PaintingModuleMapCanvas_Map_Transfers;
    /// <summary>
    /// 画作地图UI传送点button组
    /// </summary>
    public Button[] PaintingModuleMapCanvas_Map_TransfersButtons;
    /// <summary>
    /// 画作地图UI传送点button背景图
    /// </summary>
    /// <returns></returns>
    public Sprite[] PaintingModuleMapCanvas_Map_TransfersButtonsBgSprites;
    /// <summary>
    /// 画作地图UI传送点parent
    /// </summary>
    public Transform paintingModuleMap_Transfers;
    /// <summary>
    /// 画作地图UI传送点button组
    /// </summary>
    public Transform[] paintingModuleMap_TransfersTransforms;

    /// <summary>
    /// 画作模块地图UI_内容父类
    /// </summary>
    public Transform paintingModuleMapCanvas_Transfers_Content;
    /// <summary>
    /// 画作模块地图UI_内容prefabs
    /// </summary>
    public GameObject[] paintingModuleMapCanvas_Transfers_Content_Point;
    /// <summary>
    /// 画作模块地图UI_内容prefabs
    /// </summary>
    public Button[] paintingModuleMapCanvas_Transfers_Content_Point_Function_Buttons;
    /// <summary>
    /// 地图实时坐标
    /// </summary>
    public RectTransform paintingModuleMap_XYZ;
    /// <summary>
    /// 玩家
    /// </summary>
    public Transform player;
    /// <summary>
    /// 画作帮助进入按钮
    /// </summary>
    public Button paintingModuleHelpUICanvasEnterButton;
    /// <summary>
    /// 画作帮助进入按钮事件
    /// </summary>
    public UnityAction paintingModuleHelpUICanvasEnterButton_UnityAction;
    /// <summary>
    /// 画作帮助Canvas
    /// </summary>
    public GameObject paintingModuleHelpCanvas;
    /// <summary>
    /// 画作帮助退出按钮
    /// </summary>
    public Button paintingModuleHelpUICanvasExitButton;
    /// <summary>
    /// 画作帮助退出按钮事件
    /// </summary>
    public UnityAction paintingModuleHelpUICanvasExitButton_UnityAction;
    Color nowColor;


    /// <summary>
    /// 画作设置进入按钮
    /// </summary>
    public Button paintingModuleSettingUICanvasEnterButton;
    /// <summary>
    /// 画作设置进入按钮事件
    /// </summary>
    public UnityAction paintingModuleSettingUICanvasEnterButton_UnityAction;
    /// <summary>
    /// 画作设置Canvas
    /// </summary>
    public GameObject paintingModuleSettingCanvas;
    /// <summary>
    /// 画作设置退出按钮
    /// </summary>
    public Button paintingModuleSettingUICanvasExitButton;
    /// <summary>
    /// 画作设置退出按钮事件
    /// </summary>
    public UnityAction paintingModuleSettingUICanvasExitButton_UnityAction;
    /// <summary>
    /// 画作设置摇杆滑动条
    /// </summary>
    public Slider paintingModuleSettingUICanvasYaoganSlider;
    /// <summary>
    /// 画作设置视觉滑动条
    /// </summary>
    public Slider paintingModuleSettingUICanvasShijueSlider;
    ///// <summary>
    ///// 画作设置摇杆滑动条Text
    ///// </summary>
    //public Text paintingModuleSettingUICanvasYaoganSliderText;
    ///// <summary>
    ///// 画作设置视觉滑动条Text
    ///// </summary>
    //public Text paintingModuleSettingUICanvasShijueSliderText;
    ///// <summary>
    ///// 画作设置静音
    ///// </summary>
    //public Toggle paintingModuleSettingUICanvasBGMMuteToggle;
    /// <summary>
    /// 画作设置静音按钮
    /// </summary>
    public Button PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn;
    /// <summary>
    /// 画作设置不静音按钮
    /// </summary>
    public Button PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff;
    /// <summary>
    /// 画作设置静音图片
    /// </summary>
    public Image PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn;
    /// <summary>
    /// 画作设置不静音图片
    /// </summary>
    public Image PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff;
    /// <summary>
    /// 画作设置静音图片组
    /// </summary>
    public Sprite[] PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites;
    /// <summary>
    /// 画作设置退出登录
    /// </summary>
    public Button PaintingModuleSettingCanvas_LogOut;
    /// <summary>
    /// 画作设置删除账户
    /// </summary>
    public Button PaintingModuleSettingCanvas_DeleteAccount;
    /// <summary>
    /// 画作设置删除账户弹窗
    /// </summary>
    public CanvasGroup PaintingModuleSettingCanvas_DeleteAccountPopup;
    /// <summary>
    /// 画作设置删除账户
    /// </summary>
    public Button PaintingModuleSettingCanvas_DeleteAccountPopupButtonYes;
    /// <summary>
    /// 画作设置删除账户
    /// </summary>
    public Button PaintingModuleSettingCanvas_DeleteAccountPopupButtonNo;

    /// <summary>
    /// 帮助和设置文字替换
    /// </summary>
    public TextLanguageReplace[] textReplaces;


    //----------------------------------------------------20230420新增----------------------------------------------------------
    /// <summary>
    /// 设置侧边栏设置按钮
    /// </summary>
    public Button PaintingModuleSettingCanvas_CebianlanShezhiButton;
    /// <summary>
    /// 设置侧边栏账户操作按钮
    /// </summary>
    public Button PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButton;

    /// <summary>
    /// 设置侧边栏关于应用按钮
    /// </summary>
    public Button PaintingModuleSettingCanvas_CebianlanGuanyuyingyongButton;
    /// <summary>
    /// 设置侧边栏设置CanvasGroup
    /// </summary>
    public CanvasGroup PaintingModuleSettingCanvasGroupSetting;
    /// <summary>
    /// 设置侧边栏账户操作CanvasGroup
    /// </summary>
    public CanvasGroup PaintingModuleSettingCanvasGroupZhanghucaozuo;

    //----------------------------------------------------20230523新增----------------------------------------------------------
    /// <summary>
    /// 是否从帧动画到设置
    /// </summary>
    public bool isFrameToSetting;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#D59E00", out nowColor);
        paintingModuleMap_XYZ = GameObject.Find("PaintingModuleMapUICanvasButtonXYZ").GetComponent<RectTransform>();
        player = GameObject.Find("Player").transform;

    }

    public void Update()
    {
        if(PaintingModuleDataManager.Instance.currentSelectSceneDataName == "newroom01"|| PaintingModuleDataManager.Instance.currentSelectSceneDataName == "newroom02")
        {
            paintingModuleMap_XYZ.localPosition = new Vector3(-player.localPosition.x * 7.5f, -player.localPosition.z * 7.25f, 0);
            paintingModuleMap_XYZ.localEulerAngles = new Vector3(0, 0, -player.localEulerAngles.y);
        }
        else
        {
            paintingModuleMap_XYZ.localPosition = new Vector3(-player.localPosition.x * 12, -player.localPosition.z * 12, 0);
            paintingModuleMap_XYZ.localEulerAngles = new Vector3(0, 0, -player.localEulerAngles.y);
        }

    }
    //PaintingModuleScene_01MapCanvas_bg
    /// <summary>
    /// 画作Map初始化
    /// </summary>
    public void Initial()
    {
        Debug.Log("画作Map初始化完成");

        _SmartToFrame = false;

        textReplaces = this.GetComponentsInChildren<TextLanguageReplace>();

        SwitchLanguageEvent(PaintingModuleDataManager.Instance.language);

        paintingModuleMapUICanvas = GameObject.Find("PaintingModuleMapUICanvas");
        paintingModuleHelpUICanvas = GameObject.Find("PaintingModuleHelpUICanvas");

        paintingModuleMapCanvas = GameObject.Find("PaintingModuleMapCanvas");
        PaintingModuleMapCanvas_Normal = GameObject.Find("PaintingModuleMapCanvas_Normal");
        PaintingModuleMapCanvas_SamrtMode = GameObject.Find("PaintingModuleMapCanvas_SamrtMode");

        paintingModuleMapCanvas_bg = GameObject.Find("PaintingModuleMapCanvas_bg").GetComponent<Image>();
        PaintingModuleMapCanvas_Map_bg = GameObject.Find("PaintingModuleMapCanvas_Map_bg").GetComponent<Image>();

        paintingModuleMapUICanvasButton = GameObject.Find("PaintingModuleMapUICanvasButton").GetComponent<Button>();
        paintingModuleMapUICanvasButton.GetComponent<Image>().sprite = GameObject.Find("paintingModuleMapUICanvasButton_ABsprite").GetComponent<Image>().sprite;

        paintingModuleMapCanvas_Exit = GameObject.Find("PaintingModuleMapCanvas_Exit").GetComponent<Button>();

        paintingModuleMapUICanvasButton_UnityAction += PaintingModuleMapUICanvasExit;
        paintingModuleMapUICanvasButton_UnityAction += PaintingModuleMapCanvasStart;
        paintingModuleMapUICanvasButton_UnityAction += PaintingModuleHelpUICanvasExit;
        paintingModuleMapUICanvasButton.onClick.AddListener(paintingModuleMapUICanvasButton_UnityAction);

        paintingModuleMapCanvas_Exit_UnityAction += PaintingModuleMapUICanvasStart;
        paintingModuleMapCanvas_Exit_UnityAction += PaintingModuleMapCanvasExit;
        paintingModuleMapCanvas_Exit_UnityAction += PaintingModuleHelpUICanvasStart;
        paintingModuleMapCanvas_Exit.onClick.AddListener(paintingModuleMapCanvas_Exit_UnityAction);

        PaintingModuleMapCanvas_Button_Normal = GameObject.Find("PaintingModuleMapCanvas_Button_Normal").GetComponent<Button>();
        PaintingModuleMapCanvas_Button_Samrt = GameObject.Find("PaintingModuleMapCanvas_Button_Samrt").GetComponent<Button>();
        PaintingModuleMapCanvas_Button_Setting = GameObject.Find("PaintingModuleMapCanvas_Button_Setting").GetComponent<Button>();
        PaintingModuleMapCanvas_Button_Normal_Image = GameObject.Find("PaintingModuleMapCanvas_Button_Normal").GetComponent<Image>();
        PaintingModuleMapCanvas_Button_Samrt_Image = GameObject.Find("PaintingModuleMapCanvas_Button_Samrt").GetComponent<Image>();
        PaintingModuleMapCanvas_Button_Normal_Text = GameObject.Find("PaintingModuleMapCanvas_Button_Normal_Text").GetComponent<Image>();
        PaintingModuleMapCanvas_Button_Samrt_Text = GameObject.Find("PaintingModuleMapCanvas_Button_Samrt_Text").GetComponent<Image>();

        paintingModuleHelpCanvas = GameObject.Find("PaintingModuleHelpCanvas");
        paintingModuleHelpUICanvasEnterButton = GameObject.Find("PaintingModuleHelpUICanvasButton").GetComponent<Button>();
        paintingModuleHelpUICanvasExitButton = GameObject.Find("PaintingModuleHelpCanvas_ExitButton").GetComponent<Button>();
        paintingModuleHelpUICanvasEnterButton_UnityAction += PaintingModuleHelpCanvasStart;
        paintingModuleHelpUICanvasExitButton_UnityAction += PaintingModuleHelpCanvasExit;
        paintingModuleHelpUICanvasEnterButton.onClick.AddListener(paintingModuleHelpUICanvasEnterButton_UnityAction);
        paintingModuleHelpUICanvasExitButton.onClick.AddListener(paintingModuleHelpUICanvasExitButton_UnityAction);


        paintingModuleHelpCanvas.SetActive(false);

        paintingModuleSettingCanvas = GameObject.Find("PaintingModuleSettingCanvas");
        paintingModuleSettingUICanvasEnterButton = GameObject.Find("PaintingModuleSettingUICanvasButton").GetComponent<Button>();
        paintingModuleSettingUICanvasExitButton = GameObject.Find("PaintingModuleSettingCanvas_Exit").GetComponent<Button>();
        PaintingModuleSettingCanvas_LogOut = GameObject.Find("PaintingModuleSettingCanvas_LogOut").GetComponent<Button>();
        PaintingModuleSettingCanvas_DeleteAccount = GameObject.Find("PaintingModuleSettingCanvas_DeleteAccount").GetComponent<Button>();
        PaintingModuleSettingCanvas_DeleteAccountPopup = GameObject.Find("PaintingModuleSettingCanvas_DeleteAccountPopup").GetComponent<CanvasGroup>();
        PaintingModuleSettingCanvas_DeleteAccountPopupButtonYes = GameObject.Find("PaintingModuleSettingCanvas_DeleteAccountPopup_OptionButtonYes").GetComponent<Button>();
        PaintingModuleSettingCanvas_DeleteAccountPopupButtonNo = GameObject.Find("PaintingModuleSettingCanvas_DeleteAccountPopup_OptionButtonNo").GetComponent<Button>();

        PaintingModuleSettingCanvas_DeleteAccountPopup.alpha = 0;
        PaintingModuleSettingCanvas_DeleteAccountPopup.blocksRaycasts = false;
        PaintingModuleSettingCanvas_DeleteAccountPopup.interactable = false;

        paintingModuleSettingUICanvasEnterButton_UnityAction += PaintingModuleSettingCanvasStart;
        paintingModuleSettingUICanvasExitButton_UnityAction += PaintingModuleSettingCanvasExit;
        paintingModuleSettingUICanvasEnterButton.onClick.AddListener(paintingModuleSettingUICanvasEnterButton_UnityAction);
        paintingModuleSettingUICanvasExitButton.onClick.AddListener(paintingModuleSettingUICanvasExitButton_UnityAction);
        PaintingModuleSettingCanvas_LogOut.onClick.AddListener(PaintingModuleDataManager.Instance.LogOut);
        PaintingModuleSettingCanvas_DeleteAccount.onClick.AddListener(PaintingModuleSettingCanvas_DeleteAccountEvent);  //加入删除账户事件
        PaintingModuleSettingCanvas_DeleteAccountPopupButtonYes.onClick.AddListener(PaintingModuleSettingCanvas_DeleteAccountPopupButtonYesEvent);  //加入删除账户弹窗确定事件
        PaintingModuleSettingCanvas_DeleteAccountPopupButtonNo.onClick.AddListener(PaintingModuleSettingCanvas_DeleteAccountPopupButtonNoEvent);  //加入删除账户弹窗取消事件


        paintingModuleSettingUICanvasYaoganSlider = GameObject.Find("PaintingModuleSettingCanvasSlider_yaogan").GetComponent<Slider>();
        paintingModuleSettingUICanvasShijueSlider = GameObject.Find("PaintingModuleSettingCanvasSlider_shijue").GetComponent<Slider>();
        //paintingModuleSettingUICanvasYaoganSliderText = GameObject.Find("PaintingModuleSettingCanvasSlider_yaoganText").GetComponent<Text>();
        //paintingModuleSettingUICanvasShijueSliderText = GameObject.Find("PaintingModuleSettingCanvasSlider_shijueText").GetComponent<Text>();
        //paintingModuleSettingUICanvasYaoganSlider.onValueChanged.AddListener((float value) => PaintingModuleSettingYaoganSliderTextEvent(value));
        //paintingModuleSettingUICanvasShijueSlider.onValueChanged.AddListener((float value) => PaintingModuleSettingShijueSliderTextEvent(value));

        //paintingModuleSettingUICanvasBGMMuteToggle = GameObject.Find("PaintingModuleSettingCanvasSlider_BGMMuteLogoToggle").GetComponent<Toggle>();

        //----------------------------------------------------20230420新增----------------------------------------------------------

        PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn = GameObject.Find("PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn").GetComponent<Button>();
        PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff = GameObject.Find("PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff").GetComponent<Button>();
        PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn = GameObject.Find("PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn").GetComponent<Image>();
        PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff = GameObject.Find("PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff").GetComponent<Image>();

        PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn.onClick.AddListener(PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOnEvent);
        PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff.onClick.AddListener(PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOffEvent);

        if (SceneBGMManager.Instance.isMute)
        {
            PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn.interactable = true;
            PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff.interactable = false;
            PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[0];
            PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[1];

            //paintingModuleSettingUICanvasBGMMuteToggle.isOn = true;
        }
        else
        {
            PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn.interactable = false;
            PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff.interactable = true;
            PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[1];
            PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[0];

            //paintingModuleSettingUICanvasBGMMuteToggle.isOn = false;
        }
        //paintingModuleSettingUICanvasBGMMuteToggle.onValueChanged.AddListener((bool value) => PaintingModuleSettingUICanvasBGMMuteToggleEvent(value));


        PaintingModuleSettingCanvas_CebianlanShezhiButton = GameObject.Find("PaintingModuleSettingCanvas_CebianlanShezhiButton").GetComponent<Button>();
        PaintingModuleSettingCanvas_CebianlanShezhiButton.onClick.AddListener(PaintingModuleSettingCanvas_CebianlanShezhiButtonEvent);
        PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButton = GameObject.Find("PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButton").GetComponent<Button>();
        PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButton.onClick.AddListener(PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButtonEvent);
        PaintingModuleSettingCanvas_CebianlanGuanyuyingyongButton = GameObject.Find("PaintingModuleSettingCanvas_CebianlanGuanyuyingyongButton").GetComponent<Button>();
        PaintingModuleSettingCanvas_CebianlanGuanyuyingyongButton.onClick.AddListener(PaintingModuleSettingCanvas_CebianlanGuanyuyingyongButtonEvent);

        PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButton.interactable = false;

        PaintingModuleSettingCanvasGroupSetting = GameObject.Find("PaintingModuleSettingCanvasGroupSetting").GetComponent<CanvasGroup>();
        PaintingModuleSettingCanvasGroupSetting.alpha = 0;
        PaintingModuleSettingCanvasGroupSetting.interactable = false;
        PaintingModuleSettingCanvasGroupSetting.blocksRaycasts = false;
        PaintingModuleSettingCanvasGroupZhanghucaozuo = GameObject.Find("PaintingModuleSettingCanvasGroupZhanghucaozuo").GetComponent<CanvasGroup>();
        PaintingModuleSettingCanvasGroupZhanghucaozuo.alpha = 1;
        PaintingModuleSettingCanvasGroupZhanghucaozuo.interactable = true;
        PaintingModuleSettingCanvasGroupZhanghucaozuo.blocksRaycasts = true;   

        paintingModuleSettingCanvas.SetActive(false);

        PaintingModuleMapCanvas_ButtonModeSprites = new Sprite[2];
        PaintingModuleMapCanvas_ButtonModeSprites[0] = GameObject.Find("PaintingModuleMapCanvasModeSprite0").GetComponent<Image>().sprite;
        PaintingModuleMapCanvas_ButtonModeSprites[1] = GameObject.Find("PaintingModuleMapCanvasModeSprite1").GetComponent<Image>().sprite;

        PaintingModuleMapCanvas_Map_TransfersButtonsBgSprites = new Sprite[2];
        PaintingModuleMapCanvas_Map_TransfersButtonsBgSprites[0] = GameObject.Find("PaintingModuleMapCanvas_Transfers_Content_Point_bg01").GetComponent<Image>().sprite;
        PaintingModuleMapCanvas_Map_TransfersButtonsBgSprites[1] = GameObject.Find("PaintingModuleMapCanvas_Transfers_Content_Point_bg02").GetComponent<Image>().sprite;

        PaintingModuleMapCanvas_Map_bg.sprite = GameObject.Find("PaintingModuleMapCanvas_Normal_bg").GetComponent<Image>().sprite;

        paintingModuleMapCanvas_bg.sprite = GameObject.Find("PaintingModuleScene_01MapCanvas_bg").GetComponent<Image>().sprite;
        paintingModuleMapCanvas_Transfers_Content = GameObject.Find("PaintingModuleMapCanvas_Transfers_Content").transform;
        paintingModuleMapCanvas_Transfers_Content_Point = new GameObject[PaintingModuleDataManager.Instance.Datas.Count];
        paintingModuleMapCanvas_Transfers_Content_Point_Function_Buttons = new Button[PaintingModuleDataManager.Instance.Datas.Count];
        GameObject contentPoint = Resources.Load<GameObject>("PaintingModuleMapCanvas_Transfers_Content_Point");

        bool isBG = false;
        for (int i = 0; i < paintingModuleMapCanvas_Transfers_Content_Point.Length; i++)
        {
            if (!string.IsNullOrEmpty(PaintingModuleDataManager.Instance.Datas[i].paintingName))
            {
                PaintingModuleDataManager.Instance.AddAvailablePMEvent(i);
                paintingModuleMapCanvas_Transfers_Content_Point[i] =
                     Instantiate(contentPoint, paintingModuleMapCanvas_Transfers_Content);
                if (isBG)
                {
                    paintingModuleMapCanvas_Transfers_Content_Point[i].GetComponent<Image>().sprite =
                        PaintingModuleMapCanvas_Map_TransfersButtonsBgSprites[0];

                    isBG = !isBG;
                }
                else
                {
                    paintingModuleMapCanvas_Transfers_Content_Point[i].GetComponent<Image>().sprite =
                        PaintingModuleMapCanvas_Map_TransfersButtonsBgSprites[1];

                    isBG = !isBG;
                }

                Text no = paintingModuleMapCanvas_Transfers_Content_Point[i].transform.GetChild(1).GetComponent<Text>();
                no.text = (i + 1).ToString();
                Text name = paintingModuleMapCanvas_Transfers_Content_Point[i].transform.GetChild(2).GetComponent<Text>();
                name.text = PaintingModuleDataManager.Instance.Datas[i].paintingName;
                RawImage mainGraph = paintingModuleMapCanvas_Transfers_Content_Point[i].transform.GetChild(3)
                    .GetComponentInChildren<RawImage>();
                if (PaintingModuleDataManager.Instance.Datas[i].mainGraphHttpUrl != null &&
                    PaintingModuleDataManager.Instance.Datas[i].mainGraphHttpUrl != " ")
                    mainGraph.texture = PaintingModuleDataManager.Instance.Datas[i].mainGraphTexture;
                else
                    mainGraph.texture = null;
                paintingModuleMapCanvas_Transfers_Content_Point_Function_Buttons[i] = paintingModuleMapCanvas_Transfers_Content_Point[i].transform.GetChild(3)
                    .GetComponentInChildren<Button>();
            }
        }

        paintingModuleMap_Transfers = GameObject.Find("PaintingModuleMap_Transfers").transform;
        paintingModuleMap_TransfersTransforms = new Transform[paintingModuleMap_Transfers.childCount];
        for (int i = 0; i < paintingModuleMap_TransfersTransforms.Length; i++)
        {
            paintingModuleMap_TransfersTransforms[i] = paintingModuleMap_Transfers.GetChild(i);
        }

        int index1 = 0;

        GameObject[] trans1 = null;
        GameObject Transfer;
        Transfer = Resources.Load("PaintingModuleMapCanvas_Transfer") as GameObject;
        PaintingModuleMapCanvas_Map_Transfers = GameObject.Find("PaintingModuleMapCanvas_Map_Transfers").transform;
        PaintingModuleMapCanvas_Map_TransfersButtons = new Button[paintingModuleMap_TransfersTransforms.Length];
        trans1 = new GameObject[paintingModuleMap_TransfersTransforms.Length];
        for (int i = 0; i < PaintingModuleMapCanvas_Map_TransfersButtons.Length; i++)
        {
            trans1[i] = Instantiate(Transfer, PaintingModuleMapCanvas_Map_Transfers);
            PaintingModuleMapCanvas_Map_TransfersButtons[i] = trans1[i].GetComponent<Button>();
            RectTransform rect = PaintingModuleMapCanvas_Map_TransfersButtons[i].GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(float.Parse(paintingModuleMap_TransfersTransforms[i].GetChild(0).name), float.Parse(paintingModuleMap_TransfersTransforms[i].GetChild(1).name));
            rect.anchorMax = new Vector2(float.Parse(paintingModuleMap_TransfersTransforms[i].GetChild(2).name), float.Parse(paintingModuleMap_TransfersTransforms[i].GetChild(3).name));
            rect.Rotate(0, 0, float.Parse(paintingModuleMap_TransfersTransforms[i].GetChild(4).name));
        }
        for (int i = 0; i < PaintingModuleMapCanvas_Map_TransfersButtons.Length; i++)
        {
            index1 = i;
            Vector2 v2 = new Vector2(paintingModuleMap_TransfersTransforms[index1].localPosition.x, paintingModuleMap_TransfersTransforms[index1].localPosition.z);
            Debug.Log(paintingModuleMap_TransfersTransforms[index1].name);
            Debug.Log(paintingModuleMap_TransfersTransforms[index1].localEulerAngles);
            Vector2 v3 = paintingModuleMap_TransfersTransforms[index1].eulerAngles;

            PaintingModuleMapCanvas_Map_TransfersButtons[index1].onClick.AddListener(() =>
            {
                PaintingModuleMapCanvasExit();
                PaintingModuleMapUICanvasStart();
                PaintingModuleHelpUICanvasStart();
                EasyTouchControlsManager.Instance.PlayerTransferMap(v2, v3);
            });
        }


        PaintingModuleMapCanvas_Button_Normal_Image.sprite = PaintingModuleMapCanvas_ButtonModeSprites[1];
        PaintingModuleMapCanvas_Button_Normal.transition = Selectable.Transition.SpriteSwap;
        SpriteState state = new SpriteState();
        state.disabledSprite = PaintingModuleMapCanvas_ButtonModeSprites[0];
        PaintingModuleMapCanvas_Button_Normal.spriteState = state;
        PaintingModuleMapCanvas_Button_Normal.onClick.AddListener(PaintingModuleMapCanvasNormalMode);

        PaintingModuleMapCanvas_Button_Samrt_Image.sprite = PaintingModuleMapCanvas_ButtonModeSprites[1];
        PaintingModuleMapCanvas_Button_Samrt.transition = Selectable.Transition.SpriteSwap;
        SpriteState state1 = new SpriteState();
        state1.disabledSprite = PaintingModuleMapCanvas_ButtonModeSprites[0];
        PaintingModuleMapCanvas_Button_Samrt.spriteState = state1;
        PaintingModuleMapCanvas_Button_Samrt.onClick.AddListener(PaintingModuleMapCanvasSamrtMode);

        PaintingModuleMapCanvas_Button_Setting.onClick.AddListener(PaintingModuleMapCanvasSettingEvent);

        PaintingModuleMapCanvas_Button_Normal.interactable = false;
        PaintingModuleMapCanvas_Button_Samrt.interactable = true;
        //PaintingModuleMapCanvas_Button_Normal_Text.color = Color.white;
        //PaintingModuleMapCanvas_Button_Samrt_Text.color = nowColor;

        int index = 0;
        for (int i = 0; i < paintingModuleMapCanvas_Transfers_Content_Point_Function_Buttons.Length; i++)
        {
            if (!string.IsNullOrEmpty(PaintingModuleDataManager.Instance.Datas[i].paintingName))
            {
                index = i;
                //Transform trans = PaintingModuleDataManager.Instance.Datas[index].teleporter;
                PaintingModule pm = PaintingModuleDataManager.Instance.Datas[index];
                paintingModuleMapCanvas_Transfers_Content_Point_Function_Buttons[index].onClick.AddListener(() =>
                    {
                        PaintingModuleFrameCanvas.Instance.PaintingModuleFrameStart(pm);
                        _SmartToFrame = true;
                        PaintingModuleMapUICanvasExit();
                        PaintingModuleHelpUICanvasExit();
                        PaintingModuleMapCanvasExit();
                        //PaintingModuleMapUICanvasStart();
                        //EasyTouchControlsManager.Instance.PlayerTransferMapNew(trans);
                    }
                );
            }

        }

        //Transfer = Resources.Load("PaintingModuleMapCanvas_Transfer") as GameObject;
        //paintingModuleMapCanvas_Transfers = GameObject.Find("PaintingModuleMapCanvas_Transfers").transform;
        //paintingModuleMapCanvas_TransfersButtons = new Button[paintingModuleMap_TransfersTransforms.Length];
        //trans = new GameObject[paintingModuleMap_TransfersTransforms.Length];
        //for (int i = 0; i < paintingModuleMapCanvas_TransfersButtons.Length; i++)
        //{
        //    trans[i] = Instantiate(Transfer, paintingModuleMapCanvas_Transfers);
        //    paintingModuleMapCanvas_TransfersButtons[i] = trans[i].GetComponent<Button>();
        //    RectTransform rect = paintingModuleMapCanvas_TransfersButtons[i].GetComponent<RectTransform>();
        //    rect.anchorMin = new Vector2(float.Parse(paintingModuleMap_TransfersTransforms[i].GetChild(0).name), float.Parse(paintingModuleMap_TransfersTransforms[i].GetChild(1).name));
        //    rect.anchorMax = new Vector2(float.Parse(paintingModuleMap_TransfersTransforms[i].GetChild(2).name), float.Parse(paintingModuleMap_TransfersTransforms[i].GetChild(3).name));
        //}
        ////paintingModuleMapCanvas_TransfersButtons = paintingModuleMapCanvas_Transfers.GetComponentsInChildren<Button>();


        //for (int i = 0; i < paintingModuleMapCanvas_TransfersButtons.Length; i++)
        //{
        //    index = i;
        //    Vector2 v2 = new Vector2(paintingModuleMap_TransfersTransforms[index].localPosition.x, paintingModuleMap_TransfersTransforms[index].localPosition.z);
        //    paintingModuleMapCanvas_TransfersButtons[index].onClick.AddListener(() =>
        //    {
        //        PaintingModuleMapCanvasExit();
        //        PaintingModuleMapUICanvasStart();
        //        EasyTouchControlsManager.Instance.PlayerTransferMap(v2);
        //    });
        //}

        if (PaintingModuleDataManager.Instance.isYouke)
            PaintingModuleSettingCanvas_DeleteAccount.gameObject.SetActive(false);

        GetLoadingProgress.Instance.isFake = true;
        GetLoadingProgress.Instance.currentAlreadyIndex += 1;

        paintingModuleMapCanvas.SetActive(false);
        PaintingModuleMapCanvas_SamrtMode.SetActive(false);
    }


    /// <summary>
    /// 画作地图UI打开
    /// </summary>
    public void PaintingModuleMapUICanvasStart()
    {
        paintingModuleMapUICanvas.SetActive(true);
    }
    /// <summary>
    /// 画作地图UI关闭
    /// </summary>
    public void PaintingModuleMapUICanvasExit()
    {
        paintingModuleMapUICanvas.SetActive(false);
    }
    /// <summary>
    /// 画作设置UI打开
    /// </summary>
    public void PaintingModuleHelpUICanvasStart()
    {
        paintingModuleHelpUICanvas.SetActive(true);
    }
    /// <summary>
    /// 画作设置UI关闭
    /// </summary>
    public void PaintingModuleHelpUICanvasExit()
    {
        paintingModuleHelpUICanvas.SetActive(false);
    }
    /// <summary>
    /// 画作地图normal模式
    /// </summary>
    public void PaintingModuleMapCanvasNormalMode()
    {
        PaintingModuleMapCanvas_Button_Normal.interactable = false;
        PaintingModuleMapCanvas_Button_Samrt.interactable = true;
        //PaintingModuleMapCanvas_Button_Normal_Text.color = Color.white;
        //PaintingModuleMapCanvas_Button_Samrt_Text.color = nowColor;
        PaintingModuleMapCanvas_Normal.SetActive(true);
        PaintingModuleMapCanvas_SamrtMode.SetActive(false);
    }
    /// <summary>
    /// 画作地图samrt模式
    /// </summary>
    public void PaintingModuleMapCanvasSamrtMode()
    {
        PaintingModuleMapCanvas_Button_Normal.interactable = true;
        PaintingModuleMapCanvas_Button_Samrt.interactable = false;
        //PaintingModuleMapCanvas_Button_Normal_Text.color = nowColor;
        //PaintingModuleMapCanvas_Button_Samrt_Text.color = Color.white;
        PaintingModuleMapCanvas_Normal.SetActive(false);
        PaintingModuleMapCanvas_SamrtMode.SetActive(true);
    }
    /// <summary>
    /// 画作地图Setting模式
    /// </summary>
    public void PaintingModuleMapCanvasSettingEvent()
    {
        isFrameToSetting = true;

        PaintingModuleMapUICanvasStart();
        PaintingModuleMapCanvasExit();
        PaintingModuleHelpUICanvasStart();
        PaintingModuleSettingCanvasStart();
    }
    /// <summary>
    /// 画作地图打开
    /// </summary>
    public void PaintingModuleMapCanvasStart()
    {
        paintingModuleMapCanvas.SetActive(true);
    }
    /// <summary>
    /// 画作地图关闭
    /// </summary>
    public void PaintingModuleMapCanvasExit()
    {
        paintingModuleMapCanvas.SetActive(false);
    }
    /// <summary>
    /// 画作帮助打开
    /// </summary>
    public void PaintingModuleHelpCanvasStart()
    {
        paintingModuleHelpCanvas.SetActive(true);
    }
    /// <summary>
    /// 画作帮助关闭
    /// </summary>
    public void PaintingModuleHelpCanvasExit()
    {
        paintingModuleHelpCanvas.SetActive(false);
    }
    /// <summary>
    /// 加载地图缩略图
    /// </summary>
    public void LoadMapAnimationThumbnailTexture()
    {
        for (int i = 0; i < paintingModuleMapCanvas_Transfers_Content_Point.Length; i++)
        {
            if (PaintingModuleDataManager.Instance.Datas[i].status == 0)
            {
                RawImage mainGraph = paintingModuleMapCanvas_Transfers_Content_Point[i].transform.GetChild(3)
                    .GetComponentInChildren<RawImage>();

                if (PaintingModuleDataManager.Instance.Datas[i].animationThumbnailHttpUrl != null && PaintingModuleDataManager.Instance.Datas[i].animationThumbnailHttpUrl != " ")
                    mainGraph.texture = PaintingModuleDataManager.Instance.Datas[i].animationThumbnailTexture;
                else
                    mainGraph.texture = null;

            }
        }
    }
    /// <summary>
    /// 画作帮助打开
    /// </summary>
    public void PaintingModuleSettingCanvasStart()
    {
        paintingModuleSettingCanvas.SetActive(true);

        if (PlayerPrefs.HasKey("JoystickSpeed"))
        {
            //paintingModuleSettingUICanvasYaoganSliderText.text = PlayerPrefs.GetFloat("JoystickSpeed").ToString();
            paintingModuleSettingUICanvasYaoganSlider.value = PlayerPrefs.GetFloat("JoystickSpeed");
        }
        else
        {
            //paintingModuleSettingUICanvasYaoganSliderText.text = "5";
        }

        if (PlayerPrefs.HasKey("TouchPadSpeed"))
        {
            //paintingModuleSettingUICanvasShijueSliderText.text = PlayerPrefs.GetFloat("TouchPadSpeed").ToString();
            paintingModuleSettingUICanvasShijueSlider.value = PlayerPrefs.GetFloat("TouchPadSpeed");
        }
        else
        {
            //paintingModuleSettingUICanvasShijueSliderText.text = "30";
        }


        if (PlayerPrefs.HasKey("BGMMute"))
        {

            if (PlayerPrefs.GetString("BGMMute") == "True")
            {
                SceneBGMManager.Instance.isMute = true;

                PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn.interactable = true;
                PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff.interactable = false;
                PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[0];
                PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[1];
            }
            else
            {
                SceneBGMManager.Instance.isMute = false;
                PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn.interactable = false;
                PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff.interactable = true;
                PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[1];
                PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[0];
            }
        }
        else
        {
            SceneBGMManager.Instance.isMute = false;
            PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn.interactable = false;
            PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff.interactable = true;
            PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[1];
            PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[0];
        }
    }
    /// <summary>
    /// 画作帮助关闭
    /// </summary>
    public void PaintingModuleSettingCanvasExit()
    {

        float joystickSpeed = paintingModuleSettingUICanvasYaoganSlider.value;
        float touchPadSpeed = paintingModuleSettingUICanvasShijueSlider.value;

        PlayerPrefs.SetFloat("JoystickSpeed", joystickSpeed);
        PlayerPrefs.SetFloat("TouchPadSpeed", touchPadSpeed);
        PlayerPrefs.SetString("BGMMute", SceneBGMManager.Instance.isMute.ToString());

        EasyTouchControlsManager.Instance.SetETCJoystickSpeed(joystickSpeed);
        EasyTouchControlsManager.Instance.SetETCTouchPadSpeed(touchPadSpeed);

        paintingModuleSettingCanvas.SetActive(false);

        if (isFrameToSetting)
        {
            isFrameToSetting = false;

            PaintingModuleMapUICanvasExit();
            PaintingModuleHelpUICanvasExit();
            PaintingModuleMapCanvasStart();
        }
    }

    //public void PaintingModuleSettingYaoganSliderTextEvent(float a)
    //{
    //    paintingModuleSettingUICanvasYaoganSliderText.text = a.ToString();

    //}
    //public void PaintingModuleSettingShijueSliderTextEvent(float a)
    //{
    //    paintingModuleSettingUICanvasShijueSliderText.text = a.ToString();
    //}

    //public void PaintingModuleSettingUICanvasBGMMuteToggleEvent(bool a)
    //{
    //    SceneBGMManager.Instance.MuteSceneBGM();
    //}
    /// <summary>
    /// 不静音按钮打开事件
    /// </summary>
    public void PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOnEvent()
    {
        Debug.Log("不静音");

        PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn.interactable = false;
        PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff.interactable = true;
        PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[1];
        PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[0];

        SceneBGMManager.Instance.MuteSceneBGM();
    }
    /// <summary>
    /// 静音按钮打开事件
    /// </summary>
    public void PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOffEvent()
    {
        Debug.Log("静音");

        PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOn.interactable = true;
        PaintingModuleSettingCanvasSlider_BGMMuteLogoButtonOff.interactable = false;
        PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOn.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[0];
        PaintingModuleSettingCanvasSlider_BGMMuteLogoImageOff.sprite = PaintingModuleSettingCanvasSlider_BGMMuteLogoImageSprites[1];

        SceneBGMManager.Instance.MuteSceneBGM();
    }
    /// <summary>
    /// 设置侧边栏设置按钮事件
    /// </summary>
    public void PaintingModuleSettingCanvas_CebianlanShezhiButtonEvent()
    {
        PaintingModuleSettingCanvasGroupSetting.alpha = 1;
        PaintingModuleSettingCanvasGroupSetting.interactable = true;
        PaintingModuleSettingCanvasGroupSetting.blocksRaycasts = true;

        PaintingModuleSettingCanvasGroupZhanghucaozuo.alpha = 0;
        PaintingModuleSettingCanvasGroupZhanghucaozuo.interactable = false;
        PaintingModuleSettingCanvasGroupZhanghucaozuo.blocksRaycasts = false;

        PaintingModuleSettingCanvas_CebianlanShezhiButton.interactable = false;
        PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButton.interactable = true;
    }
    /// <summary>
    /// 设置侧边栏账号操作按钮事件
    /// </summary>
    public void PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButtonEvent()
    {
        PaintingModuleSettingCanvasGroupSetting.alpha = 0;
        PaintingModuleSettingCanvasGroupSetting.interactable = false;
        PaintingModuleSettingCanvasGroupSetting.blocksRaycasts = false;

        PaintingModuleSettingCanvasGroupZhanghucaozuo.alpha = 1;
        PaintingModuleSettingCanvasGroupZhanghucaozuo.interactable = true;
        PaintingModuleSettingCanvasGroupZhanghucaozuo.blocksRaycasts = true;

        PaintingModuleSettingCanvas_CebianlanShezhiButton.interactable = true;
        PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButton.interactable = false;
    }
    /// <summary>
    /// 设置侧边栏付款管理按钮事件
    /// </summary>
    public void PaintingModuleSettingCanvas_CebianlanFukuanguanliButtonEvent()
    {
        PaintingModuleSettingCanvasGroupSetting.alpha = 0;
        PaintingModuleSettingCanvasGroupSetting.interactable = false;
        PaintingModuleSettingCanvasGroupSetting.blocksRaycasts = false;

        PaintingModuleSettingCanvasGroupZhanghucaozuo.alpha = 0;
        PaintingModuleSettingCanvasGroupZhanghucaozuo.interactable = false;
        PaintingModuleSettingCanvasGroupZhanghucaozuo.blocksRaycasts = false;

        PaintingModuleSettingCanvas_CebianlanShezhiButton.interactable = true;
        PaintingModuleSettingCanvas_CebianlanZhanghucaozuoButton.interactable = true;

    }
    /// <summary>
    /// 设置侧边栏关于应用按钮事件
    /// </summary>
    public void PaintingModuleSettingCanvas_CebianlanGuanyuyingyongButtonEvent()
    {
        CustomUrlOpener.Open("https://vr-gallery-j.com/terms.html");
    }

    public void PaintingModuleSceneModeEvent()
    {
        PaintingModuleMapUICanvasExit();
        PaintingModuleHelpUICanvasExit();
        PaintingModuleMapCanvasStart();
        PaintingModuleMapCanvasSamrtMode();
    }

    /// <summary>
    /// 帮助和设置语言切换
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
    /// 画作设置删除账户事件
    /// </summary>
    public void PaintingModuleSettingCanvas_DeleteAccountEvent()
    {
        PaintingModuleSettingCanvas_DeleteAccountPopup.alpha = 1;
        PaintingModuleSettingCanvas_DeleteAccountPopup.blocksRaycasts = true;
        PaintingModuleSettingCanvas_DeleteAccountPopup.interactable = true;
    }
    /// <summary>
    /// 画作设置删除账户弹窗确认事件
    /// </summary>
    public void PaintingModuleSettingCanvas_DeleteAccountPopupButtonYesEvent()
    {
        //PaintingModuleSettingCanvas_DeleteAccountPopup.alpha = 0;
        //PaintingModuleSettingCanvas_DeleteAccountPopup.blocksRaycasts = false;
        //PaintingModuleSettingCanvas_DeleteAccountPopup.interactable = false;

        HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.DeleteUserByIdURL, null);
    }
    /// <summary>
    /// 画作设置删除账户弹窗取消事件
    /// </summary>
    public void PaintingModuleSettingCanvas_DeleteAccountPopupButtonNoEvent()
    {
        PaintingModuleSettingCanvas_DeleteAccountPopup.alpha = 0;
        PaintingModuleSettingCanvas_DeleteAccountPopup.blocksRaycasts = false;
        PaintingModuleSettingCanvas_DeleteAccountPopup.interactable = false;
    }

}

public enum MapState
{
    None,
    Smart,
    Map
}
