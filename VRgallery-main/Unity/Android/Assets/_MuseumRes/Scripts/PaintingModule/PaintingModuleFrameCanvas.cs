using System;
using System.Collections;
using System.IO;
using DigitalRubyShared;
using LitJson;
using RenderHeads.Media.AVProVideo;
using RenderHeads.Media.AVProVideo.Demos;
using UnityEditor;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PaintingModuleFrameCanvas : UnitySingleton<PaintingModuleFrameCanvas>
{
    /// <summary>
    /// 画作选项枚举
    /// </summary>
    public PaintingModuleOptionEnum options = PaintingModuleOptionEnum.None;
    /// <summary>
    /// 画作帧动画Canvas
    /// </summary>
    public Transform paintingModuleFrameCanvas;
    /// <summary>
    /// 画作帧动画RawImage
    /// </summary>
    public RawImage paintingModuleFrameRawImage;
    /// <summary>
    /// 画作帧动画UI关闭按钮
    /// </summary>
    public Button paintingModuleFrameCanvasCloseButton;
    /// <summary>
    /// 画作帧动画UI关闭按钮事件
    /// </summary>
    public UnityAction paintingModuleFrameCanvasCloseButtonAction;
    /// <summary>
    /// 画作模块选项
    /// </summary>
    public GameObject paintingModuleOption;

    //Loading

    /// <summary>
    /// 画作模块Loading
    /// </summary>
    public CanvasGroup paintingModuleLoading;
    /// <summary>
    /// 画作模块LoadingText
    /// </summary>
    public Text paintingModuleLoadingText;
    /// <summary>
    /// 画作模块Loading进度
    /// </summary>
    public float paintingModuleLoadingSchedule;

    //Video模块
    /// <summary>
    /// 画作模块选项_视频
    /// </summary>
    public Button paintingModuleOption_Video;
    /// <summary>
    /// 画作模块选项_视频事件
    /// </summary>
    public UnityAction paintingModuleOption_VideoButtonAction;
    /// <summary>
    /// 画作模块UI组_视频
    /// </summary>
    public CanvasGroup paintingModuleCanvasGroup_Video;
    /// <summary>
    /// 视频播放器
    /// </summary>
    public MediaPlayer videoPlayer;
    /// <summary>
    /// VCR
    /// </summary>
    public VCR VCR;

    //Introduce模块
    /// <summary>
    /// 画作模块选项_介绍
    /// </summary>
    public Button paintingModuleOption_Introduce;
    /// <summary>
    /// 画作模块选项_介绍事件
    /// </summary>
    public UnityAction paintingModuleOption_IntroduceButtonAction;
    /// <summary>
    /// 画作模块UI组_介绍
    /// </summary>
    public CanvasGroup paintingModuleCanvasGroup_Introduce;
    /// <summary>
    /// 画作模块UI组_Image
    /// </summary>
    public RawImage paintingModuleCanvasGroup_Introduce_Image;

    //Web
    /// <summary>
    /// 画作模块选项_Web
    /// </summary>
    public Button paintingModuleOption_Web;
    /// <summary>
    /// 画作模块Web选项按钮
    /// </summary>
    public Button paintingModuleCanvasGroup_Web_Button;
    /// <summary>
    /// 画作模块Web选项图片
    /// </summary>
    public Image paintingModuleOption_Web_Image;
    /// <summary>
    /// 画作模块选项_Web事件
    /// </summary>
    public UnityAction paintingModuleOption_WebButtonAction;
    /// <summary>
    /// 画作模块UI组_Web
    /// </summary>
    public CanvasGroup paintingModuleCanvasGroup_Web;

    public GraphicRaycaster gr;

    //WebVideo
    /// <summary>
    /// 画作模块选项_视频网站
    /// </summary>
    public Button PaintingModuleOption_WebVideo;
    /// <summary>
    /// 画作模块视频网站选项图片
    /// </summary>
    public Image PaintingModuleOption_WebVideo_Image;
    /// <summary>
    /// 画作模块选项视频网站事件
    /// </summary>
    public UnityAction PaintingModuleOption_WebVideoButtonAction;

    //Sound
    /// <summary>
    /// 画作模块选项_Sound
    /// </summary>
    public Button paintingModuleOption_Sound;
    /// <summary>
    /// 画作模块选项_Sound事件
    /// </summary>
    public UnityAction paintingModuleCanvasGroup_SoundButtonAction;
    /// <summary>
    /// 画作模块UI组_Sound
    /// </summary>
    public CanvasGroup paintingModuleCanvasGroup_Sound;
    /// <summary>
    /// 画作模块UI组_Sound_AS
    /// </summary>
    public AudioSource paintingModuleCanvasGroup_Sound_AudioSource;

    public Sprite[] audioUISprites;
    /// <summary>
    /// 画作模块选项_Sound
    /// </summary>
    public Image paintingModuleOption_Sound_Image;

    //Store
    /// <summary>
    /// 画作模块选项_Store
    /// </summary>
    public Button paintingModuleOption_Store;
    /// <summary>
    /// 画作模块Store选项图片
    /// </summary>
    public Image paintingModuleOption_Store_Image;
    /// <summary>
    /// 画作模块选项_Store事件
    /// </summary>
    public UnityAction paintingModuleOption_StoreButtonAction;

    public bool isPlayAudio = false;
    /// <summary>
    /// 画作名称
    /// </summary>
    public Text paintingModuleName;
    /// <summary>
    /// 上一幅画作
    /// </summary>
    public Button paintingModuleFrameLastButton;
    /// <summary>
    /// 下一幅画作
    /// </summary>
    public Button paintingModuleFrameNextButton;

    private PaintingModuleDataManager PMDM;
    public void Awake()
    {
        PMDM = PaintingModuleDataManager.Instance;
    }

    /// <summary>
    /// 画作UI初始化
    /// </summary>
    public void Initial()
    {
        Debug.Log("画作UI初始化完成");

        paintingModuleFrameCanvas = this.transform.GetChild(0);

        paintingModuleFrameRawImage = this.GetComponentInChildren<RawImage>();

        paintingModuleOption = GameObject.Find("PaintingModuleOption");
        paintingModuleName = GameObject.Find("FrameName").GetComponent<Text>();

        paintingModuleFrameCanvasCloseButton = GameObject.Find("PaintingModuleFrameCanvasCloseButton").GetComponent<Button>();

        paintingModuleFrameCanvasCloseButtonAction += PaintingModuleFrameStop;
        paintingModuleFrameCanvasCloseButtonAction += PaintingModuleVideoStop;
        paintingModuleFrameCanvasCloseButtonAction += PaintingModuleIntroduceStop;
        //paintingModuleFrameCanvasCloseButtonAction += PaintingModuleWebStop;
        //paintingModuleFrameCanvasCloseButtonAction += PaintingModuleSoundStop;

        paintingModuleFrameCanvasCloseButton.onClick.AddListener(paintingModuleFrameCanvasCloseButtonAction);


        paintingModuleLoading = GameObject.Find("PaintingModuleLoading").GetComponent<CanvasGroup>();

        paintingModuleLoadingText = paintingModuleLoading.GetComponentInChildren<Text>();



        paintingModuleOption_Video = GameObject.Find("PaintingModuleOption_Video").GetComponent<Button>();

        paintingModuleOption_VideoButtonAction += PaintingModuleVideoStart;

        paintingModuleOption_Video.onClick.AddListener(paintingModuleOption_VideoButtonAction);

        paintingModuleCanvasGroup_Video = GameObject.Find("PaintingModuleCanvasGroup_Video").GetComponent<CanvasGroup>();

        videoPlayer = paintingModuleCanvasGroup_Video.GetComponentInChildren<MediaPlayer>();



        paintingModuleOption_Introduce = GameObject.Find("PaintingModuleOption_Introduce").GetComponent<Button>();

        paintingModuleCanvasGroup_Introduce_Image = GameObject.Find("PaintingModuleCanvasGroup_Introduce_Image").GetComponent<RawImage>();

        paintingModuleOption_IntroduceButtonAction += PaintingModuleIntroduceStart;

        paintingModuleOption_Introduce.onClick.AddListener(paintingModuleOption_IntroduceButtonAction);

        paintingModuleCanvasGroup_Introduce = GameObject.Find("PaintingModuleCanvasGroup_Introduce").GetComponent<CanvasGroup>();



        gr = this.GetComponentInChildren<GraphicRaycaster>();
        paintingModuleOption_Web = GameObject.Find("PaintingModuleOption_Web").GetComponent<Button>();
        paintingModuleOption_Web_Image = paintingModuleOption_Web.GetComponent<Image>();
        //paintingModuleCanvasGroup_Web_Button = GameObject.Find("PaintingModuleCanvasGroup_Web_Button").GetComponent<Button>();

        paintingModuleOption_WebButtonAction += PaintingModuleWebStart;

        paintingModuleOption_Web.onClick.AddListener(paintingModuleOption_WebButtonAction);

        //paintingModuleCanvasGroup_Web = GameObject.Find("PaintingModuleCanvasGroup_Web").GetComponent<CanvasGroup>();


        PaintingModuleOption_WebVideo = GameObject.Find("PaintingModuleOption_WebVideo").GetComponent<Button>();
        PaintingModuleOption_WebVideo_Image = PaintingModuleOption_WebVideo.GetComponent<Image>();
        PaintingModuleOption_WebVideoButtonAction += PaintingModuleWebVideoStart;
        PaintingModuleOption_WebVideo.onClick.AddListener(PaintingModuleOption_WebVideoButtonAction);

        paintingModuleOption_Sound = GameObject.Find("PaintingModuleOption_Sound").GetComponent<Button>();

        paintingModuleOption_Sound_Image = paintingModuleOption_Sound.GetComponent<Image>();

        paintingModuleCanvasGroup_SoundButtonAction += PaintingModuleSoundStart;

        paintingModuleOption_Sound.onClick.AddListener(paintingModuleCanvasGroup_SoundButtonAction);

        paintingModuleCanvasGroup_Sound = GameObject.Find("PaintingModuleCanvasGroup_Sound").GetComponent<CanvasGroup>();

        paintingModuleCanvasGroup_Sound_AudioSource =
            GameObject.Find("PaintingModuleCanvasGroup_Sound_Voice").GetComponent<AudioSource>();

        paintingModuleOption_Store = GameObject.Find("PaintingModuleOption_Store").GetComponent<Button>();
        paintingModuleOption_Store_Image = paintingModuleOption_Store.GetComponent<Image>();
        paintingModuleOption_StoreButtonAction += PaintingModuleStoreStart;
        paintingModuleOption_Store.onClick.AddListener(paintingModuleOption_StoreButtonAction);


        paintingModuleFrameLastButton = GameObject.Find("PaintingModuleFrameLastButton").GetComponent<Button>();
        paintingModuleFrameLastButton.onClick.AddListener(PaintingModuleFrameLastButtonEvent);
        paintingModuleFrameNextButton = GameObject.Find("PaintingModuleFrameNextButton").GetComponent<Button>();
        paintingModuleFrameNextButton.onClick.AddListener(PaintingModuleFrameNextButtonEvent);

        Reset();

        GetLoadingProgress.Instance.isFake = true;
        GetLoadingProgress.Instance.currentAlreadyIndex += 1;
    }

    /// <summary>
    /// 画作UI重置
    /// </summary>
    public void Reset()
    {
        options = PaintingModuleOptionEnum.None;

        paintingModuleLoadingSchedule = 0f;

        paintingModuleFrameCanvas.gameObject.SetActive(false);

    }
    /// <summary>
    /// 按钮初始化
    /// </summary>
    public void OptionButtonInitial()
    {
        paintingModuleOption_Video.interactable = true;
        paintingModuleOption_Introduce.interactable = true;
        paintingModuleOption_Web.interactable = true;
        paintingModuleOption_Sound.interactable = true;
        paintingModuleOption_Store.interactable = true;
        PaintingModuleOption_WebVideo.interactable = true;
    }
    /// <summary>
    /// 画作帧动画开始
    /// </summary>
    public void PaintingModuleFrameStart(PaintingModule pm)
    {

        Debug.Log("画作帧动画开始");

        options = PaintingModuleOptionEnum.Frame;

        pm.ClickColliderExitEvent();

        EasyTouchControlsManager.Instance.EasyTouchControlsStop();

        paintingModuleFrameCanvas.gameObject.SetActive(true);

        paintingModuleName.enabled = true;

        FingersScript.Instance.enabled = true;

        //if (!PMDM.isYouke)
        //    FingersScript.Instance.enabled = true;

        paintingModuleName.text = pm.paintingName;

        //添加PaintingModule模块按钮关闭
        PaintingModuleDataManager.Instance.LoadScenePaintingModules(pm);
    }

    /// <summary>
    /// 画作帧动画结束
    /// </summary>
    public void PaintingModuleFrameStop()
    {

        Debug.Log("画作帧动画结束");

        switch (options)
        {
            case PaintingModuleOptionEnum.None:
                break;
            case PaintingModuleOptionEnum.Frame:

                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.UploadUserClickLogs, null);

                if (PaintingModuleDataManager.Instance.currentColliderPaintingModule != null)
                    PaintingModuleDataManager.Instance.currentColliderPaintingModule.ColliderEnterEvent();

                if (PaintingModuleMap.Instance._SmartToFrame)
                {
                    PaintingModuleMap.Instance.PaintingModuleMapCanvasStart();
                }
                else
                {
                    PaintingModuleMap.Instance.PaintingModuleMapUICanvasStart();

                    PaintingModuleMap.Instance.PaintingModuleHelpUICanvasStart();
                }
                EasyTouchControlsManager.Instance.EasyTouchControlsStart();

                PaintingModuleMap.Instance._SmartToFrame = false;

                paintingModuleFrameRawImage.texture = null;

                paintingModuleFrameCanvas.gameObject.SetActive(false);

                PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStop();

                PaintingModuleFrameControl.Instance.PaintingModuleDistanceInitial();

                PaintingModuleDataManager.Instance.DeleteCurrentPaintingModule();


                if (paintingModuleCanvasGroup_Sound_AudioSource.clip != null)
                {
                    if (isPlayAudio)
                        SceneBGMManager.Instance.PlaySceneBGM();

                    paintingModuleCanvasGroup_Sound_AudioSource.clip = null;

                    paintingModuleOption_Sound_Image.sprite = audioUISprites[0];

                    isPlayAudio = false;
                }

                OptionButtonInitial();

                paintingModuleName.enabled = false;

                FingersScript.Instance.enabled = false;

                options = PaintingModuleOptionEnum.None;

                break;
            case PaintingModuleOptionEnum.Video:
                break;
            case PaintingModuleOptionEnum.Introduce:
                break;
            case PaintingModuleOptionEnum.Web:
                break;
            case PaintingModuleOptionEnum.Sound:
                break;
        }
    }


    /// <summary>
    /// 画作模块视频播放
    /// </summary>
    public void PaintingModuleVideoStart()
    {
        switch (options)
        {
            case PaintingModuleOptionEnum.None:
                break;
            case PaintingModuleOptionEnum.Frame:

                PMDM.videoClickCount++;

                PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStop();

                paintingModuleFrameLastButton.gameObject.SetActive(false);
                paintingModuleFrameNextButton.gameObject.SetActive(false);

                SceneBGMManager.Instance.PauseSceneBGM();

                paintingModuleName.enabled = false;

                FingersScript.Instance.enabled = false;


                if (paintingModuleCanvasGroup_Sound_AudioSource.clip != null)
                {
                    paintingModuleCanvasGroup_Sound_AudioSource.Pause();

                    paintingModuleOption_Sound_Image.sprite = audioUISprites[0];

                    isPlayAudio = false;
                }


                if (!string.IsNullOrEmpty(PaintingModuleDataManager.Instance.currentPaintingModule.videoSaveUrl))
                {
                    videoPlayer.m_VideoPath = PaintingModuleDataManager.Instance.CurrentPaintingModuleVideoURL();
                }
                else
                {
                    videoPlayer.m_VideoPath = PaintingModuleDataManager.Instance.currentPaintingModule.videoLink;
                }
                videoPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, videoPlayer.m_VideoPath);

                //FileStream fileStream = new FileStream(videoPlayer.m_VideoPath, FileMode.Open, FileAccess.ReadWrite);
                //fileStream.Seek(0, SeekOrigin.Begin);
                //byte[] bytes = new byte[fileStream.Length];
                //fileStream.Read(bytes, 0, (int)fileStream.Length);
                //fileStream.Close();
                //fileStream.Dispose();
                //fileStream = null;

                //int butsize = 1048576;
                //int index = 0;

                //Debug.Log("bytes:" + bytes.Length);

                //if ((bytes.Length <= butsize) && (bytes.Length > 3))
                //{
                //    bytes[3] = (byte)~bytes[3];
                //}
                //else
                //{
                //    index = bytes.Length / butsize;

                //    Debug.Log("index0:" + index);


                //    for (int i = 0; i < index; i++)
                //    {

                //        Debug.Log("indexCC:" + i);
                //        if (i == 0)
                //        {
                //            bytes[3] = (byte)~bytes[3];
                //        }
                //        else
                //        {
                //            Debug.Log("indexAA:" + ((butsize * i) + 3));
                //            bytes[(butsize * i) + 3] = (byte)~bytes[(butsize * i) + 3];
                //        }
                //    }

                //    if (bytes.Length % butsize > 3)
                //    {
                //        Debug.Log("indexBB:" + ((index * butsize) + 3));

                //        bytes[index * butsize + 3] = (byte)~bytes[index * butsize + 3];
                //    }
                //}

                //videoPlayer.OpenVideoFromBuffer(bytes, false);

                VCR.OnFirstPlayVideo();
                paintingModuleCanvasGroup_Video.alpha = 1;
                paintingModuleCanvasGroup_Video.blocksRaycasts = true;
                paintingModuleCanvasGroup_Video.interactable = true;

                options = PaintingModuleOptionEnum.Video;

                break;
            case PaintingModuleOptionEnum.Video:
                break;
            case PaintingModuleOptionEnum.Introduce:
                break;
            case PaintingModuleOptionEnum.Web:
                break;
            case PaintingModuleOptionEnum.Sound:
                break;
        }
    }
    /// <summary>
    /// 画作模块视频停止
    /// </summary>
    public void PaintingModuleVideoStop()
    {
        switch (options)
        {
            case PaintingModuleOptionEnum.None:
                break;
            case PaintingModuleOptionEnum.Frame:
                break;
            case PaintingModuleOptionEnum.Video:

                paintingModuleName.enabled = true;

                videoPlayer.Rewind(true);

                paintingModuleCanvasGroup_Video.alpha = 0;
                paintingModuleCanvasGroup_Video.blocksRaycasts = false;
                paintingModuleCanvasGroup_Video.interactable = false;

                SceneBGMManager.Instance.PlaySceneBGM();

                FingersScript.Instance.enabled = true;

                paintingModuleFrameLastButton.gameObject.SetActive(true);
                paintingModuleFrameNextButton.gameObject.SetActive(true);

                options = PaintingModuleOptionEnum.Frame;

                if (PMDM.currentPaintingModule.frameAnimationHttpUrl != null && PMDM.currentPaintingModule.frameAnimationHttpUrl.Length != 0)
                    PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();

                break;
            case PaintingModuleOptionEnum.Introduce:
                break;
            case PaintingModuleOptionEnum.Web:
                break;
            case PaintingModuleOptionEnum.Sound:
                break;
        }
    }

    /// <summary>
    /// 画作模块介绍打开
    /// </summary>
    public void PaintingModuleIntroduceStart()
    {
        switch (options)
        {
            case PaintingModuleOptionEnum.None:
                break;
            case PaintingModuleOptionEnum.Frame:

                PMDM.introductionClickCount++;

                paintingModuleFrameLastButton.gameObject.SetActive(false);
                paintingModuleFrameNextButton.gameObject.SetActive(false);

                paintingModuleName.enabled = false;

                //PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStop();

                //FingersScript.Instance.enabled = false;

                PaintingModuleFrameControl.Instance.isFrameFingers = false;

                SceneBGMManager.Instance.PlayIntroduceSceneBGM();

                paintingModuleCanvasGroup_Introduce.alpha = 1;
                paintingModuleCanvasGroup_Introduce.blocksRaycasts = true;
                paintingModuleCanvasGroup_Introduce.interactable = true;

                PaintingModuleIntroduceButtonEventAdd();

                if (paintingModuleCanvasGroup_Sound_AudioSource.clip != null)
                {
                    paintingModuleCanvasGroup_Sound_AudioSource.Pause();

                    paintingModuleOption_Sound_Image.sprite = audioUISprites[0];

                    isPlayAudio = false;
                }

                options = PaintingModuleOptionEnum.Introduce;
                break;
            case PaintingModuleOptionEnum.Video:
                break;
            case PaintingModuleOptionEnum.Introduce:
                break;
            case PaintingModuleOptionEnum.Web:
                break;
            case PaintingModuleOptionEnum.Sound:
                break;
        }
    }

    /// <summary>
    /// 画作模块介绍关闭
    /// </summary>
    public void PaintingModuleIntroduceStop()
    {
        switch (options)
        {
            case PaintingModuleOptionEnum.None:
                break;
            case PaintingModuleOptionEnum.Frame:
                break;
            case PaintingModuleOptionEnum.Video:
                break;
            case PaintingModuleOptionEnum.Introduce:

                paintingModuleName.enabled = true;

                PaintingModuleFrameControl.Instance.isFrameFingers = true;

                PaintingModuleFrameControl.Instance.PaintingModuleIntroduceTextureInitial();

                paintingModuleCanvasGroup_Introduce.alpha = 0;
                paintingModuleCanvasGroup_Introduce.blocksRaycasts = false;
                paintingModuleCanvasGroup_Introduce.interactable = false;

                paintingModuleFrameLastButton.gameObject.SetActive(true);
                paintingModuleFrameNextButton.gameObject.SetActive(true);

                options = PaintingModuleOptionEnum.Frame;

                paintingModuleCanvasGroup_Introduce_Image.texture = null;


                if (PMDM.currentPaintingModule.frameAnimationHttpUrl != null &&
                    PMDM.currentPaintingModule.frameAnimationHttpUrl.Length != 0)
                {
                    FingersScript.Instance.enabled = true;
                    PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();
                    PaintingModuleFrameControl.Instance.PaintingModuleLocalPositionInitial();
                    PaintingModuleFrameRawImageLocalScale(PMDM.currentPaintingModule.width,
                        PMDM.currentPaintingModule.height);
                }


                break;
            case PaintingModuleOptionEnum.Web:
                break;
            case PaintingModuleOptionEnum.Sound:
                break;
        }
    }

    /// <summary>
    /// 画作模块Web打开
    /// </summary>
    public void PaintingModuleWebStart()
    {
        switch (options)
        {
            case PaintingModuleOptionEnum.None:
                break;
            case PaintingModuleOptionEnum.Frame:

                //paintingModuleName.enabled = false;

                //PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStop();

                //paintingModuleCanvasGroup_Web.alpha = 1;
                //paintingModuleCanvasGroup_Web.blocksRaycasts = true;
                //paintingModuleCanvasGroup_Web.interactable = true;

                PMDM.linkClickCount++;

                PaintingModuleWebButtonEventAdd();

                paintingModuleOption_Web_Image.raycastTarget = false;

                StartCoroutine(openWeb());

                //gr.enabled = false;

                //SceneBGMManager.Instance.PauseSceneBGM();


                //if (paintingModuleCanvasGroup_Sound_AudioSource.clip != null)
                //{
                //    paintingModuleCanvasGroup_Sound_AudioSource.Pause();

                //    paintingModuleOption_Sound_Image.sprite = audioUISprites[0];

                //    isPlayAudio = false;
                //}

                //options = PaintingModuleOptionEnum.Web;

                break;
            case PaintingModuleOptionEnum.Video:
                break;
            case PaintingModuleOptionEnum.Introduce:
                break;
            case PaintingModuleOptionEnum.Web:
                break;
            case PaintingModuleOptionEnum.Sound:
                break;
        }
    }

    IEnumerator openWeb()
    {
        yield return new WaitForSeconds(1f);

        paintingModuleOption_Web_Image.raycastTarget = true;
    }

    /// <summary>
    /// 画作模块Web关闭
    /// </summary>
    public void PaintingModuleWebStop()
    {
        //switch (options)
        //{
        //    case PaintingModuleOptionEnum.None:
        //        break;
        //    case PaintingModuleOptionEnum.Frame:
        //        break;
        //    case PaintingModuleOptionEnum.Video:
        //        break;
        //    case PaintingModuleOptionEnum.Introduce:
        //        break;
        //    case PaintingModuleOptionEnum.Web:

        //        //paintingModuleName.enabled = true;

        //        //paintingModuleCanvasGroup_Web.alpha = 0;
        //        //paintingModuleCanvasGroup_Web.blocksRaycasts = false;
        //        //paintingModuleCanvasGroup_Web.interactable = false;



        //        //options = PaintingModuleOptionEnum.Frame;



        //        break;
        //    case PaintingModuleOptionEnum.Sound:
        //        break;
        //}

        PaintingModuleWebButtonEventExit();

        gr.enabled = true;

        SceneBGMManager.Instance.PlaySceneBGM();

        if (PMDM.currentPaintingModule.frameAnimationHttpUrl != null && PMDM.currentPaintingModule.frameAnimationHttpUrl.Length != 0)
            PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();
    }

    /// <summary>
    /// 画作模块音频打开
    /// </summary>
    public void PaintingModuleSoundStart()
    {
        if (!isPlayAudio)
        {
            PMDM.voiceClickCount++;

            PaintingModuleSoundButtonEventAdd();

            SceneBGMManager.Instance.PauseSceneBGM();

            paintingModuleOption_Sound_Image.sprite = audioUISprites[1];

            isPlayAudio = true;
        }
        else
        {
            paintingModuleCanvasGroup_Sound_AudioSource.Pause();

            SceneBGMManager.Instance.UnPauseSceneBGM();

            paintingModuleOption_Sound_Image.sprite = audioUISprites[0];

            isPlayAudio = false;
        }


        //switch (options)
        //{
        //    case PaintingModuleOptionEnum.None:
        //        break;
        //    case PaintingModuleOptionEnum.Frame:

        //        //paintingModuleName.enabled = false;

        //        //PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStop();

        //        //paintingModuleCanvasGroup_Sound.alpha = 1;
        //        //paintingModuleCanvasGroup_Sound.blocksRaycasts = true;
        //        //paintingModuleCanvasGroup_Sound.interactable = true;


        //        //options = PaintingModuleOptionEnum.Sound;

        //        break;
        //    case PaintingModuleOptionEnum.Video:
        //        break;
        //    case PaintingModuleOptionEnum.Introduce:
        //        break;
        //    case PaintingModuleOptionEnum.Web:
        //        break;
        //    case PaintingModuleOptionEnum.Sound:
        //        break;
        //}
    }

    /// <summary>
    /// 画作模块音频关闭
    /// </summary>
    public void PaintingModuleSoundStop()
    {
        //switch (options)
        //{
        //    case PaintingModuleOptionEnum.None:
        //        break;
        //    case PaintingModuleOptionEnum.Frame:
        //        break;
        //    case PaintingModuleOptionEnum.Video:
        //        break;
        //    case PaintingModuleOptionEnum.Introduce:
        //        break;
        //    case PaintingModuleOptionEnum.Web:
        //        break;
        //    case PaintingModuleOptionEnum.Sound:

        //        paintingModuleName.enabled = true;

        //        paintingModuleCanvasGroup_Sound.alpha = 0;
        //        paintingModuleCanvasGroup_Sound.blocksRaycasts = false;
        //        paintingModuleCanvasGroup_Sound.interactable = false;

        //        paintingModuleCanvasGroup_Sound_AudioSource.Stop();

        //        SceneBGMManager.Instance.PlaySceneBGM();

        //        options = PaintingModuleOptionEnum.Frame;

        //        if (PMDM.currentPaintingModule.frameAnimationHttpUrl != null)
        //            PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();
        //        break;
        //}
    }

    /// <summary>
    /// 画作模块Store打开
    /// </summary>
    public void PaintingModuleStoreStart()
    {
        switch (options)
        {
            case PaintingModuleOptionEnum.None:
                break;
            case PaintingModuleOptionEnum.Frame:

                PMDM.shopClickCount++;

                PaintingModuleStoreButtonEventAdd();

                paintingModuleOption_Store_Image.raycastTarget = false;

                StartCoroutine(openStore());

                break;
            case PaintingModuleOptionEnum.Video:
                break;
            case PaintingModuleOptionEnum.Introduce:
                break;
            case PaintingModuleOptionEnum.Web:
                break;
            case PaintingModuleOptionEnum.Sound:
                break;
            case PaintingModuleOptionEnum.Store:
                break;
        }
    }

    /// <summary>
    /// 画作模块视频网页打开
    /// </summary>
    public void PaintingModuleWebVideoStart()
    {
        switch (options)
        {
            case PaintingModuleOptionEnum.None:
                break;
            case PaintingModuleOptionEnum.Frame:

                //PMDM.shopClickCount++;
                PMDM.linkClickCount++;

                PaintingModuleWebVideoButtonEventAdd();

                PaintingModuleOption_WebVideo_Image.raycastTarget = false;

                StartCoroutine(openWebVideo());

                break;
            case PaintingModuleOptionEnum.Video:
                break;
            case PaintingModuleOptionEnum.Introduce:
                break;
            case PaintingModuleOptionEnum.Web:
                break;
            case PaintingModuleOptionEnum.Sound:
                break;
            case PaintingModuleOptionEnum.Store:
                break;
        }
    }

    IEnumerator openStore()
    {
        yield return new WaitForSeconds(1f);

        paintingModuleOption_Store_Image.raycastTarget = true;
    }
    IEnumerator openWebVideo()
    {
        yield return new WaitForSeconds(1f);

        PaintingModuleOption_WebVideo_Image.raycastTarget = true;
    }
    /// <summary>
    /// 画作模块Loading打开
    /// </summary>
    public void PaintingModuleLoadingStart()
    {
        paintingModuleLoading.alpha = 1;
        paintingModuleLoading.blocksRaycasts = true;
        paintingModuleLoading.interactable = true;
        PaintingModuleLoadingLoad(0);
    }
    /// <summary>
    /// 画作模块Loading关闭
    /// </summary>
    public void PaintingModuleLoadingStop()
    {
        paintingModuleLoading.alpha = 0;
        paintingModuleLoading.blocksRaycasts = false;
        paintingModuleLoading.interactable = false;
        PaintingModuleLoadingLoad(0);
    }
    /// <summary>
    /// 画作模块Loading加载
    /// </summary>
    public void PaintingModuleLoadingLoad(int index)
    {
        paintingModuleLoadingText.text = "Loading：" + index + "%";
    }


    /// <summary>
    /// 画作帧图片切换
    /// </summary>
    public void PaintingModuleFrameRawImageReplace(int eulerindex)
    {
        paintingModuleFrameRawImage.texture = PaintingModuleDataManager.Instance.PaintingModuleManagerReplacedTexture(eulerindex);
    }
    /// <summary>
    /// 设置缩略图
    /// </summary>
    public void PaintingModuleFrameRawSetAnimationThumbnail()
    {
        paintingModuleFrameRawImage.texture =
            PaintingModuleDataManager.Instance.currentPaintingModule.animationThumbnailTexture;
    }

    /// <summary>
    /// 画作帧图片大小调整
    /// </summary>
    public void PaintingModuleFrameRawImageLocalScale(int width, int height)
    {
        //float _heightIndex = (float)height / 9;
        //float _widthIndex = (float)width / 16;
        //if (_widthIndex == _heightIndex)
        //{
        //    paintingModuleFrameRawImage.transform.localScale = Vector3.one;
        //}
        //else if (_widthIndex > _heightIndex)
        //{
        //    paintingModuleFrameRawImage.transform.localScale = new Vector3(1, _heightIndex / _widthIndex, 1);
        //}
        //else if (_widthIndex < _heightIndex)
        //{
        //    paintingModuleFrameRawImage.transform.localScale = new Vector3(_widthIndex / _heightIndex, 1, 1);
        //}       

        float _heightIndex = (float)width / ((float)height / 640 * 1280);

        paintingModuleFrameRawImage.transform.localScale = new Vector3(_heightIndex, 1, 1);

    }
    /// <summary>
    /// 画作简介图片大小调整
    /// </summary>
    public void PaintingModuleIntroduceRawImageLocalScale(int width, int height)
    {
        float _heightIndex = (float)width / ((float)height / 640 * 1280);

        paintingModuleCanvasGroup_Introduce_Image.transform.localScale = new Vector3(_heightIndex, 1, 1);


        //float _heightIndex = (float)height / 9;
        //float _widthIndex = (float)width / 16;
        //if (_widthIndex == _heightIndex)
        //{
        //    paintingModuleCanvasGroup_Introduce_Image.transform.localScale = Vector3.one;
        //}
        //else if (_widthIndex > _heightIndex)
        //{
        //    paintingModuleCanvasGroup_Introduce_Image.transform.localScale = new Vector3(1, _heightIndex / _widthIndex, 1);
        //}
        //else if (_widthIndex < _heightIndex)
        //{
        //    paintingModuleCanvasGroup_Introduce_Image.transform.localScale = new Vector3(_widthIndex / _heightIndex, 1, 1);
        //}
    }
    /// <summary>
    /// 画作模块Web按钮事件添加
    /// </summary>
    public void PaintingModuleWebButtonEventAdd()
    {
        JsonData datas;
        datas = JsonMapper.ToObject(PaintingModuleDataManager.Instance.currentPaintingModule.webLink);
        if (datas.Count == 1)
        {
            if (datas[0][0].ToString() == "web")
            {
                CustomUrlOpener.Open(datas[0][1].ToString());

            }
        }
        else if (datas.Count == 2)
        {
            if (datas[0][0].ToString() == "web")
            {
                CustomUrlOpener.Open(datas[0][1].ToString());
                return;
            }

            if (datas[1][0].ToString() == "web")
            {
                CustomUrlOpener.Open(datas[1][1].ToString());
            }
        }
        else if (datas.Count == 3 || datas.Count == 4)
        {
            if (datas[0][0].ToString() == "web")
            {
                CustomUrlOpener.Open(datas[0][1].ToString());
                return;
            }

            if (datas[1][0].ToString() == "web")
            {
                CustomUrlOpener.Open(datas[1][1].ToString());
            }
        }
    }

    public void PaintingModuleWebButtonEventExit()
    {
        BrowserOpener.Instance.OnClearCacheClicked();
    }

    //private IEnumerator OpenWeb()
    //{
    //    paintingModuleCanvasGroup_Web_Button.interactable = false;

    //    yield return new WaitForSeconds(3f);

    //    paintingModuleCanvasGroup_Web_Button.interactable = true;
    //}

    /// <summary>
    /// 画作模块简介按钮事件添加
    /// </summary>
    public void PaintingModuleIntroduceButtonEventAdd()
    {
        if (PMDM.currentPaintingModule.introductionTexture == null)
        {
            SetPaintingModuleIntroduce(PMDM.currentPaintingModule.introductionImageSaveUrl);
        }
        else
        {
            paintingModuleCanvasGroup_Introduce_Image.texture = PMDM.currentPaintingModule.introductionTexture;
            PaintingModuleIntroduceRawImageLocalScale(PMDM.currentPaintingModule.introductionTexture.width, PMDM.currentPaintingModule.introductionTexture.height);
        }
    }
    public void SetPaintingModuleIntroduce(string url)
    {
        if (PMDM.currentPaintingModule.introductionImageHttpUrl != null &&
            PMDM.currentPaintingModule.introductionImageHttpUrl != " ")
        {
            //            string _url = "";
            //#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            //            _url = url;
            //#elif UNITY_ANDROID
            //        _url = "file://"+url;
            //#elif UNITY_IOS || UNITY_IPHONE
            //        _url = "file://"+url;
            //#endif

            //            UnityWebRequest wr = new UnityWebRequest(_url);

            //            DownloadHandlerTexture texdl = new DownloadHandlerTexture(true);

            //            wr.downloadHandler = texdl;

            //            yield return wr.SendWebRequest();

            //            PMDM.currentPaintingModule.introductionTexture = texdl.texture;

            //            PaintingModuleIntroduceRawImageLocalScale(texdl.texture.width, texdl.texture.height);

            //            paintingModuleCanvasGroup_Introduce_Image.texture = PMDM.currentPaintingModule.introductionTexture;

            //            texdl.Dispose();

            //            wr.Dispose();


            FileStream fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;

            int butsize = 1048576;
            int index = 0;


            if ((bytes.Length <= butsize) && (bytes.Length > 3))
            {
                bytes[3] = (byte)~bytes[3];
            }
            else
            {
                index = bytes.Length / butsize;
                for (int j = 0; j < index - 1; j++)
                {
                    bytes[(butsize * j) + 3] = (byte)~bytes[(butsize * j) + 3];
                }

                if (bytes.Length % butsize > 3)
                {
                    bytes[index * butsize + 3] = (byte)~bytes[index * butsize + 3];
                }
            }

            //读取图片
            Texture2D texture1 = new Texture2D(0, 0);
            texture1.LoadImage(bytes);
            texture1.Apply(false, true);

            PMDM.currentPaintingModule.introductionTexture = texture1;

            PaintingModuleIntroduceRawImageLocalScale(texture1.width, texture1.height);

            paintingModuleCanvasGroup_Introduce_Image.texture = PMDM.currentPaintingModule.introductionTexture;

        }
        else
        {
            PMDM.currentPaintingModule.introductionTexture = null;
            paintingModuleCanvasGroup_Introduce_Image.texture = null;
        }

    }
    /// <summary>
    /// 画作模块音频按钮事件添加
    /// </summary>
    public void PaintingModuleSoundButtonEventAdd()
    {
        if (paintingModuleCanvasGroup_Sound_AudioSource.clip != null)
        {
            paintingModuleCanvasGroup_Sound_AudioSource.Play();
        }
        else
        {
            if (PMDM.currentPaintingModule.voiceClip != null)
            {
                paintingModuleCanvasGroup_Sound_AudioSource.clip = PMDM.currentPaintingModule.voiceClip;
                paintingModuleCanvasGroup_Sound_AudioSource.Play();
            }
            else
            {
                StartCoroutine(GetAudioClip(PMDM.currentPaintingModule.voiceSaveUrl));
            }
        }

    }

    IEnumerator GetAudioClip(string path)
    {
        //path = "file://" + path;
        //（目录如果为Application.persistentDataPath + "/1" 下必须添加“file://”，这里可以写个宏）

        string _url = "";
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        _url = path;
#elif UNITY_ANDROID
        _url = "file://"+path;
#elif UNITY_IOS|| UNITY_IPHONE
        _url = "file://"+path;
#endif

        using (var uwr = UnityWebRequestMultimedia.GetAudioClip(_url, AudioType.MPEG))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError)
            {
                Debug.LogError("uwrERROR:" + uwr.error
                );
            }
            else
            {
                PMDM.currentPaintingModule.voiceClip = DownloadHandlerAudioClip.GetContent(uwr);
                paintingModuleCanvasGroup_Sound_AudioSource.clip = PMDM.currentPaintingModule.voiceClip;

            }
        }
        paintingModuleCanvasGroup_Sound_AudioSource.Play();
    }
    /// <summary>
    /// 设置画作按钮开关
    /// </summary>
    public void SetPaintingModuleOptionButton(PaintingModule pm)
    {
        if (pm.introductionImageSaveUrl == " " || pm.introductionImageSaveUrl == "")
        {
            paintingModuleOption_Introduce.interactable = false;
        }

        if (pm.videoSaveUrl == " " || pm.videoSaveUrl == "")
        {
            paintingModuleOption_Video.interactable = false;
        }

        if (pm.webLink == " " || pm.webLink == "")
        {
            paintingModuleOption_Web.interactable = false;
            paintingModuleOption_Store.interactable = false;
            PaintingModuleOption_WebVideo.interactable = false;
        }
        else
        {
            JsonData datas;
            datas = JsonMapper.ToObject(pm.webLink);
            if (datas.Count == 1)
            {
                paintingModuleOption_Web.interactable = false;
                paintingModuleOption_Store.interactable = false;
                PaintingModuleOption_WebVideo.interactable = false;

                if (datas[0][0].ToString() == "web")
                {
                    paintingModuleOption_Store.interactable = false;
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[0][0].ToString() == "cart")
                {
                    paintingModuleOption_Web.interactable = false;
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[0][0].ToString() == "YT" || datas[0][0].ToString() == "BL")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }

            }
            else if (datas.Count == 2)
            {
                paintingModuleOption_Web.interactable = false;
                paintingModuleOption_Store.interactable = false;
                PaintingModuleOption_WebVideo.interactable = false;

                if (datas[0][0].ToString() == "web")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[0][0].ToString() == "cart")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[0][0].ToString() == "YT" || datas[0][0].ToString() == "BL")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }

                if (datas[1][0].ToString() == "web")
                {
                    if (datas[1][1].ToString() == " " || datas[1][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[1][0].ToString() == "cart")
                {
                    if (datas[1][1].ToString() == " " || datas[1][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[0][0].ToString() == "YT" || datas[0][0].ToString() == "BL")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }
            }
            else if (datas.Count == 3)
            {
                paintingModuleOption_Web.interactable = false;
                paintingModuleOption_Store.interactable = false;
                PaintingModuleOption_WebVideo.interactable = false;

                if (datas[0][0].ToString() == "web")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[0][0].ToString() == "cart")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[0][0].ToString() == "YT" || datas[0][0].ToString() == "BL")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }

                if (datas[1][0].ToString() == "web")
                {
                    if (datas[1][1].ToString() == " " || datas[1][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[1][0].ToString() == "cart")
                {
                    if (datas[1][1].ToString() == " " || datas[1][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[1][0].ToString() == "YT" || datas[1][0].ToString() == "BL")
                {
                    if (datas[1][1].ToString() == " " || datas[1][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }

                if (datas[2][0].ToString() == "web")
                {
                    if (datas[2][1].ToString() == " " || datas[2][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[2][0].ToString() == "cart")
                {
                    if (datas[2][1].ToString() == " " || datas[2][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[2][0].ToString() == "YT" || datas[2][0].ToString() == "BL")
                {
                    if (datas[2][1].ToString() == " " || datas[2][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }
            }
            else if (datas.Count == 4)
            {
                paintingModuleOption_Web.interactable = false;
                paintingModuleOption_Store.interactable = false;
                PaintingModuleOption_WebVideo.interactable = false;

                if (datas[0][0].ToString() == "web")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[0][0].ToString() == "cart")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[0][0].ToString() == "YT" || datas[0][0].ToString() == "BL")
                {
                    if (datas[0][1].ToString() == " " || datas[0][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }

                if (datas[1][0].ToString() == "web")
                {
                    if (datas[1][1].ToString() == " " || datas[1][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[1][0].ToString() == "cart")
                {
                    if (datas[1][1].ToString() == " " || datas[1][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[1][0].ToString() == "YT" || datas[1][0].ToString() == "BL")
                {
                    if (datas[1][1].ToString() == " " || datas[1][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }

                if (datas[2][0].ToString() == "web")
                {
                    if (datas[2][1].ToString() == " " || datas[2][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[2][0].ToString() == "cart")
                {
                    if (datas[2][1].ToString() == " " || datas[2][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[2][0].ToString() == "YT" || datas[2][0].ToString() == "BL")
                {
                    if (datas[2][1].ToString() == " " || datas[2][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }

                if (datas[3][0].ToString() == "web")
                {
                    if (datas[3][1].ToString() == " " || datas[3][1].ToString() == "")
                    {
                        paintingModuleOption_Web.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Web.interactable = true;
                    }
                }
                else if (datas[3][0].ToString() == "cart")
                {
                    if (datas[3][1].ToString() == " " || datas[3][1].ToString() == "")
                    {
                        paintingModuleOption_Store.interactable = false;
                    }
                    else
                    {
                        paintingModuleOption_Store.interactable = true;
                    }
                }
                else if (datas[3][0].ToString() == "YT" || datas[3][0].ToString() == "BL")
                {
                    if (datas[3][1].ToString() == " " || datas[3][1].ToString() == "")
                    {
                        PaintingModuleOption_WebVideo.interactable = false;
                    }
                    else
                    {
                        PaintingModuleOption_WebVideo.interactable = true;
                    }
                }
            }
        }

        if (pm.voiceSaveUrl == " " || pm.voiceSaveUrl == "")
        {
            paintingModuleOption_Sound.interactable = false;
        }
    }
    /// <summary>
    /// 上一幅画作事件
    /// </summary>
    public void PaintingModuleFrameLastButtonEvent()
    {
        HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.UploadUserClickLogs, null);

        int index = PMDM.availablePM.IndexOf(PMDM.currentPaintingModule);

        paintingModuleFrameRawImage.texture = null;

        PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStop();

        PaintingModuleFrameControl.Instance.PaintingModuleDistanceInitial();

        OptionButtonInitial();

        PMDM.DeleteCurrentPaintingModule();

        if (paintingModuleCanvasGroup_Sound_AudioSource.clip != null)
        {
            paintingModuleCanvasGroup_Sound_AudioSource.clip = null;

            paintingModuleOption_Sound_Image.sprite = audioUISprites[0];

            isPlayAudio = false;
        }

        FingersScript.Instance.enabled = false;

        if (index == 0)
        {
            index = PMDM.availablePM.Count - 1;
        }
        else
        {
            index--;
        }

        SceneBGMManager.Instance.UnPauseSceneBGM();

        Debug.Log(index);

        PaintingModuleFrameStart(PMDM.availablePM[index]);

    }
    /// <summary>
    /// 下一幅画作事件
    /// </summary>
    public void PaintingModuleFrameNextButtonEvent()
    {
        HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.UploadUserClickLogs, null);

        int index = PMDM.availablePM.IndexOf(PMDM.currentPaintingModule);

        paintingModuleFrameRawImage.texture = null;

        PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStop();

        PaintingModuleFrameControl.Instance.PaintingModuleDistanceInitial();

        PMDM.DeleteCurrentPaintingModule();

        OptionButtonInitial();

        if (paintingModuleCanvasGroup_Sound_AudioSource.clip != null)
        {
            paintingModuleCanvasGroup_Sound_AudioSource.clip = null;

            paintingModuleOption_Sound_Image.sprite = audioUISprites[0];

            isPlayAudio = false;
        }

        FingersScript.Instance.enabled = false;

        if (index == PMDM.availablePM.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        Debug.Log(index);

        SceneBGMManager.Instance.UnPauseSceneBGM();

        PaintingModuleFrameStart(PMDM.availablePM[index]);
    }

    /// <summary>
    /// 画作帧动画锁退出
    /// </summary>
    public void PaintingModuleLockExit()
    {

        Debug.Log("画作帧动画结束");

        switch (options)
        {
            case PaintingModuleOptionEnum.None:
                break;
            case PaintingModuleOptionEnum.Frame:

                if (PaintingModuleDataManager.Instance.currentColliderPaintingModule != null)
                    PaintingModuleDataManager.Instance.currentColliderPaintingModule.ColliderEnterEvent();

                if (PaintingModuleMap.Instance._SmartToFrame)
                {
                    PaintingModuleMap.Instance.PaintingModuleMapCanvasStart();
                }
                else
                {
                    PaintingModuleMap.Instance.PaintingModuleMapUICanvasStart();

                    PaintingModuleMap.Instance.PaintingModuleHelpUICanvasStart();
                }
                EasyTouchControlsManager.Instance.EasyTouchControlsStart();

                PaintingModuleMap.Instance._SmartToFrame = false;

                paintingModuleFrameRawImage.texture = null;

                paintingModuleFrameCanvas.gameObject.SetActive(false);

                PaintingModuleFrameControl.Instance.PaintingModuleDistanceInitial();

                PaintingModuleDataManager.Instance.DeleteCurrentPaintingModule();


                if (paintingModuleCanvasGroup_Sound_AudioSource.clip != null)
                {
                    if (isPlayAudio)
                        SceneBGMManager.Instance.PlaySceneBGM();

                    paintingModuleCanvasGroup_Sound_AudioSource.clip = null;

                    paintingModuleOption_Sound_Image.sprite = audioUISprites[0];

                    isPlayAudio = false;
                }

                OptionButtonInitial();

                paintingModuleName.enabled = false;

                options = PaintingModuleOptionEnum.None;

                break;
            case PaintingModuleOptionEnum.Video:
                break;
            case PaintingModuleOptionEnum.Introduce:
                break;
            case PaintingModuleOptionEnum.Web:
                break;
            case PaintingModuleOptionEnum.Sound:
                break;
        }
    }

    /// <summary>
    /// 画作模块Store按钮事件添加
    /// </summary>
    public void PaintingModuleStoreButtonEventAdd()
    {
        JsonData datas;
        datas = JsonMapper.ToObject(PaintingModuleDataManager.Instance.currentPaintingModule.webLink);
        if (datas.Count == 1)
        {
            if (datas[0][0].ToString() == "cart")
            {
                CustomUrlOpener.Open(datas[0][1].ToString());

            }
        }
        else if (datas.Count == 2)
        {
            if (datas[0][0].ToString() == "cart")
            {
                CustomUrlOpener.Open(datas[0][1].ToString());
                return;
            }

            if (datas[1][0].ToString() == "cart")
            {
                CustomUrlOpener.Open(datas[1][1].ToString());
            }
        }
        else if (datas.Count == 3 || datas.Count == 4)
        {
            if (datas[0][0].ToString() == "cart")
            {
                CustomUrlOpener.Open(datas[0][1].ToString());
                return;
            }

            if (datas[1][0].ToString() == "cart")
            {
                CustomUrlOpener.Open(datas[1][1].ToString());
            }
        }
    }

    /// <summary>
    /// 画作模块Store按钮事件添加
    /// </summary>
    public void PaintingModuleWebVideoButtonEventAdd()
    {
        JsonData datas;
        datas = JsonMapper.ToObject(PaintingModuleDataManager.Instance.currentPaintingModule.webLink);

        switch (PaintingModuleDataManager.Instance.language)
        {
            case Language.中文:
                if (datas.Count == 1)
                {
                    if (datas[0][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());

                    }
                }
                else if (datas.Count == 2)
                {
                    if (datas[0][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());
                        return;
                    }

                    if (datas[1][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[1][1].ToString());
                    }
                }
                else if (datas.Count == 3)
                {
                    if (datas[0][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());
                        return;
                    }
                    if (datas[1][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[1][1].ToString());
                        return;
                    }
                    if (datas[2][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[2][1].ToString());
                    }
                }
                else if (datas.Count == 4)
                {
                    if (datas[0][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());
                        return;
                    }
                    if (datas[1][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[1][1].ToString());
                        return;
                    }
                    if (datas[2][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[2][1].ToString());
                        return;
                    }
                    if (datas[3][0].ToString() == "BL")
                    {
                        CustomUrlOpener.Open(datas[3][1].ToString());
                    }
                }
                break;
            case Language.英语:
                if (datas.Count == 1)
                {
                    if (datas[0][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());

                    }
                }
                else if (datas.Count == 2)
                {
                    if (datas[0][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());
                        return;
                    }

                    if (datas[1][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[1][1].ToString());
                    }
                }
                else if (datas.Count == 3)
                {
                    if (datas[0][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());
                        return;
                    }
                    if (datas[1][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[1][1].ToString());
                        return;
                    }
                    if (datas[2][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[2][1].ToString());
                    }
                }
                else if (datas.Count == 4)
                {
                    if (datas[0][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());
                        return;
                    }
                    if (datas[1][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[1][1].ToString());
                        return;
                    }
                    if (datas[2][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[2][1].ToString());
                        return;
                    }
                    if (datas[3][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[3][1].ToString());
                    }
                }
                break;
            case Language.日文:
                if (datas.Count == 1)
                {
                    if (datas[0][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());

                    }
                }
                else if (datas.Count == 2)
                {
                    if (datas[0][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());
                        return;
                    }

                    if (datas[1][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[1][1].ToString());
                    }
                }
                else if (datas.Count == 3)
                {
                    if (datas[0][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());
                        return;
                    }
                    if (datas[1][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[1][1].ToString());
                        return;
                    }
                    if (datas[2][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[2][1].ToString());
                    }
                }
                else if (datas.Count == 4)
                {
                    if (datas[0][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[0][1].ToString());
                        return;
                    }
                    if (datas[1][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[1][1].ToString());
                        return;
                    }
                    if (datas[2][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[2][1].ToString());
                        return;
                    }
                    if (datas[3][0].ToString() == "YT")
                    {
                        CustomUrlOpener.Open(datas[3][1].ToString());
                    }
                }
                break;
        }
    }
}
