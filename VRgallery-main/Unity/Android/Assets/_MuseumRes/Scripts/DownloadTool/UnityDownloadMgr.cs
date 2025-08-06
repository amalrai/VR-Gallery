using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Cn.Ktgames.Util.Android;
using static DataJsonClass;

/// <summary>
/// 描述：
/// 功能：
/// 作者：yoyohan
/// 创建时间：2019-05-13 10:59:56
/// </summary>

namespace yoyohan
{
    public class UnityDownloadMgr
    {
        private static UnityDownloadMgr _instance;
        public static UnityDownloadMgr instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnityDownloadMgr();
                }
                return _instance;
            }
        }

        public const int TIMEOUT = 1000;

        public Action<DownloadObj> OnDownloadingAction;
        public Action<DownloadObj> OnDownloadedAction;
        public Action<DownloadObj> OnGetFileSizeAction;


        /// <summary>
        /// 触发下载事件 0GetFileSize 1Downloading 2Downloaded
        /// </summary>
        public void FireAction(int id, DownloadObj downloadObj)
        {
            if (id == 0 && OnGetFileSizeAction != null)
            {
                Debug.Log("id:" + id + "____downloadObj:" + downloadObj.fileName);
                OnGetFileSizeAction(downloadObj);
            }
            else if (id == 1 && OnDownloadingAction != null)
            {
                OnDownloadingAction(downloadObj);
            }
            else if (id == 2 && OnDownloadedAction != null)
            {
                Debug.Log("id:" + id + "____downloadObj:" + downloadObj.fileName);
                OnDownloadedAction(downloadObj);
            }
        }


        #region 定义的私有变量
        private Pool _unityDownloadPool = null;
        private Pool unityDownloadPool
        {
            get
            {
                if (_unityDownloadPool == null)
                {
                    GameObject go = new GameObject("UnityDownload");
                    go.AddComponent<UnityWeb>();
                    _unityDownloadPool = PoolMgr.instance.CreatPool(go, true, "UnityDownloadPool");
                    go.transform.SetParent(_unityDownloadPool.transform);
                    go.SetActive(false);
                }
                return _unityDownloadPool;
            }
        }


        /// <summary>
        /// 下载列表
        /// </summary>
        public List<DownloadObj> lisDownloadObj;





        public Pool GetUnityDownloadPool()
        {
            return unityDownloadPool;
        }

        public DownloadObj GetDownloadObjByID(string id)
        {
            for (int i = 0; i < lisDownloadObj.Count; i++)
            {
                if (lisDownloadObj[i].id == id)
                {
                    return lisDownloadObj[i];
                }
            }
            return null;
        }

        public void AddDownloadObj(DownloadObj downloadObj)
        {
            bool isFind = false;
            //必须这样赋值 才生效
            for (int i = 0; i < lisDownloadObj.Count; i++)
            {
                if (lisDownloadObj[i].id == downloadObj.id)
                {
                    lisDownloadObj[i] = downloadObj;
                    isFind = true;
                }
            }

            if (isFind == false)
            {
                lisDownloadObj.Add(downloadObj);
            }
        }


        public List<DownloadObj> GetAllDownloadObj()
        {
            return lisDownloadObj;
        }

        private void RemoveDownloadObj(DownloadObj downloadObj)
        {
            DownloadObj temp = GetDownloadObjByID(downloadObj.id);
            if (temp == null)
                return;

            if (downloadObj.currentDownloadState == DownloadState.downloading)
            {
                downloadObj.webDownload.CloseDownLoad();
            }

            lisDownloadObj.Remove(downloadObj);
        }
        #endregion


        /// <summary>
        /// 初始化下载器
        /// </summary>
        public void DownloaderInitialize()
        {
            Debug.Log("下载文件工具类初始化！");

            lisDownloadObj = new List<DownloadObj>();

            Downloader.Instance.failDownloadObj = new List<DownloadObj>();

            Downloader.Instance.currentDownloadObjsTotal = 0;

            Downloader.Instance.currentDownloadObjsAlready = 0;

            Downloader.Instance.currentDownloadSucceedIndex = 0;

            Downloader.Instance.currentDownloadFailIndex = 0;

            Downloader.Instance.isLoadDownload = false;
        }


        public DownloadObj StartDownloadOne(DownloadObj downloadObj)
        {
            UnityWeb unityWeb = unityDownloadPool.GetOneGo().GetComponent<UnityWeb>();
            unityWeb.gameObject.SetActive(true);
            downloadObj.SetWebDownload(unityWeb);
            Debug.Log(downloadObj.fileName + "：" + downloadObj.url);
            unityWeb.DownloadFileSize(downloadObj);
            Downloader.Instance.currentDownloadObjsTotal++;
            return downloadObj;
        }

        public void StartDownloadSingle(DownloadSingle[] sceneDownload)
        {

        }

        public void DeleteOneDownload(DownloadObj downloadObj, bool isDeleteLoaclFile)
        {
            RemoveDownloadObj(downloadObj);
            if (isDeleteLoaclFile == true)
            {
                if (File.Exists(downloadObj.fullPath))
                    File.Delete(downloadObj.fullPath);

                if (File.Exists(downloadObj.fullPath + ".tmp"))
                    File.Delete(downloadObj.fullPath + ".tmp");
            }
        }

        /// <summary>
        /// 开始下载更新文件
        /// </summary>
        public void StartDownloaderUpdate(string url, string name)
        {
            Downloader.Instance.isDownloadCheck = true;

            Downloader.Instance.isBatch = false;

            Downloader.Instance.downloadNum = 0;

            HttpManager.Instance.currentDownloadState = currentDownloadState.Update;

            Downloader.Instance.requestDownloadObj = new List<DownloadObj>();

            DownloadObj[] download = new DownloadObj[1];

            download[0] = new DownloadObj().SetUrl(url).SetParentPath(Application.persistentDataPath + "/1" + "/Update").SetFileName(name);
            Downloader.Instance.requestDownloadObj.Add(download[0]);
            Downloader.Instance.totalDownload++;

            //GetDownloadProgress.Instance.isDownload = true;
            BatchDownloadPMData();
        }
        /// <summary>
        /// 第一次创建xml并开启下载器
        /// </summary>
        public void CreateXMLStartDownloader()
        {
            DownloaderInitialize();

            Downloader.Instance.isBatch = false;

            Downloader.Instance.downloadNum = 0;

            HttpManager.Instance.currentDownloadState = currentDownloadState.Scene;

            Downloader.Instance.requestDownloadObj = new List<DownloadObj>();
        }

        //1.第一步启动下载器
        public void StartDownloaderScene()
        {
            DownloaderInitialize();

            Downloader.Instance.isDownloadCheck = true;

            Downloader.Instance.isBatch = false;

            Downloader.Instance.downloadNum = 0;

            HttpManager.Instance.currentDownloadState = currentDownloadState.Scene;

            Downloader.Instance.requestDownloadObj = new List<DownloadObj>();

            PaintingModuleDataManager PMData = PaintingModuleDataManager.Instance;

            List<DownloadObj> objs = new List<DownloadObj>();

            //获取本地xml文件的场景列表
            if (!PaintingModuleDataManager.Instance.GetXMLFile())
            {
                //如果xml文件场景列表为空，则直接下载服务器所有场景
                PMData.firstDownload = true;
                PMData.CreateXML();
                PMData.ExistSceneURL();

                int downloadCount = (PMData.allRoomResults.Count) * 2;
                DownloadObj[] download = new DownloadObj[downloadCount];

                for (int i = 0; i < PMData.allRoomResults.Count; i++)
                {
                    List<string> datas = new List<string>();

                    string str = "";

                    string[] musicGet = PMData.allRoomResults[i].musicUrl.Split('/');
                    //musicGet[musicGet.Length - 1]

                    string[] resourceGet = PMData.allRoomResults[i].resourceUrl.Split('/');
                    //resourceGet[resourceGet.Length - 1]

                    string[] resourceAndroidGet = PMData.allRoomResults[i].resourceAndroidUrl.Split('/');
                    //resourceAndroidGet[resourceAndroidGet.Length - 1]

                    string[] resourceIosGet = PMData.allRoomResults[i].resourceIosUrl.Split('/');
                    //resourceIosGet[resourceIosGet.Length - 1]

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                    str = PMData.allRoomResults[i].id +
                          "," + resourceGet[resourceGet.Length - 1] +
                          "," + PMData.allRoomResults[i].resourceUrl +
                          "," + PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources/" +
                          resourceGet[resourceGet.Length - 1] +
                          "," + musicGet[musicGet.Length - 1] +
                          "," + PMData.allRoomResults[i].musicUrl +
                          "," + PMData.sceneURL + PMData.allRoomResults[i].id + "/BGM/" +
                          musicGet[musicGet.Length - 1];
#elif UNITY_IOS


                     str = PMData.allRoomResults[i].id +
                                 "," + resourceIosGet[resourceIosGet.Length - 1] +
                                 "," + PMData.allRoomResults[i].resourceIosUrl +
                                 "," + PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources/" +
                                 resourceIosGet[resourceIosGet.Length - 1] +
                                 "," + musicGet[musicGet.Length - 1] +
                                 "," + PMData.allRoomResults[i].musicUrl +
                                 "," + PMData.sceneURL + PMData.allRoomResults[i].id + "/BGM/" +
                                 musicGet[musicGet.Length - 1];
#elif UNITY_ANDROID
                     str = PMData.allRoomResults[i].id +
                                 "," + resourceAndroidGet[resourceAndroidGet.Length - 1] +
                                 "," + PMData.allRoomResults[i].resourceAndroidUrl +
                                 "," + PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources/" +
                                 resourceAndroidGet[resourceAndroidGet.Length - 1] +
                                 "," + musicGet[musicGet.Length - 1] +
                                 "," + PMData.allRoomResults[i].musicUrl +
                                 "," + PMData.sceneURL + PMData.allRoomResults[i].id + "/BGM/" +
                                 musicGet[musicGet.Length - 1];
#endif
                    datas.Add(str);
                    PMData.FirstAddSceneXML(datas[i]);
                }



                for (int i = 0; i < PMData.allRoomResults.Count; i++)
                {
                    PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].id + "/");
                    PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].id + "/Resources");
                    PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].id + "/BGM");



