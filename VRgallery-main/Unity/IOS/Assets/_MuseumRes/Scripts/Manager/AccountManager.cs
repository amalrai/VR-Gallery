using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using yoyohan;

public class AccountManager : UnitySingleton<AccountManager>
{
    #region 通用属性 
    public List<languageSprite> languageUIs;
    public TextLanguageReplace[] textReplaces;
    public Sprite[] rememberPasswordSprites;
    public Sprite[] visiblePasswordSprites;
    public Sprite[] selectPMSprites;
    public Sprite[] consentClauseSprites;

    public bool isCountdown = false;
    public float timer = 60f;
    public bool zuceCountdown = false;
    public bool wangjiCountdown = false;
    public bool consentClause = false;
    #endregion

    #region 首页属性
    //---------------------------------------------------------------首页---------------------------------------------------------------

    //public CanvasGroup AccountHomepageCanvasGroup;
    //public Button AccountHomepageButtonShuoming;

    ////public Button AccountHomepageSelectPMButtonAkita;
    ////public Image AccountHomepageSelectPMButtonImageAkita;
    ////public TextLanguageReplace AccountHomepageSelectPMButtonTextLanguageReplaceAkita;
    ////public Button AccountHomepageSelectPMButtonSettsu;
    ////public Image AccountHomepageSelectPMButtonImageSettsu;
    ////public TextLanguageReplace AccountHomepageSelectPMButtonTextLanguageReplaceSettsu;
    //public Button AccountHomepageButtonDenglu;

    #endregion

    #region 登录属性
    //---------------------------------------------------------------登录---------------------------------------------------------------

    public CanvasGroup AccountSigninCanvasGroup;
    public InputField AccountSigninButtonUsernameInputField;
    public CanHideInputField AccountSigninButtonPasswordInputField;
    public Image AccountSigninButtonPasswordInputFieldImage;
    public Button AccountSigninButtonPasswordInputFieldButton;
    public Button AccountSigninButtonDenglu;
    public Button AccountSigninButtonWangjimima;
    public Button AccountSigninButtonShuoming;
    public Button AccountSigninButtonJizhumima;
    public Image AccountSigninImagJizhumima;
    public bool IsRemember;
    //public Button AccountSigninButtonShuoming;
    //public Button AccountSigninButtonReturn;
    //public Button AccountSigninButtonChooseRoom;
    //public TextLanguageReplace AccountSigninButtonChooseRoomlanguageSprite;
    //public Text AccountSigninTextMeishuguan;

    public Button AccountSigninLanguageButtonZhongwen;
    public Button AccountSigninLanguageButtonYingwen;
    public Button AccountSigninLanguageButtonRiwen;
    public Image AccountSigninLanguageImageZhongwen;
    public Image AccountSigninLanguageImageYingwen;
    public Image AccountSigninLanguageImageRiwen;

    public Button AccountSigninButtonZhuce;
    public Button AccountSigninButtonYouke;
    #endregion

    #region 注册属性
    //---------------------------------------------------------------注册---------------------------------------------------------------

    public CanvasGroup AccountRegisterCanvasGroup;
    public InputField AccountRegisterButtonYouxiangInputField;
    public InputField AccountRegisterButtonYanzhengmaInputField;
    public CanHideInputField AccountRegisterButtonShurumimaInputField;
    public Image AccountRegisterButtonShurumimaInputFieldImage;
    public Button AccountRegisterButtonShurumimaInputFieldButton;
    public CanHideInputField AccountRegisterButtonZaicimimaInputField;
    public Image AccountRegisterButtonZaicimimaInputFieldImage;
    public Button AccountRegisterButtonZaicimimaInputFieldButton;
    public Button AccountRegisterButtonFasong;
    public Button AccountRegisterButtonQueding;
    public Button AccountRegisterButtonReturn;
    public Button AccountRegisterButtonChongxin;
    public Text AccountRegisterTextdaojishi;
    //public Dropdown AccountRegisterDropdownNiandai;
    //public Image AccountRegisterDropdownNiandaiLabelImageMask;
    //public Dropdown AccountRegisterDropdownXingbie;
    //public Image AccountRegisterDropdownXingbieLabelImageMask;
    //public Text AccountRegisterDropdownDiyuTishiText;
    //public Text AccountRegisterDropdownDiyuText;
    //public Button AccountRegisterButtonDiyu;
    //public Button AccountRegisterButtonDiyuMask;
    //public ScrollRect AccountRegisterScrollRectDiyu;
    //public RectTransform AccountRegisterScrollRectDiyuContent;
    //public TextLanguageReplace AccountRegisterDropdownDiyuTishiTextLanguageReplace;
    #endregion

    #region 忘记密码属性
    //---------------------------------------------------------------忘记密码---------------------------------------------------------------

    public CanvasGroup ChangePasswordCanvasGroup;
    public InputField ChangePasswordButtonYouxiangInputField;
    public InputField ChangePasswordButtonYanzhengmaInputField;
    public CanHideInputField ChangePasswordButtonShurumimaInputField;
    public Image ChangePasswordButtonShurumimaInputFieldImage;
    public Button ChangePasswordButtonShurumimaInputFieldButton;
    public CanHideInputField ChangePasswordButtonZaicimimaInputField;
    public Image ChangePasswordButtonZaicimimaInputFieldImage;
    public Button ChangePasswordButtonZaicimimaInputFieldButton;
    public Button ChangePasswordButtonFasong;
    public Button ChangePasswordButtonQueding;
    public Button ChangePasswordButtonReturn;
    public Button ChangePasswordButtonChongxin;
    public Text ChangePasswordTextdaojishi;
    #endregion

    #region 同意条款
    //---------------------------------------------------------------同意条款---------------------------------------------------------------

    public CanvasGroup ConsentClauseCanvasGroup;
    public Button ConsentClauseButtonNext;
    public Button ConsentClauseButtonReturn;
    public Button ConsentClauseButtonAgree2dCollider;
    public Image ConsentClauseImageAgree;
    public Scrollbar ConsentClauseTiaokuanScrollbar;


    #endregion

    #region 选择场景
    //---------------------------------------------------------------选择场景---------------------------------------------------------------

    public CanvasGroup SceneSelectionCanvasGroup;
    public Button SceneSelectionButtonNext;
    public Button SceneSelectionButtonReturn;

    public Button[] SceneSelectionLayoutGroupButtons;
    #endregion

    #region 弹窗属性
    //---------------------------------------------------------------弹窗---------------------------------------------------------------

    public CanvasGroup PopupWindow;
    public Button PopupWindowMaskBG;
    public Text PopupWindowTextYifasong;
    public Text PopupWindowTextYouxianggeshi;
    public Text PopupWindowTextChongxinhuoqu;
    public Text PopupWindowTextYouxiangzhuce;
    public Text PopupWindowTextYanzhengma;
    public Text PopupWindowTextMimachangdu;
    public Text PopupWindowTextZhanghaocuowu;
    public Text PopupWindowTextZhanghaomimacuowu;
    public Text PopupWindowTextWangluochaoshi;
    public Text PopupWindowTextMimabuxiangtong;
    public Text PopupWindowTextMimaxiugaichenggong;
    public Text PopupWindowTextZhengzaixiazairenwu;
    public Text PopupWindowTextZidingyi;
    #endregion

