using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : UnitySingleton<DataManager>
{
    /// <summary>
    /// 画作模块管理
    /// </summary>
    public PaintingModuleDataManager PMDM;
    /// <summary>
    /// 画作模块帧动画UI_Canvas
    /// </summary>
    public PaintingModuleFrameCanvas paintingModuleFrameCanvas;
    /// <summary>
    /// 画作模块帧动画控制
    /// </summary>
    public PaintingModuleFrameControl paintingModuleFrameControl;
    /// <summary>
    /// 画作模块地图控制
    /// </summary>
    public PaintingModuleMap paintingModuleMap;
    /// <summary>
    /// EasyTouch控制管理
    /// </summary>
    public EasyTouchControlsManager easyTouchControlsManager;


    public void Awake()
    {
        //paintingModuleManager = PaintingModuleManager.Instance;
        paintingModuleFrameCanvas = PaintingModuleFrameCanvas.Instance;
        paintingModuleMap = PaintingModuleMap.Instance;
        paintingModuleFrameControl = PaintingModuleFrameControl.Instance;
        easyTouchControlsManager = EasyTouchControlsManager.Instance;

        PMDM = PaintingModuleDataManager.Instance;

    }

    /// <summary>
    /// 脚本的初始化
    /// </summary>
    public void Initial()
    {
        StartCoroutine(IInitial());
    }

    IEnumerator IInitial()
    {
        yield return new WaitForSeconds(1f);
        SceneBGMManager.Instance.GetSceneBGM(PMDM.GetEnterSceneBGM(PMDM.currentSelectSceneDataID));
        Debug.Log("场景BGM加载");

        yield return new WaitForSeconds(0.5f);
        PMDM.EnterScenePMInitial();
        Debug.Log("画作模块管理初始化");

        yield return new WaitForSeconds(0.5f);
        paintingModuleFrameCanvas.Initial();
        Debug.Log("画作模块帧动画UI初始化");

        yield return new WaitForSeconds(0.5f);
        paintingModuleMap.Initial();
        Debug.Log("画作模块地图初始化");

        yield return new WaitForSeconds(0.5f);
        paintingModuleFrameControl.Initial();
        Debug.Log("画作模块帧动画控制初始化");
        easyTouchControlsManager.Initial();
        Debug.Log("摇杆控制初始化");
    }

    /// <summary>
    /// 开始加载场景事件
    /// </summary>
    public void StartLoadSceneEvent()
    {
        Debug.Log("开始加载场景事件");
    }
    /// <summary>
    /// 结束加载场景事件
    /// </summary>
    public void FinishLoadSceneEvent()
    {
        Debug.Log("结束加载场景事件");
        GetLoadingProgress.Instance.isFake = true;
        GetLoadingProgress.Instance.currentAlreadyIndex += 1;
        Initial();
    }

}



[System.Serializable]
public class PaintingModuleData
{
    public string paintingName;
    public int id;
    public string url;
    public float angle;
    public int quantity;
    public string resolution;
    public string[] paintingArrayName;
    public Texture2D[] paintingArrayTexture;
}

[System.Serializable]
public enum PaintingModuleOptionEnum
{
    None,
    Frame,
    Video,
    Introduce,
    Web,
    Sound,
    Store,
    WebVideo
}
