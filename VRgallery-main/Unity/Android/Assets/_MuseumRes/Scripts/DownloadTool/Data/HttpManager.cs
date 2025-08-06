using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using LitJson;
using yoyohan;
using static DataJsonClass;
using UnityEngine.SceneManagement;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public enum HttpState
{
    None,
    GetAppVersionsWebRequest,//1.获取app版本网络环境
    GetAppVersionsWeb,//2.获取app版本post网络环境
    JudgeUpdate,//3.判断更新
    JudgeUpdateAPKFile,//判断更新apk文件是否存在
    DownloadUpdateAPK,//下载更新的apk
    GetDataListWebRequest,//获取列表网络环境 查看需要下载的文件大小
    GetDataList,//获取列表网络环境 查看需要下载的文件大小
    GetGalleryListWebRequest,//4.获取展馆列表网络环境
    GetGalleryListWeb,//5.获取展馆列表post网络环境
    GetRoomDataWebRequest,//6.获取房间列表网络环境
    GetRoomDataWeb,//7.获取房间列表post网络环境
    GetRoomDataLocal,
    GetRoomPMDataWebRequest,//8.获取房间资源网络环境
    GetRoomPMDataWeb,//9.获取房间资源post网络环境
    GetRoomPMDataLocal,
    GetLocalData,//10.获取本地房间信息
    DownloadRoomData,//11.下载房间数据
    DownloadRoomPMData,
    DownloadCheckoutData,
    SendRegisterCode,
    Register,
    Login,//12.登录界面
    UpdateUserInfo,
    GetUserInfo,
    ResetPwd,
    SendForgetPwdCode,
    ResetPwdCode,
    DeleteUser


}


public class HttpManager : MonoBehaviour
{
    private static HttpManager _instance;
    public static HttpManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(HttpManager)) as HttpManager;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    //obj.hideFlags = HideFlags.DontSave;  
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = (HttpManager)obj.AddComponent(typeof(HttpManager));
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// 当前网络状态
    /// </summary>
    public HttpState httpState;
    /// <summary>
    /// 循环获取网络间隔时间
    /// </summary>
    public int loopGetNetworkIntervalTime;
    /// <summary>
    /// 是否循环获取网络
    /// </summary>
    public bool isLoopGetNetwork;
    /// <summary>
    /// 计时器
    /// </summary>
    private float timer;
    /// <summary>
    /// 是否根据app id获取展馆列表，如果为true，则选择场景的时候不需要从服务器再次获取
    /// </summary>
    public bool isGetMuseumList;

    public int currentGetRoomAllExhibitsDataIndex;

    public currentDownloadState currentDownloadState;

    public PaintingModuleDataManager PMDM;

    void Awake()
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

        isGetMuseumList = false;
        isLoopGetNetwork = false;
        timer = 0;
        httpState = HttpState.None;
        currentGetRoomAllExhibitsDataIndex = 1;
        PMDM = PaintingModuleDataManager.Instance;
    }
    public void Start()
    {
        StartCoroutine(IStart());
    }

    public IEnumerator IStart()
    {
        yield return new WaitForSeconds(1f);

        FirstEnterApp();
    }

    public void Update()
    {
        if (isLoopGetNetwork)
        {
            timer += Time.deltaTime;
            if (timer >= loopGetNetworkIntervalTime)
            {
                GetNetworkState();
                timer = 0;
            }

        }
    }

    /// <summary>
    /// 0.首次进入app检测本地数据
    /// </summary>
    public void FirstEnterApp()
    {
        httpState = HttpState.GetAppVersionsWebRequest;

        GetAppVersions();
    }

    /// <summary>
    /// 1.获取APP版本信息
    /// </summary>
    public void GetAppVersions()
    {
        if (GetNetworkState())
        {
            Debug.Log("网络连接成功，继续获取APP版本信息");

            NetWorkUpdate(NetTypeListen.AppIDGetAppDataURL, null);
        }
        else
        {
            Debug.Log("网络连接失败，弹窗警告重连");

            PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.HttpError);
        }
    }
    /// <summary>
    /// 2.获取app信息
    /// </summary>
    public void GetAppData(AppData results)
    {
        PMDM.appName = results.name;
        PMDM.appDescription = results.description;
        PMDM.appAndroidName = results.appAndroidName;
        PMDM.appAndroidStoreUrl = results.appAndroidStoreUrl;
        PMDM.appAndroidUrl = results.appAndroidUrl;
        PMDM.appAndroidVersion = results.appAndroidVersion;
        PMDM.appIosName = results.appIosName;
        PMDM.appIosStoreUrl = results.appIosStoreUrl;
        PMDM.appIosUrl = results.appIosUrl;
        PMDM.appIosVersion = results.appIosVersion;
        PMDM.appWinName = results.appName;
        PMDM.appWinStoreUrl = results.appStoreUrl;
        PMDM.appWinUrl = results.appUrl;
        PMDM.appWinVersion = results.appVersion;

        httpState = HttpState.JudgeUpdate;
        //判断是否需要更新
        JudgeUpdate();
    }

    /// <summary>
    /// 3.判断更新
    /// </summary>
    public void JudgeUpdate()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Debug.Log("Win平台 or UnityEditor");
        Debug.Log("appWinVersion=" + PMDM.appWinVersion);
        Debug.Log("appCurrentWinVersions=" + PMDM.appCurrentWinVersions);
        string winCurrentVersions = "";
        winCurrentVersions = PMDM.appCurrentWinVersions.Replace(".", "");
        winCurrentVersions = winCurrentVersions.Replace("v", "");
        int intWinCurrentVersions = 0;
        int.TryParse(winCurrentVersions, out intWinCurrentVersions);
        Debug.Log("BB=" + intWinCurrentVersions);

        string winVersions = "";
        winVersions = PMDM.appWinVersion.Replace(".", "");
        winVersions = winVersions.Replace("v", "");
        int intWinVersions = 0;
        int.TryParse(winVersions, out intWinVersions);
        Debug.Log("BB=" + intWinVersions);

        if (intWinCurrentVersions < intWinVersions)
        {
            Debug.Log("当前版本跟服务器版本不相同，需要更新");
            GetWinAppVersionsEvent();
        }
        else
        {
            Debug.Log("当前版本跟服务器版本相同，进入到登录界面");
            JumpLoginInterface();
        }
#elif UNITY_IOS
        Debug.Log("IOS平台");
        Debug.Log("appIosVersion=" + PMDM.appIosVersion);
        Debug.Log("appCurrentIosVersions=" + PMDM.appCurrentIosVersions);
        string iosCurrentVersions = "";
        iosCurrentVersions = PMDM.appCurrentIosVersions.Replace(".", "");
        iosCurrentVersions = iosCurrentVersions.Replace("v", "");
        int intIosCurrentVersions = 0;
        int.TryParse(iosCurrentVersions, out intIosCurrentVersions);
        Debug.Log("BB=" + intIosCurrentVersions);

        string iosVersions = "";
        iosVersions = PMDM.appIosVersion.Replace(".", "");
        iosVersions = iosVersions.Replace("v", "");
        int intIosVersions = 0;
        int.TryParse(iosVersions, out intIosVersions);
        Debug.Log("BB=" + intIosVersions);
        if (intIosCurrentVersions < intIosVersions)
        {
            Debug.Log("当前版本跟服务器版本不相同，需要更新");
            GetIosAppVersionsEvent();
        }
        else
        {
            Debug.Log("当前版本跟服务器版本相同，进去资源检查");
            JumpLoginInterface();
        }
#elif UNITY_ANDROID
        Debug.Log("ANDROID平台");
        Debug.Log("appAndroidVersion=" + PMDM.appAndroidVersion);
        Debug.Log("appCurrentAndroidVersions=" + PMDM.appCurrentAndroidVersions);
        string androidCurrentVersions = "";
        androidCurrentVersions = PMDM.appCurrentAndroidVersions.Replace(".", "");
        androidCurrentVersions = androidCurrentVersions.Replace("v", "");
        int intAndroidCurrentVersions = 0;
        int.TryParse(androidCurrentVersions, out intAndroidCurrentVersions);
        Debug.Log("BB=" + intAndroidCurrentVersions);

        string androidVersions = "";
        androidVersions = PMDM.appAndroidVersion.Replace(".", "");
        androidVersions = androidVersions.Replace("v", "");
        int intAndroidVersions = 0;
        int.TryParse(androidVersions, out intAndroidVersions);
        Debug.Log("BB=" + intAndroidVersions);
        if (intAndroidCurrentVersions < intAndroidVersions)
        {
            Debug.Log("当前版本跟服务器版本不相同，需要更新");
            GetAndroidAppVersionsEvent(); 
        }
        else
        {
            Debug.Log("当前版本跟服务器版本相同，进去资源检查");
            JumpLoginInterface();
        }
