using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PaintingModule : MonoBehaviour
{
    /// <summary>
    /// 画作名称
    /// </summary>
    public string paintingName;
    /// <summary>
    /// 画作id
    /// </summary>
    public string id;
    /// <summary>
    /// 文字介绍
    /// </summary>
    public string textIntroduction;
    /// <summary>
    /// 主图名字
    /// </summary>
    public string mainGraphName;   
    /// <summary>
    /// 主图HttpURL
    /// </summary>
    public string mainGraphHttpUrl;    
    /// <summary>
    /// 主图保存URL
    /// </summary>
    public string mainGraphSaveUrl;
    /// <summary>
    /// 画作缩略图
    /// </summary>
    public Texture2D mainGraphTexture;
    /// <summary>
    /// 简介名字
    /// </summary>
    public string introductionImageName;
    /// <summary>
    /// 简介HttpURL
    /// </summary>
    public string introductionImageHttpUrl;
    /// <summary>
    /// 简介保存URL
    /// </summary>
    public string introductionImageSaveUrl;
    /// <summary>
    /// 画作简介图
    /// </summary>
    public Texture2D introductionTexture;
    /// <summary>
    /// 画作文字简介
    /// </summary>
    public Text introductionText;
    /// <summary>
    /// 画作文字Image
    /// </summary>
    public Image introductionImage;
    /// <summary>
    /// 视频名字
    /// </summary>
    public string videoName;
    /// <summary>
    /// 视频HttpURL
    /// </summary>
    public string videoHttpUrl;
    /// <summary>
    /// 视频保存URL
    /// </summary>
    public string videoSaveUrl;  
    /// <summary>
    /// 音频名字
    /// </summary>
    public string voiceName;
    /// <summary>
    /// 音频HttpURL
    /// </summary>
    public string voiceHttpUrl;
    /// <summary>
    /// 音频保存URL
    /// </summary>
    public string voiceSaveUrl;
    /// <summary>
    /// 音频
    /// </summary>
    public AudioClip voiceClip;
    /// <summary>
    /// 缩略图名字
    /// </summary>
    public string animationThumbnailName;
    /// <summary>
    /// 缩略图HttpURL
    /// </summary>
    public string animationThumbnailHttpUrl;
    /// <summary>
    /// 缩略图保存URL
    /// </summary>
    public string animationThumbnailSaveUrl;
    /// <summary>
    /// 画作缩略图
    /// </summary>
    public Texture2D animationThumbnailTexture;
    /// <summary>
    /// 帧动画HttpURL
    /// </summary>
    public string[] frameAnimationHttpUrl;   
    /// <summary>
    /// 帧动画保存URL
    /// </summary>
    public string[] frameAnimationSaveUrl;  
    /// <summary>
    /// 视频Link
    /// </summary>
    public string videoLink;    
    /// <summary>
    /// 网页Link
    /// </summary>
    public string webLink;
    /// <summary>
    /// 画作分辨率
    /// </summary>
    public string resolution;
    /// <summary>
    /// 画作组
    /// </summary>
    public Texture2D[] frameAnimationArrayTexture;
    /// <summary>
    /// 主图的材质球
    /// </summary>
    public Material mainGraphMaterial;
    /// <summary>
    /// 遮挡
    /// </summary>
    public GameObject Mask;
    /// <summary>
    /// 传送点
    /// </summary>
    public Transform teleporter;
    /// <summary>
    /// 帧动画宽度
    /// </summary>
    public int width;
    /// <summary>
    /// 帧动画高度
    /// </summary>
    public int height;

    public int status;
    public int auth;
    public int version;
    /// <summary>
    /// 碰撞器开始事件
    /// </summary>
    public Action colliderEnterEvent;
    /// <summary>
    /// 碰撞器结束事件
    /// </summary>
    public Action colliderExitEvent;
    /// <summary>
    /// 画作帧动画UI打开按钮
    /// </summary>
    public Button paintingModuleFrameCanvasOpenButton;
    public UnityAction paintingModuleFrameCanvasOpenButtonAction;

    void Awake()
    {
        width = 0;
        height = 0;

        //添加碰撞器事件
        colliderEnterEvent += ColliderEnterEvent;
        colliderExitEvent += ColliderExitEvent;

        //paintingModuleFrameCanvasOpenButtonAction += PaintingModuleFrameCanvas.Instance.PaintingModuleFrameStart;
        //paintingModuleFrameCanvasOpenButtonAction += PaintingModuleMap.Instance.PaintingModuleMapUICanvasExit; ;

        paintingModuleFrameCanvasOpenButton = this.GetComponentInChildren<Button>();
        paintingModuleFrameCanvasOpenButton.onClick.AddListener(() =>
            {
                PaintingModuleFrameCanvas.Instance.PaintingModuleFrameStart(this);
                PaintingModuleMap.Instance.PaintingModuleMapUICanvasExit();
                PaintingModuleMap.Instance.PaintingModuleHelpUICanvasExit();

            }
        );
        paintingModuleFrameCanvasOpenButton.gameObject.SetActive(false);


    }

    /// <summary>
    /// 碰撞器开始方法
    /// </summary>
    public void ColliderEnterEvent()
    {
        if (status == 1)
            return;
        PaintingModuleDataManager.Instance.currentColliderPaintingModule = this;
        paintingModuleFrameCanvasOpenButton.gameObject.SetActive(true);
        Debug.Log("触碰画作");
    }

    /// <summary>
    /// 碰撞器结束方法
    /// </summary>
    public void ColliderExitEvent()
    {
        if (status == 1)
            return;
        PaintingModuleDataManager.Instance.currentColliderPaintingModule = null;
        paintingModuleFrameCanvasOpenButton.gameObject.SetActive(false);
        Debug.Log("离开画作");
    }

    /// <summary>
    /// 点击后碰撞器结束方法
    /// </summary>
    public void ClickColliderExitEvent()
    {
        if (status == 1)
            return;
        paintingModuleFrameCanvasOpenButton.gameObject.SetActive(false);
        Debug.Log("离开画作");
    }

    /// <summary>
    /// 清除画作图片组
    /// </summary>
    public void DeletePaintingModuleTextures()
    {
        for (int i = 0; i < frameAnimationArrayTexture.Length; i++)
        {
            Destroy(frameAnimationArrayTexture[i]);
            frameAnimationArrayTexture[i] = null;
        }

        Destroy(introductionTexture);
        introductionTexture = null;

        GC.Collect();
        Resources.UnloadUnusedAssets();
    }
}
