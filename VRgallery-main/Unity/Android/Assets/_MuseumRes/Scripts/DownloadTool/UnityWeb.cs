using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace yoyohan
{
    public struct RequestHandler
    {
        public UnityWebRequest request;
        public UnityDownloadFileHandler handler;
        public RequestHandler(UnityWebRequest request, UnityDownloadFileHandler handler)
        {
            this.request = request;
            this.handler = handler;
        }
    }
    public class UnityWeb : MonoBehaviour, IWebDownload
    {
        private Dictionary<string, RequestHandler> requestDict = new Dictionary<string, RequestHandler>();
        private DownloadObj mDownloadObj;

        public void DownloadFileSize(DownloadObj downloadObj)
        {
            this.mDownloadObj = downloadObj;
            mDownloadObj.ChangeDownloadState(DownloadState.getSize);
            StartCoroutine(DownloadFileSizeHandler());
        }
        public void DownloadFile(DownloadObj downloadObj)
        {
            this.mDownloadObj = downloadObj;
            mDownloadObj.ChangeDownloadState(DownloadState.downloading);
            UnityDownloadMgr.instance.AddDownloadObj(mDownloadObj);
            StartCoroutine(DownloadFileHandler());
        }
        IEnumerator DownloadFileSizeHandler()
        {
            HeadHandler handler = new HeadHandler();
            UnityWebRequest request = UnityWebRequest.Head(mDownloadObj.url);

            request.downloadHandler = handler;
            request.timeout = UnityDownloadMgr.TIMEOUT;
            request.chunkedTransfer = true;
            request.disposeDownloadHandlerOnDispose = true;
            yield return request.SendWebRequest();
            //Dictionary<string, string> dic = request.GetResponseHeaders();
            //foreach (var item in dic) Debug.Log(item.Key + "     " + item.Value);
            //item.Key:Content-Type  item.Value:video/mp4
            //item.Key:Content-Disposition  item.Value:inline; filename = "o_1d6hoeipu1bem1rdjvqtush1gu8l.mp4"; filename *= utf - 8' 'o_1d6hoeipu1bem1rdjvqtush1gu8l.mp4
            //设置文件名的后缀
            string content_Type = request.GetResponseHeader("Content-Type");

            if (string.IsNullOrEmpty(content_Type) == false)
            {
                string[] arr = content_Type.Split('/');
                string houzhui = string.Format(".{0}", arr[arr.Length - 1]);
                Debug.Log("测试：" + mDownloadObj.url);
                Debug.Log("测试：" + mDownloadObj.fileName);
                if (!mDownloadObj.fileName.Contains("."))
                {
                    mDownloadObj.fileName += houzhui;
                    Debug.Log("添加后缀：" + mDownloadObj.fileName);
                }
            }
            Debug.Log("判断request有没有错误：" + request.error);
            Debug.Log("获取文件大小得到回应！code:" + request.responseCode);
            Debug.Log("获取文件大小:" + handler.ContentLength);
            if (handler.ContentLength == 0)
            {
                Downloader.Instance.failDownloadObj.Add(mDownloadObj);
                mDownloadObj.ChangeDownloadState(DownloadState.failed);
                Downloader.Instance.currentDownloadFailIndex++;
                Downloader.Instance.currentDownloadObjsAlready++;
                UnityDownloadMgr.instance.JudgeDownloader();
                UnityDownloadMgr.instance.FireAction(2, mDownloadObj);
            }
            else if (request.responseCode == 200 || request.responseCode == 0)//request.responseCode == 200 代表获取文件大小成功
            {
                Debug.Log(mDownloadObj.fileName + "设置长度为：" + handler.ContentLength);
                mDownloadObj.SetContentLength(handler.ContentLength);
                UnityDownloadMgr.instance.FireAction(0, mDownloadObj);
            }
            else
            {
                Downloader.Instance.failDownloadObj.Add(mDownloadObj);
                mDownloadObj.ChangeDownloadState(DownloadState.failed);
                Downloader.Instance.currentDownloadFailIndex++;
                Downloader.Instance.currentDownloadObjsAlready++;
                UnityDownloadMgr.instance.JudgeDownloader();
                UnityDownloadMgr.instance.FireAction(2, mDownloadObj);
            }

            request.Abort();
            request.Dispose();
        }
        IEnumerator DownloadFileHandler()
        {
            UnityDownloadFileHandler handler = new UnityDownloadFileHandler(mDownloadObj);
            UnityWebRequest request = UnityWebRequest.Get(mDownloadObj.url);
            requestDict.Add(mDownloadObj.url, new RequestHandler(request, handler));

            int rangeFrom = handler.GetDownloadObj().sDownloadFileResult.downloadedLength;
            int rangeTo = handler.GetDownloadObj().sDownloadFileResult.contentLength;

            Debug.Log("fileName:" + mDownloadObj.fileName);
            Debug.Log("rangeFrom:" + rangeFrom);
            Debug.Log("rangeTo:" + rangeTo);

            if (rangeFrom >= rangeTo)
            {
                Debug.Log("设置Range超出！ rangeFrom:" + rangeFrom + " rangeTo:" + rangeTo);
                OnResponse(200);//防止range太大 出现416报错
                yield break;
            }

            request.SetRequestHeader("Range", string.Format("bytes={0}-", rangeFrom)); //成功返回206
            request.downloadHandler = handler;
            request.timeout = UnityDownloadMgr.TIMEOUT;
            request.disposeDownloadHandlerOnDispose = true;
            yield return request.SendWebRequest();
            int code = (int)request.responseCode;
            OnResponse(code);
        }

        private void OnResponse(int code)
        {
            Dispose(mDownloadObj.url);//结束UnityDownloadFileHandler的文件操作内存共享

            Debug.Log("下载结束!!!!!!   code:" + code + "  localPath:" + mDownloadObj.fullPath);

            //0无网络，206下载断线，200下载完成，404网址请求失败
            if (code == 200 || code == 0 || code == 206)
            {
                if (mDownloadObj.sDownloadFileResult.contentLength == mDownloadObj.sDownloadFileResult.downloadedLength && mDownloadObj.sDownloadFileResult.contentLength != 0)
                {
                    string tmp = mDownloadObj.fullPath + ".tmp";
                    if (File.Exists(tmp))
                    {
                        if (File.Exists(mDownloadObj.fullPath))
                            File.Delete(mDownloadObj.fullPath);
                        File.Move(tmp, mDownloadObj.fullPath);
                        Debug.Log("下载成功 并移除tmp后缀 并保存到本地下载历史！");
                        //下载完成更新本地Data
                        //GetDownloadProgress.Instance.alreadyDownload += GetDownloadProgress.Instance.ByteConversion(mDownloadObj.sDownloadFileResult.contentLength);

                        Downloader.Instance.currentDownloadSucceedIndex++;
                        Downloader.Instance.currentDownload++;
                        Downloader.Instance.currentDownloadObjsAlready++;
                        //GetDownloadProgress.Instance.UpdateDownloadProgress();

                        PaintingModuleDataManager.Instance.currentSelectSceneMask.fillAmount = 1 - ((float)Downloader.Instance.currentDownload / Downloader.Instance.totalDownload);


                        UnityDownloadMgr.instance.JudgeDownloader();
                        mDownloadObj.ChangeDownloadState(DownloadState.downloaded);

                    }
                }
                else
                {
                    if (code == 0)
                    {
                        Debug.Log("下载失败");
                        Downloader.Instance.failDownloadObj.Add(mDownloadObj);
                        Downloader.Instance.currentDownloadFailIndex++;
                        Downloader.Instance.currentDownloadObjsAlready++;
                        UnityDownloadMgr.instance.JudgeDownloader();
                        mDownloadObj.ChangeDownloadState(DownloadState.failed);
                    }
                    else if (code == 206)
                    {
                        Debug.Log("下载暂停");
                        Downloader.Instance.failDownloadObj.Add(mDownloadObj);
                        Downloader.Instance.currentDownloadFailIndex++;
                        Downloader.Instance.currentDownloadObjsAlready++;
                        UnityDownloadMgr.instance.JudgeDownloader();
                        mDownloadObj.ChangeDownloadState(DownloadState.pause);
                    }
                }
            }
            else
            {
                Downloader.Instance.failDownloadObj.Add(mDownloadObj);
                Downloader.Instance.currentDownloadFailIndex++;
                Downloader.Instance.currentDownloadObjsAlready++;
                UnityDownloadMgr.instance.JudgeDownloader();
                mDownloadObj.ChangeDownloadState(DownloadState.failed);
            }

            UnityDownloadMgr.instance.FireAction(2, mDownloadObj);
            CloseDownLoad();
        }


        private void Dispose(string key)
        {
            RequestHandler result;
            if (requestDict.TryGetValue(key, out result))
            {
                result.handler.Dispose();
                result.request.Abort();
                requestDict.Remove(key);
            }
        }

        /// <summary>
        /// 暂停下载
        /// </summary>
        public void PauseDownLoad()
        {
            Debug.Log("停止下载-------------------------------------Close");

            //暂停更新本地Data
            mDownloadObj.ChangeDownloadState(DownloadState.pause);

            CloseDownLoad();
        }

        /// <summary>
        /// 关闭下载
        /// </summary>
        public void CloseDownLoad()
        {
            StopAllCoroutines();
            Dispose(mDownloadObj.url);
            UnityDownloadMgr.instance.GetUnityDownloadPool().RestoreOneGo(this.gameObject);
        }



        void OnDestroy()
        {
            Dictionary<string, RequestHandler>.Enumerator e = requestDict.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.handler.Dispose();
                e.Current.Value.request.Abort();
            }
            e.Dispose();
            StopAllCoroutines();
            requestDict.Clear();
            requestDict = null;
        }
    }
}

