using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRubyShared;

public class PaintingModuleLockManager : UnitySingleton<PaintingModuleLockManager>
{
    public TextLanguageReplace[] textReplaces;

    public CanvasGroup PaintingModuleLock_CanvasGroup;

    public Button PaintingModuleLock_Exit;

    public Button PaintingModuleLock_Register;

    public Button PaintingModuleSettingCanvas_ChangePassword;

    //--------------------------------------------------注册--------------------------------------------------------
    public Sprite[] visiblePasswordSprites;

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

    //--------------------------------------------------弹窗--------------------------------------------------------
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
    public Text PopupWindowTextZidingyi;


    public bool isCountdown = false;
    public float timer = 60f;
    public bool zuceCountdown = false;

    public void Awake()
    {
        PaintingModuleLock_CanvasGroup = this.GetComponentInChildren<CanvasGroup>();
        PaintingModuleLock_Exit = GameObject.Find("PaintingModuleLock_Exit").GetComponent<Button>();
        PaintingModuleLock_Register = GameObject.Find("PaintingModuleLock_Register_Button").GetComponent<Button>();
        PaintingModuleSettingCanvas_ChangePassword = GameObject.Find("PaintingModuleSettingCanvas_ChangePassword").GetComponent<Button>();

        //-------------------------------------------------------获取需要切换语言的脚本---------------------------------------------------------------
        textReplaces = this.GetComponentsInChildren<TextLanguageReplace>();

        //--------------------------------------------------注册--------------------------------------------------------

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

        AccountRegisterButtonQueding = GameObject.Find("AccountRegisterButtonQueding").GetComponent<Button>();
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
        PopupWindowTextZidingyi.enabled = false;

        PopupWindowMaskBG = GameObject.Find("PopupWindowMaskBG").GetComponent<Button>();
        PopupWindowMaskBG.onClick.AddListener(PopupWindowCloseEvent);
    }

    public void Start()
    {
        Language language = PaintingModuleDataManager.Instance.language;

        if (textReplaces != null)
        {
            for (int i = 0; i < textReplaces.Length; i++)
            {
                textReplaces[i].ReplaceLanguageText(language);
            }
        }

        PaintingModuleLock_CanvasGroup.alpha = 0;
        PaintingModuleLock_CanvasGroup.interactable = false;
        PaintingModuleLock_CanvasGroup.blocksRaycasts = false;

        AccountRegisterButtonChongxin.gameObject.SetActive(false);

        PaintingModuleLock_Exit.onClick.AddListener(PaintingModuleLockExitButtonEvent);
        PaintingModuleLock_Register.onClick.AddListener(PaintingModuleLockRegisterButtonEvent);
        PaintingModuleSettingCanvas_ChangePassword.onClick.AddListener(PaintingModuleLockChangePasswordButtonEvent);
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
            }
            else
            {
                if (zuceCountdown)
                {
                    AccountRegisterTextdaojishi.text = ((int)timer).ToString();
                }
                timer -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// 画作锁打开
    /// </summary>
    public void PaintingModuleLockOpenEvent()
    {
        PaintingModuleLock_CanvasGroup.alpha = 1;
        PaintingModuleLock_CanvasGroup.interactable = true;
        PaintingModuleLock_CanvasGroup.blocksRaycasts = true;
    }
    /// <summary>
    /// 画作锁关闭
    /// </summary>
    public void PaintingModuleLockCloseEvent()
    {
        PaintingModuleLock_CanvasGroup.alpha = 0;
        PaintingModuleLock_CanvasGroup.interactable = false;
        PaintingModuleLock_CanvasGroup.blocksRaycasts = false;
    }
    /// <summary>
    /// 画作锁退出事件
    /// </summary>
    public void PaintingModuleLockExitButtonEvent()
    {
        PaintingModuleLock_CanvasGroup.alpha = 0;
        PaintingModuleLock_CanvasGroup.interactable = false;
        PaintingModuleLock_CanvasGroup.blocksRaycasts = false;

        PaintingModuleFrameCanvas.Instance.PaintingModuleLockExit();
    }

    /// <summary>
    /// 主页注册按钮事件
    /// </summary>
    public void PaintingModuleLockRegisterButtonEvent()
    {
        Debug.Log("游客跳转注册界面");

        PaintingModuleDataManager.Instance.YoukeRegister();
    }

    /// <summary>
    /// 主页修改密码按钮事件
    /// </summary>
    public void PaintingModuleLockChangePasswordButtonEvent()
    {
        Debug.Log("跳转修改密码界面");

        PaintingModuleDataManager.Instance.ChangePasswordEvent();
    }
    //--------------------------------------------------注册--------------------------------------------------------

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

    //---------------------------------------------------------------注册函数---------------------------------------------------------------
    /// <summary>
    /// 注册发送按钮事件
    /// </summary>
    public void AccountRegisterButtonFasongEvent()
    {
        Debug.Log("游客注册验证码发送");
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
                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.YoukeSendRegisterCodeURL, null);
            }
        }
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
                                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.YoukeRegisterURL, null);
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

    }

    /// <summary>
    /// 登录重新发送
    /// </summary>
    public void AccountRegisterButtonChongxinEvent()
    {
        AccountRegisterButtonFasongEvent();
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
    /// 注册成功
    /// </summary>
    public void RegisterSuccessEvent()
    {
        PlayerPrefs.SetString("UserName", AccountRegisterButtonYouxiangInputField.text);
        PlayerPrefs.SetString("Password", "");
        PlayerPrefs.SetString("IsRemember", "false");

        PaintingModuleDataManager.Instance.email = "";
        PaintingModuleDataManager.Instance.code = "";
        PaintingModuleDataManager.Instance.pwd = "";

        PaintingModuleDataManager.Instance.isYouke = false;

        PaintingModuleLockCloseEvent();

        AccountRegisterButtonReturnEvent();

        FingersScript.Instance.enabled = true;

        if (PaintingModuleDataManager.Instance.isFrame)
            PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();

        Debug.Log("游客注册登录成功");
    }

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
