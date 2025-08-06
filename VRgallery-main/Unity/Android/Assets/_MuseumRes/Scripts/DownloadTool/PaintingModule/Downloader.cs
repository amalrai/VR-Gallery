using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using yoyohan;
using UnityEngine.UI;

public class Downloader : UnitySingleton<Downloader>
{
    /// <summary>
    /// 是否还在获取需要下载的数据
    /// </summary>
    public bool isLoadDownload;
    /// <summary>
    /// 当前需要下载总数
    /// </summary>
    public int currentDownloadObjsTotal;
    /// <summary>
    /// 当前已经下载总数
    /// </summary>
    public int currentDownloadObjsAlready;
    /// <summary>
    /// 当前下载成功数量
    /// </summary>
    public int currentDownloadSucceedIndex;
    /// <summary>
    /// 当前下载失败数量
    /// </summary>
    public int currentDownloadFailIndex;
    /// <summary>
    /// 当前下载房间画作系数
    /// </summary>
    public int currentDownloadRoomPMIndex;
    /// <summary>
    /// 当前画作模块下载个数
    /// </summary>
    public int PMIndex;
    /// <summary>
    /// 是否有本地画作xml数据
    /// </summary>
    public bool isPMXMLData;
    /// <summary>
    /// 是否下载画作到最后一组
    /// </summary>
    public bool isDownloadPMEnd;
    /// <summary>
    /// 需要下载的画作模块
    /// </summary>
    public DownloadPaintingModule[] PM;
    /// <summary>
    /// 请求下载的文件
    /// </summary>
    public List<DownloadObj> requestDownloadObj;
    /// <summary>
    /// 正在下载的文件
    /// </summary>
    public List<DownloadObj> downloadingDownloadObj;
    /// <summary>
    /// 是否在下载
    /// </summary>
    public bool isDownloadCheck = false;

    public int downloadNum = 0;
    public bool isBatch = false;

    public int totalDownload = 0;
    public int currentDownload = 0;
    /// <summary>
    /// 下载有问题的文件
    /// </summary>
    public List<DownloadObj> failDownloadObj;

    void OnProgress(DownloadObj downloadObj)
    {
        //if (mDownloadObj == null || mDownloadObj.id != downloadObj.id)
        //    return;

        //Debug.Log(downloadObj.sDownloadFileResult.downloadedLength + "   " + downloadObj.sDownloadFileResult.contentLength);

        //if (downloadAmount == null)
        //{
        //    downloadObj.OnProgressAction -= OnProgress;
        //    downloadObj.OnCompleteAction -= OnComplete;
        //    return;
        //}

        //downloadAmount.fillAmount = (float)downloadObj.sDownloadFileResult.downloadedLength / downloadObj.sDownloadFileResult.contentLength;
    }

    void OnComplete(DownloadObj downloadObj)
    {
        //if (mDownloadObj == null || mDownloadObj.id != downloadObj.id)
        //    return;

        Debug.Log("收到下载完成回调!  下载状态:" + downloadObj.currentDownloadState.ToString() + "  下载路径:" + downloadObj.parentPath + "下载名称:" + downloadObj.fileName);

    }

    void OnGetFileSize(DownloadObj downloadObj)
    {
        //if (mDownloadObj == null || mDownloadObj.id != downloadObj.id)
        //    return;
        Debug.Log("获取下载的文件大小成功！ 下载状态：" + downloadObj.currentDownloadState.ToString() + " 文件大小:" + downloadObj.sDownloadFileResult.contentLengthStr);
        downloadObj.webDownload.DownloadFile(downloadObj);
    }

    void OnEnable()
    {
        UnityDownloadMgr.instance.OnDownloadingAction += OnProgress;
        UnityDownloadMgr.instance.OnDownloadedAction += OnComplete;
        UnityDownloadMgr.instance.OnGetFileSizeAction += OnGetFileSize;
    }
    void OnDisable()
    {
        UnityDownloadMgr.instance.OnDownloadingAction -= OnProgress;
        UnityDownloadMgr.instance.OnDownloadedAction -= OnComplete;
        UnityDownloadMgr.instance.OnGetFileSizeAction -= OnGetFileSize;
    }

    public Text text;
    public void Update()
    {
        //text.text = "Progress:" + currentDownload + "/" + totalDownload + "\n" + "isLoadDownload:  " + isLoadDownload + "\n" + "currentDownloadObjsTotal: " + currentDownloadObjsTotal + "\n" + "currentDownloadObjsAlready: " + currentDownloadObjsAlready + "\n" + "currentDownloadSucceedIndex: " + currentDownloadSucceedIndex + "\n" + "currentDownloadFailIndex: " + currentDownloadFailIndex;

        if (isDownloadCheck)
        {
            downloadTimer += Time.deltaTime;
            if (downloadTimer >= 2f)
            {
                DownloadCheck();
                downloadTimer = 0;
            }
        }
    }

    public float downloadTimer = 0;
    /// <summary>
    /// 下载校验
    /// </summary>
    public void DownloadCheck()
    {
        Debug.Log("下载校验");

        if (!HttpManager.Instance.GetNetworkState())
            PopupWindowCanvasManager.Instance.PopupWindow3DOpenEvent(PopupWindow3DState.HttpError);

        if (HttpManager.Instance.GetNetworkState())
        {
            if(PopupWindowCanvasManager.Instance.PopupWindow3D.blocksRaycasts)
                PopupWindowCanvasManager.Instance.PopupWindow3DCloseEvent();
        }


        //if (downloadingDownloadObj != null || downloadingDownloadObj.Count != 0)
        //{
        //    Debug.Log("校验文件是否在下载");

        //    for (int i = 0; i < downloadingDownloadObj.Count; i++)
        //    {
        //        Debug.Log(downloadingDownloadObj[i].fileName + "_StaRu2u_contentLength" + downloadingDownloadObj[i].sDownloadFileResult.contentLength);
        //        Debug.Log(downloadingDownloadObj[i].fileName + "_StaRu2u_downloadedLength" + downloadingDownloadObj[i].sDownloadFileResult.downloadedLength);
        //    }
        //}
        //else
        //{
        //    Debug.Log("没有正在下载的文件");
        //}
    }

    /// <summary>
    /// 下载器切换房间重置数据
    /// </summary>
    public void DownloaderSwitchRoomsResetData()
    {
        failDownloadObj = new List<DownloadObj>();

        currentDownloadObjsTotal = 0;

        currentDownloadObjsAlready = 0;

        currentDownloadSucceedIndex = 0;

        currentDownloadFailIndex = 0;

        isLoadDownload = false;
    }
}
