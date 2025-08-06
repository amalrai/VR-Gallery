using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using yoyohan;
using static DataJsonClass;

public class PaintingModuleDataManager : MonoBehaviour
{
    private static PaintingModuleDataManager _instance;
    public static PaintingModuleDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PaintingModuleDataManager)) as PaintingModuleDataManager;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    //obj.hideFlags = HideFlags.DontSave;  
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = (PaintingModuleDataManager)obj.AddComponent(typeof(PaintingModuleDataManager));
                }
            }
            return _instance;
        }
    }
    /// <summary>
    /// AppID
    /// </summary>
    public string appID = "";
    /// <summary>
    /// App名称
    /// </summary>
    public string appName = "";
    /// <summary>
    /// APP描述
    /// </summary>
    public string appDescription;
    /// <summary>
    /// 安卓安装包名称
    /// </summary>
    public string appAndroidName = "";
    /// <summary>
    /// 安卓商店地址
    /// </summary>
    public string appAndroidStoreUrl = "";
    /// <summary>
    /// 安卓安装包地址
    /// </summary>
    public string appAndroidUrl = "";
    /// <summary>
    /// 安卓安装包版本
    /// </summary>
    public string appAndroidVersion = "";
    /// <summary>
    /// 苹果安装包名称
    /// </summary>
    public string appIosName = "";
    /// <summary>
    /// 苹果商店地址
    /// </summary>
    public string appIosStoreUrl = "";
    /// <summary>
    /// 苹果安装包地址
    /// </summary>
    public string appIosUrl = "";
    /// <summary>
    /// 苹果安装包版本
    /// </summary>
    public string appIosVersion = "";
    /// <summary>
    /// Win安装包名称
    /// </summary>
    public string appWinName = "";
    /// <summary>
    /// Win商店地址
    /// </summary>
    public string appWinStoreUrl = "";
    /// <summary>
    /// Win安装包地址
    /// </summary>
    public string appWinUrl = "";
    /// <summary>
    /// Win安装包版本
    /// </summary>
    public string appWinVersion = "";
    /// <summary>
    /// Win App当前版本
    /// </summary>
    public string appCurrentWinVersions = "v1.0";
    /// <summary>
    /// 苹果 App当前版本
    /// </summary>
    public string appCurrentIosVersions = "v1.0";
    /// <summary>
    /// 安卓 App当前版本
    /// </summary>
    public string appCurrentAndroidVersions = "v1.0";
    /// <summary>
    /// 资源版本
    /// </summary>
    public string dataVersions = "v1.0";
    /// <summary>
    /// 平台
    /// </summary>
    public Platform platform;
    /// <summary>
    /// 语言
    /// </summary>
    public Language language;
    /// <summary>
    /// 弹窗状态
    /// </summary>
    public PopupWindowState popupState;
    ///// <summary>
    ///// 当前版本房间结果
    ///// </summary>
    //public List<string> currentVersionRoomResults;
    /// <summary>
    /// 所有展馆信息
    /// </summary>
    public List<AllGalleryResult> allGalleryResult;
    /// <summary>
    /// 所有房间信息
    /// </summary>
    public List<AllRoomResult> allRoomResults;
    /// <summary>
    /// 房间展览信息
    /// </summary>
    //public List<RoomAllExhibitsData> roomAllExhibitsData;
    public List<List<RoomAllExhibitsData>> roomAllExhibitsData;
    /// <summary>
    /// 需要下载的
    /// </summary>
    public List<DownloadObj> lackDownloadObj;
    /// <summary>
    /// 所有地址信息
    /// </summary>
    public List<GetRegionInfoResult> allRegionInfoResults;
    /// <summary>
    /// 所有展馆列表id信息
    /// </summary>
    public List<GalleryListIdInfo> allGalleryListIdInfo;
    /// <summary>
    /// 文件路径
    /// </summary>
    public string url;
    /// <summary>
    /// XML路径
    /// </summary>
    public string xmlURL;
    /// <summary>
    /// 本地XML
    /// </summary>
    public XmlDocument localXmlDoc;
    /// <summary>
    /// 场景路径
    /// </summary>
    public string sceneURL;
    /// <summary>
    /// 是否首次下载资源
    /// </summary>
    public bool firstDownload;
    /// <summary>
    /// 当前场景编号
    /// </summary>
    public int currentSceneIndex;
    /// <summary>
    /// 当前场景资源包
    /// </summary>
    public string currentSceneResourcesURL;
    /// <summary>
    /// 当前场景BGM
    /// </summary>
    public AudioClip currentSceneBGM;
    /// <summary>
    /// 需要下载的文件大小
    /// </summary>
    public float fileSize = 0;
    /// <summary>
    /// 是否获取到当前房间的下载资源
    /// </summary>
    public bool GetDownloadData;
    /// <summary>
    /// 当前选择的场景数据id
    /// </summary>
    public string currentSelectSceneDataID;
    /// <summary>
    /// 当前选择的展馆id
    /// </summary>
    public string currentSelectGalleryID;
    /// <summary>
    /// 当前选择的场景数据Name
    /// </summary>
    public string currentSelectSceneDataName;
    /// <summary>
    /// 当前选择的场景数据
    /// </summary>
    public SceneData currentSelectSceneData;
    /// <summary>
    /// 已经获取过的场景数据
    /// </summary>
    public SceneData getSceneData;
    /// <summary>
    /// 当前选择的场景下载遮挡
    /// </summary>
    public Image currentSelectSceneMask;

    //------------------------------------------登录注册信息-------------------------------------

    /// <summary>
    /// UserName：账号
    /// Password：密码
    /// IsRemember：是否记住
    /// </summary>
    public string PlayerPrefs_UserName;
    public string PlayerPrefs_Password;
    public string PlayerPrefs_IsRemember;
    public string PlayerPrefs_Language;
    public float PlayerPrefs_JoystickSpeed;
    public float PlayerPrefs_TouchPadSpeed;
    public string PlayerPrefs_BGM_Mute;

    /// <summary>
    /// 邮箱
    /// </summary>
    public string email;
    /// <summary>
    /// 密码
    /// </summary>
    public string pwd;
    /// <summary>
    /// 游客
    /// </summary>
    public bool isYouke;
    /// <summary>
    /// 是否修改密码
    /// </summary>
    public bool isChangePassword;
    /// <summary>
    /// 新密码
    /// </summary>
    public string newPwd;
    /// <summary>
    /// 旧密码
    /// </summary>
    public string oldPwd;
    /// <summary>
    /// 验证码
    /// </summary>
    public string code;
    /// <summary>
    /// 用户ID
    /// </summary>
    public string id;
    /// <summary>
    /// 收货地址
    /// </summary>
    public string address;
    /// <summary>
    /// 年龄
    /// </summary>
    public int age;
    /// <summary>
    /// 头像base64编码
    /// </summary>
    public string avatar;
    /// <summary>
    /// 昵称
    /// </summary>
    public string nickName;
    /// <summary>
    /// 手机号
    /// </summary>
    public string phone;
    /// <summary>
    /// 姓名
    /// </summary>
    public string realName;
    /// <summary>
    /// 性别
    /// </summary>
    public int sex;
    /// <summary>
    /// 地区
    /// </summary>
    public string area;
    /// <summary>
    /// 地区id
    /// </summary>
    public int areaId;
    /// <summary>
    /// 登录日志ID
    /// </summary>
    public string loginLogID;
    /// <summary>
    /// 是否有帧动画
    /// </summary>
    public bool isFrame;
    /// <summary>
    /// 循环展馆的id
    /// </summary>
    [HideInInspector]
    public string loopGalleryID = "";
    /// <summary>
    /// 简介点击次数
    /// </summary>
    public int introductionClickCount = 0;
    /// <summary>
    /// 链接点击次数
    /// </summary>
    public int linkClickCount = 0;
    /// <summary>
    /// 视频点击次数
    /// </summary>
    public int videoClickCount = 0;
    /// <summary>
    /// 音频点击次数
    /// </summary>
    public int voiceClickCount = 0;
    /// <summary>
    /// 商店点击次数
    /// </summary>
    public int shopClickCount = 0;


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
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
    }

    public void Start()
    {
        if (PlayerPrefs.HasKey("versions"))
        {
            Debug.Log(PlayerPrefs.GetString("versions"));
            if (PlayerPrefs.GetString("versions") == dataVersions)
            {

            }
            else
            {
                DeleteFiles(Application.persistentDataPath + "/1" + "/XML");
                DeleteFiles(Application.persistentDataPath + "/1" + "/Scene");
                PlayerPrefs.SetString("versions", dataVersions);
                Debug.Log(PlayerPrefs.GetString(dataVersions));
            }
        }
        else
        {
            Debug.Log("新版本");
            DeleteFiles(Application.persistentDataPath + "/1" + "/XML");
            DeleteFiles(Application.persistentDataPath + "/1" + "/Scene");
            PlayerPrefs.SetString("versions", dataVersions);
        }

        url = Application.persistentDataPath + "/1" + "/";
        xmlURL = url + "XML/" + "MuseumPaintingData.xml";
        sceneURL = url + "Scene/";

#if UNITY_IOS || UNITY_IPHONE
        Application.targetFrameRate = 60;
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
        Debug.Log("");
#endif

        language = Language.日文;
        popupState = PopupWindowState.None;
        DetectionPlayerPrefs();
        PopupWindowCanvasManager.Instance.SwitchLanguageEvent(language);
        //GetDownloadProgress.Instance.SwitchLanguageEvent(language);

    }
    /// <summary>
    /// 1.获取XML文件是否存在
    /// </summary>
    /// <returns></returns>
    public bool GetXMLFile()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/1" + "/XML"))
        {
            return false;
        }
        else
        {
            if (File.Exists(xmlURL))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    /// <summary>
    /// 2.创建XML文件
    /// </summary>
    public void CreateXML()
    {
        XmlDocument xml = new XmlDocument();
        XmlElement sceneResources = xml.CreateElement("SceneResources");
        xml.AppendChild(sceneResources);
        xml.Save(xmlURL);
        localXmlDoc = xml;
    }
    /// <summary>
    /// 3.场景路径是否存在
    /// </summary>
    public void ExistSceneURL()
    {
        if (!Directory.Exists(sceneURL))
        {
            Directory.CreateDirectory(sceneURL);
        }
    }
    /// <summary>
    /// 4.场景下载路径是否存在
    /// </summary>
    /// <param name="url"></param>
    public void ExistSceneDownloadURL(string url)
    {
        if (!Directory.Exists(sceneURL + url))
        {
            Debug.Log("创建路径：" + sceneURL + url);
            Directory.CreateDirectory(sceneURL + url);
        }
    }
    /// <summary>
    /// 5.首次添加场景资源XML信息
    /// </summary>
    public void FirstAddSceneXML(string datas)
    {
        //1,场景名
        //2,ResourcesName
        //3,ResourcesHttpURL(对比下载版本)
        //4,ResourcesSaveURL(保存的路径)

        string[] data = datas.Split(',');

        XmlElement sceneName = localXmlDoc.CreateElement("MuseumID" + "_" + data[7] + "_" + "RoomID" + "_" + data[0]);
        XmlElement bgm = localXmlDoc.CreateElement("BGM");
        XmlElement resources = localXmlDoc.CreateElement("Resources");
        XmlElement resourcesName = localXmlDoc.CreateElement("ResourcesName");
        XmlElement resourcesHttpURL = localXmlDoc.CreateElement("ResourcesHttpURL");
        XmlElement resourcesSaveURL = localXmlDoc.CreateElement("ResourcesSaveURL");
        XmlElement BGMName = localXmlDoc.CreateElement("BGMName");
        XmlElement BGMHttpURL = localXmlDoc.CreateElement("BGMHttpURL");
        XmlElement BGMSaveURL = localXmlDoc.CreateElement("BGMSaveURL");

        if (!string.IsNullOrEmpty(data[2]))
        {
            resourcesName.InnerText = data[1];
            resourcesHttpURL.InnerText = data[2];
            resourcesSaveURL.InnerText = data[3];
        }
        else
        {
            resourcesName.InnerText = " ";
            resourcesHttpURL.InnerText = " ";
            resourcesSaveURL.InnerText = " ";
        }

        if (!string.IsNullOrEmpty(data[5]))
        {
            BGMHttpURL.InnerText = data[5];
            BGMName.InnerText = data[4];
            BGMSaveURL.InnerText = data[6];
        }
        else
        {
            BGMHttpURL.InnerText = " ";
            BGMName.InnerText = " ";
            BGMSaveURL.InnerText = " ";
        }

        resources.AppendChild(resourcesName);
        resources.AppendChild(resourcesHttpURL);
        resources.AppendChild(resourcesSaveURL);
        bgm.AppendChild(BGMName);
        bgm.AppendChild(BGMHttpURL);
        bgm.AppendChild(BGMSaveURL);
        sceneName.AppendChild(resources);
        sceneName.AppendChild(bgm);
        localXmlDoc.ChildNodes[0].AppendChild(sceneName);

        SaveXML();
    }

    /// <summary>
    /// 6.首次下载并创建XML文件
    /// </summary>
    /// <param name="index">房间序号</param>
    public void FirstDownloadCreateXML(string strID)
    {
        //获取本地xml文件的场景列表
        if (!GetXMLFile())
        {
            //如果xml文件场景列表为空，则直接下载服务器所有场景
            firstDownload = true;
            CreateXML();
            ExistSceneURL();

            UnityDownloadMgr.instance.CreateXMLStartDownloader();

            DownloadObj[] download = new DownloadObj[2];

            string str = "";

            int index = 0;

            Debug.Log("strID:" + strID);

            if (allRoomResults != null && allRoomResults.Count != 0)
            {
                for (int i = 0; i < allRoomResults.Count; i++)
                {

                    Debug.Log("allRoomResults[i].id:" + allRoomResults[i].id + "_______" + "strID:" + strID);

                    if (allRoomResults[i].id == strID)
                    {
                        Debug.Log("allRoomResults[i].id:" + allRoomResults[i].id + "_______" + "strID:" + strID);
                        index = i;
                        break;
                    }
                }
            }

            string[] musicGet = allRoomResults[index].musicUrl.Split('/');
            //musicGet[musicGet.Length - 1]

            string[] resourceGet = allRoomResults[index].resourceUrl.Split('/');
            //resourceGet[resourceGet.Length - 1]

            string[] resourceAndroidGet = allRoomResults[index].resourceAndroidUrl.Split('/');
            //resourceAndroidGet[resourceAndroidGet.Length - 1]

            string[] resourceIosGet = allRoomResults[index].resourceIosUrl.Split('/');
            //resourceIosGet[resourceIosGet.Length - 1]

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            str = allRoomResults[index].id +
                  "," + resourceGet[resourceGet.Length - 1] +
                  "," + allRoomResults[index].resourceUrl +
                  "," + sceneURL + allRoomResults[index].id + "/Resources/" +
                  resourceGet[resourceGet.Length - 1] +
                  "," + musicGet[musicGet.Length - 1] +
                  "," + allRoomResults[index].musicUrl +
                  "," + sceneURL + allRoomResults[index].id + "/BGM/" +
                  musicGet[musicGet.Length - 1] +
                  "," + allRoomResults[index].showMuseumId;
#elif UNITY_IOS


                     str = allRoomResults[index].id +
                                 "," + resourceIosGet[resourceIosGet.Length - 1] +
                                 "," + allRoomResults[index].resourceIosUrl +
                                 "," + sceneURL + allRoomResults[index].id + "/Resources/" +
                                 resourceIosGet[resourceIosGet.Length - 1] +
                                 "," + musicGet[musicGet.Length - 1] +
                                 "," + allRoomResults[index].musicUrl +
                                 "," + sceneURL + allRoomResults[index].id + "/BGM/" +
                                 musicGet[musicGet.Length - 1] +
                  "," + allRoomResults[index].showMuseumId;
#elif UNITY_ANDROID
                     str = allRoomResults[index].id +
                                 "," + resourceAndroidGet[resourceAndroidGet.Length - 1] +
                                 "," + allRoomResults[index].resourceAndroidUrl +
                                 "," + sceneURL + allRoomResults[index].id + "/Resources/" +
                                 resourceAndroidGet[resourceAndroidGet.Length - 1] +
                                 "," + musicGet[musicGet.Length - 1] +
                                 "," + allRoomResults[index].musicUrl +
                                 "," + sceneURL + allRoomResults[index].id + "/BGM/" +
                                 musicGet[musicGet.Length - 1] +
                  "," + allRoomResults[index].showMuseumId;
#endif
            FirstAddSceneXML(str);

            ExistSceneDownloadURL(allRoomResults[index].id + "/");
            ExistSceneDownloadURL(allRoomResults[index].id + "/Resources");
            ExistSceneDownloadURL(allRoomResults[index].id + "/BGM");



#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (!string.IsNullOrEmpty(allRoomResults[index].resourceUrl))
            {
                string[] resourceGetWin = allRoomResults[index].resourceUrl.Split('/');
                //resourceGet[resourceGet.Length - 1]

                download[0] = new DownloadObj().SetUrl(allRoomResults[index].resourceUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/Resources").SetFileName(resourceGetWin[resourceGetWin.Length - 1]);
                Downloader.Instance.requestDownloadObj.Add(download[0]);
                Downloader.Instance.totalDownload++;
                //GetDownloadProgress.Instance.DownloadDataConvert(allRoomResults[i].resourceSize);

                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/Resources", allRoomResults[index].resourceSize, resourceGet[resourceGet.Length - 1]);

                Debug.Log("文件大小：" + allRoomResults[index].resourceSize.Substring(0, allRoomResults[index].resourceSize.Length - 1));
            }


#elif UNITY_IOS
                     if (!string.IsNullOrEmpty(allRoomResults[index].resourceIosUrl))
                    {
                        string[] resourceGetIos = allRoomResults[index].resourceIosUrl.Split('/');
                        //resourceGetIos[resourceGetIos.Length - 1]

                        download[0] = new DownloadObj().SetUrl(allRoomResults[index].resourceIosUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/Resources").SetFileName(resourceGetIos[resourceGetIos.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(download[0]);
                        Downloader.Instance.totalDownload++;
                        //GetDownloadProgress.Instance.DownloadDataConvert(allRoomResults[i].resourceSize);

                        UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/Resources", allRoomResults[index].resourceIosSize, resourceIosGet[resourceIosGet.Length - 1]);

                        Debug.Log("文件大小：" + allRoomResults[index].resourceIosSize.Substring(0, allRoomResults[index].resourceIosSize.Length - 1));
                    }

#elif UNITY_ANDROID
  
                    if (!string.IsNullOrEmpty(allRoomResults[index].resourceAndroidUrl))
                    {
                         string[] resourceGetAndroid = allRoomResults[index].resourceAndroidUrl.Split('/');
                        //resourceGetAndroid[resourceGetAndroid.Length - 1]

                        download[0] = new DownloadObj().SetUrl(allRoomResults[index].resourceAndroidUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/Resources").SetFileName(resourceGetAndroid[resourceGetAndroid.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(download[0]);
                        Downloader.Instance.totalDownload++;
                        //GetDownloadProgress.Instance.DownloadDataConvert(allRoomResults[index].resourceSize);

                        UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/Resources", allRoomResults[index].resourceAndroidSize, resourceAndroidGet[resourceAndroidGet.Length - 1]);

                        Debug.Log("文件大小：" + allRoomResults[index].resourceAndroidSize.Substring(0, allRoomResults[index].resourceAndroidSize.Length - 1));
                    }

#endif

            if (!string.IsNullOrEmpty(allRoomResults[index].musicUrl))
            {
                string[] musicURL = allRoomResults[index].musicUrl.Split('/');
                //musicGet[musicGet.Length - 1]

                download[1] = new DownloadObj().SetUrl(allRoomResults[index].musicUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/BGM").SetFileName(musicURL[musicURL.Length - 1]);
                Downloader.Instance.requestDownloadObj.Add(download[1]);
                Downloader.Instance.totalDownload++;

                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/BGM", allRoomResults[index].musicSize, musicURL[musicURL.Length - 1]);

                //GetDownloadProgress.Instance.DownloadDataConvert(allRoomResults[i].musicSize);
            }

            //BatchDownloadPMData();
            FirstDownloadRoomExhibits(currentSelectSceneData.sceneID);
        }
    }
    /// <summary>
    /// 7.首次添加画作资源XML信息
    /// </summary>
    public void FirstAddPMXML(List<PMHttpData> datas)
    {
        XmlElement root = localXmlDoc.DocumentElement;
        XmlNodeList classList = root.ChildNodes;

        CC[] cc = new CC[classList.Count];
        for (int i = 0; i < classList.Count; i++)
        {
            cc[i] = new CC();
            cc[i].a = 0;
            string[] roomName = classList[i].Name.Split('_');
            cc[i].b = roomName[roomName.Length - 1];
        }

        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].info.PMStatus == "0")
            {
                int index = 0;

                int PMindex = 0;

                for (int j = 0; j < classList.Count; j++)
                {
                    if (datas[i].info.PMroom == cc[j].b)
                    {
                        cc[j].a++;
                        index = j;
                        PMindex = cc[j].a;
                    }
                }

                XmlNode PM = null;
                if (classList[index].ChildNodes.Count > 2)
                {
                    PM = classList[index].ChildNodes[2];
                }
                else
                {
                    PM = localXmlDoc.CreateElement("PM");
                }
                XmlElement info = localXmlDoc.CreateElement("Info");
                XmlElement PMList = localXmlDoc.CreateElement("PM" + (PMindex));
                XmlElement PMNo = localXmlDoc.CreateElement("PMNo");
                XmlElement PMText = localXmlDoc.CreateElement("PMText");
                XmlElement mainGraphName = localXmlDoc.CreateElement("mainGraphName");
                XmlElement mainGraphHttpUrl = localXmlDoc.CreateElement("mainGraphHttpUrl");
                XmlElement mainGraphSaveUrl = localXmlDoc.CreateElement("mainGraphSaveUrl");
                XmlElement introductionImageName = localXmlDoc.CreateElement("introductionImageName");
                XmlElement introductionImageHttpUrl = localXmlDoc.CreateElement("introductionImageHttpUrl");
                XmlElement introductionImageSaveUrl = localXmlDoc.CreateElement("introductionImageSaveUrl");
                XmlElement videoName = localXmlDoc.CreateElement("videoName");
                XmlElement videoHttpUrl = localXmlDoc.CreateElement("videoHttpUrl");
                XmlElement videoSaveUrl = localXmlDoc.CreateElement("videoSaveUrl");
                XmlElement voiceName = localXmlDoc.CreateElement("voiceName");
                XmlElement voiceHttpUrl = localXmlDoc.CreateElement("voiceHttpUrl");
                XmlElement voiceSaveUrl = localXmlDoc.CreateElement("voiceSaveUrl");
                XmlElement animationThumbnailName = localXmlDoc.CreateElement("animationThumbnailName");
                XmlElement animationThumbnailHttpUrl = localXmlDoc.CreateElement("animationThumbnailHttpUrl");
                XmlElement animationThumbnailSaveUrl = localXmlDoc.CreateElement("animationThumbnailSaveUrl");
                XmlElement frameAnimationHttpUrl = localXmlDoc.CreateElement("frameAnimationHttpUrl");
                List<XmlElement> elementsHttp = new List<XmlElement>();
                if (datas[i].frameAnimation.frameAnimationHttpUrl != null)
                {
                    for (int j = 0; j < datas[i].frameAnimation.frameAnimationHttpUrl.Length; j++)
                    {
                        XmlElement ele = localXmlDoc.CreateElement("Name" + j);
                        ele.InnerText = datas[i].frameAnimation.frameAnimationHttpUrl[j];
                        elementsHttp.Add(ele);
                    }
                }


                XmlElement frameAnimationSaveUrl = localXmlDoc.CreateElement("frameAnimationSaveUrl");
                List<XmlElement> elementsSave = new List<XmlElement>();
                if (datas[i].frameAnimation.frameAnimationSaveUrl != null)
                {
                    for (int j = 0; j < datas[i].frameAnimation.frameAnimationSaveUrl.Length; j++)
                    {
                        XmlElement ele = localXmlDoc.CreateElement("Name" + j);
                        ele.InnerText = datas[i].frameAnimation.frameAnimationSaveUrl[j];
                        elementsSave.Add(ele);
                    }
                }


                XmlElement videoLink = localXmlDoc.CreateElement("videoLink");
                XmlElement webLink = localXmlDoc.CreateElement("webLink");
                XmlElement status = localXmlDoc.CreateElement("status");
                XmlElement auth = localXmlDoc.CreateElement("auth");
                XmlElement version = localXmlDoc.CreateElement("version");
                XmlElement PMName = localXmlDoc.CreateElement("PMName");

                if (!string.IsNullOrEmpty(datas[i].info.PMNo.ToString()))
                    PMNo.InnerText = datas[i].info.PMNo.ToString();
                else
                    PMNo.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].info.PMText))
                    PMText.InnerText = datas[i].info.PMText;
                else
                    PMText.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].mainGraph.mainGraphName))
                    mainGraphName.InnerText = datas[i].mainGraph.mainGraphName;
                else
                    mainGraphName.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].mainGraph.mainGraphHttpUrl))
                    mainGraphHttpUrl.InnerText = datas[i].mainGraph.mainGraphHttpUrl;
                else
                    mainGraphHttpUrl.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].mainGraph.mainGraphSaveUrl))
                    mainGraphSaveUrl.InnerText = datas[i].mainGraph.mainGraphSaveUrl;
                else
                    mainGraphSaveUrl.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].introductionImage.introductionImageName))
                    introductionImageName.InnerText = datas[i].introductionImage.introductionImageName;
                else
                    introductionImageName.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].introductionImage.introductionImageHttpUrl))
                    introductionImageHttpUrl.InnerText = datas[i].introductionImage.introductionImageHttpUrl;
                else
                    introductionImageHttpUrl.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].introductionImage.introductionImageSaveUrl))
                    introductionImageSaveUrl.InnerText = datas[i].introductionImage.introductionImageSaveUrl;
                else
                    introductionImageSaveUrl.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].video.videoName))
                    videoName.InnerText = datas[i].video.videoName;
                else
                    videoName.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].video.videoHttpUrl))
                    videoHttpUrl.InnerText = datas[i].video.videoHttpUrl;
                else
                    videoHttpUrl.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].video.videoSaveUrl))
                    videoSaveUrl.InnerText = datas[i].video.videoSaveUrl;
                else
                    videoSaveUrl.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].voice.voiceName))
                    voiceName.InnerText = datas[i].voice.voiceName;
                else
                    voiceName.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].voice.voiceHttpUrl))
                    voiceHttpUrl.InnerText = datas[i].voice.voiceHttpUrl;
                else
                    voiceHttpUrl.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].voice.voiceSaveUrl))
                    voiceSaveUrl.InnerText = datas[i].voice.voiceSaveUrl;
                else
                    voiceSaveUrl.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].animationThumbnail.animationThumbnailName))
                    animationThumbnailName.InnerText = datas[i].animationThumbnail.animationThumbnailName;
                else
                    animationThumbnailName.InnerText = " ";
                if (!string.IsNullOrEmpty(datas[i].animationThumbnail.animationThumbnailHttpUrl))
                    animationThumbnailHttpUrl.InnerText = datas[i].animationThumbnail.animationThumbnailHttpUrl;
                else
                    animationThumbnailHttpUrl.InnerText = " ";
                if (!string.IsNullOrEmpty(datas[i].animationThumbnail.animationThumbnailSaveUrl))
                    animationThumbnailSaveUrl.InnerText = datas[i].animationThumbnail.animationThumbnailSaveUrl;
                else
                    animationThumbnailSaveUrl.InnerText = " ";

                if (elementsHttp.Count != 0)
                {
                    for (int k = 0; k < elementsHttp.Count; k++)
                    {
                        frameAnimationHttpUrl.AppendChild(elementsHttp[k]);
                    }
                }
                else
                {
                    frameAnimationHttpUrl.InnerText = " ";
                }

                if (elementsSave.Count != 0)
                {
                    for (int k = 0; k < elementsSave.Count; k++)
                    {
                        frameAnimationSaveUrl.AppendChild(elementsSave[k]);
                    }
                }
                else
                {
                    frameAnimationSaveUrl.InnerText = " ";
                }


                if (!string.IsNullOrEmpty(datas[i].link.videoLink))
                    videoLink.InnerText = datas[i].link.videoLink;
                else
                    videoLink.InnerText = " ";
                if (!string.IsNullOrEmpty(datas[i].link.webLink))
                    webLink.InnerText = datas[i].link.webLink;
                else
                    webLink.InnerText = " ";

                if (!string.IsNullOrEmpty(datas[i].info.PMStatus))
                    status.InnerText = datas[i].info.PMStatus;
                else
                    status.InnerText = " ";
                if (!string.IsNullOrEmpty(datas[i].info.PMAuth))
                    auth.InnerText = datas[i].info.PMAuth;
                else
                    auth.InnerText = " ";
                if (!string.IsNullOrEmpty(datas[i].info.PMVersion))
                    version.InnerText = datas[i].info.PMVersion;
                else
                    version.InnerText = " ";
                if (!string.IsNullOrEmpty(datas[i].info.PMName))
                    PMName.InnerText = datas[i].info.PMName;
                else
                    PMName.InnerText = " ";

                info.AppendChild(PMNo);
                info.AppendChild(PMText);

                PMList.AppendChild(PMNo);
                PMList.AppendChild(PMText);
                PMList.AppendChild(mainGraphName);
                PMList.AppendChild(mainGraphHttpUrl);
                PMList.AppendChild(mainGraphSaveUrl);
                PMList.AppendChild(introductionImageName);
                PMList.AppendChild(introductionImageHttpUrl);
                PMList.AppendChild(introductionImageSaveUrl);
                PMList.AppendChild(videoName);
                PMList.AppendChild(videoHttpUrl);
                PMList.AppendChild(videoSaveUrl);
                PMList.AppendChild(voiceName);
                PMList.AppendChild(voiceHttpUrl);
                PMList.AppendChild(voiceSaveUrl);
                PMList.AppendChild(animationThumbnailName);
                PMList.AppendChild(animationThumbnailHttpUrl);
                PMList.AppendChild(animationThumbnailSaveUrl);
                PMList.AppendChild(frameAnimationHttpUrl);
                PMList.AppendChild(frameAnimationSaveUrl);
                PMList.AppendChild(videoLink);
                PMList.AppendChild(webLink);
                PMList.AppendChild(status);
                PMList.AppendChild(auth);
                PMList.AppendChild(version);
                PMList.AppendChild(PMName);

                PM.AppendChild(PMList);
                classList[index].AppendChild(PM);
                //}
            }
            else if (datas[i].info.PMStatus == "1")
            {
                int index = 0;

                int PMindex = 0;

                for (int j = 0; j < classList.Count; j++)
                {
                    if (datas[i].info.PMroom == cc[j].b)
                    {
                        cc[j].a++;
                        index = j;
                        PMindex = cc[j].a;
                    }
                }


                XmlNode PM = null;
                if (classList[index].ChildNodes.Count > 2)
                {
                    PM = classList[index].ChildNodes[2];
                }
                else
                {
                    PM = localXmlDoc.CreateElement("PM");
                }

                XmlElement info = localXmlDoc.CreateElement("Info");
                XmlElement PMList = localXmlDoc.CreateElement("PM" + PMindex);
                XmlElement PMNo = localXmlDoc.CreateElement("PMNo");
                XmlElement PMText = localXmlDoc.CreateElement("PMText");
                XmlElement mainGraphName = localXmlDoc.CreateElement("mainGraphName");
                XmlElement mainGraphHttpUrl = localXmlDoc.CreateElement("mainGraphHttpUrl");
                XmlElement mainGraphSaveUrl = localXmlDoc.CreateElement("mainGraphSaveUrl");
                XmlElement introductionImageName = localXmlDoc.CreateElement("introductionImageName");
                XmlElement introductionImageHttpUrl = localXmlDoc.CreateElement("introductionImageHttpUrl");
                XmlElement introductionImageSaveUrl = localXmlDoc.CreateElement("introductionImageSaveUrl");
                XmlElement videoName = localXmlDoc.CreateElement("videoName");
                XmlElement videoHttpUrl = localXmlDoc.CreateElement("videoHttpUrl");
                XmlElement videoSaveUrl = localXmlDoc.CreateElement("videoSaveUrl");
                XmlElement voiceName = localXmlDoc.CreateElement("voiceName");
                XmlElement voiceHttpUrl = localXmlDoc.CreateElement("voiceHttpUrl");
                XmlElement voiceSaveUrl = localXmlDoc.CreateElement("voiceSaveUrl");
                XmlElement animationThumbnailName = localXmlDoc.CreateElement("animationThumbnailName");
                XmlElement animationThumbnailHttpUrl = localXmlDoc.CreateElement("animationThumbnailHttpUrl");
                XmlElement animationThumbnailSaveUrl = localXmlDoc.CreateElement("animationThumbnailSaveUrl");
                XmlElement frameAnimationHttpUrl = localXmlDoc.CreateElement("frameAnimationHttpUrl");
                XmlElement frameAnimationSaveUrl = localXmlDoc.CreateElement("frameAnimationSaveUrl");
                XmlElement videoLink = localXmlDoc.CreateElement("videoLink");
                XmlElement webLink = localXmlDoc.CreateElement("webLink");
                XmlElement status = localXmlDoc.CreateElement("status");
                XmlElement auth = localXmlDoc.CreateElement("auth");
                XmlElement version = localXmlDoc.CreateElement("version");
                XmlElement PMName = localXmlDoc.CreateElement("PMName");


                PMNo.InnerText = datas[i].info.PMNo.ToString();
                PMText.InnerText = " ";
                mainGraphName.InnerText = " ";
                mainGraphHttpUrl.InnerText = " ";
                mainGraphSaveUrl.InnerText = " ";
                introductionImageName.InnerText = " ";
                introductionImageHttpUrl.InnerText = " ";
                introductionImageSaveUrl.InnerText = " ";
                videoName.InnerText = " ";
                videoHttpUrl.InnerText = " ";
                videoSaveUrl.InnerText = " ";
                voiceName.InnerText = " ";
                voiceHttpUrl.InnerText = " ";
                voiceSaveUrl.InnerText = " ";
                animationThumbnailName.InnerText = " ";
                animationThumbnailHttpUrl.InnerText = " ";
                animationThumbnailSaveUrl.InnerText = " ";
                frameAnimationHttpUrl.InnerText = " ";
                frameAnimationSaveUrl.InnerText = " ";
                videoLink.InnerText = " ";
                webLink.InnerText = " ";
                status.InnerText = "1";
                auth.InnerText = " ";
                version.InnerText = " ";
                PMName.InnerText = " ";

                info.AppendChild(PMNo);
                info.AppendChild(PMText);

                PMList.AppendChild(PMNo);
                PMList.AppendChild(PMText);
                PMList.AppendChild(mainGraphName);
                PMList.AppendChild(mainGraphHttpUrl);
                PMList.AppendChild(mainGraphSaveUrl);
                PMList.AppendChild(introductionImageName);
                PMList.AppendChild(introductionImageHttpUrl);
                PMList.AppendChild(introductionImageSaveUrl);
                PMList.AppendChild(videoName);
                PMList.AppendChild(videoHttpUrl);
                PMList.AppendChild(videoSaveUrl);
                PMList.AppendChild(voiceName);
                PMList.AppendChild(voiceHttpUrl);
                PMList.AppendChild(voiceSaveUrl);
                PMList.AppendChild(animationThumbnailName);
                PMList.AppendChild(animationThumbnailHttpUrl);
                PMList.AppendChild(animationThumbnailSaveUrl);
                PMList.AppendChild(frameAnimationHttpUrl);
                PMList.AppendChild(frameAnimationSaveUrl);
                PMList.AppendChild(videoLink);
                PMList.AppendChild(webLink);
                PMList.AppendChild(status);
                PMList.AppendChild(auth);
                PMList.AppendChild(version);
                PMList.AppendChild(PMName);

                PM.AppendChild(PMList);
                classList[index].AppendChild(PM);
                Debug.Log(datas[i].info.PMNo + ":已被禁用");
            }
        }

        SaveXML();
    }
    /// <summary>
    /// 8.首次下载房间展品资源
    /// </summary>
    /// <param name="index">房间序号</param>
    public void FirstDownloadRoomExhibits(string strID)
    {
        List<PMHttpData> PMHttpDatas = new List<PMHttpData>();

        int index = 0;

        if (allRoomResults != null && allRoomResults.Count != 0)
        {
            for (int i = 0; i < allRoomResults.Count; i++)
            {
                if (allRoomResults[i].id == strID)
                {
                    index = i;
                    break;
                }
            }
        }

        ExistSceneDownloadURL(allRoomResults[index].id + "/PM");

        int indexxx = 0;

        for (int i = 0; i < roomAllExhibitsData.Count; i++)
        {
            if (roomAllExhibitsData[i][0].showRoomId == currentSelectSceneData.sceneID)
            {
                indexxx = i;
                break;
            }
        }

        for (int j = 0; j < roomAllExhibitsData[indexxx].Count; j++)
        {
            if (roomAllExhibitsData[indexxx][j].status == 0)
            {
                PMHttpData data = new PMHttpData();

                data.info = new PMInfo();
                if (!string.IsNullOrEmpty(allRoomResults[index].id))
                    data.info.PMroom = allRoomResults[index].id;
                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].name))
                    data.info.PMName = roomAllExhibitsData[indexxx][j].name;
                data.info.PMNo = roomAllExhibitsData[indexxx][j].exhibitsNo;
                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].text))
                    data.info.PMText = roomAllExhibitsData[indexxx][j].text;
                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].status.ToString()))
                    data.info.PMStatus = roomAllExhibitsData[indexxx][j].status.ToString();

                data.mainGraph = new PMMainGraph();
                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].mainGraphEncodeUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].mainGraphEncodeUrl.Split('/');
                    //get[get.Length - 1]

                    data.mainGraph.mainGraphName = get[get.Length - 1];
                    data.mainGraph.mainGraphHttpUrl = roomAllExhibitsData[indexxx][j].mainGraphEncodeUrl;
                    data.mainGraph.mainGraphSaveUrl = sceneURL + allRoomResults[index].id + "/PM/" +
                                                      roomAllExhibitsData[indexxx][j].exhibitsNo + "/MainGraph/" +
                                                      get[get.Length - 1];
                }

                data.introductionImage = new PMIntroductionImage();
                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].introductionImageEncodeUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].introductionImageEncodeUrl.Split('/');
                    //get[get.Length - 1]
                    data.introductionImage.introductionImageName = get[get.Length - 1];
                    data.introductionImage.introductionImageHttpUrl = roomAllExhibitsData[indexxx][j].introductionImageEncodeUrl;
                    data.introductionImage.introductionImageSaveUrl =
                        sceneURL + allRoomResults[index].id + "/PM/" +
                        roomAllExhibitsData[indexxx][j].exhibitsNo + "/IntroductionImage/" +
                        get[get.Length - 1];
                }

                data.video = new PMVideo();
                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].videoUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].videoUrl.Split('/');
                    //get[get.Length - 1]
                    data.video.videoName = get[get.Length - 1];
                    data.video.videoHttpUrl = roomAllExhibitsData[indexxx][j].videoUrl;
                    data.video.videoSaveUrl = sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/Video/" + get[get.Length - 1];
                }

                data.voice = new PMVoice();

                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].voiceUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].voiceUrl.Split('/');
                    //get[get.Length - 1]
                    data.voice.voiceName = get[get.Length - 1];
                    data.voice.voiceHttpUrl = roomAllExhibitsData[indexxx][j].voiceUrl;
                    data.voice.voiceSaveUrl = sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/Voice/" + get[get.Length - 1];

                }


                data.animationThumbnail = new PMAnimationThumbnail();

                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].animationThumbnailEncodeUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].animationThumbnailEncodeUrl.Split('/');
                    //get[get.Length - 1]
                    data.animationThumbnail.animationThumbnailName = get[get.Length - 1];
                    data.animationThumbnail.animationThumbnailHttpUrl = roomAllExhibitsData[indexxx][j].animationThumbnailEncodeUrl;
                    data.animationThumbnail.animationThumbnailSaveUrl = sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/AnimationThumbnail/" + get[get.Length - 1];

                }

                data.frameAnimation = new PMFrameAnimation();
                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].frameAnimationEncodeUrl))
                {
                    data.frameAnimation.frameAnimationHttpUrl = roomAllExhibitsData[indexxx][j].frameAnimationEncodeUrl.Split(',');
                    data.frameAnimation.frameAnimationSaveUrl = new string[data.frameAnimation.frameAnimationHttpUrl.Length];
                    for (int k = 0; k < data.frameAnimation.frameAnimationSaveUrl.Length; k++)
                    {
                        string[] get = data.frameAnimation.frameAnimationHttpUrl[k].Split('/');
                        data.frameAnimation.frameAnimationSaveUrl[k] = sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/FrameAnimationUrl/" + get[get.Length - 1];
                    }
                }

                data.link = new PMLink();
                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].videoLink))
                    data.link.videoLink = roomAllExhibitsData[indexxx][j].videoLink;
                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].webLink))
                    data.link.webLink = roomAllExhibitsData[indexxx][j].webLink;

                PMHttpDatas.Add(data);
            }
            else
            {
                PMHttpData data = new PMHttpData();

                data.info = new PMInfo();
                data.info.PMroom = allRoomResults[index].id;
                data.info.PMNo = roomAllExhibitsData[indexxx][j].exhibitsNo;
                data.info.PMStatus = roomAllExhibitsData[indexxx][j].status.ToString();

                PMHttpDatas.Add(data);

            }
        }

        FirstAddPMXML(PMHttpDatas);

        for (int j = 0; j < roomAllExhibitsData[indexxx].Count; j++)
        {
            if (roomAllExhibitsData[indexxx][j].status == 0)
            {
                ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/MainGraph");
                ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/IntroductionImage");
                ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/Video");
                ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/Voice");
                ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/AnimationThumbnail");
                ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/FrameAnimationUrl");

                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].mainGraphEncodeUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].mainGraphEncodeUrl.Split('/');
                    //get[get.Length - 1]

                    DownloadObj download01 = new DownloadObj().SetUrl(roomAllExhibitsData[indexxx][j].mainGraphEncodeUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/MainGraph").SetFileName(get[get.Length - 1]);
                    Downloader.Instance.requestDownloadObj.Add(download01);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(PMData.roomAllExhibitsData[i][j].mainGraphSize);
                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/MainGraph", roomAllExhibitsData[indexxx][j].mainGraphSize, get[get.Length - 1]);
                }

                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].introductionImageEncodeUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].introductionImageEncodeUrl.Split('/');
                    //get[get.Length - 1]

                    DownloadObj download02 = new DownloadObj().SetUrl(roomAllExhibitsData[indexxx][j].introductionImageEncodeUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/IntroductionImage").SetFileName(get[get.Length - 1]);

                    Downloader.Instance.requestDownloadObj.Add(download02);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(PMData.roomAllExhibitsData[i][j].introductionImageSize);

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/IntroductionImage", roomAllExhibitsData[indexxx][j].introductionImageSize, get[get.Length - 1]);

                }


                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].videoUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].videoUrl.Split('/');
                    //get[get.Length - 1]

                    DownloadObj download03 = new DownloadObj().SetUrl(roomAllExhibitsData[indexxx][j].videoUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/Video").SetFileName(get[get.Length - 1]);
                    Downloader.Instance.requestDownloadObj.Add(download03);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(PMData.roomAllExhibitsData[i][j].videoSize);

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/Video", roomAllExhibitsData[indexxx][j].videoSize, get[get.Length - 1]);

                }


                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].voiceUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].voiceUrl.Split('/');
                    //get[get.Length - 1]

                    DownloadObj download04 = new DownloadObj().SetUrl(roomAllExhibitsData[indexxx][j].voiceUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/Voice").SetFileName(get[get.Length - 1]);
                    Downloader.Instance.requestDownloadObj.Add(download04);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(PMData.roomAllExhibitsData[i][j].voiceSize);

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/Voice", roomAllExhibitsData[indexxx][j].voiceSize, get[get.Length - 1]);

                }


                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].animationThumbnailEncodeUrl))
                {
                    string[] get = roomAllExhibitsData[indexxx][j].animationThumbnailEncodeUrl.Split('/');
                    //get[get.Length - 1]

                    DownloadObj download05 = new DownloadObj().SetUrl(roomAllExhibitsData[indexxx][j].animationThumbnailEncodeUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/AnimationThumbnail").SetFileName(get[get.Length - 1]);
                    Downloader.Instance.requestDownloadObj.Add(download05);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(PMData.roomAllExhibitsData[i][j].animationThumbnailSize);

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/AnimationThumbnail", roomAllExhibitsData[indexxx][j].animationThumbnailSize, get[get.Length - 1]);

                }

                if (!string.IsNullOrEmpty(roomAllExhibitsData[indexxx][j].frameAnimationEncodeUrl))
                {
                    string[] urls = roomAllExhibitsData[indexxx][j].frameAnimationEncodeUrl.Split(',');
                    string[] names = new string[urls.Length];
                    DownloadObj[] frames = new DownloadObj[urls.Length];
                    for (int k = 0; k < urls.Length; k++)
                    {
                        string[] get = urls[k].Split('/');
                        names[k] = get[get.Length - 1];
                        frames[k] = new DownloadObj().SetUrl(urls[k]).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + roomAllExhibitsData[indexxx][j].exhibitsNo + "/FrameAnimationUrl").SetFileName(names[k]);

                        Downloader.Instance.requestDownloadObj.Add(frames[k]);
                        Downloader.Instance.totalDownload++;

                    }
                    //GetDownloadProgress.Instance.DownloadDataConvert(PMData.roomAllExhibitsData[i][j].frameAnimationSize);

                    UnityDownloadMgr.instance.FileSizeAdd(roomAllExhibitsData[indexxx][j].frameAnimationSize);
                }
            }

            //BatchDownloadPMData();

            if (j == roomAllExhibitsData[indexxx].Count - 1)
            {
                if (fileSize != 0f)
                {
                    Debug.Log("提示文件下载的大小");

                    GetDownloadData = true;

                    getSceneData = currentSelectSceneData;

                    PopupWindowCanvasManager.Instance.DownloadFileCanvasGroupOpenEvent();
                    //fileSize = 0;
                    //allRoomResults = new List<AllRoomResult>();
                    //roomAllExhibitsData = new List<List<RoomAllExhibitsData>>();
                    HttpManager.Instance.currentGetRoomAllExhibitsDataIndex = 1;
                }
                else
                {
                    Debug.Log("没有需要下载的文件");
                    //fileSize = 0;
                    //allRoomResults = new List<AllRoomResult>();
                    //roomAllExhibitsData = new List<List<RoomAllExhibitsData>>();
                    HttpManager.Instance.currentGetRoomAllExhibitsDataIndex = 1;
                    //HttpManager.Instance.DetectionRoomData();

                }
                break;
            }
        }
    }
    /// <summary>
    /// 9.删除房间资源
    /// </summary>
    /// <param name="deletePM"></param>
    private void DeleteRoomResources(XmlNode node)
    {
        string[] roomName = node.Name.Split('_');
        Debug.Log("需要删除的房间id：" + roomName[roomName.Length - 1]);
        //roomName[roomName.Length - 1]
        DeleteFiles(sceneURL + roomName[roomName.Length - 1]);
        //Directory.Delete(sceneURL + node.Name);
        Debug.Log(node.Name);
        localXmlDoc.DocumentElement.RemoveChild(node);
        SaveXML();
    }
    /// <summary>
    /// 10.添加房间资源
    /// </summary>
    private void AddRoomResources(AllRoomResult result)
    {
        XmlElement sceneName = localXmlDoc.CreateElement("MuseumID" + "_" + result.showMuseumId + "_" + "RoomID" + "_" + result.id);
        XmlElement resources = localXmlDoc.CreateElement("Resources");
        XmlElement bgm = localXmlDoc.CreateElement("BGM");
        XmlElement resourcesName = localXmlDoc.CreateElement("ResourcesName");
        XmlElement resourcesHttpURL = localXmlDoc.CreateElement("ResourcesHttpURL");
        XmlElement resourcesSaveURL = localXmlDoc.CreateElement("ResourcesSaveURL");
        XmlElement BGMName = localXmlDoc.CreateElement("BGMName");
        XmlElement BGMHttpURL = localXmlDoc.CreateElement("BGMHttpURL");
        XmlElement BGMSaveURL = localXmlDoc.CreateElement("BGMSaveURL");

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (!string.IsNullOrEmpty(result.resourceUrl))
        {
            string[] get = result.resourceUrl.Split('/');
            //get[get.Length - 1]
            resourcesName.InnerText = get[get.Length - 1];
            resourcesHttpURL.InnerText = result.resourceUrl;
            resourcesSaveURL.InnerText = sceneURL + result.id + "/Resources/" + get[get.Length - 1];
        }
        else
        {
            resourcesName.InnerText = " ";
            resourcesHttpURL.InnerText = " ";
            resourcesSaveURL.InnerText = " ";
        }
#elif UNITY_IOS
                if (!string.IsNullOrEmpty(result.resourceIosUrl))
        {
            string[] get = result.resourceIosUrl.Split('/');
            //get[get.Length - 1]

            resourcesName.InnerText = get[get.Length - 1];
            resourcesHttpURL.InnerText = result.resourceIosUrl;
            resourcesSaveURL.InnerText = sceneURL + result.id + "/Resources/" + get[get.Length - 1];
        }
        else
        {
            resourcesName.InnerText = " ";
            resourcesHttpURL.InnerText = " ";
            resourcesSaveURL.InnerText = " ";
        }
#elif UNITY_ANDROID
                if (!string.IsNullOrEmpty(result.resourceAndroidUrl))
        {
            string[] get = result.resourceAndroidUrl.Split('/');
            //get[get.Length - 1]


            resourcesName.InnerText = get[get.Length - 1];
            resourcesHttpURL.InnerText = result.resourceAndroidUrl;
            resourcesSaveURL.InnerText = sceneURL + result.id + "/Resources/" + get[get.Length - 1];
        }
        else
        {
            resourcesName.InnerText = " ";
            resourcesHttpURL.InnerText = " ";
            resourcesSaveURL.InnerText = " ";
        }
#endif



        //if (!string.IsNullOrEmpty(result.resourceName))
        //    resourcesName.InnerText = result.resourceName;
        //if (!string.IsNullOrEmpty(result.resourceUrl))
        //    resourcesHttpURL.InnerText = result.resourceUrl;
        //resourcesSaveURL.InnerText = sceneURL + result.name + "/Resources/" + result.resourceName;

        if (!string.IsNullOrEmpty(result.musicUrl))
        {
            string[] get = result.musicUrl.Split('/');
            //musicGet[musicGet.Length - 1]

            BGMHttpURL.InnerText = result.musicUrl;
            BGMName.InnerText = get[get.Length - 1];
            BGMSaveURL.InnerText = sceneURL + result.id + "/BGM/" + get[get.Length - 1];
        }
        else
        {
            BGMHttpURL.InnerText = " ";
            BGMName.InnerText = " ";
            BGMSaveURL.InnerText = " ";
        }


        resources.AppendChild(resourcesName);
        resources.AppendChild(resourcesHttpURL);
        resources.AppendChild(resourcesSaveURL);
        bgm.AppendChild(BGMName);
        bgm.AppendChild(BGMHttpURL);
        bgm.AppendChild(BGMSaveURL);
        sceneName.AppendChild(resources);
        sceneName.AppendChild(bgm);
        localXmlDoc.ChildNodes[0].AppendChild(sceneName);

        DownloadObj obj1 = null;
        DownloadObj obj2 = null;

        string[] resourceGet = result.resourceUrl.Split('/');
        //resourceGet[resourceGet.Length - 1]

        string[] resourceAndroidGet = result.resourceAndroidUrl.Split('/');
        //resourceAndroidGet[resourceAndroidGet.Length - 1]

        string[] resourceIosGet = result.resourceIosUrl.Split('/');
        //resourceIosGet[resourceIosGet.Length - 1]

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        obj1 = new DownloadObj().SetUrl(result.resourceUrl)
            .SetParentPath(sceneURL + result.id + "/Resources/")
            .SetFileName(resourceGet[resourceGet.Length - 1]);

        UnityDownloadMgr.instance.ExistsFileSize(sceneURL + result.id + "/Resources", result.resourceSize, resourceGet[resourceGet.Length - 1]);


#elif UNITY_IOS
                obj1 = new DownloadObj().SetUrl(result.resourceIosUrl)
            .SetParentPath(sceneURL + result.id + "/Resources/")
            .SetFileName(resourceIosGet[resourceIosGet.Length - 1]);

             UnityDownloadMgr.instance.ExistsFileSize(sceneURL + result.id + "/Resources", result.resourceIosSize, resourceIosGet[resourceIosGet.Length - 1]);
#elif UNITY_ANDROID
                obj1 = new DownloadObj().SetUrl(result.resourceAndroidUrl)
            .SetParentPath(sceneURL + result.id + "/Resources/")
            .SetFileName(resourceAndroidGet[resourceAndroidGet.Length - 1]);

            UnityDownloadMgr.instance.ExistsFileSize(sceneURL + result.id + "/Resources", result.resourceAndroidSize, resourceAndroidGet[resourceAndroidGet.Length - 1]);

#endif

        //obj1 = new DownloadObj().SetUrl(result.resourceUrl)
        //    .SetParentPath(sceneURL + result.name + "/Resources/" + result.resourceName)
        //    .SetFileName(result.resourceName);

        ////lackDownloadObj.Add(obj1);
        ////Downloader.Instance.totalDownload++;

        Downloader.Instance.requestDownloadObj.Add(obj1);
        Downloader.Instance.totalDownload++;
        //GetDownloadProgress.Instance.DownloadDataConvert(result.resourceSize);

        string[] musicGet = result.musicUrl.Split('/');
        //musicGet[musicGet.Length - 1]

        obj2 = new DownloadObj().SetUrl(result.musicUrl)
            .SetParentPath(sceneURL + result.id + "/BGM/")
            .SetFileName(musicGet[musicGet.Length - 1]);

        UnityDownloadMgr.instance.ExistsFileSize(sceneURL + result.id + "/BGM", result.musicSize, musicGet[musicGet.Length - 1]);

        //lackDownloadObj.Add(obj2);
        //Downloader.Instance.totalDownload++;


        Downloader.Instance.requestDownloadObj.Add(obj2);
        Downloader.Instance.totalDownload++;
        //GetDownloadProgress.Instance.DownloadDataConvert(result.musicSize);

        SaveXML();
    }
    /// <summary>
    /// 11.对比房间资源
    /// </summary>
    private void CompareRoomResources(XmlNode compare1, AllRoomResult compare2)
    {

        DownloadObj obj1 = null;
        DownloadObj obj2 = null;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (!string.IsNullOrEmpty(compare2.resourceUrl))
        {
            if (compare1.ChildNodes[0].ChildNodes[1].InnerText != compare2.resourceUrl)
            {
                if (File.Exists(compare1.ChildNodes[0].ChildNodes[2].InnerText))
                    File.Delete(compare1.ChildNodes[0].ChildNodes[2].InnerText);
                if (File.Exists(compare1.ChildNodes[0].ChildNodes[2].InnerText + ".tmp"))
                    File.Delete(compare1.ChildNodes[0].ChildNodes[2].InnerText + ".tmp");

                string[] resourceGet = compare2.resourceUrl.Split('/');
                //resourceGet[resourceGet.Length - 1]

                obj1 = new DownloadObj().SetUrl(compare2.resourceUrl)
                    .SetParentPath(sceneURL + compare2.id + "/Resources/")
                    .SetFileName(resourceGet[resourceGet.Length - 1]);

                compare1.ChildNodes[0].ChildNodes[0].InnerText = resourceGet[resourceGet.Length - 1];
                compare1.ChildNodes[0].ChildNodes[1].InnerText = compare2.resourceUrl;
                compare1.ChildNodes[0].ChildNodes[2].InnerText = sceneURL + compare2.id + "/Resources/" + resourceGet[resourceGet.Length - 1];

                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.id + "/Resources", compare2.resourceSize, resourceGet[resourceGet.Length - 1]);

                //lackDownloadObj.Add(obj1);
                //Downloader.Instance.totalDownload++;

                Downloader.Instance.requestDownloadObj.Add(obj1);
                Downloader.Instance.totalDownload++;
                ////GetDownloadProgress.Instance.DownloadDataConvert(compare2.resourceSize);
            }
            else
            {
                if (!File.Exists(compare1.ChildNodes[0].ChildNodes[2].InnerText))
                {
                    string[] resourceGet = compare2.resourceUrl.Split('/');
                    //resourceGet[resourceGet.Length - 1]

                    obj1 = new DownloadObj().SetUrl(compare2.resourceUrl)
                        .SetParentPath(sceneURL + compare2.id + "/Resources/")
                        .SetFileName(resourceGet[resourceGet.Length - 1]);

                    compare1.ChildNodes[0].ChildNodes[0].InnerText = resourceGet[resourceGet.Length - 1];
                    compare1.ChildNodes[0].ChildNodes[1].InnerText = compare2.resourceUrl;
                    compare1.ChildNodes[0].ChildNodes[2].InnerText = sceneURL + compare2.id + "/Resources/" + resourceGet[resourceGet.Length - 1];

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.id + "/Resources", compare2.resourceSize, resourceGet[resourceGet.Length - 1]);

                    Downloader.Instance.requestDownloadObj.Add(obj1);
                    Downloader.Instance.totalDownload++;
                    ////GetDownloadProgress.Instance.DownloadDataConvert(compare2.resourceSize);
                }
            }
        }