#endif

    }

    /// <summary>
    /// 4.获取到Win app版本事件
    /// </summary>
    public void GetWinAppVersionsEvent()
    {
        //弹窗点击更新
        //ios：跳转商店
        //android：1、先判断路径下有没有新apk。2、没有的话下载apk，有的话直接调用安装。
        //pc：下载（暂时不做）
        //GetDownloadProgress.Instance.DownloadApp();
    }
    /// <summary>
    /// 4.获取到Android app版本事件
    /// </summary>
    public void GetAndroidAppVersionsEvent()
    {
        //弹窗点击更新
        //ios：跳转商店
        //android：1、先判断路径下有没有新apk。2、没有的话下载apk，有的话直接调用安装。
        //pc：下载（暂时不做）
        //GetDownloadProgress.Instance.DownloadApp();
        if (!Directory.Exists(PMDM.url + "/Update/"))
        {
            Directory.CreateDirectory(PMDM.url + "/Update/");
        }

        if (File.Exists(PMDM.url + "/Update/" + PMDM.appAndroidName))
        {
            Debug.Log("检测到文件，准备更新");
            UnityDownloadMgr.instance.ClickInstall();
        }
        else
        {
            Debug.Log("检测不到文件，准备下载");
            PMDM.DeleteFiles(Application.persistentDataPath + "/1" + "/Update");
            Directory.CreateDirectory(PMDM.url + "/Update/");
            UnityDownloadMgr.instance.StartDownloaderUpdate(PMDM.appAndroidUrl, PMDM.appAndroidName);
        }
    }
    /// <summary>
    /// 4.获取到Ios app版本事件
    /// </summary>
    public void GetIosAppVersionsEvent()
    {
        //弹窗点击更新
        //ios：跳转商店
        //android：1、先判断路径下有没有新apk。2、没有的话下载apk，有的话直接调用安装。
        //pc：下载（暂时不做）
        //GetDownloadProgress.Instance.DownloadApp();
        PopupWindowCanvasManager.Instance.UpdateCanvasGroupOpenEvent();
    }
    /// <summary>
    /// 5.跳转登录界面
    /// </summary>
    public void JumpLoginInterface()
    {
        httpState = HttpState.Login;
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// 6.获取网络所有展馆信息
    /// </summary>
    /// <param name="results"></param>
    public void GetHttpAllGalleryData(List<AllGalleryResult> results)
    {
        Debug.Log("AllGalleryResult.Count = " + results.Count);
        AllGalleryResult[] result = new AllGalleryResult[results.Count];
        for (int i = 0; i < results.Count; i++)
        {
            result[i] = new AllGalleryResult();
            result[i].id = results[i].id;
            result[i].name = results[i].name;
            result[i].contactName = results[i].contactName;
            result[i].contactMobile = results[i].contactMobile;
            result[i].showAppId = results[i].showAppId;
            result[i].createBy = results[i].createBy;
            result[i].createTime = results[i].createTime;
            result[i].updateBy = results[i].updateBy;
            result[i].updateTime = results[i].updateTime;
            PaintingModuleDataManager.Instance.allGalleryResult.Add(result[i]);
        }
        DetectionRoomData();
        //DetectionDataList();
    }

    /// <summary>
    /// 检测需要下载的文件
    /// </summary>
    public void DetectionDataList()
    {
        httpState = HttpState.GetDataListWebRequest;

        GetDataList();

    }


    /// <summary>
    /// 检测展馆列表
    /// </summary>
    public void DetectionGalleryList()
    {
        httpState = HttpState.GetGalleryListWebRequest;

        GetGalleryList();

        PMDM.DeleteFiles(Application.persistentDataPath + "/1" + "/Update");
    }

    /// <summary>
    /// 检测房间信息
    /// </summary>
    public void DetectionRoomData()
    {
        httpState = HttpState.GetRoomDataWebRequest;

        GetHttpRoomData();
    }

    /// <summary>
    /// 检测房间画作信息
    /// </summary>
    public void DetectionRoomPMData()
    {
        httpState = HttpState.GetRoomPMDataWebRequest;

        GetHttpRoomPMData();
    }



    /// <summary>
    /// 获取文件列表
    /// </summary>
    public void GetDataList()
    {
        if (GetNetworkState())
        {
            Debug.Log("网络连接成功，继续获取文件列表");

            NetWorkUpdate(NetTypeListen.AppIDGetDataListURL, null);
        }
        else
        {
            Debug.Log("网络连接失败，弹窗警告重连");

            PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.HttpError);
        }
    }

    /// <summary>
    /// 获取展馆列表
    /// </summary>
    public void GetGalleryList()
    {
        if (GetNetworkState())
        {
            Debug.Log("网络连接成功，继续获取展馆列表");

            NetWorkUpdate(NetTypeListen.AppIDGetGalleryListURL, null);
        }
        else
        {
            Debug.Log("网络连接失败，弹窗警告重连");

            PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.HttpError);
        }
    }

    /// <summary>
    /// 获取网络房间信息
    /// </summary>
    public void GetHttpRoomData()
    {
        if (GetNetworkState())
        {
            Debug.Log("网络连接成功，继续获取服务器房间信息");

            NetWorkUpdate(NetTypeListen.GalleryIDGetAllRoomURL, null);
        }
        else
        {
            Debug.Log("网络连接失败，弹窗警告重连");

            PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.HttpError);
        }
    }
    /// <summary>
    /// 获取网络房间资源信息
    /// </summary>
    public void GetHttpRoomPMData()
    {
        if (GetNetworkState())
        {
            Debug.Log("网络连接成功，继续获取服务器房间画作信息");

            NetWorkUpdate(NetTypeListen.GetRoomAllExhibitsData, null);
        }
        else
        {
            Debug.Log("网络连接失败，弹窗警告重连");

            PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
        }
    }
    /// <summary>
    /// 获取网络房间资源信息大小
    /// </summary>
    public void GetHttpRoomPMDataSize()
    {
        if (GetNetworkState())
        {
            Debug.Log("网络连接成功，继续获取服务器房间画作信息");

            NetWorkUpdate(NetTypeListen.GetRoomAllExhibitsDataSize, null);
        }
        else
        {
            Debug.Log("网络连接失败，弹窗警告重连");

            PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
        }
    }

    public void NetWorkUpdate(NetTypeListen NetType, Action[] SuccessAction = null)
    {
        switch (NetType)
        {
            case NetTypeListen.AppIDGetAppDataURL:
                Debug.Log("根据APPID获取APP信息");
                httpState = HttpState.GetAppVersionsWeb;
                string appidAppData = "appId=" + PMDM.appID;
                BackGetAppData appData = JsonMapper.ToObject<BackGetAppData>(PostWebRequest(DefUrl + AppIDGetAppDataURL, appidAppData));
                if (appData == null)
                {
                    Debug.Log("Post：获取APP信息连接超时");
                    PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                }
                else
                {
                    if (appData.success)
                    {
                        Debug.Log("Post：获取APP信息成功");
                        if (appData.result != null)
                        {
                            //获取房间信息准备下载！！！
                            Debug.Log(appData.result);
                            GetAppData(appData.result);

                            //NetWorkUpdate(NetTypeListen.GetRegionInfoURL, null);
                        }
                        else
                        {
                            Debug.Log("appData.result为空！");
                        }
                    }
                    else
                    {
                        Debug.Log("Post：获取APP信息失败");

                        PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                    }
                }
                //1456837915288096769
                break;
            case NetTypeListen.AppIDGetGalleryListURL:
                Debug.Log("根据APPID获取展馆列表");
                httpState = HttpState.GetGalleryListWeb;
                string appidGalleryList = "appId=" + PMDM.appID;
                BackGalleryAllExhibitsData AllExhibitsData = JsonMapper.ToObject<BackGalleryAllExhibitsData>(PostWebRequest(DefUrl + AppIDGetGalleryListURL, appidGalleryList));
                if (AllExhibitsData == null)
                {
                    Debug.Log("Post：获取展馆列表连接超时");
                    PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                }
                else
                {
                    if (AllExhibitsData.success)
                    {
                        Debug.Log("Post：获取获取展馆列表成功");
                        if (AllExhibitsData.result != null)
                        {
                            Debug.Log("获取展馆列表数量为：" + AllExhibitsData.result.Count);

                            PMDM.allGalleryListIdInfo = new List<GalleryListIdInfo>();

                            for (int i = 0; i < AllExhibitsData.result.Count; i++)
                            {
                                //获取房间信息准备下载！！！
                                GalleryListIdInfo info = new GalleryListIdInfo();
                                info.GalleryId = AllExhibitsData.result[i].id;
                                info.GalleryName = AllExhibitsData.result[i].name;
                                PMDM.allGalleryListIdInfo.Add(info);
                                Debug.LogFormat("展馆ID为：{0}，展馆名称为：{1},所属APPID为：{2}", AllExhibitsData.result[i].id, AllExhibitsData.result[i].name, AllExhibitsData.result[i].showAppId);
                            }
                            Debug.Log(AllExhibitsData.result);
                            GetHttpAllGalleryData(AllExhibitsData.result);
                        }
                    }
                    else
                    {
                        Debug.Log("Post：获取获取展馆列表失败");

                        PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                    }
                }
                break;
            case NetTypeListen.GalleryIDGetAllRoomURL:
                Debug.Log("根据展馆ID获取房间");
                httpState = HttpState.GetRoomDataWeb;
                PMDM.allRoomResults = new List<AllRoomResult>();
                for (int m = 0; m < PMDM.allGalleryResult.Count; m++)
                {
                    PMDM.loopGalleryID = PMDM.allGalleryResult[m].id;
                    string galleryIDGetRoom = "museumId=" + PMDM.loopGalleryID;
                    BackGetAllRoom allRoomData = JsonMapper.ToObject<BackGetAllRoom>(PostWebRequest(DefUrl + GalleryIDGetAllRoomURL, galleryIDGetRoom));
                    if (allRoomData == null)
                    {
                        Debug.Log("Post：获取房间信息连接超时");
                        PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                    }
                    else
                    {
                        if (allRoomData.success)
                        {
                            Debug.Log("Post：获取房间信息成功");
                            if (allRoomData.result != null)
                            {
                                Debug.Log("房间数量为：" + allRoomData.result.Count);
                                PMDM.allGalleryListIdInfo[m].RoomInfo = new RoomListIdInfo[allRoomData.result.Count];
                                for (int i = 0; i < allRoomData.result.Count; i++)
                                {
                                    //获取房间信息准备下载！！！
                                    PMDM.allGalleryListIdInfo[m].RoomInfo[i] = new RoomListIdInfo();
                                    PMDM.allGalleryListIdInfo[m].RoomInfo[i].RoomName = allRoomData.result[i].name;
                                    PMDM.allGalleryListIdInfo[m].RoomInfo[i].RoomId = allRoomData.result[i].id;
                                    Debug.LogFormat("房间ID为：{0}，房间Name为：{1}", allRoomData.result[i].id, allRoomData.result[i].name);
                                }

                                if (m == PMDM.allGalleryResult.Count - 1)
                                    GetHttpAllRoomDataEnd(allRoomData.result);
                                else
                                    GetHttpAllRoomData(allRoomData.result);

                                Debug.Log(allRoomData.result);


                            }
                        }
                        else
                        {
                            Debug.Log("Post：获取房间信息失败");

                            PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                        }
                    }
                }

                break;
            case NetTypeListen.GetRoomAllExhibitsData:
                Debug.Log("获取房间内所有展品信息");
                httpState = HttpState.GetRoomPMDataWeb;
                PaintingModuleDataManager data = PaintingModuleDataManager.Instance;

                //int index = currentGetRoomAllExhibitsDataIndex;
                //int count = data.allRoomResults.Count;

                string roomId = "roomId=" + PaintingModuleDataManager.Instance.currentSelectSceneData.sceneID;

                BackRoomAllExhibitsData allExhibitsData = JsonMapper.ToObject<BackRoomAllExhibitsData>(PostWebRequest(DefUrl + GetRoomAllExhibitsDataURL, roomId));
                if (allExhibitsData == null)
                {
                    Debug.Log("Post：获取所有房间内所有展品信息连接超时");
                    PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                }
                else
                {
                    if (allExhibitsData.success)
                    {
                        Debug.Log("Post：获取展览信息成功，房间为：" + roomId);

                        if (allExhibitsData.result != null)
                        {
                            Debug.Log("allExhibitsData不为空");
                            Debug.Log("code:=" + allExhibitsData.code);
                            Debug.Log("message:=" + allExhibitsData.message);
                            Debug.Log("success:=" + allExhibitsData.success);
                            Debug.Log("timestamp:=" + allExhibitsData.timestamp);
                            Debug.Log("result:=" + allExhibitsData.result);

                            GetHttpAllRoomExhibitsData(allExhibitsData.result);
                        }
                    }
                    else
                    {
                        Debug.Log("Post：获取展览信息失败");

                        PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);

                        return;
                    }
                }

                httpState = HttpState.GetLocalData;
                //PaintingModuleDataManager.Instance.GetRoomPMDataLocal();
                PaintingModuleDataManager.Instance.GetRoomDataLocal();
                break;

            case NetTypeListen.SendRegisterCodeURL:
                Debug.Log("注册发送验证码");
                httpState = HttpState.SendRegisterCode;
                string RegisterCode = "appId=" + PMDM.appID + "&" + "email=" + PMDM.email;
                Debug.Log(DefUrl + SendRegisterCodeURL);
                Debug.Log(RegisterCode);
                BackSendDataResult SendEmailResult = JsonMapper.ToObject<BackSendDataResult>(PostWebRequest(DefUrl + SendRegisterCodeURL, RegisterCode));
                if (SendEmailResult == null)
                {
                    Debug.Log("Post：网络连接超时");
                    AccountManager.Instance.PopupWindowOpenEvent(PopupWindowState.网络连接超时);
                }
                else
                {
                    if (SendEmailResult.success)
                    {
                        Debug.Log("Post：注册发送验证码成功");
                        Debug.Log("code:=" + SendEmailResult.code);
                        Debug.Log("message:=" + SendEmailResult.message);
                        Debug.Log("success:=" + SendEmailResult.success);
                        AccountManager.Instance.AccountRegisterButtonFasongEventSuccess();
                        AccountManager.Instance.PopupWindowOpenEvent(PopupWindowState.已发送);
                    }
                    else
                    {
                        Debug.Log("Post：注册发送验证码失败");
                        Debug.Log("code:=" + SendEmailResult.code);
                        Debug.Log("message:=" + SendEmailResult.message);
                        Debug.Log("success:=" + SendEmailResult.success);
                        AccountManager.Instance.PopupWindowZidingyiEvent(SendEmailResult.message);
                    }
                }
                break;
            case NetTypeListen.RegisterURL:
                Debug.Log("注册提交接口");
                httpState = HttpState.Register;
                string register = "appId=" + PMDM.appID + "&" + "appName=" + PMDM.appName + "&" + "code=" + PMDM.code + "&" + "email=" + PMDM.email +
                                  "&" + "pwd=" + PMDM.pwd/* + "&" + "sex=" + PMDM.sex + "&" + "years=" + PMDM.age + "&" + "areaId=" + PMDM.areaId*/;
                Debug.Log(register);
                BackRegisterDataResult RegisterResult =
                    JsonMapper.ToObject<BackRegisterDataResult>(PostWebRequest(DefUrl + RegisterURL, register));
                if (RegisterResult == null)
                {
                    Debug.Log("Post：网络连接超时");
                    AccountManager.Instance.PopupWindowOpenEvent(PopupWindowState.网络连接超时);
                }
                else
                {
                    if (RegisterResult.success)
                    {
                        Debug.Log("Post：注册提交接口成功");
                        Debug.Log("code:=" + RegisterResult.code);
                        Debug.Log("message:=" + RegisterResult.message);
                        Debug.Log("success:=" + RegisterResult.success);
                        Debug.Log("success:=" + RegisterResult.result);
                        if (!String.IsNullOrEmpty(RegisterResult.result))
                            PMDM.id = RegisterResult.result;
                        AccountManager.Instance.RegisterSuccessEvent();
                    }
                    else
                    {
                        Debug.Log("Post：注册提交接口失败");
                        Debug.Log("code:=" + RegisterResult.code);
                        Debug.Log("message:=" + RegisterResult.message);
                        Debug.Log("success:=" + RegisterResult.success);
                        AccountManager.Instance.PopupWindowZidingyiEvent(RegisterResult.message);
                    }
                }

                break;
            case NetTypeListen.LoginURL:
                Debug.Log("登录接口");
                httpState = HttpState.Login;
                string login = "appId=" + PMDM.appID + "&" + "email=" + PMDM.email + "&" + "pwd=" + PMDM.pwd;
                Debug.Log(login);
                BackLoginDataResult BackLoginDataResult = JsonMapper.ToObject<BackLoginDataResult>(PostWebRequest(DefUrl + LoginURL, login));
                if (BackLoginDataResult == null)
                {
                    Debug.Log("Post：网络连接超时");
                    AccountManager.Instance.PopupWindowOpenEvent(PopupWindowState.网络连接超时);
                }
                else
                {
                    if (BackLoginDataResult.success)
                    {
                        Debug.Log("Post：登录接口成功");
                        PMDM.id = BackLoginDataResult.result.id;


                        AccountManager.Instance.LoginSuccessEvent();
                        Debug.Log("code:=" + BackLoginDataResult.code);
                        Debug.Log("message:=" + BackLoginDataResult.message);
                        Debug.Log("success:=" + BackLoginDataResult.success);
                        Debug.Log("timestamp:=" + BackLoginDataResult.timestamp);
                        Debug.Log("result:=" + BackLoginDataResult.result);

                    }
                    else
                    {
                        Debug.Log("Post：登录接口失败");
                        AccountManager.Instance.PopupWindowZidingyiEvent(BackLoginDataResult.message);
                        Debug.Log("code:=" + BackLoginDataResult.code);
                        Debug.Log("message:=" + BackLoginDataResult.message);
                        Debug.Log("success:=" + BackLoginDataResult.success);
                        Debug.Log("timestamp:=" + BackLoginDataResult.timestamp);
                        Debug.Log("result:=" + BackLoginDataResult.result);
                    }
                }


                break;
            case NetTypeListen.UpdateUserInfoURL:
                Debug.Log("完善用户信息接口");
                httpState = HttpState.UpdateUserInfo;
                string userInfo = "id=" + PMDM.id + "&" + "address=" + PMDM.address + "&" + "age=" + PMDM.age
                     + "&" + "avatar=" + PMDM.avatar + "&" + "nickName=" + PMDM.nickName + "&" + "phone=" + PMDM.phone
                      + "&" + "realName=" + PMDM.realName + "&" + "sex=" + PMDM.sex;
                BackUpdateUserInfoResult backUpdateUserInfoResult = JsonMapper.ToObject<BackUpdateUserInfoResult>(PostWebRequest(DefUrl + UpdateUserInfoURL, userInfo));
                if (backUpdateUserInfoResult.success)
                {
                    Debug.Log("Post：完善用户信息接口成功");
                }
                else
                {
                    Debug.Log("Post：完善用户信息接口失败");
                    AccountManager.Instance.PopupWindowZidingyiEvent(backUpdateUserInfoResult.message);
                }
                break;
            case NetTypeListen.GetUserInfoURL:
                Debug.Log("获取用户信息");
                httpState = HttpState.GetUserInfo;
                string userID = "id=" + PMDM.id;
                BackGetUserInfoResult backGetUserInfoResult = JsonMapper.ToObject<BackGetUserInfoResult>(PostWebRequest(DefUrl + GetUserInfoURL, userID));
                if (backGetUserInfoResult.success)
                {
                    Debug.Log("Post：获取用户信息成功");
                    if (backGetUserInfoResult.result != null)
                    {
                        Debug.Log("Result：获取用户信息");
                    }
                }
                else
                {
                    Debug.Log("Post：获取用户信息失败");

                    DetectionUI.Instance.DebugButtonOpen("Post：获取用户信息失败，请检测网络点击按钮重试");
                }
                break;
            case NetTypeListen.ResetPwdURL:
                Debug.Log("使用旧密码修改密码");
                httpState = HttpState.ResetPwd;
                string resetPwdData = "appId=" + PMDM.appID + "&" + "email=" + PMDM.email + "&" + "newPwd=" + PMDM.newPwd + "&" + "oldPwd=" + PMDM.oldPwd;
                BackResetPwdResult backResetPwdResult = JsonMapper.ToObject<BackResetPwdResult>(PostWebRequest(DefUrl + ResetPwdURL, resetPwdData));
                if (backResetPwdResult.success)
                {
                    Debug.Log("Post：使用旧密码修改密码成功");
                }
                else
                {
                    Debug.Log("Post：使用旧密码修改密码失败");

                    DetectionUI.Instance.DebugButtonOpen("Post：使用旧密码修改密码失败，请检测网络点击按钮重试");
                }
                break;
            case NetTypeListen.SendForgetPwdCodeURL:
                Debug.Log("忘记密码发送验证码");
                httpState = HttpState.SendForgetPwdCode;
                string sendForgetPwdCode = "appId=" + PMDM.appID + "&" + "email=" + PMDM.email;
                BackSendForgetPwdCodeResult backSendForgetPwdCodeResult = JsonMapper.ToObject<BackSendForgetPwdCodeResult>(PostWebRequest(DefUrl + SendForgetPwdCodeURL, sendForgetPwdCode));
                if (backSendForgetPwdCodeResult == null)
                {
                    Debug.Log("Post：网络连接超时");
                    AccountManager.Instance.PopupWindowOpenEvent(PopupWindowState.网络连接超时);
                }
                else
                {
                    if (backSendForgetPwdCodeResult.success)
                    {
                        Debug.Log("Post：忘记密码发送验证码成功");
                        Debug.Log("code:=" + backSendForgetPwdCodeResult.code);
                        Debug.Log("message:=" + backSendForgetPwdCodeResult.message);
                        Debug.Log("success:=" + backSendForgetPwdCodeResult.success);
                        AccountManager.Instance.ChangePasswordButtonFasongEventSuccess();
                        AccountManager.Instance.PopupWindowOpenEvent(PopupWindowState.已发送);
                    }
                    else
                    {
                        Debug.Log("Post：忘记密码发送验证码失败");
                        Debug.Log("code:=" + backSendForgetPwdCodeResult.code);
                        Debug.Log("message:=" + backSendForgetPwdCodeResult.message);
                        Debug.Log("success:=" + backSendForgetPwdCodeResult.success);
                        AccountManager.Instance.PopupWindowZidingyiEvent(backSendForgetPwdCodeResult.message);
                    }
                }


                break;
            case NetTypeListen.ForgetPwdURL:
                Debug.Log("使用旧密码修改密码");
                httpState = HttpState.ResetPwdCode;
                string resetPwdDataCode = "appId=" + PMDM.appID + "&" + "email=" + PMDM.email + "&" + "newPwd=" + PMDM.pwd + "&" + "code=" + PMDM.code;
                BackResetPwdDataCodeResult backResetPwdDataCodeResult = JsonMapper.ToObject<BackResetPwdDataCodeResult>(PostWebRequest(DefUrl + ForgetPwdURL, resetPwdDataCode));

                if (backResetPwdDataCodeResult == null)
                {
                    Debug.Log("Post：网络连接超时");
                    AccountManager.Instance.PopupWindowOpenEvent(PopupWindowState.网络连接超时);
                }
                else
                {
                    if (backResetPwdDataCodeResult.success)
                    {
                        Debug.Log("Post：使用旧密码修改密码成功");
                        Debug.Log("code:=" + backResetPwdDataCodeResult.code);
                        Debug.Log("message:=" + backResetPwdDataCodeResult.message);
                        Debug.Log("success:=" + backResetPwdDataCodeResult.success);
                        AccountManager.Instance.PopupWindowOpenEvent(PopupWindowState.密码修改成功);
                    }
                    else
                    {
                        Debug.Log("Post：使用旧密码修改密码失败");
                        Debug.Log("code:=" + backResetPwdDataCodeResult.code);
                        Debug.Log("message:=" + backResetPwdDataCodeResult.message);
                        Debug.Log("success:=" + backResetPwdDataCodeResult.success);
                        AccountManager.Instance.PopupWindowZidingyiEvent(backResetPwdDataCodeResult.message);
                    }
                }
                break;
            case NetTypeListen.YoukeLoginURL:
                Debug.Log("游客登录接口");
                string youkeInfo = "";
                BackYoukeCodeResult YoukeLoginResult =
                    JsonMapper.ToObject<BackYoukeCodeResult>(PostWebRequest(DefUrl + YoukeLoginURL, youkeInfo));
                if (YoukeLoginResult == null)
                {
                    Debug.Log("Post：游客登录接口超时");
                }
                else
                {
                    if (YoukeLoginResult.success)
                    {
                        Debug.Log("Post：游客登录接口成功");
                        Debug.Log("code:=" + YoukeLoginResult.code);
                        Debug.Log("message:=" + YoukeLoginResult.message);
                        Debug.Log("success:=" + YoukeLoginResult.success);
                    }
                    else
                    {
                        Debug.Log("Post：游客登录接口失败");
                        Debug.Log("code:=" + YoukeLoginResult.code);
                        Debug.Log("message:=" + YoukeLoginResult.message);
                        Debug.Log("success:=" + YoukeLoginResult.success);
                    }
                }
                break;
            case NetTypeListen.DeleteUserByIdURL:
                Debug.Log("注销用户接口");
                httpState = HttpState.DeleteUser;
                string userDeleteID = "id=" + PMDM.id;
                BackDeleteUserByIdCodeResult backDeleteUserByIdCodeResult = JsonMapper.ToObject<BackDeleteUserByIdCodeResult>(PostWebRequest(DefUrl + DeleteUserByIdURL, userDeleteID));
                if (backDeleteUserByIdCodeResult == null)
                {
                    Debug.Log("Post：网络连接超时");
                    PaintingModuleLockManager.Instance.PopupWindowOpenEvent(PopupWindowState.网络连接超时);
                }
                else
                {
                    if (backDeleteUserByIdCodeResult.success)
                    {
                        Debug.Log("Post：注册发送验证码成功");
                        Debug.Log("code:=" + backDeleteUserByIdCodeResult.code);
                        Debug.Log("message:=" + backDeleteUserByIdCodeResult.message);
                        Debug.Log("success:=" + backDeleteUserByIdCodeResult.success);
                        PMDM.LogOut();
                        PlayerPrefs.SetString("UserName", "");
                        PlayerPrefs.SetString("Password", "");
                        PlayerPrefs.SetString("IsRemember", "false");
                        PMDM.PlayerPrefs_UserName = "";
                        PMDM.PlayerPrefs_Password = "";
                        PMDM.PlayerPrefs_IsRemember = "false";
                        PMDM.email = "";
                        PMDM.pwd = "";
                        PMDM.loginLogID = "";
                        //PMDM.age = -1;
                        //PMDM.sex = -1;
                        //PMDM.areaId = -1;
                        //PMDM.area = "";
                    }
                    else
                    {
                        Debug.Log("Post：注册发送验证码失败");
                        Debug.Log("code:=" + backDeleteUserByIdCodeResult.code);
                        Debug.Log("message:=" + backDeleteUserByIdCodeResult.message);
                        Debug.Log("success:=" + backDeleteUserByIdCodeResult.success);
                        PaintingModuleLockManager.Instance.PopupWindowZidingyiEvent(backDeleteUserByIdCodeResult.message);
                    }
                }
                break;
            case NetTypeListen.YoukeSendRegisterCodeURL:
                Debug.Log("游客注册发送验证码");
                httpState = HttpState.SendRegisterCode;
                string YoukeRegisterCode = "appId=" + PMDM.appID + "&" + "email=" + PMDM.email;
                Debug.Log(DefUrl + SendRegisterCodeURL);
                Debug.Log(YoukeRegisterCode);
                BackSendDataResult YoukeSendEmailResult = JsonMapper.ToObject<BackSendDataResult>(PostWebRequest(DefUrl + SendRegisterCodeURL, YoukeRegisterCode));
                if (YoukeSendEmailResult == null)
                {
                    Debug.Log("Post：网络连接超时");
                    PaintingModuleLockManager.Instance.PopupWindowOpenEvent(PopupWindowState.网络连接超时);
                }
                else
                {
                    if (YoukeSendEmailResult.success)
                    {
                        Debug.Log("Post：注册发送验证码成功");
                        Debug.Log("code:=" + YoukeSendEmailResult.code);
                        Debug.Log("message:=" + YoukeSendEmailResult.message);
                        Debug.Log("success:=" + YoukeSendEmailResult.success);
                        PaintingModuleLockManager.Instance.AccountRegisterButtonFasongEventSuccess();
                        PaintingModuleLockManager.Instance.PopupWindowOpenEvent(PopupWindowState.已发送);
                    }
                    else
                    {
                        Debug.Log("Post：注册发送验证码失败");
                        Debug.Log("code:=" + YoukeSendEmailResult.code);
                        Debug.Log("message:=" + YoukeSendEmailResult.message);
                        Debug.Log("success:=" + YoukeSendEmailResult.success);
                        PaintingModuleLockManager.Instance.PopupWindowZidingyiEvent(YoukeSendEmailResult.message);
                    }
                }
                break;
            case NetTypeListen.YoukeRegisterURL:
                Debug.Log("游客注册提交接口");
                httpState = HttpState.Register;
                string YoukeRegister = "appId=" + PMDM.appID + "&" + "appName=" + PMDM.appName + "&" + "code=" + PMDM.code + "&" + "email=" + PMDM.email +
                                  "&" + "pwd=" + PMDM.pwd;
                BackRegisterDataResult YoukeRegisterResult =
                    JsonMapper.ToObject<BackRegisterDataResult>(PostWebRequest(DefUrl + RegisterURL, YoukeRegister));
                if (YoukeRegisterResult == null)
                {
                    Debug.Log("Post：网络连接超时");
                    PaintingModuleLockManager.Instance.PopupWindowOpenEvent(PopupWindowState.网络连接超时);
                }
                else
                {
                    if (YoukeRegisterResult.success)
                    {
                        Debug.Log("Post：注册提交接口成功");
                        Debug.Log("code:=" + YoukeRegisterResult.code);
                        Debug.Log("message:=" + YoukeRegisterResult.message);
                        Debug.Log("success:=" + YoukeRegisterResult.success);
                        PaintingModuleLockManager.Instance.RegisterSuccessEvent();
                    }
                    else
                    {
                        Debug.Log("Post：注册提交接口失败");
                        Debug.Log("code:=" + YoukeRegisterResult.code);
                        Debug.Log("message:=" + YoukeRegisterResult.message);
                        Debug.Log("success:=" + YoukeRegisterResult.success);
                        PaintingModuleLockManager.Instance.PopupWindowZidingyiEvent(YoukeRegisterResult.message);
                    }
                }
                break;

            case NetTypeListen.AppIDGetDataListURL:
                Debug.Log("根据展馆ID获取房间");
                PMDM.allRoomResults = new List<AllRoomResult>();
                for (int m = 0; m < PMDM.allGalleryResult.Count; m++)
                {
                    PMDM.loopGalleryID = PMDM.allGalleryResult[m].id;
                    string galleryIDGetRoom = "museumId=" + PMDM.loopGalleryID;
                    BackGetAllRoom allRoomData = JsonMapper.ToObject<BackGetAllRoom>(PostWebRequest(DefUrl + GalleryIDGetAllRoomURL, galleryIDGetRoom));
                    if (allRoomData == null)
                    {
                        Debug.Log("Post：获取房间信息连接超时");
                        PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                    }
                    else
                    {
                        if (allRoomData.success)
                        {
                            Debug.Log("Post：获取房间信息成功");
                            if (allRoomData.result != null)
                            {
                                Debug.Log("房间数量为：" + allRoomData.result.Count);
                                for (int i = 0; i < allRoomData.result.Count; i++)
                                {
                                    //获取房间信息准备下载！！！
                                    Debug.LogFormat("房间ID为：{0}，房间Name为：{1}", allRoomData.result[i].id, allRoomData.result[i].name);
                                }
                                Debug.Log(allRoomData.result);

                                if (m == PMDM.allGalleryResult.Count - 1)
                                    GetHttpAllRoomDataSizeEnd(allRoomData.result);
                                else
                                    GetHttpAllRoomData(allRoomData.result);
                            }
                        }
                        else
                        {
                            Debug.Log("Post：获取房间信息失败");
                            PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                        }
                    }
                }
                break;
            case NetTypeListen.GetRoomAllExhibitsDataSize:
                Debug.Log("获取房间内所有展品信息");
                PaintingModuleDataManager data1 = PaintingModuleDataManager.Instance;
                PaintingModuleDataManager.Instance.roomAllExhibitsData = new List<List<RoomAllExhibitsData>>();
                int index1 = currentGetRoomAllExhibitsDataIndex;
                int count1 = data1.allRoomResults.Count;
                for (int i = index1; i <= count1; i++)
                {
                    //string roomId = "roomId=" + data1.allRoomResults[i - 1].id;
                    //BackRoomAllExhibitsData allExhibitsData = JsonMapper.ToObject<BackRoomAllExhibitsData>(PostWebRequest(DefUrl + GetRoomAllExhibitsDataURL, roomId));
                    //if (allExhibitsData == null)
                    //{
                    //    Debug.Log("Post：获取所有房间内所有展品信息连接超时");
                    //    PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);
                    //}
                    //else
                    //{
                    //    if (allExhibitsData.success)
                    //    {
                    //        Debug.Log("Post：获取展览信息成功，房间为：" + roomId);
                    //        currentGetRoomAllExhibitsDataIndex++;

                    //        if (allExhibitsData.result != null)
                    //        {
                    //            Debug.Log("allExhibitsData不为空");
                    //            Debug.Log("code:=" + allExhibitsData.code);
                    //            Debug.Log("message:=" + allExhibitsData.message);
                    //            Debug.Log("success:=" + allExhibitsData.success);
                    //            Debug.Log("timestamp:=" + allExhibitsData.timestamp);
                    //            Debug.Log("result:=" + allExhibitsData.result);

                    //            //JsonData datas;
                    //            //datas = JsonMapper.ToObject(allExhibitsData.result[0].webLink);
                    //            //Debug.Log("ceshiyixia:" + datas[0][0]);
                    //            //Debug.Log("ceshiyixia:" + datas[0][1]);
                    //            GetHttpAllRoomExhibitsData(allExhibitsData.result);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Debug.Log("Post：获取展览信息失败");

                    //        PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.ServerError);

                    //        return;
                    //    }
                    //}
                }
                PaintingModuleDataManager.Instance.GetRoomPMDataSizeLocal();
                break;

            case NetTypeListen.GetRegionInfoURL:
                Debug.Log("获取地区信息接口");
                string regionInfo = "";
                BackRegionInfoByIdCodeResult RegionInfoResult =
                    JsonMapper.ToObject<BackRegionInfoByIdCodeResult>(PostWebRequest(DefUrl + GetRegionInfoURL, regionInfo));
                if (RegionInfoResult == null)
                {
                    Debug.Log("Post：获取地区信息接口超时");
                }
                else
                {
                    if (RegionInfoResult.success)
                    {
                        Debug.Log("Post：获取地区信息接口成功");
                        Debug.Log("code:=" + RegionInfoResult.code);
                        Debug.Log("message:=" + RegionInfoResult.message);
                        Debug.Log("success:=" + RegionInfoResult.success);
                        Debug.Log("result:=" + RegionInfoResult.result);
                        PaintingModuleDataManager.Instance.allRegionInfoResults = new List<GetRegionInfoResult>();
                        foreach (var item in RegionInfoResult.result)
                        {
                            PaintingModuleDataManager.Instance.allRegionInfoResults.Add(item);
                        }
                    }
                    else
                    {
                        Debug.Log("Post：获取地区信息接口失败");
                        Debug.Log("code:=" + RegionInfoResult.code);
                        Debug.Log("message:=" + RegionInfoResult.message);
                        Debug.Log("success:=" + RegionInfoResult.success);
                        Debug.Log("result:=" + RegionInfoResult.result);
                    }
                }
                break;

            case NetTypeListen.UploadUserLoginLog:
                Debug.Log("上传用户登录日志");
                string UserLoginLog = "";
                UserLoginLog = "userId=" + PMDM.id + "&" + "roomId=" + PMDM.currentSelectSceneDataID + "&" + "museumId=" + PMDM.currentSelectGalleryID;
                Debug.Log("UserLoginLog=" + UserLoginLog);
                BackResultByUserLoginInfoCodeResult UserLoginInfoCodeResult =
                    JsonMapper.ToObject<BackResultByUserLoginInfoCodeResult>(PostWebRequest(DefUrl + UploadUserLoginLog, UserLoginLog));
                if (UserLoginInfoCodeResult == null)
                {
                    Debug.Log("Post：网络连接超时");
                }
                else
                {
                    if (UserLoginInfoCodeResult.success)
                    {
                        Debug.Log("Post：上传用户登录日志接口成功");
                        Debug.Log("code:=" + UserLoginInfoCodeResult.code);
                        Debug.Log("message:=" + UserLoginInfoCodeResult.message);
                        Debug.Log("success:=" + UserLoginInfoCodeResult.success);
                        Debug.Log("success:=" + UserLoginInfoCodeResult.result.ToString());
                        PMDM.loginLogID = UserLoginInfoCodeResult.result.ToString();
                    }
                    else
                    {
                        Debug.Log("Post：上传用户登录日志接口失败");
                        Debug.Log("code:=" + UserLoginInfoCodeResult.code);
                        Debug.Log("message:=" + UserLoginInfoCodeResult.message);
                        Debug.Log("success:=" + UserLoginInfoCodeResult.success);
                    }
                }
                break;

            case NetTypeListen.UploadUserClickLogs:
                Debug.Log("上传用户点击日志");
                string UserClickLog = "";
                int currentRoomIndex = 0;
                int currentPaintingModuleIndex = 0;
                int.TryParse(PMDM.currentPaintingModule.id, out currentPaintingModuleIndex);
                Debug.Log("PMDM" + PMDM.currentPaintingModule.id);
                if (currentPaintingModuleIndex != 0)
                    currentPaintingModuleIndex--;

                for (int i = 0; i < PMDM.roomAllExhibitsData.Count; i++)
                {
                    if (PMDM.roomAllExhibitsData[i][0].showRoomId == PMDM.currentSelectSceneData.sceneID)
                    {
                        currentRoomIndex = i;
                        break;
                    }
                }

                if (!String.IsNullOrEmpty(PMDM.loginLogID))
                {
                    UserClickLog = "loginLogId=" + PMDM.loginLogID + "&" + "exhibitsId="
    + PMDM.roomAllExhibitsData[currentRoomIndex][currentPaintingModuleIndex].id
    + "&" + "introductionCount=" + PMDM.introductionClickCount
    + "&" + "linkCount=" + PMDM.linkClickCount
    + "&" + "videoCount=" + PMDM.videoClickCount
    + "&" + "voiceCount=" + PMDM.voiceClickCount
    + "&" + "shopCount=" + PMDM.shopClickCount;

                    Debug.Log("PMDM" + PMDM.roomAllExhibitsData[currentRoomIndex][currentPaintingModuleIndex].name);
                    Debug.Log("UserClickLog=" + UserClickLog);
                    BackResultByUserClickInfoCodeResult UserClickInfoCodeResult =
                        JsonMapper.ToObject<BackResultByUserClickInfoCodeResult>(PostWebRequest(DefUrl + UploadUserClickLogs, UserClickLog));
                    if (UserClickInfoCodeResult == null)
                    {
                        Debug.Log("Post：网络连接超时");
                    }
                    else
                    {
                        if (UserClickInfoCodeResult.success)
                        {
                            Debug.Log("Post：上传用户点击日志接口成功");
                            Debug.Log("code:=" + UserClickInfoCodeResult.code);
                            Debug.Log("message:=" + UserClickInfoCodeResult.message);
                            Debug.Log("success:=" + UserClickInfoCodeResult.success);
                        }
                        else
                        {
                            Debug.Log("Post：上传用户点击日志接口失败");
                            Debug.Log("code:=" + UserClickInfoCodeResult.code);
                            Debug.Log("message:=" + UserClickInfoCodeResult.message);
                            Debug.Log("success:=" + UserClickInfoCodeResult.success);
                        }
                    }

                    PMDM.introductionClickCount = 0;
                    PMDM.linkClickCount = 0;
                    PMDM.videoClickCount = 0;
                    PMDM.voiceClickCount = 0;
                    PMDM.shopClickCount = 0;
                }
                else
                {
                    PMDM.introductionClickCount = 0;
                    PMDM.linkClickCount = 0;
                    PMDM.videoClickCount = 0;
                    PMDM.voiceClickCount = 0;
                    PMDM.shopClickCount = 0;
                }



                break;

        }

    }
    string language = "";

    private bool OnRemoteCertificateValidationCallback(
      System.Object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
        // Making an untrusted SSL certificate "OK"
        return true;
    }

    private string PostWebRequest(string postUrl, string paramData)
    {
        string ret = string.Empty;
        try
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnRemoteCertificateValidationCallback);

            byte[] byteArray = Encoding.UTF8.GetBytes(paramData); //转化
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.ContentLength = byteArray.Length;

            switch (PaintingModuleDataManager.Instance.language)
            {
                case Language.中文:
                    language = "zh-CN";
                    break;
                case Language.英语:
                    language = "en-US";
                    break;
                case Language.日文:
                    language = "ja-JP";
                    break;
            }
            Debug.Log(language);
            webReq.Headers.Add("local", language);
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
            ret = sr.ReadToEnd();
            //Debug.Log(ret);
            sr.Close();
            response.Close();
            newStream.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return ret;
    }

    public void SetHttpState(HttpState state)
    {
        httpState = state;
    }
    public bool GetNetworkState()
    {
        //当网络不可用时
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("无网络——环境");
            return false;
        }

        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {

            Debug.Log("wifi/网线——环境");
            return true;
        }

        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("运营商数据网络——环境");
            return true;
        }

        return false;
    }





    /// <summary>
    /// 获取所属APP展馆内网络所有房间信息
    /// </summary>
    /// <param name="results"></param>
    public void GetHttpAllRoomData(List<AllRoomResult> results)
    {
        Debug.Log("AllRoomResult.Count = " + results.Count);
        AllRoomResult[] result = new AllRoomResult[results.Count];
        for (int i = 0; i < results.Count; i++)
        {
            result[i] = new AllRoomResult();
            result[i].id = results[i].id;
            result[i].name = results[i].name;
            result[i].roomNo = results[i].roomNo;
            result[i].description = results[i].description;
            result[i].musicUrl = results[i].musicUrl;
            result[i].musicName = results[i].musicName;
            result[i].musicSize = results[i].musicSize;
            result[i].resourceAndroidUrl = results[i].resourceAndroidUrl;
            result[i].resourceAndroidName = results[i].resourceAndroidName;
            result[i].resourceAndroidSize = results[i].resourceAndroidSize;
            result[i].resourceIosUrl = results[i].resourceIosUrl;
            result[i].resourceIosName = results[i].resourceIosName;
            result[i].resourceIosSize = results[i].resourceIosSize;
            result[i].resourceUrl = results[i].resourceUrl;
            result[i].resourceName = results[i].resourceName;
            result[i].resourceSize = results[i].resourceSize;
            result[i].delFlag = results[i].delFlag;
            result[i].createBy = results[i].createBy;
            result[i].createTime = results[i].createTime;
            result[i].updateBy = results[i].updateBy;
            result[i].updateTime = results[i].updateTime;
            result[i].showMuseumId = results[i].showMuseumId;
            PaintingModuleDataManager.Instance.allRoomResults.Add(result[i]);
        }
    }

    /// <summary>
    /// 获取所属APP展馆内网络所有房间信息结束
    /// </summary>
    /// <param name="results"></param>
    public void GetHttpAllRoomDataEnd(List<AllRoomResult> results)
    {
        Debug.Log("AllRoomResult.Count = " + results.Count);
        AllRoomResult[] result = new AllRoomResult[results.Count];
        for (int i = 0; i < results.Count; i++)
        {
            result[i] = new AllRoomResult();
            result[i].id = results[i].id;
            result[i].name = results[i].name;
            result[i].roomNo = results[i].roomNo;
            result[i].description = results[i].description;
            result[i].musicUrl = results[i].musicUrl;
            result[i].musicName = results[i].musicName;
            result[i].musicSize = results[i].musicSize;
            result[i].resourceAndroidUrl = results[i].resourceAndroidUrl;
            result[i].resourceAndroidName = results[i].resourceAndroidName;
            result[i].resourceAndroidSize = results[i].resourceAndroidSize;
            result[i].resourceIosUrl = results[i].resourceIosUrl;
            result[i].resourceIosName = results[i].resourceIosName;
            result[i].resourceIosSize = results[i].resourceIosSize;
            result[i].resourceUrl = results[i].resourceUrl;
            result[i].resourceName = results[i].resourceName;
            result[i].resourceSize = results[i].resourceSize;
            result[i].delFlag = results[i].delFlag;
            result[i].createBy = results[i].createBy;
            result[i].createTime = results[i].createTime;
            result[i].updateBy = results[i].updateBy;
            result[i].updateTime = results[i].updateTime;
            result[i].showMuseumId = results[i].showMuseumId;
            PaintingModuleDataManager.Instance.allRoomResults.Add(result[i]);
        }

        PaintingModuleDataManager.Instance.roomAllExhibitsData = new List<List<RoomAllExhibitsData>>();
        //isGetMuseumList = true;
        //httpState = HttpState.GetRoomDataLocal;
        //PaintingModuleDataManager.Instance.GetRoomDataLocal();
        DetectionRoomPMData();
    }

    /// <summary>
    /// 获取所属APP展馆内网络所有房间文件大小结束
    /// </summary>
    /// <param name="results"></param>
    public void GetHttpAllRoomDataSizeEnd(List<AllRoomResult> results)
    {
        Debug.Log("AllRoomResult.Count = " + results.Count);
        AllRoomResult[] result = new AllRoomResult[results.Count];
        for (int i = 0; i < results.Count; i++)
        {
            result[i] = new AllRoomResult();
            result[i].id = results[i].id;
            result[i].name = results[i].name;
            result[i].roomNo = results[i].roomNo;
            result[i].description = results[i].description;
            result[i].musicUrl = results[i].musicUrl;
            result[i].musicName = results[i].musicName;
            result[i].musicSize = results[i].musicSize;
            result[i].resourceAndroidUrl = results[i].resourceAndroidUrl;
            result[i].resourceAndroidName = results[i].resourceAndroidName;
            result[i].resourceAndroidSize = results[i].resourceAndroidSize;
            result[i].resourceIosUrl = results[i].resourceIosUrl;
            result[i].resourceIosName = results[i].resourceIosName;
            result[i].resourceIosSize = results[i].resourceIosSize;
            result[i].resourceUrl = results[i].resourceUrl;
            result[i].resourceName = results[i].resourceName;
            result[i].resourceSize = results[i].resourceSize;
            result[i].delFlag = results[i].delFlag;
            result[i].createBy = results[i].createBy;
            result[i].createTime = results[i].createTime;
            result[i].updateBy = results[i].updateBy;
            result[i].updateTime = results[i].updateTime;
            result[i].showMuseumId = results[i].showMuseumId;
            PaintingModuleDataManager.Instance.allRoomResults.Add(result[i]);
        }
        //PaintingModuleDataManager.Instance.GetRoomDataSizeLocal();
        GetHttpRoomPMDataSize();
    }

    /// <summary>
    /// 获取网络所有画作信息
    /// </summary>
    /// <param name="exhibitsDatas"></param>
    public void GetHttpAllRoomExhibitsData(List<RoomAllExhibitsData> exhibitsDatas)
    {
        Debug.Log("exhibitsDatas.Count = " + exhibitsDatas.Count);
        RoomAllExhibitsData[] exhibitsData = new RoomAllExhibitsData[exhibitsDatas.Count];
        List<RoomAllExhibitsData> datas = new List<RoomAllExhibitsData>();

        for (int i = 0; i < exhibitsDatas.Count; i++)
        {
            exhibitsData[i] = new RoomAllExhibitsData();
            //Debug.Log("-------------------------------------------------------------");
            exhibitsData[i].id = exhibitsDatas[i].id;
            //Debug.Log("exhibitsDatas:id=" + exhibitsDatas[i].id);
            exhibitsData[i].showRoomId = exhibitsDatas[i].showRoomId;
            //Debug.Log("exhibitsDatas:showRoomId=" + exhibitsDatas[i].showRoomId);
            exhibitsData[i].exhibitsNo = exhibitsDatas[i].exhibitsNo;
            //Debug.Log("exhibitsDatas:exhibitsNo=" + exhibitsDatas[i].exhibitsNo);
            exhibitsData[i].name = exhibitsDatas[i].name;
            //Debug.Log("exhibitsDatas:name=" + exhibitsDatas[i].name);
            exhibitsData[i].status = exhibitsDatas[i].status;
            //Debug.Log("exhibitsDatas:status=" + exhibitsDatas[i].status);
            exhibitsData[i].auth = exhibitsDatas[i].auth;
            //Debug.Log("exhibitsDatas:auth=" + exhibitsDatas[i].auth);
            exhibitsData[i].text = exhibitsDatas[i].text;
            //Debug.Log("exhibitsDatas:text=" + exhibitsDatas[i].text);
            exhibitsData[i].mainGraphEncodeUrl = exhibitsDatas[i].mainGraphEncodeUrl;
            //Debug.Log("exhibitsDatas:mainGraphHttpUrl=" + exhibitsDatas[i].mainGraphUrl);
            exhibitsData[i].mainGraphName = exhibitsDatas[i].mainGraphName;
            //Debug.Log("exhibitsDatas:mainGraphName=" + exhibitsDatas[i].mainGraphName);
            exhibitsData[i].mainGraphSize = exhibitsDatas[i].mainGraphSize;
            //Debug.Log("exhibitsDatas:mainGraphSize=" + exhibitsDatas[i].mainGraphSize);
            exhibitsData[i].mainGraphTime = exhibitsDatas[i].mainGraphTime;
            //Debug.Log("exhibitsDatas:mainGraphTime=" + exhibitsDatas[i].mainGraphTime);
            exhibitsData[i].introductionImageEncodeUrl = exhibitsDatas[i].introductionImageEncodeUrl;
            //Debug.Log("exhibitsDatas:introductionImageHttpUrl=" + exhibitsDatas[i].introductionImageUrl);
            exhibitsData[i].introductionImageName = exhibitsDatas[i].introductionImageName;
            //Debug.Log("exhibitsDatas:introductionImageName=" + exhibitsDatas[i].introductionImageName);
            exhibitsData[i].introductionImageSize = exhibitsDatas[i].introductionImageSize;
            //Debug.Log("exhibitsDatas:introductionImaageSize=" + exhibitsDatas[i].introductionImageSize);
            exhibitsData[i].introductionImageTime = exhibitsDatas[i].introductionImageTime;
            //Debug.Log("exhibitsDatas:introductionImageTime=" + exhibitsDatas[i].introductionImageTime);
            exhibitsData[i].videoUrl = exhibitsDatas[i].videoUrl;
            //Debug.Log("exhibitsDatas:videoHttpUrl=" + exhibitsDatas[i].videoUrl);
            exhibitsData[i].videoName = exhibitsDatas[i].videoName;
            //Debug.Log("exhibitsDatas:videoName=" + exhibitsDatas[i].videoName);
            exhibitsData[i].videoSize = exhibitsDatas[i].videoSize;
            //Debug.Log("exhibitsDatas:videoSize=" + exhibitsDatas[i].videoSize);
            exhibitsData[i].videoTime = exhibitsDatas[i].videoTime;
            //Debug.Log("exhibitsDatas:videoTime=" + exhibitsDatas[i].videoTime);
            exhibitsData[i].voiceUrl = exhibitsDatas[i].voiceUrl;
            //Debug.Log("exhibitsDatas:voiceHttpUrl=" + exhibitsDatas[i].voiceUrl);
            exhibitsData[i].voiceName = exhibitsDatas[i].voiceName;
            //Debug.Log("exhibitsDatas:voiceName=" + exhibitsDatas[i].voiceName);
            exhibitsData[i].voiceSize = exhibitsDatas[i].voiceSize;
            //Debug.Log("exhibitsDatas:voiceSize=" + exhibitsDatas[i].voiceSize);
            exhibitsData[i].voiceTime = exhibitsDatas[i].voiceTime;
            //Debug.Log("exhibitsDatas:voiceTime=" + exhibitsDatas[i].voiceTime);
            exhibitsData[i].animationThumbnailEncodeUrl = exhibitsDatas[i].animationThumbnailEncodeUrl;
            //Debug.Log("exhibitsDatas:animationThumbnailHttpUrl=" + exhibitsDatas[i].animationThumbnailUrl);
            exhibitsData[i].animationThumbnailName = exhibitsDatas[i].animationThumbnailName;
            //Debug.Log("exhibitsDatas:animationThumbnailName=" + exhibitsDatas[i].animationThumbnailName);
            exhibitsData[i].animationThumbnailSize = exhibitsDatas[i].animationThumbnailSize;
            //Debug.Log("exhibitsDatas:animationThumbnailSize=" + exhibitsDatas[i].animationThumbnailSize);
            exhibitsData[i].animationThumbnailTime = exhibitsDatas[i].animationThumbnailTime;
            //Debug.Log("exhibitsDatas:animationThumbnailTime=" + exhibitsDatas[i].animationThumbnailTime);
            exhibitsData[i].frameAnimationEncodeUrl = exhibitsDatas[i].frameAnimationEncodeUrl;
            //Debug.Log("exhibitsDatas:frameAnimationHttpUrl=" + exhibitsDatas[i].frameAnimationUrl);
            exhibitsData[i].frameAnimationName = exhibitsDatas[i].frameAnimationName;
            //Debug.Log("exhibitsDatas:frameAnimationName=" + exhibitsDatas[i].frameAnimationName);
            exhibitsData[i].frameAnimationSize = exhibitsDatas[i].frameAnimationSize;
            //Debug.Log("exhibitsDatas:frameAnimationSize=" + exhibitsDatas[i].frameAnimationSize);
            exhibitsData[i].frameAnimationTime = exhibitsDatas[i].frameAnimationTime;
            //Debug.Log("exhibitsDatas:frameAnimationTime=" + exhibitsDatas[i].frameAnimationTime);
            exhibitsData[i].videoLink = exhibitsDatas[i].videoLink;
            //Debug.Log("exhibitsDatas:videoLink=" + exhibitsDatas[i].videoLink);
            exhibitsData[i].videoLinkTime = exhibitsDatas[i].videoLinkTime;
            //Debug.Log("exhibitsDatas:videoLinkTime=" + exhibitsDatas[i].videoLinkTime);
            exhibitsData[i].webLink = exhibitsDatas[i].webLink;
            //Debug.Log("exhibitsDatas:webLink=" + exhibitsDatas[i].webLink);
            exhibitsData[i].webLinkTime = exhibitsDatas[i].webLinkTime;
            //Debug.Log("exhibitsDatas:webLinkTime=" + exhibitsDatas[i].webLinkTime);
            exhibitsData[i].version = exhibitsDatas[i].version;
            //Debug.Log("exhibitsDatas:version=" + exhibitsDatas[i].version);
            exhibitsData[i].createBy = exhibitsDatas[i].createBy;
            //Debug.Log("exhibitsDatas:createBy=" + exhibitsDatas[i].createBy);
            exhibitsData[i].createTime = exhibitsDatas[i].createTime;
            //Debug.Log("exhibitsDatas:createTime=" + exhibitsDatas[i].createTime);
            exhibitsData[i].updateBy = exhibitsDatas[i].updateBy;
            //Debug.Log("exhibitsDatas:updateBy=" + exhibitsDatas[i].updateBy);
            exhibitsData[i].updateTime = exhibitsDatas[i].updateTime;
            //Debug.Log("exhibitsDatas:updateTime=" + exhibitsDatas[i].updateTime);
            exhibitsData[i].count = exhibitsDatas[i].count;
            //Debug.Log("exhibitsDatas:count=" + exhibitsDatas[i].count);
            //Debug.Log("-------------------------------------------------------------");
            datas.Add(exhibitsData[i]);
        }
        PaintingModuleDataManager.Instance.roomAllExhibitsData.Add(datas);
    }


}

public enum currentDownloadState
{
    None,
    Update,
    Scene,
    PaintingModule,
    Checkout,
    DownloadRoomData
}
