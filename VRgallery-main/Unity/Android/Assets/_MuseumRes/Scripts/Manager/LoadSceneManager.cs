using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LoadSceneManager : UnitySingleton<LoadSceneManager>
{
    public string paintingModuleSceneURL = "";

    public UnityAction StartLoadSceneAction;
    public UnityAction FinishLoadSceneAction;

    public AssetBundle currentSceneAB;

    private void Start()
    {
        StartLoadSceneAction += DataManager.Instance.StartLoadSceneEvent;
        FinishLoadSceneAction += DataManager.Instance.FinishLoadSceneEvent;

        //选择场景编号
        PaintingModuleDataManager PMDM = PaintingModuleDataManager.Instance;


        string url = PaintingModuleDataManager.Instance.GetEnterSceneResources(PMDM.currentSelectSceneDataID);

        LoadSelectScene(url, PMDM.currentSelectSceneDataName);
    }

    public void LoadSelectScene(string url, string sceneName)
    {
        StartLoadSceneAction();
        paintingModuleSceneURL = url;
        //paintingModuleSceneURL = Application.persistentDataPath + "/1" + "/AssetsBundle/paintingmodulescene01.scene";
        StartCoroutine(ILoadAddScene(sceneName));
    }

    private IEnumerator ILoadAddScene(string sceneName)
    {
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(paintingModuleSceneURL);
        currentSceneAB = request.assetBundle;
        yield return request;
        //string sceneIndex = "";
        //if (index < 10)
        //{
        //    sceneIndex = "0" + index.ToString();
        //}
        //else
        //{
        //    sceneIndex = index.ToString();
        //}
        AsyncOperation async = null;

        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        yield return async;
        Debug.Log("读取场景");
        FinishLoadSceneAction();
    }
    /// <summary>
    /// 卸载当前场景AB包
    /// </summary>
    public void UninstallcurrentSceneAB()
    {
        SceneManager.UnloadSceneAsync(PaintingModuleDataManager.Instance.currentSelectSceneDataName);

        currentSceneAB.Unload(true);
    }

}