#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                    if (!string.IsNullOrEmpty(PMData.allRoomResults[i].resourceUrl))
                    {
                        string[] resourceGet = PMData.allRoomResults[i].resourceUrl.Split('/');
                        //resourceGet[resourceGet.Length - 1]

                        download[i * 2] = new DownloadObj().SetUrl(PMData.allRoomResults[i].resourceUrl).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources").SetFileName(resourceGet[resourceGet.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(download[i * 2]);
                        Downloader.Instance.totalDownload++;
                        //GetDownloadProgress.Instance.DownloadDataConvert(PMData.allRoomResults[i].resourceSize);
                        Debug.Log("文件大小：" + PMData.allRoomResults[i].resourceSize.Substring(0, PMData.allRoomResults[i].resourceSize.Length - 1));
                    }



#elif UNITY_IOS
                     if (!string.IsNullOrEmpty(PMData.allRoomResults[i].resourceIosUrl))
                    {
                        string[] resourceIosGet = PMData.allRoomResults[i].resourceIosUrl.Split('/');
                        //resourceIosGet[resourceIosGet.Length - 1]

                        download[i * 2] = new DownloadObj().SetUrl(PMData.allRoomResults[i].resourceIosUrl).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources").SetFileName(resourceIosGet[resourceIosGet.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(download[i * 2]);
                        Downloader.Instance.totalDownload++;
                        //GetDownloadProgress.Instance.DownloadDataConvert(PMData.allRoomResults[i].resourceSize);
                        Debug.Log("文件大小：" + PMData.allRoomResults[i].resourceIosSize.Substring(0, PMData.allRoomResults[i].resourceIosSize.Length - 1));
                    }

