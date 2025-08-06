using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yoyohan;

public class DownloadPaintingModule
{
    public DownloadSingle mainGraph;
    public DownloadSingle introduction;
    public DownloadSingle Video;
    public string videoLink;
    public string webLink;
    public DownloadSingle animationThumbnail;
    public DownloadMultiple frameAnimation;
    public string text;
    public DownloadSingle voice;
    public int downloadIndex;
    public int currentExistDownload;

    public void ImportWebData()
    {
        mainGraph = new DownloadSingle();
        mainGraph.downloadName = "";
        mainGraph.downloadURL = "";
        mainGraph.saveURL = "";
        mainGraph.versions = "";

        introduction = new DownloadSingle();
        introduction.downloadName = "";
        introduction.downloadURL = "";
        introduction.saveURL = "";
        introduction.versions = "";

        Video = new DownloadSingle();
        Video.downloadName = "";
        Video.downloadURL = "";
        Video.saveURL = "";
        Video.versions = "";

        videoLink = "";

        webLink = "";

        animationThumbnail = new DownloadSingle();
        animationThumbnail.downloadName = "";
        animationThumbnail.downloadURL = "";
        animationThumbnail.saveURL = "";
        animationThumbnail.versions = "";

        frameAnimation = new DownloadMultiple();
        frameAnimation.downloadName = new[] { "" };
        frameAnimation.downloadURL = "";
        frameAnimation.saveURL = "";
        frameAnimation.versions = "";

        text = "";

        voice = new DownloadSingle();
        voice.downloadName = "";
        voice.downloadURL = "";
        voice.saveURL = "";
        voice.versions = "";

        downloadIndex = 0;
        currentExistDownload = 0;
        if (mainGraph != null)
        {
            downloadIndex++;
        }
        if (introduction != null)
        {
            downloadIndex++;
        }
        if (Video != null)
        {
            downloadIndex++;
        }
        if (animationThumbnail != null)
        {
            downloadIndex++;
        }
        if (frameAnimation != null)
        {
            downloadIndex += frameAnimation.name.Length;
        }
        if (voice != null)
        {
            downloadIndex++;
        }
    }

    public DownloadObj[] ImportDownloadData()
    {

        DownloadObj[] objs = new DownloadObj[downloadIndex];

        if (mainGraph != null)
        {
            objs[currentExistDownload] = new DownloadObj().SetID(mainGraph.versions).SetUrl(mainGraph.downloadURL).SetParentPath(mainGraph.saveURL).SetFileName(mainGraph.downloadName);

            currentExistDownload++;
        }
        if (introduction != null)
        {
            objs[currentExistDownload] = new DownloadObj().SetID(introduction.versions).SetUrl(introduction.downloadURL).SetParentPath(introduction.saveURL).SetFileName(introduction.downloadName);

            currentExistDownload++;
        }
        if (Video != null)
        {
            objs[currentExistDownload] = new DownloadObj().SetID(Video.versions).SetUrl(Video.downloadURL).SetParentPath(Video.saveURL).SetFileName(Video.downloadName);

            currentExistDownload++;
        }
        if (animationThumbnail != null)
        {
            objs[currentExistDownload] = new DownloadObj().SetID(animationThumbnail.versions).SetUrl(animationThumbnail.downloadURL).SetParentPath(animationThumbnail.saveURL).SetFileName(animationThumbnail.downloadName);

            currentExistDownload++;
        }

        if (frameAnimation != null)
        {
            for (int i = 0; i < frameAnimation.downloadName.Length - 1; i++)
            {

                objs[i + currentExistDownload] = new DownloadObj().SetID(frameAnimation.versions).SetUrl(frameAnimation.downloadURL).SetParentPath(frameAnimation.saveURL).SetFileName(frameAnimation.downloadName[i]);

                currentExistDownload++;
            }
        }

        if (voice != null)
        {
            objs[downloadIndex - 1] = new DownloadObj().SetID(voice.versions).SetUrl(voice.downloadURL).SetParentPath(voice.saveURL).SetFileName(voice.downloadName);

        }

        return objs;
    }
}