    private void Awake()
    {
        //-------------------------------------------------------获取需要切换语言的脚本---------------------------------------------------------------
        textReplaces = this.GetComponentsInChildren<TextLanguageReplace>();

        #region 首页Awake
        //---------------------------------------------------------------首页---------------------------------------------------------------
        //    AccountHomepageCanvasGroup = GameObject.Find("AccountHomepageCanvasGroup").GetComponent<CanvasGroup>();
        //    AccountHomepageCanvasGroup.alpha = 1;
        //    AccountHomepageCanvasGroup.blocksRaycasts = true;
        //    AccountHomepageCanvasGroup.interactable = true;

        //    AccountHomepageButtonShuoming = GameObject.Find("AccountHomepageButtonShuoming").GetComponent<Button>();

        //    AccountHomepageButtonShuoming.onClick.AddListener(
        //() =>
        //{
        //    CustomUrlOpener.Open("https://vr-gallery-j.com/about.html#tutorial");
        //});


        //    //AccountHomepageSelectPMButtonAkita = GameObject.Find("AccountHomepageSelectPMButtonAkita").GetComponent<Button>();
        //    //AccountHomepageSelectPMButtonSettsu = GameObject.Find("AccountHomepageSelectPMButtonSettsu").GetComponent<Button>();

        //    //AccountHomepageSelectPMButtonImageAkita = AccountHomepageSelectPMButtonAkita.GetComponent<Image>();
        //    //AccountHomepageSelectPMButtonImageSettsu = AccountHomepageSelectPMButtonSettsu.GetComponent<Image>();

        //    //AccountHomepageSelectPMButtonTextLanguageReplaceAkita = GameObject.Find("AccountHomepageSelectPMButtonAkitaText").GetComponent<TextLanguageReplace>();
        //    //AccountHomepageSelectPMButtonTextLanguageReplaceSettsu = GameObject.Find("AccountHomepageSelectPMButtonSettsuText").GetComponent<TextLanguageReplace>();
        //    ////AccountSigninButtonChooseRoomlanguageSprite = GameObject.Find("AccountSigninButtonChooseRoomText").GetComponent<TextLanguageReplace>();

        //    //if (PlayerPrefs.HasKey("SelectPM"))
        //    //{
        //    //    if (PlayerPrefs.GetString("SelectPM") == "Akita")
        //    //    {
        //    //        AccountHomepageSelectPMButtonImageAkita.sprite = selectPMSprites[1];
        //    //        AccountHomepageSelectPMButtonImageSettsu.sprite = selectPMSprites[0];

        //    //        //AccountSigninButtonChooseRoomlanguageSprite.ZH = AccountHomepageSelectPMButtonTextLanguageReplaceAkita.ZH;
        //    //        //AccountSigninButtonChooseRoomlanguageSprite.EN = AccountHomepageSelectPMButtonTextLanguageReplaceAkita.EN;
        //    //        //AccountSigninButtonChooseRoomlanguageSprite.JA = AccountHomepageSelectPMButtonTextLanguageReplaceAkita.JA;
        //    //    }
        //    //    else if (PlayerPrefs.GetString("SelectPM") == "Settsu")
        //    //    {
        //    //        AccountHomepageSelectPMButtonImageAkita.sprite = selectPMSprites[0];
        //    //        AccountHomepageSelectPMButtonImageSettsu.sprite = selectPMSprites[1];

        //    //        //AccountSigninButtonChooseRoomlanguageSprite.ZH = AccountHomepageSelectPMButtonTextLanguageReplaceSettsu.ZH;
        //    //        //AccountSigninButtonChooseRoomlanguageSprite.EN = AccountHomepageSelectPMButtonTextLanguageReplaceSettsu.EN;
        //    //        //AccountSigninButtonChooseRoomlanguageSprite.JA = AccountHomepageSelectPMButtonTextLanguageReplaceSettsu.JA;
        //    //    }
        //    //    else
        //    //    {
        //    //        AccountHomepageSelectPMButtonImageAkita.sprite = selectPMSprites[1];
        //    //        AccountHomepageSelectPMButtonImageSettsu.sprite = selectPMSprites[0];

        //    //        //AccountSigninButtonChooseRoomlanguageSprite.ZH = AccountHomepageSelectPMButtonTextLanguageReplaceSettsu.ZH;
        //    //        //AccountSigninButtonChooseRoomlanguageSprite.EN = AccountHomepageSelectPMButtonTextLanguageReplaceSettsu.EN;
        //    //        //AccountSigninButtonChooseRoomlanguageSprite.JA = AccountHomepageSelectPMButtonTextLanguageReplaceSettsu.JA;
        //    //    }

        //    //}
        //    //else
        //    //{
        //    //    AccountHomepageSelectPMButtonImageAkita.sprite = selectPMSprites[1];
        //    //    AccountHomepageSelectPMButtonImageSettsu.sprite = selectPMSprites[0];
        //    //    PlayerPrefs.SetString("SelectPM", "Akita");
        //    //}

        //    //AccountHomepageSelectPMButtonAkita.onClick.AddListener(() =>
        //    //{
        //    //    AccountHomepageSelectPMButtonImageAkita.sprite = selectPMSprites[1];
        //    //    AccountHomepageSelectPMButtonImageSettsu.sprite = selectPMSprites[0];
        //    //    PlayerPrefs.SetString("SelectPM", "Akita");

        //    //    //AccountSigninButtonChooseRoomlanguageSprite.ZH = AccountHomepageSelectPMButtonTextLanguageReplaceAkita.ZH;
        //    //    //AccountSigninButtonChooseRoomlanguageSprite.EN = AccountHomepageSelectPMButtonTextLanguageReplaceAkita.EN;
        //    //    //AccountSigninButtonChooseRoomlanguageSprite.JA = AccountHomepageSelectPMButtonTextLanguageReplaceAkita.JA;
        //    //    //AccountSigninButtonChooseRoomlanguageSprite.ReplaceLanguageText(PaintingModuleDataManager.Instance.language);
        //    //});

        //    //AccountHomepageSelectPMButtonSettsu.onClick.AddListener(() =>
        //    //{
        //    //    AccountHomepageSelectPMButtonImageAkita.sprite = selectPMSprites[0];
        //    //    AccountHomepageSelectPMButtonImageSettsu.sprite = selectPMSprites[1];
        //    //    PlayerPrefs.SetString("SelectPM", "Settsu");


        //    //    //AccountSigninButtonChooseRoomlanguageSprite.ZH = AccountHomepageSelectPMButtonTextLanguageReplaceSettsu.ZH;
        //    //    //AccountSigninButtonChooseRoomlanguageSprite.EN = AccountHomepageSelectPMButtonTextLanguageReplaceSettsu.EN;
        //    //    //AccountSigninButtonChooseRoomlanguageSprite.JA = AccountHomepageSelectPMButtonTextLanguageReplaceSettsu.JA;
        //    //    //AccountSigninButtonChooseRoomlanguageSprite.ReplaceLanguageText(PaintingModuleDataManager.Instance.language);
        //    //});

        //    AccountHomepageButtonDenglu = GameObject.Find("AccountHomepageButtonDenglu").GetComponent<Button>();
        //    AccountHomepageButtonDenglu.onClick.AddListener(AccountHomepageButtonDengluEvent);


        #endregion

        #region 登录Awake
        //---------------------------------------------------------------登录---------------------------------------------------------------
        AccountSigninCanvasGroup = GameObject.Find("AccountSigninCanvasGroup").GetComponent<CanvasGroup>();
        AccountSigninCanvasGroup.alpha = 1;
        AccountSigninCanvasGroup.blocksRaycasts = true;
        AccountSigninCanvasGroup.interactable = true;

        AccountSigninButtonUsernameInputField =
            GameObject.Find("AccountSigninButtonUsernameInputField").GetComponent<InputField>();

        AccountSigninButtonPasswordInputField =
            GameObject.Find("AccountSigninButtonPasswordInputField").GetComponent<CanHideInputField>();

        AccountSigninButtonPasswordInputFieldButton = GameObject.Find("AccountSigninButtonPasswordInputFieldButton").GetComponent<Button>();
        AccountSigninButtonPasswordInputFieldImage = AccountSigninButtonPasswordInputFieldButton.GetComponent<Image>();
        AccountSigninButtonPasswordInputFieldButton.onClick.AddListener(AccountSigninButtonPasswordInputFieldButtonEvent);

        AccountSigninButtonDenglu = GameObject.Find("AccountSigninButtonDenglu").GetComponent<Button>();
        AccountSigninButtonDenglu.onClick.AddListener(AccountSigninButtonDengluEvent);

        AccountSigninButtonWangjimima = GameObject.Find("AccountSigninButtonWangjimima").GetComponent<Button>();
        AccountSigninButtonWangjimima.onClick.AddListener(AccountSigninButtonWangjimimaEvent);

        AccountSigninButtonShuoming = GameObject.Find("AccountSigninButtonShuoming").GetComponent<Button>();
        AccountSigninButtonShuoming.onClick.AddListener(
            () =>
            {
                CustomUrlOpener.Open("https://vr-gallery-j.com/");
            });

        //AccountSigninTextMeishuguan = GameObject.Find("AccountSigninTextMeishuguan").GetComponent<Text>();

        //AccountSigninButtonReturn = GameObject.Find("AccountSigninButtonReturn").GetComponent<Button>();
        //AccountSigninButtonReturn.onClick.AddListener(AccountSigninButtonReturnEvent);

        //AccountSigninButtonChooseRoom = GameObject.Find("AccountSigninButtonChooseRoom").GetComponent<Button>();
        //AccountSigninButtonChooseRoom.onClick.AddListener(AccountSigninButtonReturnEvent);

        AccountSigninButtonJizhumima = GameObject.Find("AccountSigninButtonJizhumima").GetComponent<Button>();
        AccountSigninButtonJizhumima.onClick.AddListener(AccountSigninButtonJizhumimaEvent);
        AccountSigninImagJizhumima = GameObject.Find("AccountSigninButtonJizhumima").GetComponent<Image>();

        //AccountSigninButtonShuoming = GameObject.Find("AccountSigninButtonShuoming").GetComponent<Button>();

        AccountSigninLanguageButtonZhongwen = GameObject.Find("AccountSigninLanguageZhongwen").GetComponent<Button>();
        AccountSigninLanguageButtonZhongwen.onClick.AddListener(
            () =>
            {
                SwitchLanguageEvent(Language.中文);
            });

        AccountSigninLanguageButtonYingwen = GameObject.Find("AccountSigninLanguageYingwen").GetComponent<Button>();
        AccountSigninLanguageButtonYingwen.onClick.AddListener(
            () =>
            {
                SwitchLanguageEvent(Language.英语);
            });

        AccountSigninLanguageButtonRiwen = GameObject.Find("AccountSigninLanguageRiwen").GetComponent<Button>();
        AccountSigninLanguageButtonRiwen.onClick.AddListener(
            () =>
            {
                SwitchLanguageEvent(Language.日文);
            });

        AccountSigninLanguageImageZhongwen = GameObject.Find("AccountSigninLanguageZhongwen").GetComponent<Image>();
        AccountSigninLanguageImageYingwen = GameObject.Find("AccountSigninLanguageYingwen").GetComponent<Image>();
        AccountSigninLanguageImageRiwen = GameObject.Find("AccountSigninLanguageRiwen").GetComponent<Image>();

        if (PlayerPrefs.HasKey("UserName"))
        {
            AccountSigninButtonUsernameInputField.text = PlayerPrefs.GetString("UserName");
        }
        else
        {
            AccountSigninButtonUsernameInputField.text = "";
        }

        if (PlayerPrefs.HasKey("IsRemember"))
        {
            if (PlayerPrefs.GetString("IsRemember") == "true")
            {
                IsRemember = true;
                AccountSigninImagJizhumima.sprite = rememberPasswordSprites[1];
                if (PlayerPrefs.HasKey("Password"))
                {
                    if (PlayerPrefs.GetString("Password") != "")
                    {
                        AccountSigninButtonPasswordInputField.text = PlayerPrefs.GetString("Password");
                        AccountSigninButtonPasswordInputField.contentType = InputField.ContentType.Password;
                        AccountSigninButtonPasswordInputFieldImage.sprite = visiblePasswordSprites[1];
                        Debug.Log("隐藏密码");
                    }
                    else
                    {
                        AccountSigninButtonPasswordInputField.text = "";
                    }
                }
            }
            else if (PlayerPrefs.GetString("IsRemember") == "false")
            {
                IsRemember = false;
                AccountSigninImagJizhumima.sprite = rememberPasswordSprites[0];
            }
            else if (PlayerPrefs.GetString("IsRemember") == "")
            {
                IsRemember = false;
                AccountSigninImagJizhumima.sprite = rememberPasswordSprites[0];
            }
        }

        if (PlayerPrefs.HasKey("IsVisible"))
        {
            Debug.Log(PlayerPrefs.GetString("IsVisible"));
            if (PlayerPrefs.GetString("IsVisible") == "true")
            {
                AccountSigninButtonPasswordInputField.contentType = InputField.ContentType.Standard;
                AccountSigninButtonPasswordInputField.lineType = InputField.LineType.MultiLineSubmit;
                AccountSigninButtonPasswordInputFieldImage.sprite = visiblePasswordSprites[0];
            }
            else if (PlayerPrefs.GetString("IsVisible") == "false")
            {
                AccountSigninButtonPasswordInputField.contentType = InputField.ContentType.Password;
                AccountSigninButtonPasswordInputFieldImage.sprite = visiblePasswordSprites[1];
            }
        }
        else
        {
            AccountSigninButtonPasswordInputField.contentType = InputField.ContentType.Standard;
            AccountSigninButtonPasswordInputField.lineType = InputField.LineType.MultiLineSubmit;
            AccountSigninButtonPasswordInputFieldImage.sprite = visiblePasswordSprites[0];
        }

        //AccountSigninButtonShuoming.onClick.AddListener(
        //    () =>
        //    {
        //        CustomUrlOpener.Open("https://vr-gallery-j.com/");
        //    });

        AccountSigninButtonZhuce = GameObject.Find("AccountSigninButtonZhuce").GetComponent<Button>();
        AccountSigninButtonZhuce.onClick.AddListener(AccountSigninButtonZhuceEvent);

        AccountSigninButtonYouke = GameObject.Find("AccountSigninButtonYouke").GetComponent<Button>();
        AccountSigninButtonYouke.onClick.AddListener(AccountSigninButtonYoukeEvent);

        #endregion

        #region 注册Awake
        //---------------------------------------------------------------注册---------------------------------------------------------------

        AccountRegisterCanvasGroup = GameObject.Find("AccountRegisterCanvasGroup").GetComponent<CanvasGroup>();
        AccountRegisterCanvasGroup.alpha = 0;
        AccountRegisterCanvasGroup.blocksRaycasts = false;
        AccountRegisterCanvasGroup.interactable = false;

        AccountRegisterButtonYouxiangInputField =
            GameObject.Find("AccountRegisterButtonYouxiangInputField").GetComponent<InputField>();
        AccountRegisterButtonYanzhengmaInputField =
            GameObject.Find("AccountRegisterButtonYanzhengmaInputField").GetComponent<InputField>();
        AccountRegisterButtonShurumimaInputField =
            GameObject.Find("AccountRegisterButtonShurumimaInputField").GetComponent<CanHideInputField>();
        AccountRegisterButtonZaicimimaInputField =
            GameObject.Find("AccountRegisterButtonZaicimimaInputField").GetComponent<CanHideInputField>();

        AccountRegisterButtonShurumimaInputFieldButton = GameObject.Find("AccountRegisterButtonShurumimaInputFieldButton").GetComponent<Button>();
        AccountRegisterButtonShurumimaInputFieldImage = AccountRegisterButtonShurumimaInputFieldButton.GetComponent<Image>();
        AccountRegisterButtonShurumimaInputFieldButton.onClick.AddListener(AccountRegisterButtonShurumimaInputFieldButtonEvent);

        AccountRegisterButtonZaicimimaInputFieldButton = GameObject.Find("AccountRegisterButtonZaicimimaInputFieldButton").GetComponent<Button>();
        AccountRegisterButtonZaicimimaInputFieldImage = AccountRegisterButtonZaicimimaInputFieldButton.GetComponent<Image>();
        AccountRegisterButtonZaicimimaInputFieldButton.onClick.AddListener(AccountRegisterButtonZaicimimaInputFieldButtonEvent);

        AccountRegisterButtonFasong = GameObject.Find("AccountRegisterButtonFasong").GetComponent<Button>();
        AccountRegisterButtonFasong.onClick.AddListener(AccountRegisterButtonFasongEvent);

        AccountRegisterButtonQueding = GameObject.Find("AccountRegisterButtonZhuce").GetComponent<Button>();
        AccountRegisterButtonQueding.onClick.AddListener(AccountRegisterButtonQuedingEvent);

        AccountRegisterButtonReturn = GameObject.Find("AccountRegisterButtonReturn").GetComponent<Button>();
        AccountRegisterButtonReturn.onClick.AddListener(AccountRegisterButtonReturnEvent);

        AccountRegisterButtonChongxin = GameObject.Find("AccountRegisterButtonChongxin").GetComponent<Button>();
        AccountRegisterButtonChongxin.onClick.AddListener(AccountRegisterButtonChongxinEvent);
        AccountRegisterTextdaojishi = GameObject.Find("AccountRegisterTextdaojishi").GetComponent<Text>();


        if (PlayerPrefs.HasKey("IsVisibleShurumima"))
        {
            Debug.Log(PlayerPrefs.GetString("IsVisibleShurumima"));
            if (PlayerPrefs.GetString("IsVisibleShurumima") == "true")
            {
                AccountRegisterButtonShurumimaInputField.contentType = InputField.ContentType.Standard;
                AccountRegisterButtonShurumimaInputField.lineType = InputField.LineType.MultiLineSubmit;
                AccountRegisterButtonZaicimimaInputField.contentType = InputField.ContentType.Standard;
                AccountRegisterButtonZaicimimaInputField.lineType = InputField.LineType.MultiLineSubmit;

                AccountRegisterButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[0];
                AccountRegisterButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[0];
            }
            else if (PlayerPrefs.GetString("IsVisibleShurumima") == "false")
            {
                AccountRegisterButtonShurumimaInputField.contentType = InputField.ContentType.Password;
                AccountRegisterButtonZaicimimaInputField.contentType = InputField.ContentType.Password;

                AccountRegisterButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[1];
                AccountRegisterButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[1];
            }
        }
        else
        {
            AccountRegisterButtonShurumimaInputField.contentType = InputField.ContentType.Standard;
            AccountRegisterButtonShurumimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            AccountRegisterButtonZaicimimaInputField.contentType = InputField.ContentType.Standard;
            AccountRegisterButtonZaicimimaInputField.lineType = InputField.LineType.MultiLineSubmit;

            AccountRegisterButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[0];
            AccountRegisterButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[0];
        }


        //AccountRegisterDropdownNiandai = GameObject.Find("AccountRegisterDropdownNiandai").GetComponent<Dropdown>();
        //AccountRegisterDropdownNiandai.onValueChanged.AddListener(AccountRegisterDropdownNiandaiOnValueChanged);
        //AccountRegisterDropdownNiandaiLabelImageMask = GameObject.Find("AccountRegisterDropdownNiandaiLabelImageMask").GetComponent<Image>();

        //AccountRegisterDropdownXingbie = GameObject.Find("AccountRegisterDropdownXingbie").GetComponent<Dropdown>();
        //AccountRegisterDropdownXingbie.onValueChanged.AddListener(AccountRegisterDropdownXingbieOnValueChanged);
        //AccountRegisterDropdownXingbieLabelImageMask = GameObject.Find("AccountRegisterDropdownXingbieiLabelImageMask").GetComponent<Image>();

        //string[] diyumingzi;

        //if (PaintingModuleDataManager.Instance.allRegionInfoResults.Count != 0)
        //{
        //    diyumingzi = new string[PaintingModuleDataManager.Instance.allRegionInfoResults.Count];
        //    for (int i = 0; i < diyumingzi.Length; i++)
        //    {
        //        diyumingzi[i] = PaintingModuleDataManager.Instance.allRegionInfoResults[i].name;
        //    }
        //    Debug.Log("读取到服务器上的地址。");
        //}
        //else
        //{
        //    diyumingzi = new string[48];
        //    diyumingzi[0] = "北海道";
        //    diyumingzi[1] = "青森県";
        //    diyumingzi[2] = "岩手県";
        //    diyumingzi[3] = "宮城県";
        //    diyumingzi[4] = "秋田県";
        //    diyumingzi[5] = "山形県";
        //    diyumingzi[6] = "福島県";
        //    diyumingzi[7] = "茨城県";
        //    diyumingzi[8] = "栃木県";
        //    diyumingzi[9] = "群馬県";
        //    diyumingzi[10] = "埼玉県";
        //    diyumingzi[11] = "千葉県";
        //    diyumingzi[12] = "東京都";
        //    diyumingzi[13] = "神奈川県";
        //    diyumingzi[14] = "新潟県";
        //    diyumingzi[15] = "山梨県";
        //    diyumingzi[16] = "長野県";
        //    diyumingzi[17] = "富山県";
        //    diyumingzi[18] = "石川県";
        //    diyumingzi[19] = "福井県";
        //    diyumingzi[20] = "岐阜県";
        //    diyumingzi[21] = "静岡県";
        //    diyumingzi[22] = "愛知県";
        //    diyumingzi[23] = "三重県";
        //    diyumingzi[24] = "滋賀県";
        //    diyumingzi[25] = "京都府";
        //    diyumingzi[26] = "大阪府";
        //    diyumingzi[27] = "兵庫県";
        //    diyumingzi[28] = "奈良県";
        //    diyumingzi[29] = "和歌山県";
        //    diyumingzi[30] = "鳥取県";
        //    diyumingzi[31] = "島根県";
        //    diyumingzi[32] = "岡山県";
        //    diyumingzi[33] = "広島県";
        //    diyumingzi[34] = "山口県";
        //    diyumingzi[35] = "徳島県";
        //    diyumingzi[36] = "香川県";
        //    diyumingzi[37] = "愛媛県";
        //    diyumingzi[38] = "高知県";
        //    diyumingzi[39] = "福岡県";
        //    diyumingzi[40] = "佐賀県";
        //    diyumingzi[41] = "長崎県";
        //    diyumingzi[42] = "熊本県";
        //    diyumingzi[43] = "大分県";
        //    diyumingzi[44] = "宮崎県";
        //    diyumingzi[45] = "鹿児島県";
        //    diyumingzi[46] = "沖縄県";
        //    diyumingzi[47] = "海外";

        //    Debug.Log("读取到本地上的地址。");
        //}

        //int hangshu = diyumingzi.Length / 4;
        //int yushu = diyumingzi.Length % 4;
        //if (yushu > 0)
        //    hangshu++;
        //AccountRegisterDropdownDiyuTishiText = GameObject.Find("AccountRegisterDropdownDiyuTishiText").GetComponent<Text>();
        //AccountRegisterDropdownDiyuTishiTextLanguageReplace = AccountRegisterDropdownDiyuTishiText.GetComponent<TextLanguageReplace>();
        //AccountRegisterDropdownDiyuText = GameObject.Find("AccountRegisterDropdownDiyuText").GetComponent<Text>();
        //AccountRegisterButtonDiyu = GameObject.Find("AccountRegisterButtonDiyu").GetComponent<Button>();
        //AccountRegisterButtonDiyuMask = GameObject.Find("AccountRegisterButtonDiyuMask").GetComponent<Button>();
        //AccountRegisterScrollRectDiyu = GameObject.Find("AccountRegisterScrollRectDiyu").GetComponent<ScrollRect>();
        //AccountRegisterScrollRectDiyuContent = GameObject.Find("AccountRegisterScrollRectDiyuContent").GetComponent<RectTransform>();

        //AccountRegisterScrollRectDiyuContent.sizeDelta = new Vector2(0, (hangshu * 80) + (hangshu + 1) * 30);

        //GameObject diyuButton = Resources.Load("AccountRegisterScrollRectDiyuButton") as GameObject;
        //List<int> indexDiyu = new List<int>();
        //List<GameObject> indexDiyuGame = new List<GameObject>();
        //for (int i = 0; i < diyumingzi.Length; i++)
        //{
        //    int index = i;
        //    int areaid = (index + 1);
        //    indexDiyu.Add(index);
        //    GameObject game = null;
        //    indexDiyuGame.Add(game);
        //    game = Instantiate(diyuButton, AccountRegisterScrollRectDiyuContent.transform);
        //    game.GetComponentInChildren<Text>().text = diyumingzi[index];
        //    game.GetComponent<Button>().onClick.AddListener(() =>
        //    {
        //        AccountRegisterDropdownDiyuTishiText.text = "";
        //        AccountRegisterDropdownDiyuText.text = diyumingzi[index];
        //        PaintingModuleDataManager.Instance.area = diyumingzi[index];
        //        PaintingModuleDataManager.Instance.areaId = areaid;
        //        AccountRegisterScrollRectDiyu.gameObject.SetActive(false);
        //        AccountRegisterButtonDiyuMask.gameObject.SetActive(false);
        //    });
        //}

        //AccountRegisterScrollRectDiyu.gameObject.SetActive(false);
        //AccountRegisterButtonDiyuMask.gameObject.SetActive(false);

        //AccountRegisterButtonDiyu.onClick.AddListener(() =>
        //{
        //    AccountRegisterScrollRectDiyu.gameObject.SetActive(true);
        //    AccountRegisterButtonDiyuMask.gameObject.SetActive(true);
        //});

        //AccountRegisterButtonDiyuMask.onClick.AddListener(() =>
        //{
        //    AccountRegisterScrollRectDiyu.gameObject.SetActive(false);
        //    AccountRegisterButtonDiyuMask.gameObject.SetActive(false);
        //});


        #endregion

        #region 忘记密码Awake
        //---------------------------------------------------------------忘记密码---------------------------------------------------------------


        ChangePasswordCanvasGroup = GameObject.Find("ChangePasswordCanvasGroup").GetComponent<CanvasGroup>();
        ChangePasswordCanvasGroup.alpha = 0;
        ChangePasswordCanvasGroup.blocksRaycasts = false;
        ChangePasswordCanvasGroup.interactable = false;

        ChangePasswordButtonYouxiangInputField =
            GameObject.Find("ChangePasswordButtonYouxiangInputField").GetComponent<InputField>();
        ChangePasswordButtonYanzhengmaInputField =
            GameObject.Find("ChangePasswordButtonYanzhengmaInputField").GetComponent<InputField>();
        ChangePasswordButtonShurumimaInputField =
            GameObject.Find("ChangePasswordButtonShurumimaInputField").GetComponent<CanHideInputField>();
        ChangePasswordButtonZaicimimaInputField =
            GameObject.Find("ChangePasswordButtonZaicimimaInputField").GetComponent<CanHideInputField>();

        ChangePasswordButtonShurumimaInputFieldButton = GameObject.Find("ChangePasswordButtonShurumimaInputFieldButton").GetComponent<Button>();
        ChangePasswordButtonShurumimaInputFieldImage = ChangePasswordButtonShurumimaInputFieldButton.GetComponent<Image>();
        ChangePasswordButtonShurumimaInputFieldButton.onClick.AddListener(ChangePasswordButtonShurumimaInputFieldButtonEvent);

        ChangePasswordButtonZaicimimaInputFieldButton = GameObject.Find("ChangePasswordButtonZaicimimaInputFieldButton").GetComponent<Button>();
        ChangePasswordButtonZaicimimaInputFieldImage = ChangePasswordButtonZaicimimaInputFieldButton.GetComponent<Image>();
        ChangePasswordButtonZaicimimaInputFieldButton.onClick.AddListener(ChangePasswordButtonZaicimimaInputFieldButtonEvent);

        ChangePasswordButtonFasong = GameObject.Find("ChangePasswordButtonFasong").GetComponent<Button>();
        ChangePasswordButtonFasong.onClick.AddListener(ChangePasswordButtonFasongEvent);

        ChangePasswordButtonQueding = GameObject.Find("ChangePasswordButtonQueding").GetComponent<Button>();
        ChangePasswordButtonQueding.onClick.AddListener(ChangePasswordButtonQuedingEvent);

        ChangePasswordButtonReturn = GameObject.Find("ChangePasswordButtonReturn").GetComponent<Button>();
        ChangePasswordButtonReturn.onClick.AddListener(ChangePasswordButtonReturnEvent);

        ChangePasswordButtonChongxin = GameObject.Find("ChangePasswordButtonChongxin").GetComponent<Button>();
        ChangePasswordButtonChongxin.onClick.AddListener(ChangePasswordButtonChongxinEvent);
        ChangePasswordTextdaojishi = GameObject.Find("ChangePasswordTextdaojishi").GetComponent<Text>();

        if (PlayerPrefs.HasKey("IsVisibleZaicimima"))
        {
            Debug.Log(PlayerPrefs.GetString("IsVisibleZaicimima"));
            if (PlayerPrefs.GetString("IsVisibleZaicimima") == "true")
            {
                ChangePasswordButtonShurumimaInputField.contentType = InputField.ContentType.Standard;
                ChangePasswordButtonShurumimaInputField.lineType = InputField.LineType.MultiLineSubmit;
                ChangePasswordButtonZaicimimaInputField.contentType = InputField.ContentType.Standard;
                ChangePasswordButtonZaicimimaInputField.lineType = InputField.LineType.MultiLineSubmit;

                ChangePasswordButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[0];
                ChangePasswordButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[0];
            }
            else if (PlayerPrefs.GetString("IsVisibleZaicimima") == "false")
            {
                ChangePasswordButtonShurumimaInputField.contentType = InputField.ContentType.Password;
                ChangePasswordButtonZaicimimaInputField.contentType = InputField.ContentType.Password;

                ChangePasswordButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[1];
                ChangePasswordButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[1];
            }
        }
        else
        {
            ChangePasswordButtonShurumimaInputField.contentType = InputField.ContentType.Standard;
            ChangePasswordButtonShurumimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            ChangePasswordButtonZaicimimaInputField.contentType = InputField.ContentType.Standard;
            ChangePasswordButtonZaicimimaInputField.lineType = InputField.LineType.MultiLineSubmit;

            ChangePasswordButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[0];
            ChangePasswordButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[0];
        }
        #endregion

        #region 同意条款Awake
        //---------------------------------------------------------------登录---------------------------------------------------------------
        ConsentClauseCanvasGroup = GameObject.Find("ConsentClauseCanvasGroup").GetComponent<CanvasGroup>();
        ConsentClauseCanvasGroup.alpha = 0;
        ConsentClauseCanvasGroup.blocksRaycasts = false;
        ConsentClauseCanvasGroup.interactable = false;

        ConsentClauseButtonNext = GameObject.Find("ConsentClauseButtonNext").GetComponent<Button>();
        ConsentClauseButtonNext.onClick.AddListener(ConsentClauseButtonNextEvent);
        ConsentClauseButtonNext.interactable = false;

        ConsentClauseButtonReturn = GameObject.Find("ConsentClauseButtonReturn").GetComponent<Button>();
        ConsentClauseButtonReturn.onClick.AddListener(ConsentClauseButtonReturnEvent);

        consentClause = false;
        ConsentClauseButtonAgree2dCollider = GameObject.Find("ConsentClauseButtonAgree2dCollider").GetComponent<Button>();
        ConsentClauseButtonAgree2dCollider.onClick.AddListener(ConsentClauseButtonAgreeEvent);
        ConsentClauseImageAgree = GameObject.Find("ConsentClauseButtonAgree").GetComponent<Image>();
        ConsentClauseImageAgree.sprite = consentClauseSprites[0];

        ConsentClauseTiaokuanScrollbar = GameObject.Find("ConsentClauseTiaokuanScrollbar").GetComponent<Scrollbar>();
        ConsentClauseTiaokuanScrollbar.value = 1;

        #endregion

        #region 选择场景Awake
        //---------------------------------------------------------------登录---------------------------------------------------------------
        SceneSelectionCanvasGroup = GameObject.Find("SceneSelectionCanvasGroup").GetComponent<CanvasGroup>();
        SceneSelectionCanvasGroup.alpha = 0;
        SceneSelectionCanvasGroup.blocksRaycasts = false;
        SceneSelectionCanvasGroup.interactable = false;

        SceneSelectionButtonNext = GameObject.Find("SceneSelectionButtonNext").GetComponent<Button>();
        SceneSelectionButtonNext.onClick.AddListener(SceneSelectionButtonNextEvent);
        SceneSelectionButtonNext.interactable = false;

        SceneSelectionButtonReturn = GameObject.Find("SceneSelectionButtonReturn").GetComponent<Button>();
        SceneSelectionButtonReturn.onClick.AddListener(SceneSelectionButtonReturnEvent);

        List<int> sceneindex = new List<int>();

        SceneSelectionLayoutGroupButtons = GameObject.Find("SceneSelectionLayoutGroup").GetComponentsInChildren<Button>();
        for (int i = 0; i < SceneSelectionLayoutGroupButtons.Length; i++)
        {
            int a = i;
            sceneindex.Add(a);
            SceneSelectionLayoutGroupButtons[a].GetComponent<SceneData>().sceneSelete.enabled = false;
            SceneSelectionLayoutGroupButtons[a].GetComponent<SceneData>().sceneImage.enabled = false;
            SceneSelectionLayoutGroupButtons[a].GetComponent<SceneData>().sceneImage.fillAmount = 1;
            SceneSelectionLayoutGroupButtons[a].onClick.AddListener(() =>
            {
                SceneSelectionLayoutGroupButtonEvent(a);
            });

        }

        #endregion

        #region 弹窗Awake
        //---------------------------------------------------------------弹窗---------------------------------------------------------------
        PopupWindow = GameObject.Find("PopupWindow").GetComponent<CanvasGroup>();
        PopupWindow.alpha = 0;
        PopupWindow.blocksRaycasts = false;
        PopupWindow.interactable = false;

        PopupWindowTextYifasong = GameObject.Find("PopupWindowTextYifasong").GetComponent<Text>();
        PopupWindowTextYouxianggeshi = GameObject.Find("PopupWindowTextYouxianggeshi").GetComponent<Text>();
        PopupWindowTextChongxinhuoqu = GameObject.Find("PopupWindowTextChongxinhuoqu").GetComponent<Text>();
        PopupWindowTextYouxiangzhuce = GameObject.Find("PopupWindowTextYouxiangzhuce").GetComponent<Text>();
        PopupWindowTextYanzhengma = GameObject.Find("PopupWindowTextYanzhengma").GetComponent<Text>();
        PopupWindowTextMimachangdu = GameObject.Find("PopupWindowTextMimachangdu").GetComponent<Text>();
        PopupWindowTextZhanghaocuowu = GameObject.Find("PopupWindowTextZhanghaocuowu").GetComponent<Text>();
        PopupWindowTextZhanghaomimacuowu = GameObject.Find("PopupWindowTextZhanghaomimacuowu").GetComponent<Text>();
        PopupWindowTextWangluochaoshi = GameObject.Find("PopupWindowTextWangluochaoshi").GetComponent<Text>();
        PopupWindowTextMimabuxiangtong = GameObject.Find("PopupWindowTextMimabuxiangtong").GetComponent<Text>();
        PopupWindowTextMimaxiugaichenggong = GameObject.Find("PopupWindowTextMimaxiugaichenggong").GetComponent<Text>();
        PopupWindowTextZhengzaixiazairenwu = GameObject.Find("PopupWindowTextZhengzaixiazairenwu").GetComponent<Text>();
        PopupWindowTextZidingyi = GameObject.Find("PopupWindowTextZidingyi").GetComponent<Text>();

        PopupWindowTextYifasong.enabled = false;
        PopupWindowTextYouxianggeshi.enabled = false;
        PopupWindowTextChongxinhuoqu.enabled = false;
        PopupWindowTextYouxiangzhuce.enabled = false;
        PopupWindowTextYanzhengma.enabled = false;
        PopupWindowTextMimachangdu.enabled = false;
        PopupWindowTextZhanghaocuowu.enabled = false;
        PopupWindowTextZhanghaomimacuowu.enabled = false;
        PopupWindowTextWangluochaoshi.enabled = false;
        PopupWindowTextMimabuxiangtong.enabled = false;
        PopupWindowTextMimaxiugaichenggong.enabled = false;
        PopupWindowTextZhengzaixiazairenwu.enabled = false;
        PopupWindowTextZidingyi.enabled = false;

        PopupWindowMaskBG = GameObject.Find("PopupWindowMaskBG").GetComponent<Button>();
        PopupWindowMaskBG.onClick.AddListener(PopupWindowCloseEvent);
        #endregion

    }