#elif UNITY_IOS
 if (!string.IsNullOrEmpty(compare2.resourceIosUrl))
        {
            if (compare1.ChildNodes[0].ChildNodes[1].InnerText != compare2.resourceIosUrl)
            {
                if (File.Exists(compare1.ChildNodes[0].ChildNodes[2].InnerText))
                    File.Delete(compare1.ChildNodes[0].ChildNodes[2].InnerText);
                if (File.Exists(compare1.ChildNodes[0].ChildNodes[2].InnerText + ".tmp"))
                    File.Delete(compare1.ChildNodes[0].ChildNodes[2].InnerText + ".tmp");

                    string[] resourceGet = compare2.resourceIosUrl.Split('/');
                    //resourceGet[resourceGet.Length - 1]

                obj1 = new DownloadObj().SetUrl(compare2.resourceIosUrl)
                    .SetParentPath(sceneURL + compare2.id + "/Resources/")
                    .SetFileName(resourceGet[resourceGet.Length - 1]);

                     
                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.id + "/Resources", compare2.resourceIosSize, resourceGet[resourceGet.Length - 1]);

                compare1.ChildNodes[0].ChildNodes[0].InnerText = resourceGet[resourceGet.Length - 1];
                compare1.ChildNodes[0].ChildNodes[1].InnerText = compare2.resourceIosUrl;
                compare1.ChildNodes[0].ChildNodes[2].InnerText = sceneURL + compare2.id + "/Resources/" + resourceGet[resourceGet.Length - 1];

                Downloader.Instance.requestDownloadObj.Add(obj1);
                Downloader.Instance.totalDownload++;

            }
            else
            {
                if (!File.Exists(compare1.ChildNodes[0].ChildNodes[2].InnerText))
                {

                    string[] resourceGet = compare2.resourceIosUrl.Split('/');
                    //resourceGet[resourceGet.Length - 1]

                    obj1 = new DownloadObj().SetUrl(compare2.resourceIosUrl)
                        .SetParentPath(sceneURL + compare2.id + "/Resources/")
                        .SetFileName(resourceGet[resourceGet.Length - 1]);

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.id + "/Resources", compare2.resourceIosSize, resourceGet[resourceGet.Length - 1]);

                    compare1.ChildNodes[0].ChildNodes[0].InnerText = resourceGet[resourceGet.Length - 1];
                    compare1.ChildNodes[0].ChildNodes[1].InnerText = compare2.resourceIosUrl;
                    compare1.ChildNodes[0].ChildNodes[2].InnerText = sceneURL + compare2.id + "/Resources/" + resourceGet[resourceGet.Length - 1];

                    Downloader.Instance.requestDownloadObj.Add(obj1);
                    Downloader.Instance.totalDownload++;
                }
            }
        }