#elif UNITY_ANDROID
  
                    if (!string.IsNullOrEmpty(PMData.allRoomResults[i].resourceAndroidUrl))
                    {
                         string[] resourceAndroidGet = PMData.allRoomResults[i].resourceAndroidUrl.Split('/');
                        //resourceAndroidGet[resourceAndroidGet.Length - 1]

                        download[i * 2] = new DownloadObj().SetUrl(PMData.allRoomResults[i].resourceAndroidUrl).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources").SetFileName(resourceAndroidGet[resourceAndroidGet.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(download[i * 2]);
                        Downloader.Instance.totalDownload++;
                        //GetDownloadProgress.Instance.DownloadDataConvert(PMData.allRoomResults[i].resourceSize);
                        Debug.Log("文件大小：" + PMData.allRoomResults[i].resourceAndroidSize.Substring(0, PMData.allRoomResults[i].resourceAndroidSize.Length - 1));
                    }

#endif

                    if (!string.IsNullOrEmpty(PMData.allRoomResults[i].musicUrl))
                    {
                        string[] musicGet = PMData.allRoomResults[i].musicUrl.Split('/');
                        //musicGet[musicGet.Length - 1]

                        download[(i * 2) + 1] = new DownloadObj().SetUrl(PMData.allRoomResults[i].musicUrl).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].id + "/BGM").SetFileName(musicGet[musicGet.Length - 1]);
                        Downloader.Instance.requestDownloadObj.Add(download[(i * 2) + 1]);
                        Downloader.Instance.totalDownload++;
                        //GetDownloadProgress.Instance.DownloadDataConvert(PMData.allRoomResults[i].musicSize);
                    }





                    if (((i * 2) + 1) == download.Length - 1)
                    {

                        //GetDownloadProgress.Instance.isDownload = true;
                        //BatchDownloadPMData();
                    }
                }
            }
            else
            {
                ////如果xml文件场景列表不为空，判断名字和版本是否一样
                //int xmlIndex = 1;
                //DownloadObj[] download = new DownloadObj[sceneDownload.Length];
                //for (int i = 0; i < sceneDownload.Length; i++)
                //{
                //    if (i <= xmlIndex - 1)
                //    {
                //        //xml存在场景记录
                //        if ("xml记录的名字" == sceneDownload[i].downloadName)
                //        {
                //            //名字一样，判断版本
                //            if ("xml记录的版本" == sceneDownload[i].versions)
                //            {
                //                //名字和版本都一样，判断文件是否下载完整
                //                if (File.Exists(sceneDownload[i].saveURL + sceneDownload[i].downloadName + ".tmp"))
                //                {
                //                    Debug.Log(sceneDownload[i].downloadName + "文件没有下载完整，重新下载");
                //                    File.Delete(sceneDownload[i].saveURL + sceneDownload[i].downloadName + ".tmp");
                //                    download[i] = new DownloadObj().SetID("1").SetUrl("").SetParentPath("").SetFileName("");
                //                    StartDownloadOne(download[i]);
                //                }
                //                else
                //                {
                //                    Debug.Log(sceneDownload[i].downloadName + "文件完整");
                //                }
                //            }
                //            else
                //            {
                //                if (File.Exists(sceneDownload[i].saveURL + sceneDownload[i].downloadName + ".tmp"))
                //                {
                //                    Debug.Log(sceneDownload[i].downloadName + "文件没有下载完整，重新下载");
                //                    File.Delete(sceneDownload[i].saveURL + sceneDownload[i].downloadName + ".tmp");
                //                }
                //                else if (File.Exists(sceneDownload[i].saveURL + sceneDownload[i].downloadName))
                //                {
                //                    Debug.Log(sceneDownload[i].downloadName + "文件版本不对，重新下载");
                //                    File.Delete(sceneDownload[i].saveURL + sceneDownload[i].downloadName);
                //                }
                //                else
                //                {
                //                    Debug.Log(sceneDownload[i].downloadName + "文件不存在，添加下载");
                //                }
                //                download[i] = new DownloadObj().SetID("1").SetUrl("").SetParentPath("").SetFileName("");
                //                StartDownloadOne(download[i]);
                //            }
                //        }
                //        else
                //        {
                //            if (File.Exists("xml本地文件路径" + ".tmp"))
                //            {
                //                Debug.Log(sceneDownload[i].downloadName + "文件没有下载对，重新下载");
                //                File.Delete("xml本地文件路径" + ".tmp");
                //            }
                //            else if (File.Exists("xml本地文件路径"))
                //            {
                //                Debug.Log(sceneDownload[i].downloadName + "文件版本不对，重新下载");
                //                File.Delete("xml本地文件路径");
                //            }
                //            else
                //            {
                //                Debug.Log(sceneDownload[i].downloadName + "文件不存在，添加下载");
                //            }
                //            //名字不一样，删除文件，重新下载
                //            download[i] = new DownloadObj().SetID("1").SetUrl("").SetParentPath("").SetFileName("");
                //            StartDownloadOne(download[i]);
                //        }
                //    }
                //    else
                //    {
                //        //xml不存在场景记录,直接添加到下载里面
                //        download[i] = new DownloadObj().SetID("1").SetUrl("").SetParentPath("").SetFileName("");
                //        StartDownloadOne(download[i]);
                //    }

                //    if (i == sceneDownload.Length - 1)
                //    {
                //        isLoadDownload = false;
                //    }
                //}
            }
        }
        /// <summary>
        /// 下载对比缺失的场景资源
        /// </summary>
        public void CompareDownloaderScene()
        {
            lisDownloadObj = new List<DownloadObj>();

            Downloader.Instance.currentDownloadObjsTotal = 0;

            Downloader.Instance.currentDownloadObjsAlready = 0;

            Downloader.Instance.currentDownloadSucceedIndex = 0;

            Downloader.Instance.currentDownloadFailIndex = 0;

            Downloader.Instance.isLoadDownload = true;

            Downloader der = Downloader.Instance;

            List<DownloadObj> fail = new List<DownloadObj>();
            foreach (DownloadObj VARIABLE in PaintingModuleDataManager.Instance.lackDownloadObj)
            {
                fail.Add(VARIABLE);
            }
            DownloadObj[] objs = new DownloadObj[fail.Count];

            for (int i = 0; i < fail.Count; i++)
            {
                objs[i] = new DownloadObj().SetUrl(fail[i].url).SetParentPath(fail[i].parentPath).SetFileName(fail[i].fileName);
                StartDownloadOne(objs[i]);
                if (i == fail.Count - 1)
                {
                    Downloader.Instance.isLoadDownload = false;
                }
            }
        }

        /// <summary>
        /// 再次下载场景资源
        /// </summary>
        public void AgainDownloaderScene()
        {
            Debug.Log("下载文件工具类初始化！");

            lisDownloadObj = new List<DownloadObj>();

            Downloader.Instance.currentDownloadObjsTotal = 0;

            Downloader.Instance.currentDownloadObjsAlready = 0;

            Downloader.Instance.currentDownloadSucceedIndex = 0;

            Downloader.Instance.currentDownloadFailIndex = 0;

            Downloader.Instance.isLoadDownload = true;

            Downloader der = Downloader.Instance;

            List<DownloadObj> fail = new List<DownloadObj>();
            for (int i = 0; i < der.failDownloadObj.Count; i++)
            {
                fail.Add(der.failDownloadObj[i]);
            }

            der.failDownloadObj = new List<DownloadObj>();

            DownloadObj[] objs = new DownloadObj[fail.Count];

            for (int i = 0; i < fail.Count; i++)
            {
                objs[i] = new DownloadObj().SetUrl(fail[i].url).SetParentPath(fail[i].parentPath).SetFileName(fail[i].fileName);
                StartDownloadOne(objs[i]);
                if (i == fail.Count - 1)
                {
                    Downloader.Instance.isLoadDownload = false;
                }
            }
        }

        /// <summary>
        /// 场景停止下载
        /// </summary>
        public void StopDownloaderScene()
        {
            HttpManager.Instance.DetectionRoomPMData();
        }

        /// <summary>
        /// 开始下载画作信息
        /// </summary>

        //public void StartDownloaderPaintingModule()
        //{
        //    DownloaderInitialize();

        //    HttpManager.Instance.currentDownloadState = currentDownloadState.PaintingModule;

        //    //Downloader.Instance.isLoadDownload = true;

        //    PaintingModuleDataManager PMData = PaintingModuleDataManager.Instance;

        //    List<DownloadObj> objs = new List<DownloadObj>();

        //    if (PMData.firstDownload)
        //    {
        //        List<PMHttpData> PMHttpDatas = new List<PMHttpData>();

        //        for (int i = 0; i < PMData.allRoomResults.Count; i++)
        //        {
        //            PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].name + "/PM");

        //            for (int j = 0; j < PMData.roomAllExhibitsData[i].Count; j++)
        //            {
        //                if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].name))
        //                {
        //                    PMHttpData data = new PMHttpData();

        //                    data.info = new PMInfo();
        //                    if (!string.IsNullOrEmpty(PMData.allRoomResults[i].name))
        //                        data.info.PMroom = PMData.allRoomResults[i].name;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].name))
        //                        data.info.PMName = PMData.roomAllExhibitsData[i][j].name;
        //                    if (PMData.roomAllExhibitsData[i][j].name != null)
        //                        data.info.PMNo = PMData.roomAllExhibitsData[i][j].exhibitsNo;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].text))
        //                        data.info.PMText = PMData.roomAllExhibitsData[i][j].text;

        //                    data.mainGraph = new PMMainGraph();
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].mainGraphName))
        //                        data.mainGraph.mainGraphName = PMData.roomAllExhibitsData[i][j].mainGraphName;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].mainGraphUrl))
        //                        data.mainGraph.mainGraphHttpUrl = PMData.roomAllExhibitsData[i][j].mainGraphUrl;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].mainGraphName))
        //                        data.mainGraph.mainGraphSaveUrl = PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" +
        //                                                      PMData.roomAllExhibitsData[i][j].name + "/MainGraph/" +
        //                                                      PMData.roomAllExhibitsData[i][j].mainGraphName;

        //                    data.introductionImage = new PMIntroductionImage();
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].introductionImageName))
        //                        data.introductionImage.introductionImageName = PMData.roomAllExhibitsData[i][j].introductionImageName;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].introductionImageUrl))
        //                        data.introductionImage.introductionImageHttpUrl = PMData.roomAllExhibitsData[i][j].introductionImageUrl;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].introductionImageName))
        //                        data.introductionImage.introductionImageSaveUrl =
        //                        PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" +
        //                        PMData.roomAllExhibitsData[i][j].name + "/IntroductionImage/" +
        //                        PMData.roomAllExhibitsData[i][j].introductionImageName;

        //                    data.video = new PMVideo();
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].videoName))
        //                        data.video.videoName = PMData.roomAllExhibitsData[i][j].videoName;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].videoUrl))
        //                        data.video.videoHttpUrl = PMData.roomAllExhibitsData[i][j].videoUrl;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].videoName))
        //                        data.video.videoSaveUrl = PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/Video/" + PMData.roomAllExhibitsData[i][j].videoName;

        //                    data.voice = new PMVoice();
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].voiceName))
        //                        data.voice.voiceName = PMData.roomAllExhibitsData[i][j].voiceName;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].voiceUrl))
        //                        data.voice.voiceHttpUrl = PMData.roomAllExhibitsData[i][j].voiceUrl;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].voiceName))
        //                        data.voice.voiceSaveUrl = PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/Voice/" + PMData.roomAllExhibitsData[i][j].voiceName;

        //                    data.animationThumbnail = new PMAnimationThumbnail();
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].animationThumbnailName))
        //                        data.animationThumbnail.animationThumbnailName = PMData.roomAllExhibitsData[i][j].animationThumbnailName;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].animationThumbnailUrl))
        //                        data.animationThumbnail.animationThumbnailHttpUrl = PMData.roomAllExhibitsData[i][j].animationThumbnailUrl;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].animationThumbnailName))
        //                        data.animationThumbnail.animationThumbnailSaveUrl = PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/AnimationThumbnail/" + PMData.roomAllExhibitsData[i][j].animationThumbnailName;

        //                    data.frameAnimation = new PMFrameAnimation();
        //                    data.frameAnimation.frameAnimationHttpUrl = PMData.roomAllExhibitsData[i][j].frameAnimationUrl.Split(',');
        //                    data.frameAnimation.frameAnimationSaveUrl = new string[data.frameAnimation.frameAnimationHttpUrl.Length];
        //                    for (int k = 0; k < data.frameAnimation.frameAnimationSaveUrl.Length; k++)
        //                    {
        //                        string[] get = data.frameAnimation.frameAnimationHttpUrl[k].Split('/');
        //                        data.frameAnimation.frameAnimationSaveUrl[k] = PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/FrameAnimationUrl/" + get[get.Length - 1];
        //                    }

        //                    data.link = new PMLink();
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].videoLink))
        //                        data.link.videoLink = PMData.roomAllExhibitsData[i][j].videoLink;
        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].webLink))
        //                        data.link.webLink = PMData.roomAllExhibitsData[i][j].webLink;

        //                    PMHttpDatas.Add(data);
        //                }
        //            }
        //        }

        //        PMData.FirstAddPMXML(PMHttpDatas);

        //        for (int i = 0; i < PMData.allRoomResults.Count; i++)
        //        {
        //            for (int j = 0; j < PMData.roomAllExhibitsData[i].Count; j++)
        //            {
        //                if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].name))
        //                {
        //                    PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/MainGraph");
        //                    PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/IntroductionImage");
        //                    PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/Video");
        //                    PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/Voice");
        //                    PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/AnimationThumbnail");
        //                    PMData.ExistSceneDownloadURL(PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/FrameAnimationUrl");

        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].mainGraphUrl))
        //                    {
        //                        DownloadObj download01 = new DownloadObj().SetUrl(PMData.roomAllExhibitsData[i][j].mainGraphUrl).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/MainGraph").SetFileName(PMData.roomAllExhibitsData[i][j].mainGraphName);
        //                        objs.Add(download01);
        //                        StartDownloadOne(download01);
        //                    }

        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].introductionImageUrl))
        //                    {
        //                        DownloadObj download02 = new DownloadObj().SetUrl(PMData.roomAllExhibitsData[i][j].introductionImageUrl).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/IntroductionImage").SetFileName(PMData.roomAllExhibitsData[i][j].introductionImageName);
        //                        objs.Add(download02);
        //                        StartDownloadOne(download02);
        //                    }


        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].videoUrl))
        //                    {
        //                        DownloadObj download03 = new DownloadObj().SetUrl(PMData.roomAllExhibitsData[i][j].videoUrl).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/Video").SetFileName(PMData.roomAllExhibitsData[i][j].videoName);
        //                        objs.Add(download03);
        //                        StartDownloadOne(download03);
        //                    }


        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].voiceUrl))
        //                    {
        //                        DownloadObj download04 = new DownloadObj().SetUrl(PMData.roomAllExhibitsData[i][j].voiceUrl).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/Voice").SetFileName(PMData.roomAllExhibitsData[i][j].voiceName);
        //                        objs.Add(download04);
        //                        StartDownloadOne(download04);
        //                    }


        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].animationThumbnailUrl))
        //                    {
        //                        DownloadObj download05 = new DownloadObj().SetUrl(PMData.roomAllExhibitsData[i][j].animationThumbnailUrl).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/AnimationThumbnail").SetFileName(PMData.roomAllExhibitsData[i][j].animationThumbnailName);
        //                        objs.Add(download05);
        //                        StartDownloadOne(download05);

        //                    }

        //                    if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].frameAnimationUrl))
        //                    {
        //                        string[] urls = PMData.roomAllExhibitsData[i][j].frameAnimationUrl.Split(',');
        //                        string[] names = new string[urls.Length];
        //                        DownloadObj[] frames = new DownloadObj[urls.Length];
        //                        for (int k = 0; k < urls.Length; k++)
        //                        {
        //                            string[] get = urls[k].Split('/');
        //                            names[k] = get[get.Length - 1];
        //                            frames[k] = new DownloadObj().SetUrl(urls[k]).SetParentPath(PMData.sceneURL + PMData.allRoomResults[i].name + "/PM/" + PMData.roomAllExhibitsData[i][j].name + "/FrameAnimationUrl").SetFileName(names[k]);
        //                            objs.Add(frames[k]);
        //                            StartDownloadOne(frames[k]);
        //                        }
        //                    }
        //                }

        //                if (i == PMData.allRoomResults.Count - 1 &&
        //                    j == PMData.roomAllExhibitsData[PMData.allRoomResults.Count - 1].Count - 1)
        //                {
        //                    Downloader.Instance.isLoadDownload = false;
        //                }
        //            }

        //        }

        //    }
        //}

        public void StartDownloaderPaintingModule()
        {
            DownloaderInitialize();

            Downloader.Instance.isDownloadCheck = true;

            Downloader.Instance.isBatch = false;

            Downloader.Instance.downloadNum = 0;


            HttpManager.Instance.currentDownloadState = currentDownloadState.PaintingModule;

            Downloader.Instance.requestDownloadObj = new List<DownloadObj>();

            PaintingModuleDataManager PMData = PaintingModuleDataManager.Instance;


        }


        public void DownloaderPaintingModulenData()
        {
            //下载画作结束
            Debug.Log("画作下载结束，可以进入场景");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }

        /// <summary>
        /// 第一次下载画作模块，没xml文件
        /// </summary>
        public void DownloaderPaintingModulenNoXML()
        {
            Downloader.Instance.PM[Downloader.Instance.PMIndex].ImportWebData();
            DownloadObj[] download = Downloader.Instance.PM[Downloader.Instance.PMIndex].ImportDownloadData();
            for (int i = 0; i < download.Length; i++)
            {
                StartDownloadOne(download[i]);
                if (i == download.Length - 1)
                {
                    Downloader.Instance.isLoadDownload = false;
                }
            }

            if (Downloader.Instance.PMIndex != Downloader.Instance.PM.Length - 1)
            {
                Downloader.Instance.PMIndex++;
            }
            else
            {
                Downloader.Instance.isDownloadPMEnd = true;
            }

        }
        /// <summary>
        /// 下载画作模块，有xml文件
        /// </summary>
        public void DownloaderPaintingModulenXML()
        {

        }

        /// <summary>
        /// 停止bgm的下载，数据下载完成，转入其他场景
        /// </summary>
        public void StopDownloaderBGM()
        {
            Debug.Log("停止bgm的下载，数据下载完成，转入其他场景");
        }


        /// <summary>
        /// 场景是否下载完成
        /// </summary>
        public void JudgeDownloader()
        {
            if (!Downloader.Instance.isLoadDownload)
            {
                if (HttpManager.Instance.GetNetworkState())
                {
                    Debug.Log("currentDownloadObjsTotal" + Downloader.Instance.currentDownloadObjsTotal);
                    Debug.Log("currentDownloadObjsAlready" + Downloader.Instance.currentDownloadObjsAlready);
                    if (Downloader.Instance.currentDownloadObjsTotal == Downloader.Instance.currentDownloadObjsAlready)
                    {
                        if (Downloader.Instance.currentDownloadFailIndex != 0)
                        {
                            Debug.Log("有下载错误的文件，重新下载");
                            AgainDownloaderScene();
                        }
                        else
                        {
                            Debug.Log("下载进程执行完成");
                            switch (HttpManager.Instance.currentDownloadState)
                            {
                                case currentDownloadState.Update:
                                    //GetDownloadProgress.Instance.DownloadSucceed();
                                    Downloader.Instance.isDownloadCheck = false;
                                    Downloader.Instance.downloadTimer = 0;
                                    ClickInstall();
                                    //添加下载成功调用
                                    break;
                                case currentDownloadState.Scene:
                                    PaintingModuleDataManager.Instance.SaveXML();
                                    //GetDownloadProgress.Instance.DownloadSucceed();
                                    Downloader.Instance.isDownloadCheck = false;
                                    Downloader.Instance.downloadTimer = 0;
                                    StopDownloaderScene();
                                    break;
                                case currentDownloadState.PaintingModule:
                                    Debug.Log("画作下载进度");
                                    if (Downloader.Instance.isBatch)
                                    {
                                        //GetDownloadProgress.Instance.DownloadSucceed();
                                        Downloader.Instance.isDownloadCheck = false;
                                        Downloader.Instance.downloadTimer = 0;
                                        CheckoutAllData();
                                    }
                                    else
                                    {
                                        Debug.Log("画作还有东西下载");
                                        BatchDownloadPMData();
                                    }
                                    break;
                                case currentDownloadState.Checkout:

                                    if (Downloader.Instance.isBatch)
                                    {
                                        Debug.Log("画作下载完成");
                                        Downloader.Instance.isDownloadCheck = false;
                                        Downloader.Instance.downloadTimer = 0;
                                        DownloaderPaintingModulenData();
                                        _unityDownloadPool.DestroyThisPool();
                                    }
                                    else
                                    {
                                        Debug.Log("画作还有东西下载");
                                        BatchDownloadPMData();
                                    }

                                    break;
                                case currentDownloadState.DownloadRoomData:
                                    if (Downloader.Instance.isBatch)
                                    {
                                        Debug.Log("首次下载完成");
                                        PaintingModuleDataManager.Instance.currentSelectSceneDataID = PaintingModuleDataManager.Instance.getSceneData.sceneID;
                                        PaintingModuleDataManager.Instance.currentSelectSceneDataName = PaintingModuleDataManager.Instance.getSceneData.sceneName;
                                        PaintingModuleDataManager.Instance.currentSelectGalleryID = PaintingModuleDataManager.Instance.getSceneData.museumID;
                                        HttpManager.Instance.NetWorkUpdate(NetTypeListen.UploadUserLoginLog, null);
                                        PaintingModuleDataManager.Instance.currentSelectSceneMask.enabled = false;
                                        PaintingModuleDataManager.Instance.currentSelectSceneMask.fillAmount = 1;
                                        PaintingModuleDataManager.Instance.getSceneData.isDownload = true;
                                        Downloader.Instance.isDownloadCheck = false;
                                        Downloader.Instance.isBatch = false;
                                        Downloader.Instance.downloadTimer = 0;
                                        Downloader.Instance.downloadNum = 0;
                                        Downloader.Instance.totalDownload = 0;
                                        Downloader.Instance.currentDownload = 0;
                                        //DownloaderPaintingModulenData();
                                        _unityDownloadPool.DestroyThisPool();
                                        PopupWindowCanvasManager.Instance.DownloadSuccessCanvasGroupEvent();
                                    }
                                    else
                                    {
                                        Debug.Log("首次下载还未完成");
                                        BatchDownloadPMData();
                                    }

                                    break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 校验所有资源
        /// </summary>
        public void CheckoutAllData()
        {
            Downloader.Instance.isBatch = false;

            Debug.Log("校验所有资源");

            Downloader.Instance.requestDownloadObj = new List<DownloadObj>();

            //GetDownloadProgress.Instance.DownloadCheckout();

            Downloader.Instance.failDownloadObj = new List<DownloadObj>();

            PaintingModuleDataManager PMData = PaintingModuleDataManager.Instance;

            for (int i = 0; i < PMData.allRoomResults.Count; i++)
            {

                string[] resourceGet = PMData.allRoomResults[i].resourceUrl.Split('/');
                //resourceGet[resourceGet.Length - 1]

                string[] resourceAndroidGet = PMData.allRoomResults[i].resourceAndroidUrl.Split('/');
                //resourceAndroidGet[resourceAndroidGet.Length - 1]

                string[] resourceIosGet = PMData.allRoomResults[i].resourceIosUrl.Split('/');
                //resourceIosGet[resourceIosGet.Length - 1]

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                ExistsFile(PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources", PMData.allRoomResults[i].resourceUrl, resourceGet[resourceGet.Length - 1]);
#elif UNITY_IOS
                ExistsFile(PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources", PMData.allRoomResults[i].resourceIosUrl, resourceIosGet[resourceIosGet.Length - 1]);
#elif UNITY_ANDROID
                ExistsFile(PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources", PMData.allRoomResults[i].resourceAndroidUrl, resourceAndroidGet[resourceAndroidGet.Length - 1]);
#endif
            }

            for (int i = 0; i < PMData.allRoomResults.Count; i++)
            {
                for (int j = 0; j < PMData.roomAllExhibitsData[i].Count; j++)
                {
                    if (PMData.roomAllExhibitsData[i][j].status == 0)
                    {
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].mainGraphEncodeUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].mainGraphEncodeUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFile(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/MainGraph", PMData.roomAllExhibitsData[i][j].mainGraphEncodeUrl, get[get.Length - 1]);
                        }
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].introductionImageEncodeUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].introductionImageEncodeUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFile(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/IntroductionImage", PMData.roomAllExhibitsData[i][j].introductionImageEncodeUrl, get[get.Length - 1]);
                        }
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].videoUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].videoUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFile(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/Video", PMData.roomAllExhibitsData[i][j].videoUrl, get[get.Length - 1]);
                        }
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].voiceUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].voiceUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFile(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/Voice", PMData.roomAllExhibitsData[i][j].voiceUrl, get[get.Length - 1]);

                        }
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].animationThumbnailEncodeUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].animationThumbnailEncodeUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFile(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/AnimationThumbnail", PMData.roomAllExhibitsData[i][j].animationThumbnailEncodeUrl, get[get.Length - 1]);
                        }

                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].frameAnimationEncodeUrl))
                        {
                            string[] urls = PMData.roomAllExhibitsData[i][j].frameAnimationEncodeUrl.Split(',');
                            string[] names = new string[urls.Length];
                            DownloadObj[] frames = new DownloadObj[urls.Length];
                            for (int k = 0; k < urls.Length; k++)
                            {
                                string[] get = urls[k].Split('/');
                                names[k] = get[get.Length - 1];
                                ExistsFile(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/FrameAnimationUrl", urls[k], names[k]);
                            }
                        }
                    }

                    if (i == PMData.allRoomResults.Count - 1 &&
                        j == PMData.roomAllExhibitsData[PMData.allRoomResults.Count - 1].Count - 1)
                    {
                        if (Downloader.Instance.requestDownloadObj.Count > 0)
                        {
                            Debug.Log("场景资源有不同的文件，需要下载");
                            Debug.Log("文件不存在个数：" + Downloader.Instance.requestDownloadObj.Count);

                            HttpManager.Instance.SetHttpState(HttpState.DownloadCheckoutData);

                            for (int k = 0; k < Downloader.Instance.requestDownloadObj.Count; k++)
                            {
                                Debug.Log(Downloader.Instance.requestDownloadObj[k].url);
                            }
                            HttpManager.Instance.currentDownloadState = currentDownloadState.Checkout;

                            Downloader.Instance.downloadNum = 0;

                            Downloader.Instance.isDownloadCheck = true;

                            //GetDownloadProgress.Instance.isDownload = true;

                            BatchDownloadPMData();
                        }
                        else
                        {
                            Debug.Log("画作资源对比相同，进入场景");
                            //GetDownloadProgress.Instance.FakeProgressScene();
                            //SceneManager.LoadScene("Main");
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 校验所有资源文件大小
        /// </summary>
        public void CheckoutAllDataFileSize()
        {
            Debug.Log("校验所有资源文件大小");

            PaintingModuleDataManager PMData = PaintingModuleDataManager.Instance;

            for (int i = 0; i < PMData.allRoomResults.Count; i++)
            {

                string[] resourceGet = PMData.allRoomResults[i].resourceUrl.Split('/');
                //resourceGet[resourceGet.Length - 1]

                string[] resourceAndroidGet = PMData.allRoomResults[i].resourceAndroidUrl.Split('/');
                //resourceAndroidGet[resourceAndroidGet.Length - 1]

                string[] resourceIosGet = PMData.allRoomResults[i].resourceIosUrl.Split('/');
                //resourceIosGet[resourceIosGet.Length - 1]

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                ExistsFileSize(PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources", PMData.allRoomResults[i].resourceSize, resourceGet[resourceGet.Length - 1]);
#elif UNITY_IOS
                ExistsFileSize(PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources", PMData.allRoomResults[i].resourceIosSize, resourceIosGet[resourceIosGet.Length - 1]);
#elif UNITY_ANDROID
                ExistsFileSize(PMData.sceneURL + PMData.allRoomResults[i].id + "/Resources", PMData.allRoomResults[i].resourceAndroidSize, resourceAndroidGet[resourceAndroidGet.Length - 1]);
#endif
            }

            for (int i = 0; i < PMData.allRoomResults.Count; i++)
            {
                for (int j = 0; j < PMData.roomAllExhibitsData[i].Count; j++)
                {
                    if (PMData.roomAllExhibitsData[i][j].status == 0)
                    {
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].mainGraphEncodeUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].mainGraphEncodeUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFileSize(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/MainGraph", PMData.roomAllExhibitsData[i][j].mainGraphSize, get[get.Length - 1]);
                        }
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].introductionImageEncodeUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].introductionImageEncodeUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFileSize(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/IntroductionImage", PMData.roomAllExhibitsData[i][j].introductionImageSize, get[get.Length - 1]);
                        }
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].videoUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].videoUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFileSize(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/Video", PMData.roomAllExhibitsData[i][j].videoSize, get[get.Length - 1]);
                        }
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].voiceUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].voiceUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFileSize(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/Voice", PMData.roomAllExhibitsData[i][j].voiceSize, get[get.Length - 1]);

                        }
                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].animationThumbnailEncodeUrl))
                        {
                            string[] get = PMData.roomAllExhibitsData[i][j].animationThumbnailEncodeUrl.Split('/');
                            //get[get.Length - 1]

                            ExistsFileSize(PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" + PMData.roomAllExhibitsData[i][j].exhibitsNo + "/AnimationThumbnail", PMData.roomAllExhibitsData[i][j].animationThumbnailSize, get[get.Length - 1]);
                        }

                        if (!string.IsNullOrEmpty(PMData.roomAllExhibitsData[i][j].frameAnimationEncodeUrl))
                        {

                            string[] urls = PMData.roomAllExhibitsData[i][j].frameAnimationEncodeUrl.Split(',');
                            string[] get = urls[urls.Length - 1].Split('/');

                            if (!ExistsFileSizeFrames(
                                PMData.sceneURL + PMData.allRoomResults[i].id + "/PM/" +
                                PMData.roomAllExhibitsData[i][j].exhibitsNo + "/FrameAnimationUrl", get[get.Length - 1]))
                            {
                                FileSizeAdd(PMData.roomAllExhibitsData[i][j].frameAnimationSize);
                            }

                        }
                    }

                    if (i == PMData.allRoomResults.Count - 1 &&
                        j == PMData.roomAllExhibitsData[PMData.allRoomResults.Count - 1].Count - 1)
                    {
                        if (PaintingModuleDataManager.Instance.fileSize != 0f)
                        {
                            Debug.Log("提示文件下载的大小");
                            PopupWindowCanvasManager.Instance.DownloadFileCanvasGroupOpenEvent();
                            PaintingModuleDataManager.Instance.fileSize = 0;
                            PaintingModuleDataManager.Instance.allRoomResults = new List<DataJsonClass.AllRoomResult>();
                            PaintingModuleDataManager.Instance.roomAllExhibitsData = new List<List<DataJsonClass.RoomAllExhibitsData>>();
                            HttpManager.Instance.currentGetRoomAllExhibitsDataIndex = 1;
                        }
                        else
                        {
                            Debug.Log("没有需要下载的文件");
                            PaintingModuleDataManager.Instance.fileSize = 0;
                            PaintingModuleDataManager.Instance.allRoomResults = new List<DataJsonClass.AllRoomResult>();
                            PaintingModuleDataManager.Instance.roomAllExhibitsData = new List<List<DataJsonClass.RoomAllExhibitsData>>();
                            HttpManager.Instance.currentGetRoomAllExhibitsDataIndex = 1;
                            HttpManager.Instance.DetectionRoomData();

                        }

                        break;
                    }
                }

            }
        }

        public void ExistsFile(string saveUrl, string httpUrl, string name)
        {
            if (!File.Exists(saveUrl + "/" + name))
            {
                if (File.Exists(saveUrl + "/" + name + ".tmp"))
                    File.Delete(saveUrl + "/" + name + ".tmp");
                Debug.Log("文件不存在：" + saveUrl + "/" + name);
                DownloadObj obj = new DownloadObj().SetUrl(httpUrl).SetParentPath(saveUrl).SetFileName(name);
                Downloader.Instance.requestDownloadObj.Add(obj);
                Downloader.Instance.totalDownload++;
            }

        }

        public void ExistsFileSize(string saveUrl, string size, string name)
        {
            Debug.Log("ExistsFileURL:" + saveUrl + "/" + name);
            Debug.Log(File.Exists(saveUrl + "/" + name).ToString());

            if (!File.Exists(saveUrl + "/" + name))
            {
                if (File.Exists(saveUrl + "/" + name + ".tmp"))
                    File.Delete(saveUrl + "/" + name + ".tmp");
                Debug.Log("文件不存在：" + saveUrl + "/" + name);

                FileSizeAdd(size);
            }
        }

        public bool ExistsFileSizeFrames(string saveUrl, string name)
        {

            if (!File.Exists(saveUrl + "/" + name))
            {
                if (File.Exists(saveUrl + "/" + name + ".tmp"))
                    File.Delete(saveUrl + "/" + name + ".tmp");
                Debug.Log("文件不存在：" + saveUrl + "/" + name);

                return false;
            }

            return true;

        }

        /// <summary>
        /// 分批下载
        /// </summary>
        public void BatchDownloadPMData()
        {
            int num = 200;

            DownloaderInitialize();
            Downloader der = Downloader.Instance;
            int index = der.downloadNum;
            int aa = index + num;
            Downloader.Instance.isLoadDownload = true;

            Downloader.Instance.downloadingDownloadObj = new List<DownloadObj>();

            if (aa > der.requestDownloadObj.Count)
            {
                der.isBatch = true;
                for (int i = index; i < der.requestDownloadObj.Count; i++)
                {
                    if (i == der.requestDownloadObj.Count - 1)
                    {
                        Downloader.Instance.isLoadDownload = false;
                    }
                    Downloader.Instance.downloadingDownloadObj.Add(der.requestDownloadObj[i]);
                    StartDownloadOne(der.requestDownloadObj[i]);
                }
            }
            else
            {
                der.downloadNum += num;
                for (int i = der.downloadNum - num; i < der.downloadNum; i++)
                {
                    if (i == der.downloadNum - 1)
                    {
                        Downloader.Instance.isLoadDownload = false;
                    }
                    Downloader.Instance.downloadingDownloadObj.Add(der.requestDownloadObj[i]);
                    StartDownloadOne(der.requestDownloadObj[i]);
                }
            }


        }

        public void ClickInstall()
        {
            ApkUtility.Init();
            ApkUtility.onInstallResult = (code, path, message) =>
            {
                Debug.LogFormat("Code:{0},文件路径:{1},信息{2}", code, path, message);
                //CodeText.text = string.Format("结果码：{0}", code);
                //PathText.text = string.Format("安装路径：{0}", path);
                //MessageText.text = string.Format("结果信息：{0}", message);
            };
            ApkUtility.InstallApk(Application.persistentDataPath + "/1" + "/Update/" + PaintingModuleDataManager.Instance.appAndroidName);
        }

        public void FileSizeAdd(string file)
        {

            float sta1 = 0;
            float.TryParse(file.Substring(0, file.Length - 1), out sta1);

            PaintingModuleDataManager.Instance.fileSize += sta1;

            Debug.Log("current File size:" + file);
            Debug.Log("File size:" + PaintingModuleDataManager.Instance.fileSize);
        }

        /// <summary>
        /// 与检测的要下载的房间数据不相同,重置统计的下载数据
        /// </summary>
        public void DownloadMgrResetData()
        {
            PaintingModuleDataManager.Instance.fileSize = 0;

            lisDownloadObj = new List<DownloadObj>();

        }

        /// <summary>
        /// 停止下载事件
        /// </summary>
        public void StopDownloadEvent()
        {
            Debug.Log("停止下载事件");

            Downloader.Instance.isDownloadCheck = false;

            Downloader.Instance.downloadTimer = 0;
            _unityDownloadPool.DestroyThisPool();
            Downloader.Instance.DownloaderSwitchRoomsResetData();

            DownloadMgrResetData();

            PaintingModuleDataManager.Instance.GetDownloadData = false;
            PaintingModuleDataManager.Instance.currentSelectSceneData = null;
            PaintingModuleDataManager.Instance.getSceneData = null;
            PaintingModuleDataManager.Instance.currentSelectSceneMask = null;

        }
    }

}