    string PlayerPrefs_Language;
    Language language;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            PlayerPrefs_Language = PlayerPrefs.GetString("Language");
             language = (Language)Enum.Parse(typeof(Language), PlayerPrefs_Language);
            SwitchLanguageEvent(PaintingModuleDataManager.Instance.language);
        }
        else
        {
            PlayerPrefs_Language = "";
            SwitchLanguageEvent(Language.日文);
        }


        AccountRegisterButtonChongxin.gameObject.SetActive(false);
        ChangePasswordButtonChongxin.gameObject.SetActive(false);

        if (PaintingModuleDataManager.Instance.isChangePassword)
        {
            AccountSigninButtonWangjimimaEvent();
            PaintingModuleDataManager.Instance.isChangePassword = false;
            PaintingModuleDataManager.Instance.isYouke = false;
        }

        if (PaintingModuleDataManager.Instance.isYouke)
        {
            AccountSigninButtonZhuceEvent();
            PaintingModuleDataManager.Instance.isYouke = false;
        }
    }

    public void Update()
    {
        if (!isCountdown)
        {
            return;
        }
        else
        {
            if (timer <= 0)
            {
                isCountdown = false;
                timer = 60f;
                if (zuceCountdown)
                {
                    zuceCountdown = false;
                    AccountRegisterButtonChongxinResetEvent();
                }
                if (wangjiCountdown)
                {
                    wangjiCountdown = false;
                    ChangePasswordButtonChongxinResetEvent();
                }
            }
            else
            {
                if (zuceCountdown)
                {
                    AccountRegisterTextdaojishi.text = ((int)timer).ToString();
                }
                if (wangjiCountdown)
                {
                    ChangePasswordTextdaojishi.text = ((int)timer).ToString();
                }
                timer -= Time.deltaTime;
            }
        }
    }

    #region 首页函数
    //---------------------------------------------------------------首页函数---------------------------------------------------------------
    ///// <summary>
    ///// 主页登录按钮事件
    ///// </summary>
    //public void AccountHomepageButtonDengluEvent()
    //{
    //    Debug.Log("跳转登录界面");
    //    AccountHomepageCanvasGroup.alpha = 0;
    //    AccountHomepageCanvasGroup.blocksRaycasts = false;
    //    AccountHomepageCanvasGroup.interactable = false;

    //    AccountSigninCanvasGroup.alpha = 1;
    //    AccountSigninCanvasGroup.blocksRaycasts = true;
    //    AccountSigninCanvasGroup.interactable = true;

    //    //if (PlayerPrefs.HasKey("SelectPM"))
    //    //{
    //    //    if (PlayerPrefs.GetString("SelectPM") == "Akita")
    //    //    {
    //    //        AccountSigninTextMeishuguan.text = "秋田伝統工芸品館";
    //    //    }
    //    //    else if (PlayerPrefs.GetString("SelectPM") == "Settsu")
    //    //    {
    //    //        AccountSigninTextMeishuguan.text = "漆工房攝津";
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    AccountSigninTextMeishuguan.text = "秋田伝統工芸品館";
    //    //}

    //    //if (PlayerPrefs.HasKey("SelectPM"))
    //    //{
    //    //    if (PlayerPrefs.GetString("SelectPM") == "Akita")
    //    //    {
    //    //        AccountSigninButtonChooseRoomlanguageSprite.ReplaceLanguageText(PaintingModuleDataManager.Instance.language);
    //    //    }
    //    //    else if (PlayerPrefs.GetString("SelectPM") == "Settsu")
    //    //    {
    //    //        AccountSigninButtonChooseRoomlanguageSprite.ReplaceLanguageText(PaintingModuleDataManager.Instance.language);
    //    //    }
    //    //    else
    //    //    {
    //    //        AccountSigninButtonChooseRoomlanguageSprite.ReplaceLanguageText(PaintingModuleDataManager.Instance.language);
    //    //    }
    //    //}
    //}

    ///// <summary>
    ///// 主页注册按钮事件
    ///// </summary>
    //public void AccountHomepageButtonZhuceEvent()
    //{
    //    Debug.Log("跳转注册界面");


    //    AccountHomepageCanvasGroup.alpha = 0;
    //    AccountHomepageCanvasGroup.blocksRaycasts = false;
    //    AccountHomepageCanvasGroup.interactable = false;

    //    ConsentClauseCanvasGroup.alpha = 1;
    //    ConsentClauseCanvasGroup.blocksRaycasts = true;
    //    ConsentClauseCanvasGroup.interactable = true;


    //}


    #endregion

    #region 登录函数
    //---------------------------------------------------------------登录函数---------------------------------------------------------------
    /// <summary>
    /// 主页登录按钮事件
    /// </summary>
    public void AccountSigninButtonDengluEvent()
    {
        Debug.Log("登录");

        string email = AccountSigninButtonUsernameInputField.text;
        string password = AccountSigninButtonPasswordInputField.text;

        if (email == "")
        {
            Debug.Log("email为空");
            PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
            return;
        }
        else
        {
            string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, expression))
            {
                Debug.Log("email格式不正确");
                PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
                return;
            }
            else
            {
                int num = CalculatePlaces(password);
                if (num < 8 || num > 20)
                {
                    Debug.Log("密码长度不正确");
                    PopupWindowOpenEvent(PopupWindowState.密码长度必须在);
                    return;
                }
                else
                {
                    Debug.Log("email登录请求成功发送");
                    PaintingModuleDataManager.Instance.email = email;
                    PaintingModuleDataManager.Instance.pwd = password;
                    HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.LoginURL, null);

                }

            }
        }
    }

    /// <summary>
    /// 主页忘记密码按钮事件
    /// </summary>
    public void AccountSigninButtonWangjimimaEvent()
    {
        Debug.Log("跳转忘记密码界面");
        AccountSigninCanvasGroup.alpha = 0;
        AccountSigninCanvasGroup.blocksRaycasts = false;
        AccountSigninCanvasGroup.interactable = false;

        ChangePasswordCanvasGroup.alpha = 1;
        ChangePasswordCanvasGroup.blocksRaycasts = true;
        ChangePasswordCanvasGroup.interactable = true;
    }
    /// <summary>
    /// 主页记住密码按钮事件
    /// </summary>
    public void AccountSigninButtonJizhumimaEvent()
    {
        if (IsRemember)
        {
            IsRemember = false;
            AccountSigninImagJizhumima.sprite = rememberPasswordSprites[0];
        }
        else
        {
            IsRemember = true;
            AccountSigninImagJizhumima.sprite = rememberPasswordSprites[1];
        }
    }

    /// <summary>
    /// 语言切换
    /// </summary>
    public void SwitchLanguageEvent(Language language)
    {
        PaintingModuleDataManager.Instance.language = language;
        Debug.Log(language.ToString());
        PlayerPrefs.SetString("Language", language.ToString());


        switch (language)
        {
            case Language.中文:
                AccountSigninLanguageButtonZhongwen.interactable = false;
                AccountSigninLanguageImageZhongwen.sprite = languageUIs[0].select;
                AccountSigninLanguageButtonYingwen.interactable = true;
                AccountSigninLanguageImageYingwen.sprite = languageUIs[1].noSelect;
                AccountSigninLanguageButtonRiwen.interactable = true;
                AccountSigninLanguageImageRiwen.sprite = languageUIs[2].noSelect;
                break;
            case Language.英语:
                AccountSigninLanguageButtonZhongwen.interactable = true;
                AccountSigninLanguageImageZhongwen.sprite = languageUIs[0].noSelect;
                AccountSigninLanguageButtonYingwen.interactable = false;
                AccountSigninLanguageImageYingwen.sprite = languageUIs[1].select;
                AccountSigninLanguageButtonRiwen.interactable = true;
                AccountSigninLanguageImageRiwen.sprite = languageUIs[2].noSelect;
                break;
            case Language.日文:
                AccountSigninLanguageButtonZhongwen.interactable = true;
                AccountSigninLanguageImageZhongwen.sprite = languageUIs[0].noSelect;
                AccountSigninLanguageButtonYingwen.interactable = true;
                AccountSigninLanguageImageYingwen.sprite = languageUIs[1].noSelect;
                AccountSigninLanguageButtonRiwen.interactable = false;
                AccountSigninLanguageImageRiwen.sprite = languageUIs[2].select;
                break;
        }

        if (textReplaces != null)
        {

            for (int i = 0; i < textReplaces.Length; i++)
            {
                textReplaces[i].ReplaceLanguageText(language);
            }
        }
    }
    /// <summary>
    /// 登陆成功
    /// </summary>
    public void LoginSuccessEvent()
    {
        if (IsRemember)
        {
            PlayerPrefs.SetString("UserName", AccountSigninButtonUsernameInputField.text);
            PlayerPrefs.SetString("Password", AccountSigninButtonPasswordInputField.text);
            PlayerPrefs.SetString("IsRemember", "true");
        }
        else
        {
            PlayerPrefs.SetString("UserName", AccountSigninButtonUsernameInputField.text);
            PlayerPrefs.SetString("Password", "");
            PlayerPrefs.SetString("IsRemember", "false");
        }
        Debug.Log("登录成功，跳转到场景选择界面");

        AccountSigninCanvasGroup.blocksRaycasts = false;
        AccountSigninCanvasGroup.interactable = false;
        AccountSigninCanvasGroup.alpha = 0;

        SceneSelectionCanvasGroup.alpha = 1;
        SceneSelectionCanvasGroup.blocksRaycasts = true;
        SceneSelectionCanvasGroup.interactable = true;

        SceneSelectionButtonNext.interactable = false;
        for (int i = 0; i < SceneSelectionLayoutGroupButtons.Length; i++)
        {
            SceneSelectionLayoutGroupButtons[i].interactable = true;
            SceneSelectionLayoutGroupButtons[i].GetComponent<SceneData>().sceneImage.enabled = false;
            SceneSelectionLayoutGroupButtons[i].GetComponent<SceneData>().sceneImage.fillAmount = 1;
            SceneSelectionLayoutGroupButtons[i].GetComponent<SceneData>().sceneSelete.enabled = false;
        }
    }

    /// <summary>
    /// 登录隐藏密码
    /// </summary>
    public void AccountSigninButtonPasswordInputFieldButtonEvent()
    {
        //隐藏密码
        if (AccountSigninButtonPasswordInputField.contentType == InputField.ContentType.Password)
        {
            AccountSigninButtonPasswordInputField.contentType = InputField.ContentType.Standard;
            AccountSigninButtonPasswordInputField.lineType = InputField.LineType.MultiLineSubmit;
            AccountSigninButtonPasswordInputFieldImage.sprite = visiblePasswordSprites[0];
            PlayerPrefs.SetString("IsVisible", "true");
            Debug.Log("显示密码");
        }
        else
        {
            AccountSigninButtonPasswordInputField.contentType = InputField.ContentType.Password;
            AccountSigninButtonPasswordInputFieldImage.sprite = visiblePasswordSprites[1];
            PlayerPrefs.SetString("IsVisible", "false");
            Debug.Log("隐藏密码");
        }
        AccountSigninButtonPasswordInputField.MyUpdateLabel();
    }

    ///// <summary>
    ///// 登录返回按钮事件
    ///// </summary>
    //public void AccountSigninButtonReturnEvent()
    //{
    //    Debug.Log("登录返回");
    //    AccountSigninCanvasGroup.blocksRaycasts = false;
    //    AccountSigninCanvasGroup.interactable = false;
    //    AccountSigninCanvasGroup.alpha = 0;

    //    AccountHomepageCanvasGroup.alpha = 1;
    //    AccountHomepageCanvasGroup.blocksRaycasts = true;
    //    AccountHomepageCanvasGroup.interactable = true;
    //}

    /// <summary>
    /// 主页注册按钮事件
    /// </summary>
    public void AccountSigninButtonZhuceEvent()
    {
        Debug.Log("跳转注册界面");


        AccountSigninCanvasGroup.alpha = 0;
        AccountSigninCanvasGroup.blocksRaycasts = false;
        AccountSigninCanvasGroup.interactable = false;

        ConsentClauseCanvasGroup.alpha = 1;
        ConsentClauseCanvasGroup.blocksRaycasts = true;
        ConsentClauseCanvasGroup.interactable = true;
    }


    /// <summary>
    /// 游客登录事件
    /// </summary>
    public void AccountSigninButtonYoukeEvent()
    {
        Debug.Log("游客登录成功");
        PaintingModuleDataManager.Instance.isYouke = true;

        AccountSigninButtonZhuceEvent();

        //HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.YoukeLoginURL, null);
        //SceneManager.LoadScene("Player");
    }
    #endregion

    #region 注册函数
    //---------------------------------------------------------------注册函数---------------------------------------------------------------
    /// <summary>
    /// 注册发送按钮事件
    /// </summary>
    public void AccountRegisterButtonFasongEvent()
    {
        Debug.Log("注册验证码发送");
        string email = AccountRegisterButtonYouxiangInputField.text;
        if (email == "")
        {
            Debug.LogError("email为空");
            PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
            return;
        }
        else
        {
            string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, expression))
            {
                Debug.LogError("email格式不正确");
                PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
                return;
            }
            else
            {
                Debug.Log("email注册验证码成功发送");
                PaintingModuleDataManager.Instance.email = email;
                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.SendRegisterCodeURL, null);
            }
        }
    }
    /// <summary>
    /// 注册发送按钮事件成功
    /// </summary>
    public void AccountRegisterButtonFasongEventSuccess()
    {
        isCountdown = true;
        zuceCountdown = true;
        AccountRegisterButtonFasong.gameObject.SetActive(false);
        AccountRegisterButtonChongxin.gameObject.SetActive(true);
        AccountRegisterButtonChongxin.interactable = false;
    }
    /// <summary>
    /// 注册重新发送按钮重置事件
    /// </summary>
    public void AccountRegisterButtonChongxinResetEvent()
    {
        AccountRegisterTextdaojishi.text = "";
        AccountRegisterButtonChongxin.interactable = true;
    }
    /// <summary>
    /// 登录重新发送
    /// </summary>
    public void AccountRegisterButtonChongxinEvent()
    {
        AccountRegisterButtonFasongEvent();
    }
    /// <summary>
    /// 注册确认按钮事件
    /// </summary>
    public void AccountRegisterButtonQuedingEvent()
    {
        Debug.Log("注册确认");
        string email = AccountRegisterButtonYouxiangInputField.text;
        string code = AccountRegisterButtonYanzhengmaInputField.text;
        string pwd1 = AccountRegisterButtonShurumimaInputField.text;
        string pwd2 = AccountRegisterButtonZaicimimaInputField.text;
        if (email == "")
        {
            Debug.LogError("email为空");
            PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
            return;
        }
        else
        {
            string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, expression))
            {
                Debug.LogError("email格式不正确");
                PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
                return;
            }
            else
            {
                if (code == "")
                {
                    Debug.Log("验证码为空");
                    PopupWindowOpenEvent(PopupWindowState.验证码错误);
                    return;
                }
                else
                {
                    int num1 = CalculatePlaces(pwd1);
                    if (num1 < 8 || num1 > 20)
                    {
                        Debug.LogError("密码长度不正确");
                        PopupWindowOpenEvent(PopupWindowState.密码长度必须在);
                        return;
                    }
                    else
                    {
                        int num2 = CalculatePlaces(pwd2);
                        if (num2 < 8 || num2 > 20)
                        {
                            Debug.LogError("密码长度不正确");
                            PopupWindowOpenEvent(PopupWindowState.密码长度必须在);
                            return;
                        }
                        else
                        {
                            if (pwd1 != pwd2)
                            {
                                Debug.LogError("密码不相同");
                                PopupWindowOpenEvent(PopupWindowState.密码不相同);
                                return;
                            }
                            else
                            {
                                Debug.Log("注册确认");
                                PaintingModuleDataManager.Instance.email = email;
                                PaintingModuleDataManager.Instance.code = code;
                                PaintingModuleDataManager.Instance.pwd = pwd1;
                                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.RegisterURL, null);
                            }
                        }

                    }
                }
            }
        }
    }
    /// <summary>
    /// 注册返回按钮事件
    /// </summary>
    public void AccountRegisterButtonReturnEvent()
    {
        Debug.Log("注册返回");

        zuceCountdown = false;
        isCountdown = false;
        timer = 60f;
        AccountRegisterTextdaojishi.text = "";
        AccountRegisterButtonFasong.gameObject.SetActive(true);
        AccountRegisterButtonChongxin.interactable = false;
        AccountRegisterButtonChongxin.gameObject.SetActive(false);

        AccountRegisterButtonYouxiangInputField.text = "";
        AccountRegisterButtonYanzhengmaInputField.text = "";
        AccountRegisterButtonShurumimaInputField.text = "";
        AccountRegisterButtonZaicimimaInputField.text = "";

        AccountRegisterCanvasGroup.blocksRaycasts = false;
        AccountRegisterCanvasGroup.interactable = false;
        AccountRegisterCanvasGroup.alpha = 0;

        AccountSigninCanvasGroup.alpha = 1;
        AccountSigninCanvasGroup.blocksRaycasts = true;
        AccountSigninCanvasGroup.interactable = true;

        //PaintingModuleDataManager.Instance.age = -1;
        //AccountRegisterDropdownNiandaiLabelImageMask.enabled = true;
        //AccountRegisterDropdownNiandai.ClearOptions();

        //PaintingModuleDataManager.Instance.sex = -1;
        //AccountRegisterDropdownXingbieLabelImageMask.enabled = true;
        //AccountRegisterDropdownXingbie.ClearOptions();

        //AccountRegisterDropdownDiyuTishiTextLanguageReplace.ReplaceLanguageText(PaintingModuleDataManager.Instance.language);
        //AccountRegisterDropdownDiyuText.text = "";
        //PaintingModuleDataManager.Instance.area = "";
        //PaintingModuleDataManager.Instance.areaId = -1;
    }

    /// <summary>
    /// 注册成功
    /// </summary>
    public void RegisterSuccessEvent()
    {
        PlayerPrefs.SetString("UserName", AccountRegisterButtonYouxiangInputField.text);
        PlayerPrefs.SetString("Password", "");
        PlayerPrefs.SetString("IsRemember", "false");
        Debug.Log("注册登录成功");

        AccountRegisterCanvasGroup.blocksRaycasts = false;
        AccountRegisterCanvasGroup.interactable = false;
        AccountRegisterCanvasGroup.alpha = 0;

        SceneSelectionCanvasGroup.alpha = 1;
        SceneSelectionCanvasGroup.blocksRaycasts = true;
        SceneSelectionCanvasGroup.interactable = true;

        SceneSelectionButtonNext.interactable = false;
        for (int i = 0; i < SceneSelectionLayoutGroupButtons.Length; i++)
        {
            SceneSelectionLayoutGroupButtons[i].interactable = true;
            SceneSelectionLayoutGroupButtons[i].GetComponent<SceneData>().sceneImage.enabled = false;
            SceneSelectionLayoutGroupButtons[i].GetComponent<SceneData>().sceneImage.fillAmount = 1;
            SceneSelectionLayoutGroupButtons[i].GetComponent<SceneData>().sceneSelete.enabled = false;
        }
    }

    /// <summary>
    /// 注册隐藏密码
    /// </summary>
    public void AccountRegisterButtonShurumimaInputFieldButtonEvent()
    {
        //隐藏密码
        if (AccountRegisterButtonShurumimaInputField.contentType == InputField.ContentType.Password)
        {
            AccountRegisterButtonShurumimaInputField.contentType = InputField.ContentType.Standard;
            AccountRegisterButtonShurumimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            AccountRegisterButtonZaicimimaInputField.contentType = InputField.ContentType.Standard;
            AccountRegisterButtonZaicimimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            AccountRegisterButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[0];
            AccountRegisterButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[0];
            PlayerPrefs.SetString("IsVisibleShurumima", "true");
            Debug.Log("显示密码");
        }
        else
        {
            AccountRegisterButtonShurumimaInputField.contentType = InputField.ContentType.Password;
            AccountRegisterButtonZaicimimaInputField.contentType = InputField.ContentType.Password;
            AccountRegisterButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[1];
            AccountRegisterButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[1];
            PlayerPrefs.SetString("IsVisibleShurumima", "false");
            Debug.Log("隐藏密码");
        }
        AccountRegisterButtonShurumimaInputField.MyUpdateLabel();
        AccountRegisterButtonZaicimimaInputField.MyUpdateLabel();
    }

    /// <summary>
    /// 注册隐藏密码
    /// </summary>
    public void AccountRegisterButtonZaicimimaInputFieldButtonEvent()
    {
        //隐藏密码
        if (AccountRegisterButtonZaicimimaInputField.contentType == InputField.ContentType.Password)
        {
            AccountRegisterButtonShurumimaInputField.contentType = InputField.ContentType.Standard;
            AccountRegisterButtonShurumimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            AccountRegisterButtonZaicimimaInputField.contentType = InputField.ContentType.Standard;
            AccountRegisterButtonZaicimimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            AccountRegisterButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[0];
            AccountRegisterButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[0];
            PlayerPrefs.SetString("IsVisibleShurumima", "true");
            Debug.Log("显示密码");
        }
        else
        {
            AccountRegisterButtonShurumimaInputField.contentType = InputField.ContentType.Password;
            AccountRegisterButtonZaicimimaInputField.contentType = InputField.ContentType.Password;
            AccountRegisterButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[1];
            AccountRegisterButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[1];
            PlayerPrefs.SetString("IsVisibleShurumima", "false");
            Debug.Log("隐藏密码");
        }
        AccountRegisterButtonShurumimaInputField.MyUpdateLabel();
        AccountRegisterButtonZaicimimaInputField.MyUpdateLabel();
    }
    ///// <summary>
    ///// 注册年代下拉菜单变化事件
    ///// </summary>
    //public void AccountRegisterDropdownNiandaiOnValueChanged(int index)
    //{
    //    switch (index)
    //    {
    //        case 0:
    //            PaintingModuleDataManager.Instance.age = 1;
    //            AccountRegisterDropdownNiandaiLabelImageMask.enabled = false;
    //            break;
    //        case 1:
    //            PaintingModuleDataManager.Instance.age = 2;
    //            AccountRegisterDropdownNiandaiLabelImageMask.enabled = false;
    //            break;
    //        case 2:
    //            PaintingModuleDataManager.Instance.age = 3;
    //            AccountRegisterDropdownNiandaiLabelImageMask.enabled = false;
    //            break;
    //        case 3:
    //            PaintingModuleDataManager.Instance.age = 4;
    //            AccountRegisterDropdownNiandaiLabelImageMask.enabled = false;
    //            break;
    //        case 4:
    //            PaintingModuleDataManager.Instance.age = 5;
    //            AccountRegisterDropdownNiandaiLabelImageMask.enabled = false;
    //            break;
    //        case 5:
    //            PaintingModuleDataManager.Instance.age = 6;
    //            AccountRegisterDropdownNiandaiLabelImageMask.enabled = false;
    //            break;
    //        case 6:
    //            PaintingModuleDataManager.Instance.age = 7;
    //            AccountRegisterDropdownNiandaiLabelImageMask.enabled = false;
    //            break;
    //    }
    //}

    ///// <summary>
    ///// 注册性别下拉菜单变化事件
    ///// </summary>
    //public void AccountRegisterDropdownXingbieOnValueChanged(int index)
    //{
    //    switch (index)
    //    {
    //        case 0:
    //            PaintingModuleDataManager.Instance.sex = 0;
    //            AccountRegisterDropdownXingbieLabelImageMask.enabled = false;
    //            break;
    //        case 1:
    //            PaintingModuleDataManager.Instance.sex = 1;
    //            AccountRegisterDropdownXingbieLabelImageMask.enabled = false;
    //            break;
    //        case 2:
    //            PaintingModuleDataManager.Instance.sex = 2;
    //            AccountRegisterDropdownXingbieLabelImageMask.enabled = false;
    //            break;
    //    }
    //}

    #endregion

    #region 忘记密码函数
    //---------------------------------------------------------------忘记密码---------------------------------------------------------------
    /// <summary>
    /// 忘记密码发送按钮事件
    /// </summary>
    public void ChangePasswordButtonFasongEvent()
    {
        Debug.Log("忘记密码验证码发送");
        string email = ChangePasswordButtonYouxiangInputField.text;
        if (email == "")
        {
            Debug.LogError("email为空");
            PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
            return;
        }
        else
        {
            string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, expression))
            {
                Debug.LogError("email格式不正确");
                PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
                return;
            }
            else
            {
                Debug.Log("email注册验证码成功发送");
                PaintingModuleDataManager.Instance.email = email;
                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.SendForgetPwdCodeURL, null);
            }
        }
    }

    /// <summary>
    /// 忘记密码发送按钮事件成功
    /// </summary>
    public void ChangePasswordButtonFasongEventSuccess()
    {
        isCountdown = true;
        wangjiCountdown = true;
        ChangePasswordButtonFasong.gameObject.SetActive(false);
        ChangePasswordButtonChongxin.gameObject.SetActive(true);
        ChangePasswordButtonChongxin.interactable = false;
    }
    /// <summary>
    /// 忘记密码重新发送按钮重置事件
    /// </summary>
    public void ChangePasswordButtonChongxinResetEvent()
    {
        ChangePasswordTextdaojishi.text = "";
        ChangePasswordButtonChongxin.interactable = true;
    }
    /// <summary>
    /// 忘记密码重新发送
    /// </summary>
    public void ChangePasswordButtonChongxinEvent()
    {
        ChangePasswordButtonFasongEvent();
    }

    /// <summary>
    /// 忘记密码确认按钮事件
    /// </summary>
    public void ChangePasswordButtonQuedingEvent()
    {
        Debug.Log("忘记密码确认");


        string email = ChangePasswordButtonYouxiangInputField.text;
        string code = ChangePasswordButtonYanzhengmaInputField.text;
        string pwd1 = ChangePasswordButtonShurumimaInputField.text;
        string pwd2 = ChangePasswordButtonZaicimimaInputField.text;
        if (email == "")
        {
            Debug.LogError("email为空");
            PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
            return;
        }
        else
        {
            string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, expression))
            {
                Debug.LogError("email格式不正确");
                PopupWindowOpenEvent(PopupWindowState.邮箱格式不正确);
                return;
            }
            else
            {
                if (code == "")
                {
                    Debug.Log("验证码为空");
                    PopupWindowOpenEvent(PopupWindowState.验证码错误);
                    return;
                }
                else
                {
                    int num1 = CalculatePlaces(pwd1);
                    if (num1 < 8 || num1 > 20)
                    {
                        Debug.LogError("密码长度不正确");
                        PopupWindowOpenEvent(PopupWindowState.密码长度必须在);
                        return;
                    }
                    else
                    {
                        int num2 = CalculatePlaces(pwd2);
                        if (num2 < 8 || num2 > 20)
                        {
                            Debug.LogError("密码长度不正确");
                            PopupWindowOpenEvent(PopupWindowState.密码长度必须在);
                            return;
                        }
                        else
                        {
                            if (pwd1 != pwd2)
                            {
                                Debug.LogError("密码不相同");
                                PopupWindowOpenEvent(PopupWindowState.密码不相同);
                                return;
                            }
                            else
                            {
                                Debug.Log("忘记密码确认");
                                PaintingModuleDataManager.Instance.email = email;
                                PaintingModuleDataManager.Instance.code = code;
                                PaintingModuleDataManager.Instance.pwd = pwd1;
                                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.ForgetPwdURL, null);
                            }
                        }

                    }
                }
            }
        }
    }
    /// <summary>
    /// 忘记密码返回按钮事件
    /// </summary>
    public void ChangePasswordButtonReturnEvent()
    {
        Debug.Log("忘记密码返回");

        wangjiCountdown = false;
        isCountdown = false;
        timer = 60f;
        ChangePasswordTextdaojishi.text = "";
        ChangePasswordButtonFasong.gameObject.SetActive(true);
        ChangePasswordButtonChongxin.interactable = false;
        ChangePasswordButtonChongxin.gameObject.SetActive(false);

        ChangePasswordButtonYouxiangInputField.text = "";
        ChangePasswordButtonYanzhengmaInputField.text = "";
        ChangePasswordButtonShurumimaInputField.text = "";
        ChangePasswordButtonZaicimimaInputField.text = "";

        IsRemember = false;
        AccountSigninImagJizhumima.sprite = rememberPasswordSprites[0];
        PlayerPrefs.SetString("Password", "");
        PlayerPrefs.SetString("IsRemember", "false");
        AccountSigninButtonPasswordInputField.text = "";
        AccountSigninButtonPasswordInputField.contentType = InputField.ContentType.Standard;
        AccountSigninButtonPasswordInputField.lineType = InputField.LineType.MultiLineSubmit;
        AccountSigninButtonPasswordInputFieldImage.sprite = visiblePasswordSprites[0];

        ChangePasswordCanvasGroup.blocksRaycasts = false;
        ChangePasswordCanvasGroup.interactable = false;
        ChangePasswordCanvasGroup.alpha = 0;

        AccountSigninCanvasGroup.alpha = 1;
        AccountSigninCanvasGroup.blocksRaycasts = true;
        AccountSigninCanvasGroup.interactable = true;
    }

    /// <summary>
    /// 修改密码隐藏密码
    /// </summary>
    public void ChangePasswordButtonShurumimaInputFieldButtonEvent()
    {
        //隐藏密码
        if (ChangePasswordButtonShurumimaInputField.contentType == InputField.ContentType.Password)
        {
            ChangePasswordButtonShurumimaInputField.contentType = InputField.ContentType.Standard;
            ChangePasswordButtonShurumimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            ChangePasswordButtonZaicimimaInputField.contentType = InputField.ContentType.Standard;
            ChangePasswordButtonZaicimimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            ChangePasswordButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[0];
            ChangePasswordButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[0];
            PlayerPrefs.SetString("IsVisibleZaicimima", "true");
            Debug.Log("显示密码");
        }
        else
        {
            ChangePasswordButtonShurumimaInputField.contentType = InputField.ContentType.Password;
            ChangePasswordButtonZaicimimaInputField.contentType = InputField.ContentType.Password;
            ChangePasswordButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[1];
            ChangePasswordButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[1];
            PlayerPrefs.SetString("IsVisibleZaicimima", "false");
            Debug.Log("隐藏密码");
        }
        ChangePasswordButtonShurumimaInputField.MyUpdateLabel();
        ChangePasswordButtonZaicimimaInputField.MyUpdateLabel();
    }

    /// <summary>
    /// 修改密码隐藏密码
    /// </summary>
    public void ChangePasswordButtonZaicimimaInputFieldButtonEvent()
    {
        //隐藏密码
        if (ChangePasswordButtonZaicimimaInputField.contentType == InputField.ContentType.Password)
        {
            ChangePasswordButtonShurumimaInputField.contentType = InputField.ContentType.Standard;
            ChangePasswordButtonShurumimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            ChangePasswordButtonZaicimimaInputField.contentType = InputField.ContentType.Standard;
            ChangePasswordButtonZaicimimaInputField.lineType = InputField.LineType.MultiLineSubmit;
            ChangePasswordButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[0];
            ChangePasswordButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[0];
            PlayerPrefs.SetString("IsVisibleZaicimima", "true");
            Debug.Log("显示密码");
        }
        else
        {
            ChangePasswordButtonShurumimaInputField.contentType = InputField.ContentType.Password;
            ChangePasswordButtonZaicimimaInputField.contentType = InputField.ContentType.Password;
            ChangePasswordButtonShurumimaInputFieldImage.sprite = visiblePasswordSprites[1];
            ChangePasswordButtonZaicimimaInputFieldImage.sprite = visiblePasswordSprites[1];
            PlayerPrefs.SetString("IsVisibleZaicimima", "false");
            Debug.Log("隐藏密码");
        }
        ChangePasswordButtonShurumimaInputField.MyUpdateLabel();
        ChangePasswordButtonZaicimimaInputField.MyUpdateLabel();
    }

    #endregion

    #region 同意条款函数
    /// <summary>
    /// 同意条款下一步按钮事件
    /// </summary>
    public void ConsentClauseButtonNextEvent()
    {
        Debug.Log("跳转注册界面");

        if (PaintingModuleDataManager.Instance.isYouke)
        {
            Debug.Log("如果是游客，跳转场景");

            //HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.YoukeLoginURL, null);
            //SceneManager.LoadScene("Player");

            ConsentClauseCanvasGroup.blocksRaycasts = false;
            ConsentClauseCanvasGroup.interactable = false;
            ConsentClauseCanvasGroup.alpha = 0;

            SceneSelectionCanvasGroup.alpha = 1;
            SceneSelectionCanvasGroup.blocksRaycasts = true;
            SceneSelectionCanvasGroup.interactable = true;

            SceneSelectionButtonNext.interactable = false;
            for (int i = 0; i < SceneSelectionLayoutGroupButtons.Length; i++)
            {
                SceneSelectionLayoutGroupButtons[i].interactable = true;
                SceneSelectionLayoutGroupButtons[i].GetComponent<SceneData>().sceneImage.enabled = false;
                SceneSelectionLayoutGroupButtons[i].GetComponent<SceneData>().sceneImage.fillAmount = 1;
                SceneSelectionLayoutGroupButtons[i].GetComponent<SceneData>().sceneSelete.enabled = false;
            }
        }
        else
        {
            Debug.Log("如果不是游客，跳转注册");

            //List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
            //optionDatas.Add(new Dropdown.OptionData("10代"));
            //optionDatas.Add(new Dropdown.OptionData("20代"));
            //optionDatas.Add(new Dropdown.OptionData("30代"));
            //optionDatas.Add(new Dropdown.OptionData("40代"));
            //optionDatas.Add(new Dropdown.OptionData("50代"));
            //optionDatas.Add(new Dropdown.OptionData("60代"));
            //optionDatas.Add(new Dropdown.OptionData("70代～"));
            //optionDatas.Add(new Dropdown.OptionData("default"));
            //AccountRegisterDropdownNiandai.AddOptions(optionDatas);
            //AccountRegisterDropdownNiandai.value = optionDatas.Count - 1;
            //AccountRegisterDropdownNiandai.options.RemoveAt(optionDatas.Count - 1);

            //List<Dropdown.OptionData> optionDatasXingbie = new List<Dropdown.OptionData>();
            //optionDatasXingbie.Add(new Dropdown.OptionData("男性"));
            //optionDatasXingbie.Add(new Dropdown.OptionData("女性"));
            //optionDatasXingbie.Add(new Dropdown.OptionData("その他"));
            //optionDatasXingbie.Add(new Dropdown.OptionData("default"));
            //AccountRegisterDropdownXingbie.AddOptions(optionDatasXingbie);
            //AccountRegisterDropdownXingbie.value = optionDatasXingbie.Count - 1;
            //AccountRegisterDropdownXingbie.options.RemoveAt(optionDatasXingbie.Count - 1);

            AccountRegisterCanvasGroup.alpha = 1;
            AccountRegisterCanvasGroup.blocksRaycasts = true;
            AccountRegisterCanvasGroup.interactable = true;

            ConsentClauseCanvasGroup.alpha = 0;
            ConsentClauseCanvasGroup.blocksRaycasts = false;
            ConsentClauseCanvasGroup.interactable = false;

            ConsentClauseTiaokuanScrollbar.value = 1;

            consentClause = false;
            ConsentClauseImageAgree.sprite = consentClauseSprites[0];
            ConsentClauseButtonNext.interactable = false;
        }






    }

    /// <summary>
    /// 同意条款返回按钮事件
    /// </summary>
    public void ConsentClauseButtonReturnEvent()
    {
        Debug.Log("返回首页界面");
        ConsentClauseCanvasGroup.alpha = 0;
        ConsentClauseCanvasGroup.blocksRaycasts = false;
        ConsentClauseCanvasGroup.interactable = false;

        AccountSigninCanvasGroup.alpha = 1;
        AccountSigninCanvasGroup.blocksRaycasts = true;
        AccountSigninCanvasGroup.interactable = true;

        ConsentClauseTiaokuanScrollbar.value = 1;

        consentClause = false;
        ConsentClauseImageAgree.sprite = consentClauseSprites[0];
        ConsentClauseButtonNext.interactable = false;

        PaintingModuleDataManager.Instance.isYouke = false;
    }

    /// <summary>
    /// 同意条款按钮事件
    /// </summary>
    public void ConsentClauseButtonAgreeEvent()
    {
        if (consentClause)
        {
            ConsentClauseImageAgree.sprite = consentClauseSprites[0];
            ConsentClauseButtonNext.interactable = false;
            consentClause = false;
        }
        else
        {
            ConsentClauseImageAgree.sprite = consentClauseSprites[1];
            ConsentClauseButtonNext.interactable = true;
            consentClause = true;
        }
    }

    #endregion

    #region 选择场景函数

    /// <summary>
    /// 选择场景返回按钮事件
    /// </summary>
    public void SceneSelectionButtonReturnEvent()
    {
        Debug.Log("返回登录界面");

        PopupWindowCanvasManager.Instance.SwitchLanguageEvent(PaintingModuleDataManager.Instance.language);

        if (Downloader.Instance.isDownloadCheck)
        {
            PopupWindowCanvasManager.Instance.DownloadingExitAccountCanvasGroupEvent();
        }
        else
        {
            PopupWindowCanvasManager.Instance.ExitAccountCanvasGroupEvent();
        }


    }

    public void SceneSelectionButtonReturnCanvasGroupEvent()
    {
        SceneSelectionCanvasGroup.alpha = 0;
        SceneSelectionCanvasGroup.blocksRaycasts = false;
        SceneSelectionCanvasGroup.interactable = false;

        AccountSigninCanvasGroup.alpha = 1;
        AccountSigninCanvasGroup.blocksRaycasts = true;
        AccountSigninCanvasGroup.interactable = true;


    }

    /// <summary>
    /// 选择场景按钮事件
    /// </summary>
    public void SceneSelectionButtonNextEvent()
    {
        if (!Downloader.Instance.isDownloadCheck)
        {
            if (PaintingModuleDataManager.Instance.allRoomResults != null && PaintingModuleDataManager.Instance.allRoomResults.Count != 0)
            {
                Debug.Log("SceneSelectionButton：已经获取服务器展馆数据");

                HttpManager.Instance.isGetMuseumList = false;
                if (PaintingModuleDataManager.Instance.roomAllExhibitsData != null && PaintingModuleDataManager.Instance.roomAllExhibitsData.Count != 0)
                {
                    for (int i = 0; i < PaintingModuleDataManager.Instance.roomAllExhibitsData.Count; i++)
                    {
                        if (PaintingModuleDataManager.Instance.currentSelectSceneData.sceneID == PaintingModuleDataManager.Instance.roomAllExhibitsData[i][0].showRoomId)
                        {
                            HttpManager.Instance.isGetMuseumList = true;
                            break;
                        }
                    }
                }

            }
            else
            {
                Debug.Log("SceneSelectionButton：还没有获取服务器数据");
                HttpManager.Instance.DetectionGalleryList();
                return;
            }


            if (!HttpManager.Instance.isGetMuseumList)
            {
                Debug.Log("SceneSelectionButton：还没有获取服务器房间数据");
                PaintingModuleDataManager.Instance.fileSize = 0;
                HttpManager.Instance.DetectionRoomPMData();
            }
            else
            {
                Debug.Log("SceneSelectionButton：已经获取服务器数据");
                if (PaintingModuleDataManager.Instance.getSceneData == null || PaintingModuleDataManager.Instance.getSceneData.sceneID != PaintingModuleDataManager.Instance.currentSelectSceneDataID)
                {
                    Debug.Log("SceneSelectionButton：还没有检测要下载的房间数据");
                    PaintingModuleDataManager.Instance.fileSize = 0;
                    HttpManager.Instance.httpState = HttpState.GetLocalData;
                    PaintingModuleDataManager.Instance.GetRoomDataLocal();
                }
                else
                {
                    if (PaintingModuleDataManager.Instance.currentSelectSceneData.isDownload)
                    {
                        Debug.Log("SceneSelectionButton：要下载的数据已经下载好，进入房间");
                        PaintingModuleDataManager.Instance.currentSelectSceneMask.enabled = false;
                        PaintingModuleDataManager.Instance.currentSelectSceneMask.fillAmount = 1;
                        PaintingModuleDataManager.Instance.currentSelectSceneData.isDownload = true;
                        Downloader.Instance.isDownloadCheck = false;
                        Downloader.Instance.isBatch = false;
                        Downloader.Instance.downloadTimer = 0;
                        Downloader.Instance.downloadNum = 0;
                        Downloader.Instance.totalDownload = 0;
                        Downloader.Instance.currentDownload = 0;

                        if (PaintingModuleDataManager.Instance.isYouke)
                        {
                            HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.YoukeLoginURL, null);
                        }

                        SceneManager.LoadScene("Player");

                        //PopupWindowCanvasManager.Instance.DownloadFileCanvasGroupOpenEvent();
                        //HttpManager.Instance.currentGetRoomAllExhibitsDataIndex = 1;
                        //PaintingModuleDataManager.Instance.ComparisonXMLRoomExhibitsData(PaintingModuleDataManager.Instance.currentSelectSceneData.sceneID);
                    }
                    else
                    {
                        Debug.Log("SceneSelectionButton：与检测的要下载的房间数据不相同");

                        Downloader.Instance.DownloaderSwitchRoomsResetData();

                        UnityDownloadMgr.instance.DownloadMgrResetData();

                        PaintingModuleDataManager.Instance.ComparisonXMLRoomExhibitsData(PaintingModuleDataManager.Instance.currentSelectSceneData.sceneID);
                    }
                }
            }
        }
        else
        {
            PopupWindowOpenEvent(PopupWindowState.正在下载任务);
        }

    }

    /// <summary>
    /// 选择场景组按钮事件
    /// </summary>
    public void SceneSelectionLayoutGroupButtonEvent(int index)
    {

        if (PaintingModuleDataManager.Instance.currentSelectSceneData == null)
        {
            SceneSelectionLayoutGroupButtons[index].GetComponent<SceneData>().sceneSelete.enabled = true;
            SceneSelectionLayoutGroupButtons[index].interactable = false;
            PaintingModuleDataManager.Instance.currentSelectSceneData = SceneSelectionLayoutGroupButtons[index].GetComponent<SceneData>();
            SceneSelectionButtonNext.interactable = true;
        }
        else
        {
            PaintingModuleDataManager.Instance.currentSelectSceneData.sceneSelete.enabled = false;
            PaintingModuleDataManager.Instance.currentSelectSceneData.GetComponent<Button>().interactable = true;
            PaintingModuleDataManager.Instance.currentSelectSceneData = SceneSelectionLayoutGroupButtons[index].GetComponent<SceneData>();
            SceneSelectionLayoutGroupButtons[index].GetComponent<SceneData>().sceneSelete.enabled = true;
            SceneSelectionLayoutGroupButtons[index].interactable = false;

        }

    }


    #endregion

    #region 弹窗函数
    //---------------------------------------------------------------弹窗---------------------------------------------------------------
    /// <summary>
    /// 弹窗打开事件
    /// </summary>
    public void PopupWindowOpenEvent(PopupWindowState state)
    {
        PopupWindow.alpha = 1;
        PopupWindow.blocksRaycasts = true;
        PopupWindow.interactable = true;

        PaintingModuleDataManager.Instance.popupState = state;

        switch (state)
        {
            case PopupWindowState.None:
                break;
            case PopupWindowState.已发送:
                PopupWindowTextYifasong.enabled = true;
                break;
            case PopupWindowState.邮箱格式不正确:
                PopupWindowTextYouxianggeshi.enabled = true;
                break;
            case PopupWindowState.重新获取:
                PopupWindowTextChongxinhuoqu.enabled = true;
                break;
            case PopupWindowState.邮箱已被注册:
                PopupWindowTextYouxiangzhuce.enabled = true;
                break;
            case PopupWindowState.验证码错误:
                PopupWindowTextYanzhengma.enabled = true;
                break;
            case PopupWindowState.密码长度必须在:
                PopupWindowTextMimachangdu.enabled = true;
                break;
            case PopupWindowState.账号错误或未注册:
                PopupWindowTextZhanghaocuowu.enabled = true;
                break;
            case PopupWindowState.账号密码错误:
                PopupWindowTextZhanghaomimacuowu.enabled = true;
                break;
            case PopupWindowState.网络连接超时:
                PopupWindowTextWangluochaoshi.enabled = true;
                break;
            case PopupWindowState.密码不相同:
                PopupWindowTextMimabuxiangtong.enabled = true;
                break;
            case PopupWindowState.密码修改成功:
                PopupWindowTextMimaxiugaichenggong.enabled = true;
                break;
            case PopupWindowState.正在下载任务:
                PopupWindowTextZhengzaixiazairenwu.enabled = true;
                break;
            case PopupWindowState.自定义:
                PopupWindowTextZidingyi.enabled = true;
                break;
        }
    }
    /// <summary>
    /// 弹窗关闭事件
    /// </summary>
    public void PopupWindowCloseEvent()
    {
        PopupWindow.alpha = 0;
        PopupWindow.blocksRaycasts = false;
        PopupWindow.interactable = false;

        PopupWindowState state = PaintingModuleDataManager.Instance.popupState;

        switch (state)
        {
            case PopupWindowState.None:
                break;
            case PopupWindowState.已发送:
                PopupWindowTextYifasong.enabled = false;
                break;
            case PopupWindowState.邮箱格式不正确:
                PopupWindowTextYouxianggeshi.enabled = false;
                break;
            case PopupWindowState.重新获取:
                PopupWindowTextChongxinhuoqu.enabled = false;
                break;
            case PopupWindowState.邮箱已被注册:
                PopupWindowTextYouxiangzhuce.enabled = false;
                break;
            case PopupWindowState.验证码错误:
                PopupWindowTextYanzhengma.enabled = false;
                break;
            case PopupWindowState.密码长度必须在:
                PopupWindowTextMimachangdu.enabled = false;
                break;
            case PopupWindowState.账号错误或未注册:
                PopupWindowTextZhanghaocuowu.enabled = false;
                break;
            case PopupWindowState.账号密码错误:
                PopupWindowTextZhanghaomimacuowu.enabled = false;
                break;
            case PopupWindowState.网络连接超时:
                PopupWindowTextWangluochaoshi.enabled = false;
                break;
            case PopupWindowState.密码不相同:
                PopupWindowTextMimabuxiangtong.enabled = false;
                break;
            case PopupWindowState.密码修改成功:
                PopupWindowTextMimaxiugaichenggong.enabled = false;
                ChangePasswordButtonReturnEvent();
                break;
            case PopupWindowState.正在下载任务:
                PopupWindowTextZhengzaixiazairenwu.enabled = false;
                break;
            case PopupWindowState.自定义:
                PopupWindowTextZidingyi.enabled = false;
                break;
        }

        PaintingModuleDataManager.Instance.popupState = PopupWindowState.None;

    }
    /// <summary>
    /// 弹窗自定义事件
    /// </summary>
    public void PopupWindowZidingyiEvent(string message)
    {
        PopupWindow.alpha = 1;
        PopupWindow.blocksRaycasts = true;
        PopupWindow.interactable = true;

        PaintingModuleDataManager.Instance.popupState = PopupWindowState.自定义;
        PopupWindowTextZidingyi.text = message;
        PopupWindowTextZidingyi.enabled = true;
    }

    #endregion

    /// <summary>
    /// 统计字符
    /// </summary>
    /// <param name="mString"></param>
    /// <returns></returns>
    public int CalculatePlaces(string mString)
    {
        int _placesNum = 0; //统计字节位数
        char[] _charArray = mString.ToCharArray();
        for (int i = 0; i < _charArray.Length; i++)
        {
            char _eachChar = _charArray[i];
            if (_eachChar >= 0x4e00 && _eachChar <= 0x9fa5) //判断中文字符
                _placesNum += 2;
            else if (_eachChar >= 0x0000 && _eachChar <= 0x00ff) //已2个字节判断
                _placesNum += 1;
        }

        return _placesNum;
    }
}

[System.Serializable]
public class languageSprite
{
    public Language language;
    public Sprite noSelect;
    public Sprite select;
}
[System.Serializable]
public enum Language
{
    中文,
    英语,
    日文
}
[System.Serializable]
public enum Platform
{
    window,
    android,
    ios
}
[System.Serializable]
public enum PopupWindowState
{
    None,
    已发送,
    邮箱格式不正确,
    重新获取,
    邮箱已被注册,
    验证码错误,
    密码长度必须在,
    账号错误或未注册,
    账号密码错误,
    网络连接超时,
    密码不相同,
    密码修改成功,
    正在下载任务,
    自定义,
}