#elif UNITY_ANDROID
 if (!string.IsNullOrEmpty(compare2.resourceAndroidUrl))
        {
            if (compare1.ChildNodes[0].ChildNodes[1].InnerText != compare2.resourceAndroidUrl)
            {
                if (File.Exists(compare1.ChildNodes[0].ChildNodes[2].InnerText))
                    File.Delete(compare1.ChildNodes[0].ChildNodes[2].InnerText);
                if (File.Exists(compare1.ChildNodes[0].ChildNodes[2].InnerText + ".tmp"))
                    File.Delete(compare1.ChildNodes[0].ChildNodes[2].InnerText + ".tmp");

                    string[] resourceGet = compare2.resourceAndroidUrl.Split('/');
                    //resourceGet[resourceGet.Length - 1]

                obj1 = new DownloadObj().SetUrl(compare2.resourceAndroidUrl)
                    .SetParentPath(sceneURL + compare2.id + "/Resources/")
                    .SetFileName(resourceGet[resourceGet.Length - 1]);

                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.id + "/Resources", compare2.resourceAndroidSize, resourceGet[resourceGet.Length - 1]);

                compare1.ChildNodes[0].ChildNodes[0].InnerText = resourceGet[resourceGet.Length - 1];
                compare1.ChildNodes[0].ChildNodes[1].InnerText = compare2.resourceAndroidUrl;
                compare1.ChildNodes[0].ChildNodes[2].InnerText = sceneURL + compare2.id + "/Resources/" + resourceGet[resourceGet.Length - 1];

                Downloader.Instance.requestDownloadObj.Add(obj1);
                Downloader.Instance.totalDownload++;

            }
            else
            {
                if (!File.Exists(compare1.ChildNodes[0].ChildNodes[2].InnerText))
                {

                    string[] resourceGet = compare2.resourceAndroidUrl.Split('/');
                    //resourceGet[resourceGet.Length - 1]

                    obj1 = new DownloadObj().SetUrl(compare2.resourceAndroidUrl)
                        .SetParentPath(sceneURL + compare2.id + "/Resources/")
                        .SetFileName(resourceGet[resourceGet.Length - 1]);

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.id + "/Resources", compare2.resourceAndroidSize, resourceGet[resourceGet.Length - 1]);

                    compare1.ChildNodes[0].ChildNodes[0].InnerText = resourceGet[resourceGet.Length - 1];
                    compare1.ChildNodes[0].ChildNodes[1].InnerText = compare2.resourceAndroidUrl;
                    compare1.ChildNodes[0].ChildNodes[2].InnerText = sceneURL + compare2.id + "/Resources/" + resourceGet[resourceGet.Length - 1];

                    Downloader.Instance.requestDownloadObj.Add(obj1);
                    Downloader.Instance.totalDownload++;
                }
            }
        }
#endif



        if (!string.IsNullOrEmpty(compare2.musicUrl))
        {
            if (compare1.ChildNodes[1].ChildNodes[1].InnerText != compare2.musicUrl)
            {
                if (File.Exists(compare1.ChildNodes[1].ChildNodes[2].InnerText))
                    File.Delete(compare1.ChildNodes[1].ChildNodes[2].InnerText);
                if (File.Exists(compare1.ChildNodes[1].ChildNodes[2].InnerText + ".tmp"))
                    File.Delete(compare1.ChildNodes[1].ChildNodes[2].InnerText + ".tmp");

                string[] musicGet = compare2.musicUrl.Split('/');
                //musicGet[musicGet.Length - 1]

                obj2 = new DownloadObj().SetUrl(compare2.musicUrl)
                    .SetParentPath(sceneURL + compare2.id + "/BGM/")
                    .SetFileName(musicGet[musicGet.Length - 1]);

                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.id + "/BGM", compare2.musicSize, musicGet[musicGet.Length - 1]);

                compare1.ChildNodes[1].ChildNodes[0].InnerText = musicGet[musicGet.Length - 1];
                compare1.ChildNodes[1].ChildNodes[1].InnerText = compare2.musicUrl;
                compare1.ChildNodes[1].ChildNodes[2].InnerText = sceneURL + compare2.id + "/BGM/" + musicGet[musicGet.Length - 1];


                //lackDownloadObj.Add(obj2);
                //Downloader.Instance.totalDownload++;

                Downloader.Instance.requestDownloadObj.Add(obj2);
                Downloader.Instance.totalDownload++;
                ////GetDownloadProgress.Instance.DownloadDataConvert(compare2.musicSize);
            }
            else
            {
                if (!File.Exists(compare1.ChildNodes[1].ChildNodes[2].InnerText))
                {
                    string[] musicGet = compare2.musicUrl.Split('/');
                    //musicGet[musicGet.Length - 1]

                    obj2 = new DownloadObj().SetUrl(compare2.musicUrl)
                        .SetParentPath(sceneURL + compare2.id + "/BGM/")
                        .SetFileName(musicGet[musicGet.Length - 1]);

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.id + "/BGM", compare2.musicSize, musicGet[musicGet.Length - 1]);

                    compare1.ChildNodes[1].ChildNodes[0].InnerText = musicGet[musicGet.Length - 1];
                    compare1.ChildNodes[1].ChildNodes[1].InnerText = compare2.musicUrl;
                    compare1.ChildNodes[1].ChildNodes[2].InnerText = sceneURL + compare2.id + "/BGM/" + musicGet[musicGet.Length - 1];

                    Downloader.Instance.requestDownloadObj.Add(obj2);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(compare2.musicSize);
                }
            }
        }
        else
        {
            if (File.Exists(compare1.ChildNodes[1].ChildNodes[2].InnerText))
                File.Delete(compare1.ChildNodes[1].ChildNodes[2].InnerText);
            if (File.Exists(compare1.ChildNodes[1].ChildNodes[2].InnerText + ".tmp"))
                File.Delete(compare1.ChildNodes[1].ChildNodes[2].InnerText + ".tmp");

            compare1.ChildNodes[1].ChildNodes[0].InnerText = " ";
            compare1.ChildNodes[1].ChildNodes[1].InnerText = " ";
            compare1.ChildNodes[1].ChildNodes[2].InnerText = " ";
        }


        SaveXML();
    }
    /// <summary>
    /// 12.添加房屋画作数据
    /// </summary>
    private void AddRoomPM(RoomAllExhibitsData data, int index, int indexy)
    {
        XmlElement root = localXmlDoc.DocumentElement;
        XmlNodeList classList = root.ChildNodes;
        XmlNode PM = null;
        if (classList[index].ChildNodes.Count > 2)
        {
            PM = classList[index].ChildNodes[2];
        }
        else
        {
            PM = localXmlDoc.CreateElement("PM");
        }
        XmlElement PMList = localXmlDoc.CreateElement("PM" + (indexy + 1));
        //XmlElement PMName = localXmlDoc.CreateElement(data.name);
        XmlElement PMNo = localXmlDoc.CreateElement("PMNo");
        XmlElement PMText = localXmlDoc.CreateElement("PMText");
        XmlElement mainGraphName = localXmlDoc.CreateElement("mainGraphName");
        XmlElement mainGraphHttpUrl = localXmlDoc.CreateElement("mainGraphHttpUrl");
        XmlElement mainGraphSaveUrl = localXmlDoc.CreateElement("mainGraphSaveUrl");
        XmlElement introductionImageName = localXmlDoc.CreateElement("introductionImageName");
        XmlElement introductionImageHttpUrl = localXmlDoc.CreateElement("introductionImageHttpUrl");
        XmlElement introductionImageSaveUrl = localXmlDoc.CreateElement("introductionImageSaveUrl");
        XmlElement videoName = localXmlDoc.CreateElement("videoName");
        XmlElement videoHttpUrl = localXmlDoc.CreateElement("videoHttpUrl");
        XmlElement videoSaveUrl = localXmlDoc.CreateElement("videoSaveUrl");
        XmlElement voiceName = localXmlDoc.CreateElement("voiceName");
        XmlElement voiceHttpUrl = localXmlDoc.CreateElement("voiceHttpUrl");
        XmlElement voiceSaveUrl = localXmlDoc.CreateElement("voiceSaveUrl");
        XmlElement animationThumbnailName = localXmlDoc.CreateElement("animationThumbnailName");
        XmlElement animationThumbnailHttpUrl = localXmlDoc.CreateElement("animationThumbnailHttpUrl");
        XmlElement animationThumbnailSaveUrl = localXmlDoc.CreateElement("animationThumbnailSaveUrl");
        XmlElement status = localXmlDoc.CreateElement("status");
        XmlElement auth = localXmlDoc.CreateElement("auth");
        XmlElement version = localXmlDoc.CreateElement("version");
        XmlElement PMName = localXmlDoc.CreateElement("PMName");

        Debug.Log(data.name);


        XmlElement frameAnimationHttpUrl = localXmlDoc.CreateElement("frameAnimationHttpUrl");
        List<XmlElement> elementsHttp = new List<XmlElement>();
        XmlElement frameAnimationSaveUrl = localXmlDoc.CreateElement("frameAnimationSaveUrl");
        List<XmlElement> elementsSave = new List<XmlElement>();
        if (!string.IsNullOrEmpty(data.frameAnimationEncodeUrl))
        {
            string[] frameAnimationHttpUrls = data.frameAnimationEncodeUrl.Split(',');
            for (int j = 0; j < frameAnimationHttpUrls.Length; j++)
            {
                XmlElement ele = localXmlDoc.CreateElement("Name" + j);
                ele.InnerText = frameAnimationHttpUrls[j];
                elementsHttp.Add(ele);
            }

            string[] frameAnimationSaveUrls = new string[frameAnimationHttpUrls.Length];
            for (int j = 0; j < frameAnimationSaveUrls.Length; j++)
            {
                XmlElement ele = localXmlDoc.CreateElement("Name" + j);
                if (!string.IsNullOrEmpty(frameAnimationHttpUrls[j]))
                {
                    string[] get = frameAnimationHttpUrls[j].Split('/');
                    ele.InnerText = sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/FrameAnimationUrl/" + get[get.Length - 1];
                }

                elementsSave.Add(ele);
            }
        }


        XmlElement videoLink = localXmlDoc.CreateElement("videoLink");
        XmlElement webLink = localXmlDoc.CreateElement("webLink");


        if (!string.IsNullOrEmpty(data.exhibitsNo.ToString()))
            PMNo.InnerText = data.exhibitsNo.ToString();
        else
            PMName.InnerText = " ";
        if (!string.IsNullOrEmpty(data.text))
            PMText.InnerText = data.text;
        else
            PMText.InnerText = " ";

        if (!string.IsNullOrEmpty(data.mainGraphEncodeUrl))
        {
            string[] get = data.mainGraphEncodeUrl.Split('/');
            //get[get.Length - 1]

            mainGraphName.InnerText = get[get.Length - 1];
            mainGraphHttpUrl.InnerText = data.mainGraphEncodeUrl;
            mainGraphSaveUrl.InnerText = sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/MainGraph/" + get[get.Length - 1];
        }
        else
        {
            mainGraphName.InnerText = " ";
            mainGraphHttpUrl.InnerText = " ";
            mainGraphSaveUrl.InnerText = " ";
        }

        if (!string.IsNullOrEmpty(data.introductionImageEncodeUrl))
        {
            string[] get = data.introductionImageEncodeUrl.Split('/');
            //get[get.Length - 1]

            introductionImageName.InnerText = get[get.Length - 1];
            introductionImageHttpUrl.InnerText = data.introductionImageEncodeUrl;
            introductionImageSaveUrl.InnerText = sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/IntroductionImage/" + get[get.Length - 1];
        }
        else
        {
            introductionImageName.InnerText = " ";
            introductionImageHttpUrl.InnerText = " ";
            introductionImageSaveUrl.InnerText = " ";
        }

        if (!string.IsNullOrEmpty(data.videoUrl))
        {
            string[] get = data.videoUrl.Split('/');
            //get[get.Length - 1]

            videoName.InnerText = get[get.Length - 1];
            videoHttpUrl.InnerText = data.videoUrl;
            videoSaveUrl.InnerText = sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/Video/" + get[get.Length - 1];
        }
        else
        {
            videoName.InnerText = " ";
            videoHttpUrl.InnerText = " ";
            videoSaveUrl.InnerText = " ";
        }


        if (!string.IsNullOrEmpty(data.voiceUrl))
        {
            string[] get = data.voiceUrl.Split('/');
            //get[get.Length - 1]

            voiceName.InnerText = get[get.Length - 1];
            voiceHttpUrl.InnerText = data.voiceUrl;
            voiceSaveUrl.InnerText = sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/Voice/" + get[get.Length - 1];
        }
        else
        {
            voiceName.InnerText = " ";
            voiceHttpUrl.InnerText = " ";
            voiceSaveUrl.InnerText = " ";
        }

        if (!string.IsNullOrEmpty(data.animationThumbnailEncodeUrl))
        {
            string[] get = data.animationThumbnailEncodeUrl.Split('/');
            //get[get.Length - 1]

            animationThumbnailName.InnerText = get[get.Length - 1];
            animationThumbnailHttpUrl.InnerText = data.animationThumbnailEncodeUrl;
            animationThumbnailSaveUrl.InnerText = sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/AnimationThumbnail/" + get[get.Length - 1];
        }
        else
        {
            animationThumbnailName.InnerText = " ";
            animationThumbnailHttpUrl.InnerText = " ";
            animationThumbnailSaveUrl.InnerText = " ";
        }

        if (elementsHttp.Count != 0)
        {
            for (int k = 0; k < elementsHttp.Count; k++)
            {
                frameAnimationHttpUrl.AppendChild(elementsHttp[k]);
            }
        }
        else
        {
            frameAnimationHttpUrl.InnerText = " ";
        }

        if (elementsSave.Count != 0)
        {
            for (int k = 0; k < elementsSave.Count; k++)
            {
                frameAnimationSaveUrl.AppendChild(elementsSave[k]);
            }
        }
        else
        {
            frameAnimationSaveUrl.InnerText = " ";
        }


        if (!string.IsNullOrEmpty(data.videoLink))
            videoLink.InnerText = data.videoLink;
        else
            videoLink.InnerText = " ";
        if (!string.IsNullOrEmpty(data.webLink))
            webLink.InnerText = data.webLink;
        else
            webLink.InnerText = " ";
        if (!string.IsNullOrEmpty(data.status.ToString()))
            status.InnerText = data.status.ToString();
        else
            status.InnerText = " ";
        if (!string.IsNullOrEmpty(data.auth.ToString()))
            auth.InnerText = data.auth.ToString();
        else
            auth.InnerText = " ";
        if (!string.IsNullOrEmpty(data.version.ToString()))
            version.InnerText = data.version.ToString();
        else
            version.InnerText = " ";
        if (!string.IsNullOrEmpty(data.name))
            PMName.InnerText = data.name;
        else
            PMName.InnerText = " ";

        PMList.AppendChild(PMNo);
        PMList.AppendChild(PMText);
        PMList.AppendChild(mainGraphName);
        PMList.AppendChild(mainGraphHttpUrl);
        PMList.AppendChild(mainGraphSaveUrl);
        PMList.AppendChild(introductionImageName);
        PMList.AppendChild(introductionImageHttpUrl);
        PMList.AppendChild(introductionImageSaveUrl);
        PMList.AppendChild(videoName);
        PMList.AppendChild(videoHttpUrl);
        PMList.AppendChild(videoSaveUrl);
        PMList.AppendChild(voiceName);
        PMList.AppendChild(voiceHttpUrl);
        PMList.AppendChild(voiceSaveUrl);
        PMList.AppendChild(animationThumbnailName);
        PMList.AppendChild(animationThumbnailHttpUrl);
        PMList.AppendChild(animationThumbnailSaveUrl);
        PMList.AppendChild(frameAnimationHttpUrl);
        PMList.AppendChild(frameAnimationSaveUrl);
        PMList.AppendChild(videoLink);
        PMList.AppendChild(webLink);
        PMList.AppendChild(status);
        PMList.AppendChild(auth);
        PMList.AppendChild(version);
        PMList.AppendChild(PMName);

        PM.AppendChild(PMList);

        classList[index].AppendChild(PM);

        SaveXML();

        if (data.status.ToString() == "0")
        {
            ExistSceneDownloadURL(data.showRoomId + "/PM/" + data.exhibitsNo + "/MainGraph");
            ExistSceneDownloadURL(data.showRoomId + "/PM/" + data.exhibitsNo + "/IntroductionImage");
            ExistSceneDownloadURL(data.showRoomId + "/PM/" + data.exhibitsNo + "/Video");
            ExistSceneDownloadURL(data.showRoomId + "/PM/" + data.exhibitsNo + "/Voice");
            ExistSceneDownloadURL(data.showRoomId + "/PM/" + data.exhibitsNo + "/AnimationThumbnail");
            ExistSceneDownloadURL(data.showRoomId + "/PM/" + data.exhibitsNo + "/FrameAnimationUrl");
        }

        if (!string.IsNullOrEmpty(data.mainGraphEncodeUrl))
        {
            string[] get = data.mainGraphEncodeUrl.Split('/');
            //get[get.Length - 1]

            DownloadObj download01 = new DownloadObj().SetUrl(data.mainGraphEncodeUrl).SetParentPath(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/MainGraph").SetFileName(get[get.Length - 1]);
            //lackDownloadObj.Add(download01);
            Downloader.Instance.requestDownloadObj.Add(download01);
            Downloader.Instance.totalDownload++;
            //GetDownloadProgress.Instance.DownloadDataConvert(data.mainGraphSize);

            UnityDownloadMgr.instance.ExistsFileSize(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/MainGraph", data.mainGraphSize, get[get.Length - 1]);

        }

        if (!string.IsNullOrEmpty(data.introductionImageEncodeUrl))
        {
            string[] get = data.introductionImageEncodeUrl.Split('/');
            //get[get.Length - 1]

            DownloadObj download02 = new DownloadObj().SetUrl(data.introductionImageEncodeUrl).SetParentPath(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/IntroductionImage").SetFileName(get[get.Length - 1]);
            //lackDownloadObj.Add(download02);
            Downloader.Instance.requestDownloadObj.Add(download02);
            Downloader.Instance.totalDownload++;
            //GetDownloadProgress.Instance.DownloadDataConvert(data.introductionImageSize);

            UnityDownloadMgr.instance.ExistsFileSize(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/IntroductionImage", data.introductionImageSize, get[get.Length - 1]);
        }


        if (!string.IsNullOrEmpty(data.videoUrl))
        {
            string[] get = data.videoUrl.Split('/');
            //get[get.Length - 1]

            DownloadObj download03 = new DownloadObj().SetUrl(data.videoUrl).SetParentPath(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/Video").SetFileName(get[get.Length - 1]);
            //lackDownloadObj.Add(download03);
            Downloader.Instance.requestDownloadObj.Add(download03);
            Downloader.Instance.totalDownload++;
            //GetDownloadProgress.Instance.DownloadDataConvert(data.videoSize);
            UnityDownloadMgr.instance.ExistsFileSize(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/Video", data.videoSize, get[get.Length - 1]);

        }


        if (!string.IsNullOrEmpty(data.voiceUrl))
        {
            string[] get = data.voiceUrl.Split('/');
            //get[get.Length - 1]

            DownloadObj download04 = new DownloadObj().SetUrl(data.voiceUrl).SetParentPath(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/Voice").SetFileName(get[get.Length - 1]);
            //lackDownloadObj.Add(download04);
            Downloader.Instance.requestDownloadObj.Add(download04);
            Downloader.Instance.totalDownload++;
            //GetDownloadProgress.Instance.DownloadDataConvert(data.voiceSize);
            UnityDownloadMgr.instance.ExistsFileSize(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/Voice", data.voiceSize, get[get.Length - 1]);
        }


        if (!string.IsNullOrEmpty(data.animationThumbnailEncodeUrl))
        {
            string[] get = data.animationThumbnailEncodeUrl.Split('/');
            //get[get.Length - 1]

            DownloadObj download05 = new DownloadObj().SetUrl(data.animationThumbnailEncodeUrl).SetParentPath(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/AnimationThumbnail").SetFileName(get[get.Length - 1]);
            //lackDownloadObj.Add(download05);
            Downloader.Instance.requestDownloadObj.Add(download05);
            Downloader.Instance.totalDownload++;
            //GetDownloadProgress.Instance.DownloadDataConvert(data.animationThumbnailSize);
            UnityDownloadMgr.instance.ExistsFileSize(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/AnimationThumbnail", data.animationThumbnailSize, get[get.Length - 1]);
        }

        if (!string.IsNullOrEmpty(data.frameAnimationEncodeUrl))
        {
            string[] urls = data.frameAnimationEncodeUrl.Split(',');
            string[] names = new string[urls.Length];
            DownloadObj[] frames = new DownloadObj[urls.Length];
            for (int k = 0; k < urls.Length; k++)
            {
                string[] get = urls[k].Split('/');
                names[k] = get[get.Length - 1];
                frames[k] = new DownloadObj().SetUrl(urls[k]).SetParentPath(sceneURL + data.showRoomId + "/PM/" + data.exhibitsNo + "/FrameAnimationUrl").SetFileName(names[k]);
                //lackDownloadObj.Add(frames[k]);
                Downloader.Instance.requestDownloadObj.Add(frames[k]);
                Downloader.Instance.totalDownload++;
            }

            UnityDownloadMgr.instance.FileSizeAdd(data.frameAnimationSize);
            //GetDownloadProgress.Instance.DownloadDataConvert(data.frameAnimationSize);
        }
    }
    /// <summary>
    /// 13.对比房屋画作数据
    /// </summary>
    private void CompareRoomPM(XmlNode compare1, RoomAllExhibitsData compare2, int index, int RMIndex)
    {

        if (compare1.ChildNodes[24].InnerText != compare2.name)
        {
            Debug.Log("compare1:" + compare1.ChildNodes[24].InnerText);
            Debug.Log("compare2:" + compare2.name);
            Debug.Log("compare3:" + sceneURL + allRoomResults[index].id + "/PM/" + compare1.ChildNodes[24].InnerText);
            Debug.Log("compare4:" + Directory.Exists(sceneURL + allRoomResults[index].id + "/PM/" + compare1.ChildNodes[24].InnerText));

            if (compare1.ChildNodes[24].InnerText != "" && compare1.ChildNodes[24].InnerText != " " &&
                !string.IsNullOrEmpty(compare1.ChildNodes[24].InnerText))
            {
                if (Directory.Exists(sceneURL + allRoomResults[index].id + "/PM/" + compare1.ChildNodes[24].InnerText))
                {
                    DeleteFiles(sceneURL + allRoomResults[index].id + "/PM/" + compare1.ChildNodes[24].InnerText);
                    //Directory.Delete(sceneURL + allRoomResults[index].name + "/PM/" + compare2.name);
                }
            }




            XmlElement root = localXmlDoc.DocumentElement;
            XmlNodeList classList = root.ChildNodes;

            XmlElement PMList = localXmlDoc.CreateElement("PM" + (RMIndex + 1));
            XmlElement PMNo = localXmlDoc.CreateElement("PMNo");
            XmlElement PMText = localXmlDoc.CreateElement("PMText");
            XmlElement mainGraphName = localXmlDoc.CreateElement("mainGraphName");
            XmlElement mainGraphHttpUrl = localXmlDoc.CreateElement("mainGraphHttpUrl");
            XmlElement mainGraphSaveUrl = localXmlDoc.CreateElement("mainGraphSaveUrl");
            XmlElement introductionImageName = localXmlDoc.CreateElement("introductionImageName");
            XmlElement introductionImageHttpUrl = localXmlDoc.CreateElement("introductionImageHttpUrl");
            XmlElement introductionImageSaveUrl = localXmlDoc.CreateElement("introductionImageSaveUrl");
            XmlElement videoName = localXmlDoc.CreateElement("videoName");
            XmlElement videoHttpUrl = localXmlDoc.CreateElement("videoHttpUrl");
            XmlElement videoSaveUrl = localXmlDoc.CreateElement("videoSaveUrl");
            XmlElement voiceName = localXmlDoc.CreateElement("voiceName");
            XmlElement voiceHttpUrl = localXmlDoc.CreateElement("voiceHttpUrl");
            XmlElement voiceSaveUrl = localXmlDoc.CreateElement("voiceSaveUrl");
            XmlElement animationThumbnailName = localXmlDoc.CreateElement("animationThumbnailName");
            XmlElement animationThumbnailHttpUrl = localXmlDoc.CreateElement("animationThumbnailHttpUrl");
            XmlElement animationThumbnailSaveUrl = localXmlDoc.CreateElement("animationThumbnailSaveUrl");

            XmlElement frameAnimationHttpUrl = localXmlDoc.CreateElement("frameAnimationHttpUrl");
            XmlElement frameAnimationSaveUrl = localXmlDoc.CreateElement("frameAnimationSaveUrl");
            List<XmlElement> elementsHttp = new List<XmlElement>();
            List<XmlElement> elementsSave = new List<XmlElement>();
            if (!string.IsNullOrEmpty(compare2.frameAnimationEncodeUrl))
            {
                string[] frameAnimationHttpUrls = compare2.frameAnimationEncodeUrl.Split(',');
                for (int j = 0; j < frameAnimationHttpUrls.Length; j++)
                {
                    XmlElement ele = localXmlDoc.CreateElement("Name" + j);
                    ele.InnerText = frameAnimationHttpUrls[j];
                    elementsHttp.Add(ele);
                }
                string[] frameAnimationSaveUrls = new string[frameAnimationHttpUrls.Length];
                for (int j = 0; j < frameAnimationSaveUrls.Length; j++)
                {
                    XmlElement ele = localXmlDoc.CreateElement("Name" + j);
                    if (!string.IsNullOrEmpty(frameAnimationHttpUrls[j]))
                    {
                        string[] get = frameAnimationHttpUrls[j].Split('/');
                        ele.InnerText = sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl/" + get[get.Length - 1];
                    }

                    elementsSave.Add(ele);
                }
            }

            XmlElement videoLink = localXmlDoc.CreateElement("videoLink");
            XmlElement webLink = localXmlDoc.CreateElement("webLink");
            XmlElement status = localXmlDoc.CreateElement("status");
            XmlElement auth = localXmlDoc.CreateElement("auth");
            XmlElement version = localXmlDoc.CreateElement("version");
            XmlElement PMName = localXmlDoc.CreateElement("PMName");

            if (!string.IsNullOrEmpty(compare2.exhibitsNo.ToString()))
                PMNo.InnerText = compare2.exhibitsNo.ToString();
            else
                PMNo.InnerText = " ";
            if (!string.IsNullOrEmpty(compare2.text))
                PMText.InnerText = compare2.text;
            else
                PMText.InnerText = " ";

            if (!string.IsNullOrEmpty(compare2.mainGraphEncodeUrl))
            {
                string[] get = compare2.mainGraphEncodeUrl.Split('/');
                //get[get.Length - 1]

                mainGraphName.InnerText = get[get.Length - 1];
                mainGraphHttpUrl.InnerText = compare2.mainGraphEncodeUrl;
                mainGraphSaveUrl.InnerText = sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/MainGraph/" + get[get.Length - 1];
            }
            else
            {
                mainGraphName.InnerText = " ";
                mainGraphHttpUrl.InnerText = " ";
                mainGraphSaveUrl.InnerText = " ";
            }

            if (!string.IsNullOrEmpty(compare2.introductionImageEncodeUrl))
            {
                string[] get = compare2.introductionImageEncodeUrl.Split('/');
                //get[get.Length - 1]

                introductionImageName.InnerText = get[get.Length - 1];
                introductionImageHttpUrl.InnerText = compare2.introductionImageEncodeUrl;
                introductionImageSaveUrl.InnerText = sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/IntroductionImage/" + get[get.Length - 1];
            }
            else
            {
                introductionImageName.InnerText = " ";
                introductionImageHttpUrl.InnerText = " ";
                introductionImageSaveUrl.InnerText = " ";
            }

            if (!string.IsNullOrEmpty(compare2.videoUrl))
            {
                string[] get = compare2.videoUrl.Split('/');
                //get[get.Length - 1]

                videoName.InnerText = get[get.Length - 1];
                videoHttpUrl.InnerText = compare2.videoUrl;
                videoSaveUrl.InnerText = sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/Video/" + get[get.Length - 1];
            }
            else
            {
                videoName.InnerText = " ";
                videoHttpUrl.InnerText = " ";
                videoSaveUrl.InnerText = " ";
            }


            if (!string.IsNullOrEmpty(compare2.voiceUrl))
            {
                string[] get = compare2.voiceUrl.Split('/');
                //get[get.Length - 1]

                voiceName.InnerText = get[get.Length - 1];
                voiceHttpUrl.InnerText = compare2.voiceUrl;
                voiceSaveUrl.InnerText = sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/Voice/" + get[get.Length - 1];
            }
            else
            {
                voiceName.InnerText = " ";
                voiceHttpUrl.InnerText = " ";
                voiceSaveUrl.InnerText = " ";
            }

            if (!string.IsNullOrEmpty(compare2.animationThumbnailEncodeUrl))
            {
                string[] get = compare2.animationThumbnailEncodeUrl.Split('/');
                //get[get.Length - 1]

                animationThumbnailName.InnerText = get[get.Length - 1];
                animationThumbnailHttpUrl.InnerText = compare2.animationThumbnailEncodeUrl;
                animationThumbnailSaveUrl.InnerText = sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail/" + get[get.Length - 1];
            }
            else
            {
                animationThumbnailName.InnerText = " ";
                animationThumbnailHttpUrl.InnerText = " ";
                animationThumbnailSaveUrl.InnerText = " ";
            }


            if (elementsHttp.Count != 0)
            {
                for (int k = 0; k < elementsHttp.Count; k++)
                {
                    frameAnimationHttpUrl.AppendChild(elementsHttp[k]);
                }
                for (int k = 0; k < elementsSave.Count; k++)
                {
                    frameAnimationSaveUrl.AppendChild(elementsSave[k]);
                }
            }
            else
            {
                frameAnimationHttpUrl.InnerText = " ";
                frameAnimationSaveUrl.InnerText = " ";
            }


            if (!string.IsNullOrEmpty(compare2.videoLink))
                videoLink.InnerText = compare2.videoLink;
            else
                videoLink.InnerText = " ";
            if (!string.IsNullOrEmpty(compare2.webLink))
                webLink.InnerText = compare2.webLink;
            else
                webLink.InnerText = " ";
            if (!string.IsNullOrEmpty(compare2.status.ToString()))
                status.InnerText = compare2.status.ToString();
            else
                status.InnerText = " ";
            if (!string.IsNullOrEmpty(compare2.auth.ToString()))
                auth.InnerText = compare2.auth.ToString();
            else
                auth.InnerText = " ";
            if (!string.IsNullOrEmpty(compare2.version.ToString()))
                version.InnerText = compare2.version.ToString();
            else
                version.InnerText = " ";
            if (!string.IsNullOrEmpty(compare2.name))
                PMName.InnerText = compare2.name;
            else
                PMName.InnerText = " ";


            PMList.AppendChild(PMNo);
            PMList.AppendChild(PMText);
            PMList.AppendChild(mainGraphName);
            PMList.AppendChild(mainGraphHttpUrl);
            PMList.AppendChild(mainGraphSaveUrl);
            PMList.AppendChild(introductionImageName);
            PMList.AppendChild(introductionImageHttpUrl);
            PMList.AppendChild(introductionImageSaveUrl);
            PMList.AppendChild(videoName);
            PMList.AppendChild(videoHttpUrl);
            PMList.AppendChild(videoSaveUrl);
            PMList.AppendChild(voiceName);
            PMList.AppendChild(voiceHttpUrl);
            PMList.AppendChild(voiceSaveUrl);
            PMList.AppendChild(animationThumbnailName);
            PMList.AppendChild(animationThumbnailHttpUrl);
            PMList.AppendChild(animationThumbnailSaveUrl);
            PMList.AppendChild(frameAnimationHttpUrl);
            PMList.AppendChild(frameAnimationSaveUrl);
            PMList.AppendChild(videoLink);
            PMList.AppendChild(webLink);
            PMList.AppendChild(status);
            PMList.AppendChild(auth);
            PMList.AppendChild(version);
            PMList.AppendChild(PMName);

            string[] RoomNames;
            int indexxxx = 0;
            for (int i = 0; i < classList.Count; i++)
            {
                RoomNames = classList[i].Name.Split('_');
                if (currentSelectSceneData.sceneID == RoomNames[3])
                {
                    indexxxx = i;
                    break;
                }
            }

            XmlNode nodes = classList[indexxxx].ChildNodes[2].AppendChild(PMList);
            classList[indexxxx].ChildNodes[2].InsertAfter(nodes, compare1);
            classList[indexxxx].ChildNodes[2].RemoveChild(compare1);




            ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/MainGraph");
            ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/IntroductionImage");
            ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/Video");
            ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/Voice");
            ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail");
            ExistSceneDownloadURL(allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl");


            if (!string.IsNullOrEmpty(compare2.mainGraphEncodeUrl))
            {
                string[] get = compare2.mainGraphEncodeUrl.Split('/');
                //get[get.Length - 1]

                DownloadObj download01 = new DownloadObj().SetUrl(compare2.mainGraphEncodeUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/MainGraph").SetFileName(get[get.Length - 1]);
                //lackDownloadObj.Add(download01);
                Downloader.Instance.requestDownloadObj.Add(download01);
                Downloader.Instance.totalDownload++;
                //GetDownloadProgress.Instance.DownloadDataConvert(compare2.mainGraphSize);
                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/MainGraph", compare2.mainGraphSize, get[get.Length - 1]);
            }

            if (!string.IsNullOrEmpty(compare2.introductionImageEncodeUrl))
            {
                string[] get = compare2.introductionImageEncodeUrl.Split('/');
                //get[get.Length - 1]

                DownloadObj download02 = new DownloadObj().SetUrl(compare2.introductionImageEncodeUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/IntroductionImage").SetFileName(get[get.Length - 1]);
                //lackDownloadObj.Add(download02);
                Downloader.Instance.requestDownloadObj.Add(download02);
                Downloader.Instance.totalDownload++;
                //GetDownloadProgress.Instance.DownloadDataConvert(compare2.introductionImageSize);
                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/IntroductionImage", compare2.introductionImageSize, get[get.Length - 1]);

            }


            if (!string.IsNullOrEmpty(compare2.videoUrl))
            {
                string[] get = compare2.videoUrl.Split('/');
                //get[get.Length - 1]

                DownloadObj download03 = new DownloadObj().SetUrl(compare2.videoUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/Video").SetFileName(get[get.Length - 1]);
                //lackDownloadObj.Add(download03);
                Downloader.Instance.requestDownloadObj.Add(download03);
                Downloader.Instance.totalDownload++;
                //GetDownloadProgress.Instance.DownloadDataConvert(compare2.videoSize);
                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/Video", compare2.videoSize, get[get.Length - 1]);
            }


            if (!string.IsNullOrEmpty(compare2.voiceUrl))
            {
                string[] get = compare2.voiceUrl.Split('/');
                //get[get.Length - 1]

                DownloadObj download04 = new DownloadObj().SetUrl(compare2.voiceUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/Voice").SetFileName(get[get.Length - 1]);
                //lackDownloadObj.Add(download04);
                Downloader.Instance.requestDownloadObj.Add(download04);
                Downloader.Instance.totalDownload++;
                //GetDownloadProgress.Instance.DownloadDataConvert(compare2.voiceSize);
                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/Voice", compare2.voiceSize, get[get.Length - 1]);
            }


            if (!string.IsNullOrEmpty(compare2.animationThumbnailEncodeUrl))
            {
                string[] get = compare2.animationThumbnailEncodeUrl.Split('/');
                //get[get.Length - 1]

                DownloadObj download05 = new DownloadObj().SetUrl(compare2.animationThumbnailEncodeUrl).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail").SetFileName(get[get.Length - 1]);
                //lackDownloadObj.Add(download05);
                Downloader.Instance.requestDownloadObj.Add(download05);
                Downloader.Instance.totalDownload++;
                //GetDownloadProgress.Instance.DownloadDataConvert(compare2.animationThumbnailSize);
                UnityDownloadMgr.instance.ExistsFileSize(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail", compare2.animationThumbnailSize, get[get.Length - 1]);

            }

            if (!string.IsNullOrEmpty(compare2.frameAnimationEncodeUrl))
            {
                string[] urls = compare2.frameAnimationEncodeUrl.Split(',');
                string[] names = new string[urls.Length];
                DownloadObj[] frames = new DownloadObj[urls.Length];
                for (int k = 0; k < urls.Length; k++)
                {
                    string[] get = urls[k].Split('/');
                    names[k] = get[get.Length - 1];
                    frames[k] = new DownloadObj().SetUrl(urls[k]).SetParentPath(sceneURL + allRoomResults[index].id + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl").SetFileName(names[k]);
                    //lackDownloadObj.Add(frames[k]);
                    Downloader.Instance.requestDownloadObj.Add(frames[k]);
                    Downloader.Instance.totalDownload++;
                }
                //GetDownloadProgress.Instance.DownloadDataConvert(compare2.frameAnimationSize);
                UnityDownloadMgr.instance.FileSizeAdd(compare2.frameAnimationSize);
            }

        }
        else
        {
            if (!string.IsNullOrEmpty(compare2.exhibitsNo.ToString()))
                compare1.ChildNodes[0].InnerText = compare2.exhibitsNo.ToString();
            else
                compare1.ChildNodes[0].InnerText = " ";

            if (!string.IsNullOrEmpty(compare2.text))
                compare1.ChildNodes[1].InnerText = compare2.text;
            else
                compare1.ChildNodes[1].InnerText = " ";

            if (!string.IsNullOrEmpty(compare2.mainGraphEncodeUrl))
            {
                if (compare1.ChildNodes[3].InnerText != compare2.mainGraphEncodeUrl)
                {
                    if (compare2.name != "" && compare2.name != " " && !string.IsNullOrEmpty(compare2.name))
                        DeleteFiles(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/MainGraph");
                    //Directory.Delete(sceneURL + allRoomResults[index].name + "/PM/" + compare2.name + "/MainGraph");

                    string[] get = compare2.mainGraphEncodeUrl.Split('/');
                    //get[get.Length - 1]

                    compare1.ChildNodes[2].InnerText = get[get.Length - 1];
                    compare1.ChildNodes[3].InnerText = compare2.mainGraphEncodeUrl;
                    compare1.ChildNodes[4].InnerText = sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo +
                                                       "/MainGraph/" + get[get.Length - 1];
                    ExistSceneDownloadURL(compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/MainGraph");
                    DownloadObj download01 = new DownloadObj().SetUrl(compare2.mainGraphEncodeUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/MainGraph").SetFileName(get[get.Length - 1]);
                    //lackDownloadObj.Add(download01);
                    Downloader.Instance.requestDownloadObj.Add(download01);
                    Downloader.Instance.totalDownload++;

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/MainGraph", compare2.mainGraphSize, get[get.Length - 1]);

                    //GetDownloadProgress.Instance.DownloadDataConvert(compare2.mainGraphSize);
                }
                else
                {
                    string[] get = compare2.mainGraphEncodeUrl.Split('/');

                    if (!File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/MainGraph" + "/" + get[get.Length - 1]))
                    {
                        if (File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/MainGraph" + "/" + get[get.Length - 1] + ".tmp"))
                            File.Delete(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/MainGraph" + "/" + get[get.Length - 1] + ".tmp");

                        DownloadObj obj = new DownloadObj().SetUrl(compare2.mainGraphEncodeUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/MainGraph").SetFileName(get[get.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(obj);
                        Downloader.Instance.totalDownload++;

                        UnityDownloadMgr.instance.FileSizeAdd(compare2.mainGraphSize);
                    }
                }
            }
            else
            {

                if (compare1.ChildNodes[4].InnerText == "" || compare1.ChildNodes[4].InnerText == " ")
                {
                    Debug.Log("删除空文件A");
                }
                else
                {
                    Debug.Log("删除空文件B");
                    if (File.Exists(compare1.ChildNodes[4].InnerText))
                        File.Delete(compare1.ChildNodes[4].InnerText);
                    if (File.Exists(compare1.ChildNodes[4].InnerText + ".tmp"))
                        File.Delete(compare1.ChildNodes[4].InnerText + ".tmp");
                }

                compare1.ChildNodes[2].InnerText = " ";
                compare1.ChildNodes[3].InnerText = " ";
                compare1.ChildNodes[4].InnerText = " ";
            }

            if (!string.IsNullOrEmpty(compare2.introductionImageEncodeUrl))
            {
                Debug.Log("compare1_introductionImageEncodeUrl:" + compare1.ChildNodes[6].InnerText);
                Debug.Log("compare2_introductionImageEncodeUrl:" + compare2.introductionImageEncodeUrl);

                if (compare1.ChildNodes[6].InnerText != compare2.introductionImageEncodeUrl)
                {
                    if (compare2.name != "" && compare2.name != " " && !string.IsNullOrEmpty(compare2.name))
                        DeleteFiles(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/IntroductionImage");

                    string[] get = compare2.introductionImageEncodeUrl.Split('/');
                    //get[get.Length - 1]

                    //Directory.Delete(sceneURL + allRoomResults[index].name + "/PM/" + compare2.name + "/IntroductionImage");
                    compare1.ChildNodes[5].InnerText = get[get.Length - 1];
                    compare1.ChildNodes[6].InnerText = compare2.introductionImageEncodeUrl;
                    compare1.ChildNodes[7].InnerText = sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo +
                                                       "/IntroductionImage/" + get[get.Length - 1];
                    ExistSceneDownloadURL(compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/IntroductionImage");
                    DownloadObj download01 = new DownloadObj().SetUrl(compare2.introductionImageEncodeUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/IntroductionImage").SetFileName(get[get.Length - 1]);
                    //lackDownloadObj.Add(download01);
                    Downloader.Instance.requestDownloadObj.Add(download01);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(compare2.introductionImageSize);

                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/IntroductionImage", compare2.introductionImageSize, get[get.Length - 1]);

                }
                else
                {
                    string[] get = compare2.introductionImageEncodeUrl.Split('/');

                    Debug.Log("检测debug：" + sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/IntroductionImage" + "/" + get[get.Length - 1]);

                    if (!File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/IntroductionImage" + "/" + get[get.Length - 1]))
                    {
                        if (File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/IntroductionImage" + "/" + get[get.Length - 1] + ".tmp"))
                            File.Delete(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/IntroductionImage" + "/" + get[get.Length - 1] + ".tmp");

                        DownloadObj obj = new DownloadObj().SetUrl(compare2.introductionImageEncodeUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/IntroductionImage").SetFileName(get[get.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(obj);
                        Downloader.Instance.totalDownload++;

                        UnityDownloadMgr.instance.FileSizeAdd(compare2.introductionImageSize);
                    }
                }
            }
            else
            {
                if (compare1.ChildNodes[7].InnerText == "" || compare1.ChildNodes[7].InnerText == " ")
                {
                    Debug.Log("检测不到简介文件");
                }
                else
                {
                    Debug.Log("检测到简介文件，并判断是否有缓存数据，有就删掉");

                    if (File.Exists(compare1.ChildNodes[7].InnerText))
                        File.Delete(compare1.ChildNodes[7].InnerText);
                    if (File.Exists(compare1.ChildNodes[7].InnerText + ".tmp"))
                        File.Delete(compare1.ChildNodes[7].InnerText + ".tmp");
                }

                compare1.ChildNodes[5].InnerText = " ";
                compare1.ChildNodes[6].InnerText = " ";
                compare1.ChildNodes[7].InnerText = " ";
            }

            if (!string.IsNullOrEmpty(compare2.videoUrl))
            {
                if (compare1.ChildNodes[9].InnerText != compare2.videoUrl)
                {
                    if (compare2.name != "" && compare2.name != " " && !string.IsNullOrEmpty(compare2.name))
                        DeleteFiles(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Video");
                    //Directory.Delete(sceneURL + allRoomResults[index].name + "/PM/" + compare2.name + "/Video");

                    string[] get = compare2.videoUrl.Split('/');
                    //get[get.Length - 1]

                    compare1.ChildNodes[8].InnerText = get[get.Length - 1];
                    compare1.ChildNodes[9].InnerText = compare2.videoUrl;
                    compare1.ChildNodes[10].InnerText = sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo +
                                                       "/Video/" + get[get.Length - 1];
                    ExistSceneDownloadURL(compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Video");
                    DownloadObj download01 = new DownloadObj().SetUrl(compare2.videoUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Video").SetFileName(get[get.Length - 1]);
                    //lackDownloadObj.Add(download01);
                    Downloader.Instance.requestDownloadObj.Add(download01);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(compare2.videoSize);
                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Video", compare2.videoSize, get[get.Length - 1]);

                }
                else
                {
                    string[] get = compare2.videoUrl.Split('/');

                    if (!File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Video" + "/" + get[get.Length - 1]))
                    {
                        if (File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Video" + "/" + get[get.Length - 1] + ".tmp"))
                            File.Delete(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Video" + "/" + get[get.Length - 1] + ".tmp");

                        DownloadObj obj = new DownloadObj().SetUrl(compare2.videoUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Video").SetFileName(get[get.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(obj);
                        Downloader.Instance.totalDownload++;

                        UnityDownloadMgr.instance.FileSizeAdd(compare2.videoSize);
                    }
                }
            }
            else
            {
                if (compare1.ChildNodes[10].InnerText == "" || compare1.ChildNodes[10].InnerText == " ")
                {
                }
                else
                {
                    if (File.Exists(compare1.ChildNodes[10].InnerText))
                        File.Delete(compare1.ChildNodes[10].InnerText);
                    if (File.Exists(compare1.ChildNodes[10].InnerText + ".tmp"))
                        File.Delete(compare1.ChildNodes[10].InnerText + ".tmp");
                }

                compare1.ChildNodes[8].InnerText = " ";
                compare1.ChildNodes[9].InnerText = " ";
                compare1.ChildNodes[10].InnerText = " ";
            }

            if (!string.IsNullOrEmpty(compare2.voiceUrl))
            {
                if (compare1.ChildNodes[12].InnerText != compare2.voiceUrl)
                {
                    if (compare2.name != "" && compare2.name != " " && !string.IsNullOrEmpty(compare2.name))
                        DeleteFiles(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Voice");
                    //Directory.Delete(sceneURL + allRoomResults[index].name + "/PM/" + compare2.name + "/Voice");

                    string[] get = compare2.voiceUrl.Split('/');
                    //get[get.Length - 1]

                    compare1.ChildNodes[11].InnerText = get[get.Length - 1];
                    compare1.ChildNodes[12].InnerText = compare2.voiceUrl;
                    compare1.ChildNodes[13].InnerText = sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo +
                                                        "/Voice/" + get[get.Length - 1];
                    ExistSceneDownloadURL(compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Voice");
                    DownloadObj download01 = new DownloadObj().SetUrl(compare2.voiceUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Voice").SetFileName(get[get.Length - 1]);
                    //lackDownloadObj.Add(download01);
                    Downloader.Instance.requestDownloadObj.Add(download01);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(compare2.voiceSize);
                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Voice", compare2.voiceSize, get[get.Length - 1]);

                }
                else
                {
                    string[] get = compare2.voiceUrl.Split('/');

                    if (!File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Voice" + "/" + get[get.Length - 1]))
                    {
                        if (File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Voice" + "/" + get[get.Length - 1] + ".tmp"))
                            File.Delete(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Voice" + "/" + get[get.Length - 1] + ".tmp");

                        DownloadObj obj = new DownloadObj().SetUrl(compare2.voiceUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/Voice").SetFileName(get[get.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(obj);
                        Downloader.Instance.totalDownload++;

                        UnityDownloadMgr.instance.FileSizeAdd(compare2.voiceSize);
                    }
                }
            }
            else
            {
                if (compare1.ChildNodes[13].InnerText == "" || compare1.ChildNodes[13].InnerText == " ")
                {
                }
                else
                {
                    if (File.Exists(compare1.ChildNodes[13].InnerText))
                        File.Delete(compare1.ChildNodes[13].InnerText);
                    if (File.Exists(compare1.ChildNodes[13].InnerText + ".tmp"))
                        File.Delete(compare1.ChildNodes[13].InnerText + ".tmp");
                }

                compare1.ChildNodes[11].InnerText = " ";
                compare1.ChildNodes[12].InnerText = " ";
                compare1.ChildNodes[13].InnerText = " ";
            }

            if (!string.IsNullOrEmpty(compare2.animationThumbnailEncodeUrl))
            {
                if (compare1.ChildNodes[15].InnerText != compare2.animationThumbnailEncodeUrl)
                {
                    Debug.Log(compare1.ChildNodes[15].InnerText);
                    Debug.Log(compare2.animationThumbnailEncodeUrl);
                    if (compare2.name != "" && compare2.name != " " && !string.IsNullOrEmpty(compare2.name))
                    {
                        DeleteFiles(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail");
                        //Directory.Delete(sceneURL + allRoomResults[index].name + "/PM/" + compare2.name + "/AnimationThumbnail");
                    }


                    string[] get = compare2.animationThumbnailEncodeUrl.Split('/');
                    //get[get.Length - 1]

                    compare1.ChildNodes[14].InnerText = get[get.Length - 1];
                    compare1.ChildNodes[15].InnerText = compare2.animationThumbnailEncodeUrl;
                    compare1.ChildNodes[16].InnerText = sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo +
                                                        "/AnimationThumbnail/" + get[get.Length - 1];
                    ExistSceneDownloadURL(compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail");
                    DownloadObj download01 = new DownloadObj().SetUrl(compare2.animationThumbnailEncodeUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail").SetFileName(get[get.Length - 1]);
                    //lackDownloadObj.Add(download01);
                    Downloader.Instance.requestDownloadObj.Add(download01);
                    Downloader.Instance.totalDownload++;
                    //GetDownloadProgress.Instance.DownloadDataConvert(compare2.animationThumbnailSize);
                    UnityDownloadMgr.instance.ExistsFileSize(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail", compare2.animationThumbnailSize, get[get.Length - 1]);

                }
                else
                {
                    string[] get = compare2.animationThumbnailEncodeUrl.Split('/');

                    if (!File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail" + "/" + get[get.Length - 1]))
                    {
                        if (File.Exists(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail" + "/" + get[get.Length - 1] + ".tmp"))
                            File.Delete(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail" + "/" + get[get.Length - 1] + ".tmp");

                        DownloadObj obj = new DownloadObj().SetUrl(compare2.animationThumbnailEncodeUrl).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/AnimationThumbnail").SetFileName(get[get.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(obj);
                        Downloader.Instance.totalDownload++;

                        UnityDownloadMgr.instance.FileSizeAdd(compare2.animationThumbnailSize);
                    }
                }
            }
            else
            {
                if (compare1.ChildNodes[16].InnerText == "" || compare1.ChildNodes[16].InnerText == " ")
                {
                }
                else
                {
                    if (File.Exists(compare1.ChildNodes[16].InnerText))
                        File.Delete(compare1.ChildNodes[16].InnerText);
                    if (File.Exists(compare1.ChildNodes[16].InnerText + ".tmp"))
                        File.Delete(compare1.ChildNodes[16].InnerText + ".tmp");
                }

                compare1.ChildNodes[14].InnerText = " ";
                compare1.ChildNodes[15].InnerText = " ";
                compare1.ChildNodes[16].InnerText = " ";
            }

            if (!string.IsNullOrEmpty(compare2.frameAnimationEncodeUrl))
            {
                string[] frameUrls = compare2.frameAnimationEncodeUrl.Split(',');
                if (frameUrls.Length >= compare1.ChildNodes[17].ChildNodes.Count)
                {
                    bool isAddSize = false;
                    for (int i = 0; i < frameUrls.Length; i++)
                    {
                        if (i <= compare1.ChildNodes[17].ChildNodes.Count - 1)
                        {
                            if (frameUrls[i] != compare1.ChildNodes[17].ChildNodes[i].InnerText)
                            {
                                if (File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText))
                                    File.Delete(compare1.ChildNodes[18].ChildNodes[i].InnerText);
                                if (File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp"))
                                    File.Delete(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp");

                                string[] get = frameUrls[i].Split('/');
                                DownloadObj download01 = new DownloadObj().SetUrl(frameUrls[i]).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl").SetFileName(get[get.Length - 1]);
                                //lackDownloadObj.Add(download01);
                                Downloader.Instance.requestDownloadObj.Add(download01);
                                Downloader.Instance.totalDownload++;

                                isAddSize = true;

                                compare1.ChildNodes[17].ChildNodes[i].InnerText = frameUrls[i];
                                compare1.ChildNodes[18].ChildNodes[i].InnerText = sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl/" + get[get.Length - 1];
                            }
                            else
                            {
                                if (!File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText))
                                {
                                    if (File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp"))
                                        File.Delete(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp");
                                    string[] get = frameUrls[i].Split('/');
                                    DownloadObj obj = new DownloadObj().SetUrl(frameUrls[i]).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl").SetFileName(get[get.Length - 1]);
                                    Downloader.Instance.requestDownloadObj.Add(obj);
                                    Downloader.Instance.totalDownload++;

                                    isAddSize = true;
                                }
                            }
                        }
                        else
                        {
                            string[] get = frameUrls[i].Split('/');
                            DownloadObj download01 = new DownloadObj().SetUrl(frameUrls[i]).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl").SetFileName(get[get.Length - 1]);
                            //lackDownloadObj.Add(download01);
                            Downloader.Instance.requestDownloadObj.Add(download01);
                            Downloader.Instance.totalDownload++;

                            isAddSize = true;

                            XmlElement ele = localXmlDoc.CreateElement("Name" + i);
                            ele.InnerText = frameUrls[i];
                            compare1.ChildNodes[17].AppendChild(ele);

                            XmlElement ele1 = localXmlDoc.CreateElement("Name" + i);
                            ele1.InnerText = sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl/" + get[get.Length - 1];
                            compare1.ChildNodes[18].AppendChild(ele1);
                        }
                    }

                    if (isAddSize)
                        UnityDownloadMgr.instance.FileSizeAdd(compare2.frameAnimationSize);

                }
                else if (frameUrls.Length < compare1.ChildNodes[17].ChildNodes.Count)
                {
                    bool isAddSize = false;
                    for (int i = 0; i < compare1.ChildNodes[17].ChildNodes.Count; i++)
                    {
                        if (i > frameUrls.Length - 1)
                        {
                            if (File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText))
                                File.Delete(compare1.ChildNodes[18].ChildNodes[i].InnerText);
                            if (File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp"))
                                File.Delete(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp");

                            compare1.ChildNodes[17].RemoveChild(compare1.ChildNodes[17].ChildNodes[i]);
                            compare1.ChildNodes[18].RemoveChild(compare1.ChildNodes[18].ChildNodes[i]);
                        }
                        else
                        {
                            if (frameUrls[i] != compare1.ChildNodes[17].ChildNodes[i].InnerText)
                            {
                                if (File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText))
                                    File.Delete(compare1.ChildNodes[18].ChildNodes[i].InnerText);
                                if (File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp"))
                                    File.Delete(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp");
                                string[] get = frameUrls[i].Split('/');
                                DownloadObj download01 = new DownloadObj().SetUrl(frameUrls[i]).SetParentPath(sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl").SetFileName(get[get.Length - 1]);
                                //lackDownloadObj.Add(download01);
                                Downloader.Instance.requestDownloadObj.Add(download01);
                                Downloader.Instance.totalDownload++;

                                isAddSize = true;

                                compare1.ChildNodes[17].ChildNodes[i].InnerText = frameUrls[i];
                                compare1.ChildNodes[18].ChildNodes[i].InnerText = sceneURL + compare2.showRoomId + "/PM/" + compare2.exhibitsNo + "/FrameAnimationUrl/" + get[get.Length - 1];


                            }
                        }
                    }
                    if (isAddSize)
                        UnityDownloadMgr.instance.FileSizeAdd(compare2.frameAnimationSize);

                }
            }
            else
            {
                Debug.Log("检测不到Q：" + compare1.Name + "__" + compare1.ChildNodes[18].InnerText);
                if (compare1.ChildNodes[18].InnerText == "" || compare1.ChildNodes[18].InnerText == " ")
                {
                    Debug.Log("检测不到A");
                }
                else
                {
                    Debug.Log("检测不到B");
                    for (int i = 0; i < compare1.ChildNodes[18].ChildNodes.Count; i++)
                    {
                        if (File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText))
                            File.Delete(compare1.ChildNodes[18].ChildNodes[i].InnerText);
                        if (File.Exists(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp"))
                            File.Delete(compare1.ChildNodes[18].ChildNodes[i].InnerText + ".tmp");
                    }
                }
                Debug.Log("检测不到C");
                compare1.ChildNodes[17].InnerText = " ";
                compare1.ChildNodes[18].InnerText = " ";
            }



            //GetDownloadProgress.Instance.DownloadDataConvert(compare2.frameAnimationSize);

            if (!string.IsNullOrEmpty(compare2.videoLink))
                compare1.ChildNodes[19].InnerText = compare2.videoLink;
            else
                compare1.ChildNodes[19].InnerText = " ";
            if (!string.IsNullOrEmpty(compare2.webLink))
                compare1.ChildNodes[20].InnerText = compare2.webLink;
            else
                compare1.ChildNodes[20].InnerText = " ";
            if (compare1.ChildNodes.Count < 22)
            {
                XmlElement status = localXmlDoc.CreateElement("status");
                XmlElement auth = localXmlDoc.CreateElement("auth");
                XmlElement version = localXmlDoc.CreateElement("version");
                XmlElement PMName = localXmlDoc.CreateElement("PMName");
                if (!string.IsNullOrEmpty(compare2.status.ToString()))
                    status.InnerText = compare2.status.ToString();
                else
                    status.InnerText = " ";
                if (!string.IsNullOrEmpty(compare2.auth.ToString()))
                    auth.InnerText = compare2.auth.ToString();
                else
                    auth.InnerText = " ";
                if (!string.IsNullOrEmpty(compare2.version.ToString()))
                    version.InnerText = compare2.version.ToString();
                else
                    version.InnerText = " ";
                if (!string.IsNullOrEmpty(compare2.name))
                    PMName.InnerText = compare2.name;
                else
                    PMName.InnerText = " ";
                compare1.AppendChild(status);
                compare1.AppendChild(auth);
                compare1.AppendChild(version);
                compare1.AppendChild(PMName);


            }
            else
            {
                if (!string.IsNullOrEmpty(compare2.status.ToString()))
                    compare1.ChildNodes[21].InnerText = compare2.status.ToString();
                else
                    compare1.ChildNodes[21].InnerText = " ";
                if (!string.IsNullOrEmpty(compare2.auth.ToString()))
                    compare1.ChildNodes[22].InnerText = compare2.auth.ToString();
                else
                    compare1.ChildNodes[22].InnerText = " ";
                if (!string.IsNullOrEmpty(compare2.version.ToString()))
                    compare1.ChildNodes[23].InnerText = compare2.version.ToString();
                else
                    compare1.ChildNodes[23].InnerText = " ";
                if (!string.IsNullOrEmpty(compare2.name))
                    compare1.ChildNodes[24].InnerText = compare2.name;
                else
                    compare1.ChildNodes[24].InnerText = " ";
            }
        }

        SaveXML();
    }
    /// <summary>
    /// 14.用XML对比本地房间展品数据
    /// </summary>
    /// <param name="index">房间序号</param>
    public void ComparisonXMLRoomExhibitsData(string strID)
    {
        Debug.Log("选择按钮后，判断本地房间数据需要下载的东西");

        Downloader.Instance.requestDownloadObj = new List<DownloadObj>();

        XmlElement root = localXmlDoc.DocumentElement;
        XmlNodeList classList = root.ChildNodes;

        Debug.Log("先删除本地不存在的数据");
        List<string> deleteXmlDoc = new List<string>();

        Debug.Log("classList.Count：" + classList.Count);

        string[] RoomNames;

        int index = 0;

        if (allRoomResults != null && allRoomResults.Count != 0)
        {
            for (int i = 0; i < allRoomResults.Count; i++)
            {
                if (allRoomResults[i].id == strID)
                {
                    index = i;
                    break;
                }
            }
        }

        for (int i = 0; i < classList.Count; i++)
        {
            RoomNames = classList[i].Name.Split('_');

            for (int j = 0; j < allRoomResults.Count; j++)
            {
                Debug.Log("web room id:" + "Room_" + allRoomResults[j].id + "_____" + "local room id:" + classList[i].Name);
                if (j == allRoomResults.Count - 1)
                {
                    Debug.Log("classList[i].Name:" + classList[i].Name);
                    Debug.Log("allRoomResults[j].id:" + allRoomResults[j].id);
                    if (RoomNames[1] == allRoomResults[j].showMuseumId && RoomNames[3] == allRoomResults[j].id)
                    {
                        Debug.Log("最后一个，跟本地相同");
                    }
                    else
                    {
                        Debug.Log("最后一个，跟本地不相同，记录下来准备删除");
                        deleteXmlDoc.Add(classList[i].Name);
                    }
                }
                else
                {
                    if (RoomNames[1] == allRoomResults[j].showMuseumId && RoomNames[3] == allRoomResults[j].id)
                    {
                        Debug.Log("不是最后一个，但跟本地相同");
                        break;
                    }
                }
            }
        }

        //for (int i = 0; i < classList.Count; i++)
        //{
        //    RoomNames = classList[i].Name.Split('_');
        //    if (i == classList.Count - 1)
        //    {
        //        if (RoomNames[1] == allRoomResults[index].showMuseumId && RoomNames[3] == allRoomResults[index].id)
        //        {
        //            Debug.Log("最后一个，跟本地相同");
        //        }
        //        else
        //        {
        //            Debug.Log("最后一个，跟本地不相同，记录下来准备删除");
        //            deleteXmlDoc.Add("Room_" + RoomNames[3]);
        //        }
        //    }
        //    else
        //    {
        //        if (RoomNames[1] == allRoomResults[index].showMuseumId && RoomNames[3] == allRoomResults[index].id)
        //        {
        //            Debug.Log("不是最后一个，但跟本地相同");
        //            break;
        //        }
        //    }

        //}

        Debug.Log("检测是否有需要删除的配置");
        if (deleteXmlDoc.Count != 0)
        {
            Debug.LogFormat("有{0}个需要删除的配置", deleteXmlDoc.Count);
            for (int i = 0; i < deleteXmlDoc.Count; i++)
            {
                Debug.Log("deleteXmlDoc:" + deleteXmlDoc[i]);
                for (int j = 0; j < classList.Count; j++)
                {
                    if (deleteXmlDoc[i] == classList[j].Name)
                    {
                        Debug.Log("删除多余的画作模块");
                        DeleteRoomResources(classList[j]);
                    }
                }
            }
        }

        Debug.Log("对比现有的数据");
        for (int i = 0; i < classList.Count; i++)
        {
            RoomNames = classList[i].Name.Split('_');
            if (index == allRoomResults.Count - 1)
            {
                if (RoomNames[1] == allRoomResults[index].showMuseumId && RoomNames[3] == allRoomResults[index].id)
                {
                    Debug.Log("最后一个，跟本地相同");
                    Debug.Log("对比画作模块信息");
                    CompareRoomResources(classList[i], allRoomResults[index]);
                }
                else
                {
                    Debug.Log("最后一个，跟本地不相同，忽略");

                }
            }
            else
            {
                if (RoomNames[1] == allRoomResults[index].showMuseumId && RoomNames[3] == allRoomResults[index].id)
                {
                    Debug.Log("不是最后一个，但跟本地相同");
                    Debug.Log("对比画作模块信息");
                    CompareRoomResources(classList[i], allRoomResults[index]);
                    break;
                }
            }
        }
        Debug.Log("添加新的数据");
        if (classList.Count == 0)
        {
            AddRoomResources(allRoomResults[index]);
        }
        else
        {
            for (int j = 0; j < classList.Count; j++)
            {
                RoomNames = classList[j].Name.Split('_');
                if (j == classList.Count - 1)
                {
                    if (RoomNames[1] == allRoomResults[index].showMuseumId && RoomNames[3] == allRoomResults[index].id)
                    {
                        Debug.Log("最后一个，跟本地相同");
                    }
                    else
                    {
                        Debug.Log("最后一个，跟本地不相同，添加新的");
                        AddRoomResources(allRoomResults[index]);
                    }
                }
                else
                {
                    if (RoomNames[1] == allRoomResults[index].showMuseumId && RoomNames[3] == allRoomResults[index].id)
                    {
                        Debug.Log("不是最后一个，但跟本地相同");
                        break;
                    }
                }
            }
        }

        //循环一下，得到classList[i].ChildNodes和roomAllExhibitsData的index

        int classListIndex = 0;

        for (int i = 0; i < classList.Count; i++)
        {
            RoomNames = classList[i].Name.Split('_');
            if (RoomNames[1] == allRoomResults[index].showMuseumId && RoomNames[3] == allRoomResults[index].id)
            {
                Debug.Log("跟本地相同");
                classListIndex = i;
            }
        }

        int indexxx = 0;

        for (int i = 0; i < roomAllExhibitsData.Count; i++)
        {
            if (roomAllExhibitsData[i][0].showRoomId == currentSelectSceneData.sceneID)
            {
                indexxx = i;
                break;
            }
        }

        if (classList[classListIndex].ChildNodes.Count == 3)
        {
            if (classList[classListIndex].ChildNodes[2].ChildNodes.Count >= roomAllExhibitsData[indexxx].Count)
            {
                Debug.Log("服务器画作信息比xml多");
                for (int j = classList[classListIndex].ChildNodes[2].ChildNodes.Count - 1; j >= 0; j--)
                {
                    if (j > roomAllExhibitsData[indexxx].Count - 1)
                    {
                        Debug.Log("删除多余的画作模块");
                        if (classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[24].InnerText != "" && classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[24].InnerText != " " && !string.IsNullOrEmpty(classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[24].InnerText))
                            DeleteFiles(sceneURL + classList[classListIndex].Name + "/PM/" + classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[24].InnerText);
                        classList[classListIndex].ChildNodes[2].RemoveChild(classList[classListIndex].ChildNodes[2].ChildNodes[j]);
                        SaveXML();
                    }
                    else
                    {
                        Debug.Log("对比画作模块信息");
                        Debug.Log("对比画作模块信息" + (roomAllExhibitsData[indexxx][j].status == 1));
                        Debug.Log("对比画作模块信息" + (classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1"));
                        if (roomAllExhibitsData[indexxx][j].status == 0 && classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "0")
                            CompareRoomPM(classList[classListIndex].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[indexxx][j], index, j);
                        else if (roomAllExhibitsData[indexxx][j].status == 0 && classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1")
                            CompareRoomPM(classList[classListIndex].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[indexxx][j], index, j);
                        else if (roomAllExhibitsData[indexxx][j].status == 1 && classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "0")
                            CompareRoomPM(classList[classListIndex].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[indexxx][j], index, j);
                        else if (roomAllExhibitsData[indexxx][j].status == 1 && classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1")
                            Debug.Log(roomAllExhibitsData[indexxx][j].exhibitsNo + "：已经禁用");
                    }
                }
            }
            else if (classList[classListIndex].ChildNodes[2].ChildNodes.Count < roomAllExhibitsData[indexxx].Count)
            {
                for (int j = 0; j < roomAllExhibitsData[indexxx].Count; j++)
                {
                    if (j > classList[classListIndex].ChildNodes[2].ChildNodes.Count - 1)
                    {
                        Debug.Log("添加新的画作模块");
                        if (roomAllExhibitsData[indexxx][j].status == 0)
                            AddRoomPM(roomAllExhibitsData[indexxx][j], index, j);
                        else if (roomAllExhibitsData[indexxx][j].status == 1)
                            Debug.Log(roomAllExhibitsData[indexxx][j].exhibitsNo + "：已经禁用");

                    }
                    else
                    {
                        Debug.Log("对比画作模块信息");
                        if (roomAllExhibitsData[indexxx][j].status == 0 && classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "0")
                            CompareRoomPM(classList[classListIndex].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[indexxx][j], index, j);
                        else if (roomAllExhibitsData[indexxx][j].status == 0 && classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1")
                            CompareRoomPM(classList[classListIndex].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[indexxx][j], index, j);
                        else if (roomAllExhibitsData[indexxx][j].status == 1 && classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "0")
                            CompareRoomPM(classList[classListIndex].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[indexxx][j], index, j);
                        else if (roomAllExhibitsData[indexxx][j].status == 1 && classList[classListIndex].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1")
                            Debug.Log(roomAllExhibitsData[indexxx][j].exhibitsNo + "：已经禁用");
                    }
                }
            }
        }
        else
        {
            Debug.Log("场景内没有下载的画作信息");
            for (int j = 0; j < roomAllExhibitsData[indexxx].Count; j++)
            {
                AddRoomPM(roomAllExhibitsData[indexxx][j], classListIndex, j);
            }
        }



        if (Downloader.Instance.requestDownloadObj.Count > 0)
        {
            Debug.Log("场景资源有不同的文件，需要下载");

            getSceneData = currentSelectSceneData;

            Downloader.Instance.isBatch = false;

            Downloader.Instance.downloadNum = 0;

            //GetDownloadProgress.Instance.isDownload = true;
            //HttpManager.Instance.SetHttpState(HttpState.DownloadRoomData);
            //HttpManager.Instance.currentDownloadState = currentDownloadState.Scene;
            //UnityDownloadMgr.instance.BatchDownloadPMData();

            PopupWindowCanvasManager.Instance.DownloadFileCanvasGroupOpenEvent();
            HttpManager.Instance.currentGetRoomAllExhibitsDataIndex = 1;
        }
        else
        {
            Debug.Log("场景资源文件相同，进入场景");
            SaveXML();
            currentSelectSceneData.isDownload = true;
            currentSelectSceneDataID = currentSelectSceneData.sceneID;
            currentSelectSceneDataName = currentSelectSceneData.sceneName;
            currentSelectGalleryID = currentSelectSceneData.museumID;
            HttpManager.Instance.NetWorkUpdate(NetTypeListen.UploadUserLoginLog, null);
            if (isYouke)
            {
                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.YoukeLoginURL, null);
            }
            SceneManager.LoadScene("Player");
            //HttpManager.Instance.DetectionRoomPMData();
            //GetDownloadProgress.Instance.FakeProgressScene();
        }
    }
    /// <summary>
    /// 15.读取XML文件
    /// </summary>
    /// <returns></returns>
    public void LoadXMLComparison()
    {
        StartCoroutine(ILoadXMLFile());
    }

    public IEnumerator ILoadXMLFile()
    {
        //WWW wwwLocal;
        UnityWebRequest request;
        string url = "";
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        url = xmlURL;
#elif UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
        url = "file://"+xmlURL;
            //WWW wwwLocal = new WWW("jar:file://" + xmlURL);
#endif
        request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();//读取数据

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string xml = request.downloadHandler.text;
            request.Dispose();
            localXmlDoc = new XmlDocument();
            localXmlDoc.LoadXml(xml);
            Debug.Log("读取到本地xml");

            ////测试！！！！！！！！！！！！！！！！！！！！！
            //SceneManager.LoadScene("Main");

            ComparisonXMLRoomExhibitsData(currentSelectSceneData.sceneID);
        }
    }

    /// <summary>
    /// 16.选择场景组按钮下载事件
    /// </summary>
    public void SceneSelectionLayoutGroupButtonDownloadEvent()
    {
        currentSelectSceneData.GetComponent<Button>().interactable = false;
        currentSelectSceneMask = currentSelectSceneData.sceneImage;
        currentSelectSceneMask.enabled = true;
        currentSelectSceneMask.fillAmount = 1;
    }

    /// <summary>
    /// 获取本地房间信息
    /// </summary>
    public void GetRoomDataLocal()
    {
        //GetDownloadProgress.Instance.DownloadRoom();
        if (!Directory.Exists(url + "XML"))
        {
            Debug.Log("未检测到XML文件夹,创建新的");
            Directory.CreateDirectory(url + "XML");
            //HttpManager.Instance.SetHttpState(HttpState.DownloadRoomData);
            FirstDownloadCreateXML(currentSelectSceneData.sceneID);
        }
        else
        {
            Debug.Log("检测到XML文件夹,检测XML文件");
            if (File.Exists(xmlURL))
            {
                Debug.Log("检测到XML文件,检测XML文件里面的信息");
                LoadXMLComparison();
            }
            else
            {
                Debug.Log("未检测到XML文件,直接下载网络文件");
                //HttpManager.Instance.SetHttpState(HttpState.DownloadRoomData);
                FirstDownloadCreateXML(currentSelectSceneData.sceneID);
            }
        }
    }
    /// <summary>
    /// 获取本地房间画作信息大小
    /// </summary>
    public void GetRoomPMDataSizeLocal()
    {

        //不需要判断直接对比所有 
        Debug.Log("判断本地数据，收集下载文件的大小");

        UnityDownloadMgr.instance.CheckoutAllDataFileSize();
    }
    /// <summary>
    /// 获取本地房间画作信息
    /// </summary>
    public void GetRoomPMDataLocal()
    {
        if (firstDownload)
        {
            Debug.Log("首次下载数据");
            HttpManager.Instance.SetHttpState(HttpState.DownloadRoomPMData);

            //GetDownloadProgress.Instance.DownloadPM();

            UnityDownloadMgr.instance.StartDownloaderPaintingModule();

        }
        else
        {
            Debug.Log("检测到画作文件夹,对比数据");
            ComparisonRoomPMXML();
        }
    }



    /// <summary>
    /// 对比房间资源XML文件信息
    /// </summary>
    public void ComparisonRoomResourcesXML()
    {
        Downloader.Instance.requestDownloadObj = new List<DownloadObj>();

        XmlElement root = localXmlDoc.DocumentElement;
        XmlNodeList classList = root.ChildNodes;

        Debug.Log("先删除本地不存在的数据");
        List<string> deleteXmlDoc = new List<string>();

        for (int i = 0; i < classList.Count; i++)
        {
            for (int j = 0; j < allRoomResults.Count; j++)
            {
                Debug.Log("web room id:" + "Room_" + allRoomResults[j].id + "_____" + "local room id:" + classList[i].Name);
                if (j == allRoomResults.Count - 1)
                {
                    Debug.Log("classList[i].Name:" + classList[i].Name);
                    Debug.Log("allRoomResults[j].id:" + allRoomResults[j].id);
                    if (classList[i].Name == ("Room_" + allRoomResults[j].id))
                    {
                        Debug.Log("最后一个，跟本地相同");
                    }
                    else
                    {
                        Debug.Log("最后一个，跟本地不相同，记录下来准备删除");
                        deleteXmlDoc.Add(classList[i].Name);
                    }
                }
                else
                {
                    if (classList[i].Name == ("Room_" + allRoomResults[j].id))
                    {
                        Debug.Log("不是最后一个，但跟本地相同");
                        break;
                    }
                }
            }
        }

        Debug.Log("检测是否有需要删除的配置");
        if (deleteXmlDoc.Count != 0)
        {
            Debug.LogFormat("有{0}个需要删除的配置", deleteXmlDoc.Count);
            for (int i = 0; i < deleteXmlDoc.Count; i++)
            {
                Debug.Log("deleteXmlDoc:" + deleteXmlDoc[i]);
                for (int j = 0; j < classList.Count; j++)
                {
                    if (deleteXmlDoc[i] == classList[j].Name)
                    {
                        Debug.Log("删除多余的画作模块");
                        DeleteRoomResources(classList[j]);
                    }
                }
            }
        }

        Debug.Log("对比现有的数据");
        for (int i = 0; i < classList.Count; i++)
        {
            for (int j = 0; j < allRoomResults.Count; j++)
            {
                Debug.Log("web room id:" + "Room_" + allRoomResults[j].id + "_____" + "local room id:" + classList[i].Name);
                if (j == allRoomResults.Count - 1)
                {
                    if (classList[i].Name == ("Room_" + allRoomResults[j].id))
                    {
                        Debug.Log("最后一个，跟本地相同");
                        Debug.Log("对比画作模块信息");
                        CompareRoomResources(classList[i], allRoomResults[j]);
                    }
                    else
                    {
                        Debug.Log("最后一个，跟本地不相同，忽略");

                    }
                }
                else
                {
                    if (classList[i].Name == ("Room_" + allRoomResults[j].id))
                    {
                        Debug.Log("不是最后一个，但跟本地相同");
                        Debug.Log("对比画作模块信息");
                        CompareRoomResources(classList[i], allRoomResults[j]);
                        break;
                    }
                }
            }
        }
        Debug.Log("添加新的数据");
        if (classList.Count == 0)
        {
            for (int i = 0; i < allRoomResults.Count; i++)
            {
                Debug.Log("classList的数量为0");
                AddRoomResources(allRoomResults[i]);
            }
        }
        else
        {
            for (int i = 0; i < allRoomResults.Count; i++)
            {
                Debug.Log("classList的数量：" + classList.Count);
                for (int j = 0; j < classList.Count; j++)
                {
                    Debug.Log("web room id:" + "Room_" + allRoomResults[i].id + "_____" + "local room id:" + classList[j].Name);
                    if (j == classList.Count - 1)
                    {
                        if (classList[j].Name == ("Room_" + allRoomResults[i].id))
                        {
                            Debug.Log("最后一个，跟本地相同");
                        }
                        else
                        {
                            Debug.Log("最后一个，跟本地不相同，添加新的");
                            AddRoomResources(allRoomResults[i]);
                        }
                    }
                    else
                    {
                        if (classList[j].Name == ("Room_" + allRoomResults[i].id))
                        {
                            Debug.Log("不是最后一个，但跟本地相同");
                            break;
                        }
                    }
                }
            }
        }


        if (Downloader.Instance.requestDownloadObj.Count > 0)
        {
            Debug.Log("场景资源有不同的文件，需要下载");

            Downloader.Instance.isBatch = false;

            Downloader.Instance.downloadNum = 0;


            //GetDownloadProgress.Instance.isDownload = true;
            HttpManager.Instance.SetHttpState(HttpState.DownloadRoomPMData);
            HttpManager.Instance.currentDownloadState = currentDownloadState.PaintingModule;
            //UnityDownloadMgr.instance.CompareDownloaderScene();
            UnityDownloadMgr.instance.BatchDownloadPMData();
        }
        else
        {
            Debug.Log("画作资源对比相同，进入文件校验");
            //GetDownloadProgress.Instance.FakeProgressScene();
            //UnityDownloadMgr.instance.CheckoutAllData();

        }

        if (Downloader.Instance.requestDownloadObj.Count > 0)
        {
            Debug.Log("场景资源有不同的文件，需要下载");


            Downloader.Instance.isBatch = false;

            Downloader.Instance.downloadNum = 0;

            //GetDownloadProgress.Instance.isDownload = true;
            //HttpManager.Instance.SetHttpState(HttpState.DownloadRoomData);
            HttpManager.Instance.currentDownloadState = currentDownloadState.Scene;
            UnityDownloadMgr.instance.BatchDownloadPMData();
        }
        else
        {
            Debug.Log("场景资源文件相同，进入画作资源对比");
            SaveXML();
            //HttpManager.Instance.DetectionRoomPMData();
            //GetDownloadProgress.Instance.FakeProgressScene();
        }
    }
    /// <summary>
    /// 对比房间画作XML信息
    /// </summary>
    public void ComparisonRoomPMXML()
    {
        Downloader.Instance.requestDownloadObj = new List<DownloadObj>();

        //GetDownloadProgress.Instance.DownloadPM();

        XmlElement root = localXmlDoc.DocumentElement;
        XmlNodeList classList = root.ChildNodes;

        for (int i = 0; i < roomAllExhibitsData.Count; i++)
        {
            Debug.Log("房间id：" + classList[i].Name + "_" + classList[i].ChildNodes.Count);
            if (classList[i].ChildNodes.Count == 3)
            {
                if (classList[i].ChildNodes[2].ChildNodes.Count >= roomAllExhibitsData[i].Count)
                {
                    Debug.Log("服务器画作信息比xml多");
                    for (int j = classList[i].ChildNodes[2].ChildNodes.Count - 1; j >= 0; j--)
                    {
                        if (j > roomAllExhibitsData[i].Count - 1)
                        {
                            Debug.Log("删除多余的画作模块");
                            if (classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[24].InnerText != "" && classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[24].InnerText != " " && !string.IsNullOrEmpty(classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[24].InnerText))
                                DeleteFiles(sceneURL + classList[i].Name + "/PM/" + classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[24].InnerText);
                            classList[i].ChildNodes[2].RemoveChild(classList[i].ChildNodes[2].ChildNodes[j]);
                            SaveXML();
                        }
                        else
                        {
                            Debug.Log("对比画作模块信息");
                            Debug.Log("对比画作模块信息" + (roomAllExhibitsData[i][j].status == 1));
                            Debug.Log("对比画作模块信息" + (classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1"));
                            if (roomAllExhibitsData[i][j].status == 0 && classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "0")
                                CompareRoomPM(classList[i].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[i][j], i, j);
                            else if (roomAllExhibitsData[i][j].status == 0 && classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1")
                                CompareRoomPM(classList[i].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[i][j], i, j);
                            else if (roomAllExhibitsData[i][j].status == 1 && classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "0")
                                CompareRoomPM(classList[i].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[i][j], i, j);
                            else if (roomAllExhibitsData[i][j].status == 1 && classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1")
                                Debug.Log(roomAllExhibitsData[i][j].exhibitsNo + "：已经禁用");
                        }
                    }
                }
                else if (classList[i].ChildNodes[2].ChildNodes.Count < roomAllExhibitsData[i].Count)
                {
                    for (int j = 0; j < roomAllExhibitsData[i].Count; j++)
                    {
                        if (j > classList[i].ChildNodes[2].ChildNodes.Count - 1)
                        {
                            Debug.Log("添加新的画作模块");
                            if (roomAllExhibitsData[i][j].status == 0)
                                AddRoomPM(roomAllExhibitsData[i][j], i, j);
                            else if (roomAllExhibitsData[i][j].status == 1)
                                Debug.Log(roomAllExhibitsData[i][j].exhibitsNo + "：已经禁用");

                        }
                        else
                        {
                            Debug.Log("对比画作模块信息");
                            if (roomAllExhibitsData[i][j].status == 0 && classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "0")
                                CompareRoomPM(classList[i].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[i][j], i, j);
                            else if (roomAllExhibitsData[i][j].status == 0 && classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1")
                                CompareRoomPM(classList[i].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[i][j], i, j);
                            else if (roomAllExhibitsData[i][j].status == 1 && classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "0")
                                CompareRoomPM(classList[i].ChildNodes[2].ChildNodes[j], roomAllExhibitsData[i][j], i, j);
                            else if (roomAllExhibitsData[i][j].status == 1 && classList[i].ChildNodes[2].ChildNodes[j].ChildNodes[21].InnerText == "1")
                                Debug.Log(roomAllExhibitsData[i][j].exhibitsNo + "：已经禁用");
                        }
                    }
                }
            }
            else
            {
                Debug.Log("场景内没有下载的画作信息");
                for (int j = 0; j < roomAllExhibitsData[i].Count; j++)
                {
                    AddRoomPM(roomAllExhibitsData[i][j], i, j);
                }
            }
        }

        if (Downloader.Instance.requestDownloadObj.Count > 0)
        {
            Debug.Log("场景资源有不同的文件，需要下载");

            Downloader.Instance.isBatch = false;

            Downloader.Instance.downloadNum = 0;


            //GetDownloadProgress.Instance.isDownload = true;
            HttpManager.Instance.SetHttpState(HttpState.DownloadRoomPMData);
            HttpManager.Instance.currentDownloadState = currentDownloadState.PaintingModule;
            //UnityDownloadMgr.instance.CompareDownloaderScene();
            UnityDownloadMgr.instance.BatchDownloadPMData();
        }
        else
        {
            Debug.Log("画作资源对比相同，进入文件校验");
            //GetDownloadProgress.Instance.FakeProgressScene();
            //UnityDownloadMgr.instance.CheckoutAllData();

        }
    }



    /// <summary>
    /// 删除房屋画作数据
    /// </summary>
    private void DeleteRoomPM(XmlNode node)
    {
        DeleteFiles(sceneURL + node.Name);
        //Directory.Delete(sceneURL + node.Name);
        localXmlDoc.RemoveChild(node);
        SaveXML();
    }




    /// <summary>
    /// 保存xml
    /// </summary>
    public void SaveXML()
    {
        localXmlDoc.Save(xmlURL);
    }
    /// <summary>
    /// 获取进去场景的ab包
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetEnterSceneResources(string id)
    {
        XmlElement root = localXmlDoc.DocumentElement;
        XmlNodeList classList = root.ChildNodes;
        string[] RoomNames;
        int index = 0;
        for (int i = 0; i < classList.Count; i++)
        {
            RoomNames = classList[i].Name.Split('_');
            if (RoomNames[3] == id)
            {
                index = i;
                Debug.Log("获取到id");
            }
        }
        return classList[index].ChildNodes[0].ChildNodes[2].InnerText;
    }
    /// <summary>
    /// 获取进去场景的BGM
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetEnterSceneBGM(string id)
    {
        XmlElement root = localXmlDoc.DocumentElement;
        XmlNodeList classList = root.ChildNodes;
        string[] RoomNames;
        int index = 0;
        for (int i = 0; i < classList.Count; i++)
        {
            RoomNames = classList[i].Name.Split('_');
            if (RoomNames[3] == id)
            {
                index = i;
                Debug.Log("获取到id");
            }
        }
        if (classList[index].ChildNodes[1].ChildNodes[2].InnerText == " ")
            return " ";
        else
            return classList[index].ChildNodes[1].ChildNodes[2].InnerText;
    }
    /// <summary>
    /// 画作数据集合
    /// </summary>
    public List<PaintingModule> Datas = new List<PaintingModule>();
    /// <summary>
    /// 可使用画作数据集合
    /// </summary>
    public List<PaintingModule> availablePM = new List<PaintingModule>();
    /// <summary>
    /// 当前已加载的画作数据集合
    /// </summary>
    public List<PaintingModule> currentLoadDatas = new List<PaintingModule>();
    /// <summary>
    /// 当前画作模块
    /// </summary>
    public PaintingModule currentPaintingModule;
    /// <summary>
    /// 当前碰撞器画作模块
    /// </summary>
    public PaintingModule currentColliderPaintingModule;
    /// <summary>
    /// 当前场景的画作模块数据
    /// </summary>
    public Transform ScenePaintingModules;
    /// <summary>
    /// 画作地图背景bg
    /// </summary>
    public Image paintingModuleCanvas_introductionBG;
    /// <summary>
    /// 进去场景画作初始化数据
    /// </summary>
    public void EnterScenePMInitial()
    {
        Transform PaintingModuleManager = GameObject.Find("PaintingModuleManager").transform;
        ScenePaintingModules = GameObject.Find("ScenePaintingModules").transform;
        GameObject pm = Resources.Load("PaintingModule") as GameObject;

        paintingModuleCanvas_introductionBG = GameObject.Find("PaintingModuleScene_01introductionBG").GetComponent<Image>();

        for (int i = 0; i < ScenePaintingModules.childCount; i++)
        {
            GameObject paintingModule = Instantiate(pm, PaintingModuleManager);
            paintingModule.name = "PaintingModule" + i;

            Material mat = null;
            if (ScenePaintingModules.GetChild(i).childCount > 2)
                mat = ScenePaintingModules.GetChild(i).GetChild(2).GetComponent<MeshRenderer>().sharedMaterial;

            Transform coll = paintingModule.transform.GetChild(0);
            coll.localPosition = ScenePaintingModules.GetChild(i).GetChild(0).localPosition;
            coll.localEulerAngles = ScenePaintingModules.GetChild(i).GetChild(0).localEulerAngles;
            coll.localScale = ScenePaintingModules.GetChild(i).GetChild(0).localScale;

            ScenePaintingModules.GetChild(i).GetChild(0).gameObject.SetActive(false);

            RectTransform rect = paintingModule.transform.GetChild(1).GetComponent<RectTransform>();
            RectTransform rectScene = ScenePaintingModules.GetChild(i).GetChild(1).GetComponent<RectTransform>();
            rect.localPosition = rectScene.localPosition;
            rect.localEulerAngles = rectScene.localEulerAngles;
            rect.sizeDelta = rectScene.sizeDelta;
            rect.localScale = rectScene.localScale;

            RectTransform rectButton = paintingModule.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>();
            RectTransform rectSceneButton = ScenePaintingModules.GetChild(i).GetChild(1).GetChild(1).GetComponent<RectTransform>();
            rectButton.localPosition = rectSceneButton.localPosition;
            rectButton.localEulerAngles = rectSceneButton.localEulerAngles;
            rectButton.sizeDelta = rectSceneButton.sizeDelta;
            rectButton.localScale = rectSceneButton.localScale;

            RectTransform introductionBG = paintingModule.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>();
            RectTransform introductionBGScene = ScenePaintingModules.GetChild(i).GetChild(1).GetChild(2).GetComponent<RectTransform>();
            introductionBG.localPosition = introductionBGScene.localPosition;
            introductionBG.localEulerAngles = introductionBGScene.localEulerAngles;
            introductionBG.sizeDelta = introductionBGScene.sizeDelta;
            introductionBG.localScale = introductionBGScene.localScale;

            paintingModule.transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = paintingModuleCanvas_introductionBG.sprite;

            RectTransform introductionText = paintingModule.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<RectTransform>();
            RectTransform introductionTextGScene = ScenePaintingModules.GetChild(i).GetChild(1).GetChild(2).GetChild(0).GetComponent<RectTransform>();
            introductionText.localPosition = introductionTextGScene.localPosition;
            introductionText.localEulerAngles = introductionTextGScene.localEulerAngles;
            introductionText.sizeDelta = introductionTextGScene.sizeDelta;
            introductionText.localScale = introductionTextGScene.localScale;

            Transform mapPT = paintingModule.transform.GetChild(4);
            mapPT.position = ScenePaintingModules.GetChild(i).GetChild(4).position;
            mapPT.eulerAngles = ScenePaintingModules.GetChild(i).GetChild(4).eulerAngles;

            PaintingModule _PM = paintingModule.AddComponent<PaintingModule>();
            coll.gameObject.AddComponent<PaintingModuleCollider>();
            paintingModule.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            _PM.mainGraphMaterial = mat;
            _PM.introductionText = paintingModule.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>();
            _PM.introductionImage = paintingModule.transform.GetChild(1).GetChild(2).GetComponent<Image>();
            ScenePaintingModules.GetChild(i).GetChild(3).GetChild(0).parent = paintingModule.transform.GetChild(3);
            _PM.Mask = _PM.transform.GetChild(3).gameObject;
            _PM.Mask.SetActive(false);
            _PM.teleporter = _PM.transform.GetChild(4);
            paintingModule.transform.GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = mat;
            ScenePaintingModules.GetChild(i).gameObject.SetActive(false);



            Datas.Add(_PM);
        }

        LoadLocalXMLToDatas();
    }
    /// <summary>
    /// 读取本地XML信息至Datas
    /// </summary>
    public void LoadLocalXMLToDatas()
    {
        XmlElement root = localXmlDoc.DocumentElement;
        XmlNodeList classList = root.ChildNodes;
        string[] RoomNames;
        int index = 0;
        for (int i = 0; i < classList.Count; i++)
        {
            RoomNames = classList[i].Name.Split('_');
            if (RoomNames[3] == currentSelectSceneDataID)
            {
                index = i;
                Debug.Log("获取到id");
            }
        }
        XmlNodeList nodes = root.ChildNodes[index].ChildNodes[2].ChildNodes;
        for (int i = 0; i < Datas.Count; i++)
        {
            if (nodes[i].ChildNodes[21].InnerText == "0")
            {
                Datas[i].paintingName = nodes[i].ChildNodes[24].InnerText;
                Datas[i].id = nodes[i].ChildNodes[0].InnerText;
                Datas[i].textIntroduction = nodes[i].ChildNodes[1].InnerText;
                Datas[i].mainGraphName = nodes[i].ChildNodes[2].InnerText;
                Datas[i].mainGraphHttpUrl = nodes[i].ChildNodes[3].InnerText;
                Datas[i].mainGraphSaveUrl = nodes[i].ChildNodes[4].InnerText;
                Datas[i].introductionImageName = nodes[i].ChildNodes[5].InnerText;
                Datas[i].introductionImageHttpUrl = nodes[i].ChildNodes[6].InnerText;
                Datas[i].introductionImageSaveUrl = nodes[i].ChildNodes[7].InnerText;
                Datas[i].videoName = nodes[i].ChildNodes[8].InnerText;
                Datas[i].videoHttpUrl = nodes[i].ChildNodes[9].InnerText;
                Datas[i].videoSaveUrl = nodes[i].ChildNodes[10].InnerText;
                Datas[i].voiceName = nodes[i].ChildNodes[11].InnerText;
                Datas[i].voiceHttpUrl = nodes[i].ChildNodes[12].InnerText;
                Datas[i].voiceSaveUrl = nodes[i].ChildNodes[13].InnerText;
                Datas[i].animationThumbnailName = nodes[i].ChildNodes[14].InnerText;
                Datas[i].animationThumbnailHttpUrl = nodes[i].ChildNodes[15].InnerText;
                Datas[i].animationThumbnailSaveUrl = nodes[i].ChildNodes[16].InnerText;
                if (nodes[i].ChildNodes[17].InnerText != " ")
                {
                    Datas[i].frameAnimationHttpUrl = new string[nodes[i].ChildNodes[17].ChildNodes.Count];
                    for (int j = 0; j < Datas[i].frameAnimationHttpUrl.Length; j++)
                    {
                        Datas[i].frameAnimationHttpUrl[j] = nodes[i].ChildNodes[17].ChildNodes[j].InnerText;
                    }
                    Datas[i].frameAnimationSaveUrl = new string[nodes[i].ChildNodes[18].ChildNodes.Count];
                    for (int j = 0; j < Datas[i].frameAnimationSaveUrl.Length; j++)
                    {
                        Datas[i].frameAnimationSaveUrl[j] = nodes[i].ChildNodes[18].ChildNodes[j].InnerText;
                    }
                    Datas[i].frameAnimationArrayTexture = new Texture2D[Datas[i].frameAnimationSaveUrl.Length];
                }
                Datas[i].videoLink = nodes[i].ChildNodes[19].InnerText;
                Datas[i].webLink = nodes[i].ChildNodes[20].InnerText;
                int sta1 = 0;
                int sta2 = 0;
                int sta3 = 0;
                int.TryParse(nodes[i].ChildNodes[21].InnerText, out sta1);
                int.TryParse(nodes[i].ChildNodes[22].InnerText, out sta2);
                int.TryParse(nodes[i].ChildNodes[23].InnerText, out sta3);
                Datas[i].status = sta1;
                Datas[i].auth = sta2;
                Datas[i].version = sta3;
                Datas[i].introductionTexture = null;
                Datas[i].voiceClip = null;

                StartCoroutine(GetPaintingModuleTexture2DResolution(Datas[i]));
            }
            else if (nodes[i].ChildNodes[21].InnerText == "1")
            {
                Datas[i].id = nodes[i].ChildNodes[0].InnerText;
                Datas[i].status = 1;
            }
        }

        InitialSceneMask();
        InitialSceneMainGraph();
        InitialSceneIntroductionText();
        InitialSceneAnimationThumbnail();
        InitialSceneIntroductionImage();
    }
    /// <summary>
    /// 获取画作模块图片的分辨率
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetPaintingModuleTexture2DResolution(PaintingModule PM)
    {
        //UnityWebRequest request = UnityWebRequestTexture.GetTexture(PM.frameAnimationSaveUrl[0]);

        //yield return request.SendWebRequest();

        //PM.resolution = DownloadHandlerTexture.GetContent(request).width + " " + DownloadHandlerTexture.GetContent(request).height;

        //request.Dispose();
        if (PM.animationThumbnailSaveUrl != null && PM.animationThumbnailSaveUrl != " " && PM.animationThumbnailSaveUrl != "")
        {
            //            string url = "";
            //#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            //            url = PM.frameAnimationSaveUrl[0];
            //#elif UNITY_ANDROID
            //        url = "file://"+PM.frameAnimationSaveUrl[0];
            //#elif UNITY_IOS || UNITY_IPHONE
            //        url = "file://"+PM.frameAnimationSaveUrl[0];
            //#endif
            //            UnityWebRequest wr = new UnityWebRequest(url);

            //            DownloadHandlerTexture texdl = new DownloadHandlerTexture(true);

            //            wr.downloadHandler = texdl;

            //            yield return wr.SendWebRequest();

            //            PM.resolution = texdl.texture.width.ToString() + " " + texdl.texture.height.ToString();

            //            Debug.Log("当前画作宽度像素为：" + texdl.texture.width);

            //            Debug.Log("当前画作高度像素为：" + texdl.texture.height);

            //            texdl.Dispose();

            //            wr.Dispose();

            yield return null;
            FileStream fileStream = new FileStream(PM.animationThumbnailSaveUrl, FileMode.Open, FileAccess.Read);
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

            PM.resolution = texture1.width.ToString() + "_" + texture1.height.ToString();
        }

    }
    /// <summary>
    /// 初始化场景Mask
    /// </summary>
    public void InitialSceneMask()
    {
        for (int i = 0; i < Datas.Count; i++)
        {
            if (Datas[i].status == 1)
            {
                Datas[i].Mask.SetActive(true);
            }

        }
    }
    /// <summary>
    /// 初始化场景简介图片
    /// </summary>
    public void InitialSceneIntroductionImage()
    {
        for (int i = 0; i < Datas.Count; i++)
        {
            if (Datas[i].status == 1)
            {
                Datas[i].introductionImage.gameObject.SetActive(false);
            }

        }
    }
    /// <summary>
    /// 初始化场景内的主图
    /// </summary>
    public void InitialSceneMainGraph()
    {
        for (int i = 0; i < Datas.Count; i++)
        {
            if (Datas[i].status == 0)
            {
                if (Datas[i].mainGraphMaterial != null)
                    SetPaintingModuleMainGraph(Datas[i]);
            }

        }
    }

    public void SetPaintingModuleMainGraph(PaintingModule PM)
    {
        //        string url = "";
        //#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        //        url = PM.mainGraphSaveUrl;
        //#elif UNITY_ANDROID
        //        url = "file://"+PM.mainGraphSaveUrl;
        //#elif UNITY_IOS || UNITY_IPHONE
        //        url = "file://"+PM.mainGraphSaveUrl;
        //#endif

        //        UnityWebRequest wr = new UnityWebRequest(url);

        //        DownloadHandlerTexture texdl = new DownloadHandlerTexture(true);

        //        wr.downloadHandler = texdl;

        //        yield return wr.SendWebRequest();

        //        PM.mainGraphMaterial.SetTexture("_MainTex", texdl.texture);

        //        texdl.Dispose();

        //        wr.Dispose();

        //        Resources.UnloadUnusedAssets();

        //        System.GC.Collect();
        if (PM.mainGraphHttpUrl != null && PM.mainGraphHttpUrl != " " && PM.mainGraphHttpUrl != "")
        {
            FileStream fileStream = new FileStream(PM.mainGraphSaveUrl, FileMode.Open, FileAccess.Read);
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

            PM.mainGraphTexture = texture1;

            PM.mainGraphMaterial.SetTexture("_MainTex", texture1);
        }
        else
        {
            PM.mainGraphTexture = null;
        }


    }
    /// <summary>
    /// 初始化场景内的简介
    /// </summary>
    public void InitialSceneIntroductionText()
    {

        for (int i = 0; i < Datas.Count; i++)
        {
            if (Datas[i].status == 0)
            {
                if (Datas[i].textIntroduction != null)
                    Datas[i].introductionText.text = Datas[i].textIntroduction;
            }
        }
    }

    private bool isLoadEnd = false;
    /// <summary>
    /// 初始化场景内的缩略图
    /// </summary>
    public void InitialSceneAnimationThumbnail()
    {
        for (int i = 0; i < Datas.Count; i++)
        {
            if (i == Datas.Count - 1)
            {
                GetLoadingProgress.Instance.isFake = true;
                GetLoadingProgress.Instance.currentAlreadyIndex += 1;
                isLoadEnd = true;
            }

            if (Datas[i].status == 0)
            {
                if (Datas[i].animationThumbnailSaveUrl != null)
                    SetPaintingModuleAnimationThumbnail(Datas[i]);
            }
        }
    }
    public void SetPaintingModuleAnimationThumbnail(PaintingModule PM)
    {
        if (PM.animationThumbnailHttpUrl != null && PM.animationThumbnailHttpUrl != " " && PM.animationThumbnailHttpUrl != "")
        {
            //            string url = "";
            //#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            //            url = PM.animationThumbnailSaveUrl;
            //#elif UNITY_ANDROID
            //        url = "file://"+PM.animationThumbnailSaveUrl;
            //#elif UNITY_IOS || UNITY_IPHONE
            //        url = "file://"+PM.animationThumbnailSaveUrl;
            //#endif

            //            UnityWebRequest wr = new UnityWebRequest(url);

            //            DownloadHandlerTexture texdl = new DownloadHandlerTexture(true);

            //            wr.downloadHandler = texdl;

            //            yield return wr.SendWebRequest();

            //            PM.animationThumbnailTexture = texdl.texture;

            //            if (isLoadEnd)
            //                PaintingModuleMap.Instance.LoadMapAnimationThumbnailTexture();

            //            texdl.Dispose();

            //            wr.Dispose();


            FileStream fileStream = new FileStream(PM.animationThumbnailSaveUrl, FileMode.Open, FileAccess.Read);
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

            PM.animationThumbnailTexture = texture1;

            if (isLoadEnd)
                PaintingModuleMap.Instance.LoadMapAnimationThumbnailTexture();
        }
        else
        {
            PM.animationThumbnailTexture = null;

            if (isLoadEnd)
                PaintingModuleMap.Instance.LoadMapAnimationThumbnailTexture();
        }
    }
    /// <summary>
    /// 读取场景画作模块
    /// </summary>
    /// <param name="pm"></param>
    public void LoadScenePaintingModules(PaintingModule pm)
    {
        StartCoroutine(ILoadXMLA(pm));
    }
    public IEnumerator ILoadXMLA(PaintingModule pm)
    {
        if (pm.frameAnimationHttpUrl != null && pm.frameAnimationHttpUrl.Length != 0)
        {
            if (currentLoadDatas.Count == 0)
            {
                currentLoadDatas.Add(pm);
            }
            else
            {
                if (currentLoadDatas.Count < 2)
                {
                    currentLoadDatas.Add(pm);
                }
                else if (currentLoadDatas.Count >= 2)
                {
                    for (int i = 0; i < currentLoadDatas.Count; i++)
                    {
                        if (currentLoadDatas[i] == pm)
                        {
                            break;
                        }
                        else
                        {
                            if (i == currentLoadDatas.Count - 1)
                            {
                                currentLoadDatas[0].DeletePaintingModuleTextures();
                                for (int j = 0; j < currentLoadDatas.Count - 1; j++)
                                {
                                    currentLoadDatas[j] = currentLoadDatas[j + 1];
                                }

                                currentLoadDatas[currentLoadDatas.Count - 1] = pm;
                            }

                        }
                    }

                }
            }

            currentPaintingModule = pm;

            PaintingModuleFrameCanvas.Instance.SetPaintingModuleOptionButton(currentPaintingModule);

            for (int l = 0; l < pm.frameAnimationArrayTexture.Length; l++)
            {
                if (pm.frameAnimationArrayTexture[l] == null)
                {
                    PaintingModuleFrameCanvas.Instance.PaintingModuleFrameRawSetAnimationThumbnail();
                    isFrame = true;
                    Debug.Log("图片为空，进行加载");
                    break;
                }
                else
                {
                    isFrame = true;
                    Debug.Log("读取到了图片");

                    PaintingModuleFrameCanvas.Instance.PaintingModuleFrameRawImageLocalScale(pm.width, pm.height);
                    PaintingModuleFrameControl.Instance.m_rotateCoefficient = 360f / pm.frameAnimationArrayTexture.Length;
                    PaintingModuleFrameControl.Instance.m_Cube.transform.localEulerAngles = new Vector3(0, 0, 0);
                    Resources.UnloadUnusedAssets();
                    System.GC.Collect();

                    PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();

                    //if (!isYouke)
                    //    PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();
                    //else
                    //{
                    //    PaintingModuleFrameCanvas.Instance.PaintingModuleFrameRawSetAnimationThumbnail();
                    //    PaintingModuleLockManager.Instance.PaintingModuleLockOpenEvent();
                    //}


                    yield break;
                }
            }

            PaintingModuleFrameCanvas.Instance.PaintingModuleLoadingStart();
            Debug.Log("开始加载 ");
            for (int i = 0; i < pm.frameAnimationArrayTexture.Length; i++)
            {
                //文件读取
                FileStream fileStream = new FileStream(pm.frameAnimationSaveUrl[i], FileMode.Open, FileAccess.Read);
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

                //获取图片的宽和高
                string[] split = pm.resolution.Split('_');
                int width = int.Parse(split[0]);
                int height = int.Parse(split[1]);

                //添加一个ui大小调整！！！
                if (i == 0)
                {
                    pm.width = width;
                    pm.height = height;
                    PaintingModuleFrameCanvas.Instance.PaintingModuleFrameRawImageLocalScale(width, height);
                }

                //读取图片
                Texture2D texture1 = new Texture2D(0, 0);
                texture1.LoadImage(bytes);
                texture1.Apply(false, true);
                pm.frameAnimationArrayTexture[i] = texture1;

                PaintingModuleFrameCanvas.Instance.PaintingModuleLoadingLoad((i * 100 / pm.frameAnimationArrayTexture.Length));
                Debug.Log("已: " + (i * 100 / pm.frameAnimationArrayTexture.Length));
                yield return null;

                Debug.Log("已经加载个数: " + i);
            }
            ////////////////
            PaintingModuleFrameControl.Instance.m_rotateCoefficient = 360f / pm.frameAnimationArrayTexture.Length;
            PaintingModuleFrameControl.Instance.m_Cube.transform.localEulerAngles = new Vector3(0, 0, 0);
            PaintingModuleFrameCanvas.Instance.PaintingModuleLoadingStop();

            PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();

            //if (!isYouke)
            //    PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();
            //else
            //    PaintingModuleLockManager.Instance.PaintingModuleLockOpenEvent();


        }
        else
        {
            isFrame = false;

            currentPaintingModule = pm;

            PaintingModuleFrameCanvas.Instance.SetPaintingModuleOptionButton(currentPaintingModule);

            //if (isYouke)
            //    PaintingModuleLockManager.Instance.PaintingModuleLockOpenEvent();

        }


    }
    /// <summary>
    /// 删除当前画作模块
    /// </summary>
    public void DeleteCurrentPaintingModule()
    {
        currentPaintingModule = null;
    }
    /// <summary>
    /// 提供贴图
    /// </summary>
    /// <returns></returns>
    public Texture2D PaintingModuleManagerReplacedTexture(int eulerindex)
    {
        return currentPaintingModule.frameAnimationArrayTexture[eulerindex];
    }
    /// <summary>
    /// 当前画作视频url
    /// </summary>
    /// <returns></returns>
    public string CurrentPaintingModuleVideoURL()
    {
        Debug.Log(currentPaintingModule.videoSaveUrl);
        return currentPaintingModule.videoSaveUrl;
    }
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="url"></param>
    public void DeleteFiles(string url)
    {
        //DirectoryInfo info = new DirectoryInfo(url);
        //FileInfo[] files = info.GetFiles();
        //if (files != null)
        //{
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        if (files[i] != null)
        //        {
        //            files[i].Delete();
        //            files[i] = null;
        //        }
        //    }
        //}
        Debug.Log("urlurlurlurl:" + url);
        if (Directory.Exists(url))
            Directory.Delete(url, true);
    }
    /// <summary>
    /// 删除文件夹
    /// </summary>
    /// <param name="url"></param>
    public void DeleteDirectory(string url)
    {
        //DirectoryInfo info = new DirectoryInfo(url);
        //FileInfo[] files = info.GetFiles();
        //if (files != null)
        //{
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        if (files[i] != null)
        //        {
        //            files[i].Delete();
        //            files[i] = null;
        //        }
        //    }
        //}
        if (Directory.Exists(url))
            Directory.Delete(url, true);
    }
    /// <summary>
    /// 检测内存里面的账号密码
    /// </summary>
    public void DetectionPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("UserName"))
        {
            PlayerPrefs_UserName = PlayerPrefs.GetString("UserName");
        }
        else
        {
            PlayerPrefs_UserName = "";
        }

        if (PlayerPrefs.HasKey("Password"))
        {
            PlayerPrefs_Password = PlayerPrefs.GetString("Password");
        }
        else
        {
            PlayerPrefs_Password = "";
        }

        if (PlayerPrefs.HasKey("IsRemember"))
        {
            PlayerPrefs_IsRemember = PlayerPrefs.GetString("IsRemember");
        }
        else
        {
            PlayerPrefs_IsRemember = "";
        }

        if (PlayerPrefs.HasKey("Language"))
        {
            PlayerPrefs_Language = PlayerPrefs.GetString("Language");
            language = (Language)Enum.Parse(typeof(Language), PlayerPrefs_Language);
        }
        else
        {
            PlayerPrefs_Language = "";
        }


        if (PlayerPrefs.HasKey("JoystickSpeed"))
        {
            PlayerPrefs_JoystickSpeed = PlayerPrefs.GetFloat("JoystickSpeed");

        }
        else
        {
            PlayerPrefs_JoystickSpeed = 0;
        }

        if (PlayerPrefs.HasKey("TouchPadSpeed"))
        {
            PlayerPrefs_TouchPadSpeed = PlayerPrefs.GetFloat("TouchPadSpeed");

        }
        else
        {
            PlayerPrefs_TouchPadSpeed = 0;
        }

        if (PlayerPrefs.HasKey("BGMMute"))
        {
            PlayerPrefs_BGM_Mute = PlayerPrefs.GetString("BGMMute");
        }
        else
        {
            PlayerPrefs_BGM_Mute = "";
        }


    }

    /// <summary>
    /// 退出登录
    /// </summary>
    public void LogOut()
    {
        isYouke = false;

        if (currentLoadDatas != null)
        {
            for (int i = 0; i < currentLoadDatas.Count; i++)
            {
                currentLoadDatas[i].DeletePaintingModuleTextures();
            }
        }

        if (Datas != null)
        {
            for (int i = 0; i < Datas.Count; i++)
            {
                Destroy(Datas[i].gameObject);
            }
        }

        PlayerPrefs.SetFloat("JoystickSpeed", PaintingModuleMap.Instance.paintingModuleSettingUICanvasYaoganSlider.value);
        PlayerPrefs.SetFloat("TouchPadSpeed", PaintingModuleMap.Instance.paintingModuleSettingUICanvasShijueSlider.value);
        PlayerPrefs.SetString("BGMMute", SceneBGMManager.Instance.isMute.ToString());

        id = "";
        currentLoadDatas = new List<PaintingModule>();
        Datas = new List<PaintingModule>();
        availablePM = new List<PaintingModule>();
        currentPaintingModule = null;
        currentColliderPaintingModule = null;
        ScenePaintingModules = null;
        paintingModuleCanvas_introductionBG = null;

        loginLogID = "";

        SceneManager.LoadScene("Main");
        LoadSceneManager.Instance.UninstallcurrentSceneAB();
    }


    /// <summary>
    /// 游客注册
    /// </summary>
    public void YoukeRegister()
    {
        if (currentLoadDatas != null)
        {
            for (int i = 0; i < currentLoadDatas.Count; i++)
            {
                currentLoadDatas[i].DeletePaintingModuleTextures();
            }
        }

        if (Datas != null)
        {
            for (int i = 0; i < Datas.Count; i++)
            {
                Destroy(Datas[i].gameObject);
            }
        }

        PlayerPrefs.SetFloat("JoystickSpeed", PaintingModuleMap.Instance.paintingModuleSettingUICanvasYaoganSlider.value);
        PlayerPrefs.SetFloat("TouchPadSpeed", PaintingModuleMap.Instance.paintingModuleSettingUICanvasShijueSlider.value);
        PlayerPrefs.SetString("BGMMute", SceneBGMManager.Instance.isMute.ToString());

        id = "";
        currentLoadDatas = new List<PaintingModule>();
        Datas = new List<PaintingModule>();
        availablePM = new List<PaintingModule>();
        currentPaintingModule = null;
        currentColliderPaintingModule = null;
        ScenePaintingModules = null;
        paintingModuleCanvas_introductionBG = null;

        loginLogID = "";
        SceneManager.LoadScene("Main");
        LoadSceneManager.Instance.UninstallcurrentSceneAB();
    }

    /// <summary>
    /// 修改密码跳转回去
    /// </summary>
    public void ChangePasswordEvent()
    {
        isChangePassword = true;

        if (currentLoadDatas != null)
        {
            for (int i = 0; i < currentLoadDatas.Count; i++)
            {
                currentLoadDatas[i].DeletePaintingModuleTextures();
            }
        }

        if (Datas != null)
        {
            for (int i = 0; i < Datas.Count; i++)
            {
                Destroy(Datas[i].gameObject);
            }
        }

        PlayerPrefs.SetFloat("JoystickSpeed", PaintingModuleMap.Instance.paintingModuleSettingUICanvasYaoganSlider.value);
        PlayerPrefs.SetFloat("TouchPadSpeed", PaintingModuleMap.Instance.paintingModuleSettingUICanvasShijueSlider.value);
        PlayerPrefs.SetString("BGMMute", SceneBGMManager.Instance.isMute.ToString());

        id = "";
        currentLoadDatas = new List<PaintingModule>();
        Datas = new List<PaintingModule>();
        availablePM = new List<PaintingModule>();
        currentPaintingModule = null;
        currentColliderPaintingModule = null;
        ScenePaintingModules = null;
        paintingModuleCanvas_introductionBG = null;

        loginLogID = "";
        SceneManager.LoadScene("Main");
        LoadSceneManager.Instance.UninstallcurrentSceneAB();
    }

    public void AddAvailablePMEvent(int index)
    {
        availablePM.Add(Datas[index]);
    }

    public class CC
    {
        public int a;
        public string b;
    }

}

public class PMHttpData
{
    public PMInfo info;
    public PMMainGraph mainGraph;
    public PMIntroductionImage introductionImage;
    public PMVideo video;
    public PMVoice voice;
    public PMAnimationThumbnail animationThumbnail;
    public PMFrameAnimation frameAnimation;
    public PMLink link;
}


public class PMInfo
{
    public string PMroom;
    public string PMName;
    public int PMNo;
    public string PMText;
    public string PMStatus;
    public string PMAuth;
    public string PMVersion;
}
public class PMMainGraph
{
    public string mainGraphName;
    public string mainGraphHttpUrl;
    public string mainGraphSaveUrl;
}
public class PMIntroductionImage
{
    public string introductionImageName;
    public string introductionImageHttpUrl;
    public string introductionImageSaveUrl;
}
public class PMVideo
{
    public string videoName;
    public string videoHttpUrl;
    public string videoSaveUrl;
}
public class PMVoice
{
    public string voiceName;
    public string voiceHttpUrl;
    public string voiceSaveUrl;
}
public class PMAnimationThumbnail
{
    public string animationThumbnailName;
    public string animationThumbnailHttpUrl;
    public string animationThumbnailSaveUrl;
}
public class PMFrameAnimation
{
    public string[] frameAnimationHttpUrl;
    public string[] frameAnimationSaveUrl;
}
public class PMLink
{
    public string videoLink;
    public string webLink;
    public string youtubeLink;
    public string bilibiliLink;
}
[System.Serializable]
public class GalleryListIdInfo
{
    public string GalleryName;
    public string GalleryId;
    public RoomListIdInfo[] RoomInfo;
}

[System.Serializable]
public class RoomListIdInfo
{
    public string RoomName;
    public string RoomId;
}




