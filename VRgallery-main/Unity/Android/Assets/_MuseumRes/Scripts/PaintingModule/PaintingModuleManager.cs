using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PaintingModuleManager : UnitySingleton<PaintingModuleManager>
{
    /// <summary>
    /// 画作数据集合
    /// </summary>
    public List<PaintingModule> Datas = new List<PaintingModule>();
    /// <summary>
    /// 当前已加载的画作数据集合
    /// </summary>
    public List<PaintingModule> currentLoadDatas = new List<PaintingModule>();
    /// <summary>
    /// 当前画作模块
    /// </summary>
    public PaintingModule currentPaintingModule;
    /// <summary>
    /// 当前场景的画作模块数据
    /// </summary>
    public Transform ScenePaintingModules;
    /// <summary>
    /// 画作路径
    /// </summary>
    private string url;
    /// <summary>
    /// 当前画作宽度像素
    /// </summary>
    private int textureWidth;
    /// <summary>
    /// 当前画作高度像素
    /// </summary>
    private int textureHeight;
    /// <summary>
    /// XML路径
    /// </summary>
    private string xmlURL;
    /// <summary>
    /// 本地XML
    /// </summary>
    public XmlDocument localXmlDoc;
    /// <summary>
    /// 读取XML结束事件
    /// </summary>
    public UnityAction loadXMLFinishEvent;

    void Awake()
    {



    }

    void Test()
    {
        //PaintingModule pm1 = new PaintingModule();
        //pm1.paintingName = "natume";
        //pm1.id = "1.00";
        //pm1.url = url + "20220420_DefaultData/natume/";
        //pm1.angle = 360;
        //pm1.quantity = 44;
        //pm1.resolution = "2768_1848";
        //pm1.paintingVideoName = "DSC_0095.mov";
        //pm1.isDownload = true;

        //PaintingModule pm2 = new PaintingModule();
        //pm2.paintingName = "obon";
        //pm2.id = "1.00";
        //pm2.url = url + "20220420_DefaultData/obon/";
        //pm2.angle = 360;
        //pm2.quantity = 46;
        //pm2.resolution = "2768_1848";
        //pm2.paintingVideoName = "DSC_0095.mov";
        //pm2.isDownload = true;

        //Datas.Add(pm1);
        //Datas.Add(pm2);


    }

    void Start()
    {
        //StartCoroutine(GetPaintingModuleTexture2DResolution());
    }

    ///// <summary>
    ///// 初始化
    ///// </summary>
    //public void Initial()
    //{
    //    url = Application.persistentDataPath + "/1" + "/";
    //    xmlURL = Application.persistentDataPath + "/1" + "/XML/" + "MuseumPaintingData.xml";

    //    ScenePaintingModules = GameObject.Find("ScenePaintingModules").transform;
    //    GameObject pm = Resources.Load("PaintingModule") as GameObject;
    //    for (int i = 0; i < ScenePaintingModules.childCount; i++)
    //    {
    //        GameObject paintingModule = Instantiate(pm, this.transform);
    //        paintingModule.name = "PaintingModule" + i;

    //        Transform coll = paintingModule.transform.GetChild(0);
    //        coll.localPosition = ScenePaintingModules.GetChild(i).GetChild(0).localPosition;
    //        coll.localEulerAngles = ScenePaintingModules.GetChild(i).GetChild(0).localEulerAngles;
    //        coll.localScale = ScenePaintingModules.GetChild(i).GetChild(0).localScale;

    //        ScenePaintingModules.GetChild(i).GetChild(0).gameObject.SetActive(false);

    //        RectTransform rect = paintingModule.transform.GetChild(1).GetComponent<RectTransform>();
    //        RectTransform rectScene = ScenePaintingModules.GetChild(i).GetChild(1).GetComponent<RectTransform>();
    //        rect.localPosition = rectScene.localPosition;
    //        rect.localEulerAngles = rectScene.localEulerAngles;
    //        rect.sizeDelta = rectScene.sizeDelta;
    //        rect.localScale = rectScene.localScale;

    //        RectTransform rectButton = paintingModule.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>();
    //        RectTransform rectSceneButton = ScenePaintingModules.GetChild(i).GetChild(1).GetChild(1).GetComponent<RectTransform>();
    //        rectButton.localPosition = rectSceneButton.localPosition;
    //        rectButton.localEulerAngles = rectSceneButton.localEulerAngles;
    //        rectButton.sizeDelta = rectSceneButton.sizeDelta;
    //        rectButton.localScale = rectSceneButton.localScale;

    //        PaintingModule _PM = paintingModule.AddComponent<PaintingModule>();
    //        coll.gameObject.AddComponent<PaintingModuleCollider>();
    //        paintingModule.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    //        ScenePaintingModules.GetChild(i).gameObject.SetActive(false);

    //        Datas.Add(_PM);
    //    }

    //    LoadLocalXMLData();
    //}

    /// <summary>
    /// 获取画作模块图片的分辨率
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetPaintingModuleTexture2DResolution(PaintingModuleData data)
    {
        UnityWebRequest wr = new UnityWebRequest(url + "/" + data.url + data.paintingArrayName[0]);

        DownloadHandlerTexture texdl = new DownloadHandlerTexture(true);

        wr.downloadHandler = texdl;

        yield return wr.SendWebRequest();

        textureWidth = texdl.texture.width;

        Debug.Log("当前画作宽度像素为：" + texdl.texture.width);

        textureHeight = texdl.texture.height;

        Debug.Log("当前画作高度像素为：" + texdl.texture.height);

        texdl.Dispose();

        wr.Dispose();

        Resources.UnloadUnusedAssets();

        System.GC.Collect();
    }

    /// <summary>
    /// 设置画作模块
    /// </summary>
    public void SetPaintingModuleData()
    {
        if (Datas.Count == 0)
        {
            Debug.Log("当前没有画作模块信息，添加画作模块信息");
            PaintingModule data = new PaintingModule(); ;
            Datas.Add(data);
        }
        else
        {
            for (int i = 0; i < Datas.Count; i++)
            {
                if (Datas[i].paintingName == "")
                {
                    Debug.Log("出现相同name");
                    if (Datas[i].id == "")
                    {
                        Debug.Log("id相同，模块已加载");
                    }
                    else
                    {
                        PaintingModule data = new PaintingModule(); ;
                        Datas.Add(data);
                        Debug.Log("id不相同，添加新模块");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 获取画作模块
    /// </summary>
    /// <param name="paintingName">画作名称</param>
    /// <param name="id">画作id</param>
    /// <returns></returns>
    public PaintingModule GetPaintingModuleData(string paintingName, string id)
    {
        for (int i = 0; i < Datas.Count; i++)
        {
            if (Datas[i].paintingName == paintingName && Datas[i].id == id)
            {
                return Datas[i];
            }
            else
            {
                if (i == Datas.Count - 1)
                {
                    Debug.Log("没有识别到需要获取的画作信息");
                    return null;
                }
            }
        }
        return null;
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
    /// 检查XML
    /// </summary>
    public void CheckXML()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/1" + "/XML"))
        {
            Debug.Log("未检测到xml文件夹,创建新的");
            Directory.CreateDirectory(Application.persistentDataPath + "/1" + "/XML");
            //CreateXML();
        }
        else
        {
            if (File.Exists(xmlURL))
            {
                Debug.Log("检测到xml");
                loadXMLFinishEvent = null;
                loadXMLFinishEvent += CompareXML;
                //LoadXMLComparison();
            }
            else
            {
                Debug.Log("未检测到xml,创建新的");
                //CreateXML();
            }
        }
    }

//    public void CreateXML()
//    {
//        //读取服务器上数据，并加载至Datas
//        //新建xml对象  
//        XmlDocument xml = new XmlDocument();
//        XmlElement museumPainting = xml.CreateElement("MuseumPainting");
//        XmlElement paintingModuleTexture = xml.CreateElement("PaintingModuleTexture");
//        for (int i = 0; i < Datas.Count; i++)
//        {
//            XmlElement paintingModule = null;
//            if (i < 10)
//            {
//                paintingModule = xml.CreateElement("PaintingModule_0" + i);
//            }
//            else
//            {
//                paintingModule = xml.CreateElement("PaintingModule_" + i);
//            }
//            XmlElement paintingName = xml.CreateElement("PaintingName");
//            XmlElement id = xml.CreateElement("ID");
//            XmlElement url = xml.CreateElement("URL");
//            XmlElement angle = xml.CreateElement("Angle");
//            XmlElement quantity = xml.CreateElement("Quantity");
//            XmlElement resolution = xml.CreateElement("Resolution");
//            XmlElement isDownload = xml.CreateElement("IsDownload");
//            XmlElement paintingVideoName = xml.CreateElement("PaintingVideoName");
//            XmlElement paintingWebUrl = xml.CreateElement("PaintingWebUrl");
//            XmlElement paintingArrayName = xml.CreateElement("PaintingArrayName");
//            List<XmlElement> elements = new List<XmlElement>();
//            DirectoryInfo dir = new DirectoryInfo(Datas[i].url + "Texture");
//            FileInfo[] fil = dir.GetFiles();
//            for (int j = 0; j < fil.Length; j++)
//            {
//                XmlElement ele = xml.CreateElement("Name" + j);
//                ele.InnerText = fil[j].Name;
//                elements.Add(ele);
//            }
//            paintingName.InnerText = Datas[i].paintingName;
//            id.InnerText = Datas[i].id;
//            url.InnerText = Datas[i].url;
//            angle.InnerText = Datas[i].angle.ToString();
//            quantity.InnerText = Datas[i].quantity.ToString();
//            resolution.InnerText = Datas[i].resolution;
//            isDownload.InnerText = Datas[i].isDownload.ToString();
//            paintingVideoName.InnerText = Datas[i].paintingVideoName;
//            paintingWebUrl.InnerText = Datas[i].paintingWebUrl;
//            for (int k = 0; k < elements.Count; k++)
//            {
//                paintingArrayName.AppendChild(elements[k]);
//            }
//            paintingModule.AppendChild(paintingName);
//            paintingModule.AppendChild(id);
//            paintingModule.AppendChild(url);
//            paintingModule.AppendChild(angle);
//            paintingModule.AppendChild(quantity);
//            paintingModule.AppendChild(resolution);
//            paintingModule.AppendChild(isDownload);
//            paintingModule.AppendChild(paintingVideoName);
//            paintingModule.AppendChild(paintingWebUrl);
//            paintingModule.AppendChild(paintingArrayName);
//            paintingModuleTexture.AppendChild(paintingModule);
//            museumPainting.AppendChild(paintingModuleTexture);
//        }
//        xml.AppendChild(museumPainting);
//        xml.Save(Application.persistentDataPath + "/1" + "/XML/" + "MuseumPaintingData.xml");
//    }

//    public void LoadXMLComparison()
//    {
//        StartCoroutine(ILoadXMLFile());
//    }

//    public IEnumerator ILoadXMLFile()
//    {
//        WWW wwwLocal;
//#if UNITY_EDITOR
//        wwwLocal = new WWW(xmlURL);
//#else
//             wwwLocal = new WWW("file://"+xmlURL);
//            //WWW wwwLocal = new WWW("jar:file://" + xmlURL);
//#endif
//        while (!wwwLocal.isDone)
//            yield return null;
//        if (!string.IsNullOrEmpty(wwwLocal.error))
//        {
//            Debug.LogError(wwwLocal.error);
//        }
//        else
//        {
//            string xml = wwwLocal.text;
//            wwwLocal.Dispose();
//            localXmlDoc = new XmlDocument();
//            localXmlDoc.LoadXml(xml);
//            Debug.Log("读取到本地xml");
//            loadXMLFinishEvent();
//        }
//    }


    /// <summary>
    /// 对比XML
    /// </summary>
    public void CompareXML()
    {
        XmlElement root = localXmlDoc.DocumentElement;
        XmlNodeList classList = root.ChildNodes;
        //datas为服务器数据
        if (classList.Count >= Datas.Count)
        {
            for (int i = 0; i < classList.Count; i++)
            {
                if (i > Datas.Count - 1)
                {
                    Debug.Log("删除多余的画作模块");
                    DeletePaintingModule(classList[i]);
                }
                else
                {
                    Debug.Log("对比画作模块信息");
                    ComparePaintingModule(classList[i], Datas[i]);
                }
            }
        }
        else if (classList.Count < Datas.Count)
        {
            for (int i = 0; i < Datas.Count; i++)
            {
                if (i > classList.Count - 1)
                {
                    Debug.Log("添加新的画作模块");
                    AddPaintingModule(classList[i], i);
                }
                else
                {
                    Debug.Log("对比画作模块信息");
                    ComparePaintingModule(classList[i], Datas[i]);
                }
            }
        }
    }


    ///// <summary>
    ///// 读取本地XML数据至Datas
    ///// </summary>
    //public void LoadLocalXMLData()
    //{
    //    loadXMLFinishEvent = null;
    //    loadXMLFinishEvent += iLoadLocalXMLData;
    //    LoadXMLComparison();
    //}
    //public void iLoadLocalXMLData()
    //{
    //    XmlNodeList paintingModuleTextureNodes = localXmlDoc.SelectSingleNode("MuseumPainting")
    //        .SelectSingleNode("PaintingModuleTexture").ChildNodes;
    //    for (int i = 0; i < paintingModuleTextureNodes.Count; i++)
    //    {

    //        Datas[i].paintingName = paintingModuleTextureNodes[i].ChildNodes[0].InnerText;
    //        Datas[i].id = paintingModuleTextureNodes[i].ChildNodes[1].InnerText;
    //        Datas[i].url = paintingModuleTextureNodes[i].ChildNodes[2].InnerText;
    //        Datas[i].angle = float.Parse(paintingModuleTextureNodes[i].ChildNodes[3].InnerText);
    //        Datas[i].quantity = int.Parse(paintingModuleTextureNodes[i].ChildNodes[4].InnerText);
    //        Datas[i].resolution = paintingModuleTextureNodes[i].ChildNodes[5].InnerText;
    //        Datas[i].isDownload = bool.Parse(paintingModuleTextureNodes[i].ChildNodes[6].InnerText);
    //        Datas[i].paintingVideoName = paintingModuleTextureNodes[i].ChildNodes[7].InnerText;
    //        Datas[i].paintingWebUrl = paintingModuleTextureNodes[i].ChildNodes[8].InnerText;
    //        Datas[i].paintingArrayName = new string[paintingModuleTextureNodes[i].ChildNodes[9].ChildNodes.Count];
    //        for (int j = 0; j < Datas[i].paintingArrayName.Length; j++)
    //        {
    //            Datas[i].paintingArrayName[j] = paintingModuleTextureNodes[i].ChildNodes[9].ChildNodes[j].InnerText;
    //        }
    //        Datas[i].frameAnimationArrayTexture = new Texture2D[Datas[i].paintingArrayName.Length];
    //    }
    //}

    ///// <summary>
    ///// 读取场景画作模块
    ///// </summary>
    ///// <param name="pm"></param>
    //public void LoadScenePaintingModules(PaintingModule pm)
    //{
    //    StartCoroutine(ILoadXMLA(pm));
    //}
    //public IEnumerator ILoadXMLA(PaintingModule pm)
    //{
    //    if (currentLoadDatas.Count == 0)
    //    {
    //        currentLoadDatas.Add(pm);
    //    }
    //    else
    //    {
    //        if (currentLoadDatas.Count < 5)
    //        {
    //            currentLoadDatas.Add(pm);
    //        }
    //        else if (currentLoadDatas.Count >= 5)
    //        {
    //            for (int i = 0; i < currentLoadDatas.Count; i++)
    //            {
    //                if (currentLoadDatas[i] == pm)
    //                {
    //                    break;
    //                }
    //                else
    //                {
    //                    if (i == currentLoadDatas.Count - 1)
    //                    {
    //                        currentLoadDatas[0].DeletePaintingModuleTextures();
    //                        for (int j = 0; j < currentLoadDatas.Count - 1; j++)
    //                        {
    //                            currentLoadDatas[j] = currentLoadDatas[j + 1];
    //                        }

    //                        currentLoadDatas[currentLoadDatas.Count - 1] = pm;
    //                    }

    //                }
    //            }

    //        }
    //    }

    //    currentPaintingModule = pm;

    //    for (int l = 0; l < pm.frameAnimationArrayTexture.Length; l++)
    //    {
    //        if (pm.frameAnimationArrayTexture[l] == null)
    //        {
    //            Debug.Log("图片为空，进行加载");
    //            break;
    //        }
    //        else
    //        {
    //            Debug.Log("读取到了图片");

    //            PaintingModuleFrameControl.Instance.m_rotateCoefficient = 360f / pm.frameAnimationArrayTexture.Length;
    //            PaintingModuleFrameControl.Instance.m_Cube.transform.localEulerAngles = new Vector3(0, 0, 0);
    //            Resources.UnloadUnusedAssets();
    //            System.GC.Collect();

    //            PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();
    //            yield break;
    //        }
    //    }

    //    PaintingModuleFrameCanvas.Instance.PaintingModuleLoadingStart();
    //    Debug.Log("开始加载 ");
    //    int index = 0;
    //    string url = pm.url;
    //    for (int i = 0; i < pm.frameAnimationArrayTexture.Length; i++)
    //    {


    //        //文件读取
    //        FileStream fileStream = new FileStream(url + "/Texture/" + pm.paintingArrayName[i], FileMode.Open, FileAccess.Read);
    //        fileStream.Seek(0, SeekOrigin.Begin);
    //        byte[] bytes = new byte[fileStream.Length];
    //        fileStream.Read(bytes, 0, (int)fileStream.Length);
    //        fileStream.Close();
    //        fileStream.Dispose();
    //        fileStream = null;

    //        //获取图片的宽和高
    //        string[] split = pm.resolution.Split('_');
    //        int width = int.Parse(split[0]);
    //        int height = int.Parse(split[1]);

    //        //添加一个ui大小调整！！！
    //        if (i == 0)
    //        {
    //            PaintingModuleFrameCanvas.Instance.PaintingModuleFrameRawImageLocalScale(width, height);
    //        }

    //        //读取图片
    //        Texture2D texture1 = new Texture2D(width, height);
    //        texture1.LoadImage(bytes);
    //        texture1.Apply(false, true);
    //        pm.frameAnimationArrayTexture[i] = texture1;

    //        PaintingModuleFrameCanvas.Instance.PaintingModuleLoadingLoad((i * 100 / pm.quantity));
    //        Debug.Log("已: " + (i * 100 / pm.quantity));
    //        yield return null;

    //        Debug.Log("已经加载个数: " + i);
    //    }
    //    ////////////////
    //    PaintingModuleFrameControl.Instance.m_rotateCoefficient = 360f / pm.frameAnimationArrayTexture.Length;
    //    PaintingModuleFrameControl.Instance.m_Cube.transform.localEulerAngles = new Vector3(0, 0, 0);
    //    PaintingModuleFrameControl.Instance.PaintingModuleFrameControlStart();
    //    PaintingModuleFrameCanvas.Instance.PaintingModuleLoadingStop();
    //}

    /// <summary>
    /// 比较画作模块
    /// </summary>
    /// <param name="compare1"></param>
    /// <param name="compare2"></param>
    private void ComparePaintingModule(XmlNode compare1, PaintingModule compare2)
    {
        if (compare1.ChildNodes[0].InnerText != compare2.paintingName)
        {
            Debug.Log("名字不相同，剔除本地文件，从服务器下载新文件");
            UpdatePaintingModuleNotName(compare1, compare2);
        }
        else
        {
            Debug.Log("名字相同，继续检测版本信息");
            if (compare1.ChildNodes[1].InnerText != compare2.id)
            {
                Debug.Log("id版本不相同，剔除本地文件，从服务器下载新文件");
                UpdatePaintingModuleNotID(compare1, compare2);
            }
            else
            {
                Debug.Log("id版本相同，继续检测是否下载");
                //if (compare1.ChildNodes[6].InnerText != compare2.isDownload.ToString())
                //{
                //    Debug.Log("文件没有下载完成，剔除本地文件，从服务器下载新文件");
                //    UpdatePaintingModuleNotDownload(compare1, compare2);
                //}
                //else
                //{
                //    Debug.Log("文件下载完成，可以使用");
                //}
            }
        }
    }
    /// <summary>
    /// 更新画作模块（名字不对）
    /// </summary>
    private void UpdatePaintingModuleNotName(XmlNode update1, PaintingModule update2)
    {
        update1.ChildNodes[0].InnerText = update2.paintingName;
    }

    /// <summary>
    /// 更新画作模块（版本不对）
    /// </summary>
    private void UpdatePaintingModuleNotID(XmlNode update1, PaintingModule update2)
    {
        update1.ChildNodes[1].InnerText = update2.id;
        SaveXML();
    }
    ///// <summary>
    ///// 更新画作模块（下载未完成）
    ///// </summary>
    //private void UpdatePaintingModuleNotDownload(XmlNode update1, PaintingModule update2)
    //{
    //    update1.ChildNodes[6].InnerText = update2.isDownload.ToString();
    //    SaveXML();
    //}
    /// <summary>
    /// 删除画作模块
    /// </summary>
    /// <param name="deletePM"></param>
    private void DeletePaintingModule(XmlNode node)
    {
        localXmlDoc.ChildNodes[0].RemoveChild(node);
        SaveXML();
    }
    /// <summary>
    /// 添加画作模块
    /// </summary>
    /// <param name="deletePM"></param>
    private void AddPaintingModule(XmlNode addPM, int index)
    {
        XmlElement paintingModule = null;
        if (index < 10)
        {
            paintingModule = localXmlDoc.CreateElement("PaintingModule_0" + index);
        }
        else
        {
            paintingModule = localXmlDoc.CreateElement("PaintingModule_" + index);
        }
        XmlElement paintingName = localXmlDoc.CreateElement("PaintingName");
        XmlElement id = localXmlDoc.CreateElement("ID");
        XmlElement url = localXmlDoc.CreateElement("URL");
        XmlElement angle = localXmlDoc.CreateElement("Angle");
        XmlElement quantity = localXmlDoc.CreateElement("Quantity");
        XmlElement resolution = localXmlDoc.CreateElement("Resolution");
        XmlElement isDownload = localXmlDoc.CreateElement("IsDownload");
        XmlElement paintingVideoName = localXmlDoc.CreateElement("PaintingVideoName");
        XmlElement paintingWebUrl = localXmlDoc.CreateElement("PaintingWebUrl");
        XmlElement paintingArrayName = localXmlDoc.CreateElement("PaintingArrayName");
        List<XmlElement> elements = new List<XmlElement>();
        //DirectoryInfo dir = new DirectoryInfo(Datas[index].url + "Texture");
        //FileInfo[] fil = dir.GetFiles();
        //for (int j = 0; j < fil.Length; j++)
        //{
        //    XmlElement ele = localXmlDoc.CreateElement("Name" + j);
        //    ele.InnerText = fil[j].Name;
        //    elements.Add(ele);
        //}
        paintingName.InnerText = Datas[index].paintingName;
        id.InnerText = Datas[index].id;
        //url.InnerText = Datas[index].url;
        //angle.InnerText = Datas[index].angle.ToString();
        //quantity.InnerText = Datas[index].quantity.ToString();
        //resolution.InnerText = Datas[index].resolution;
        //isDownload.InnerText = Datas[index].isDownload.ToString();
        //paintingVideoName.InnerText = Datas[index].paintingVideoName;
        //paintingWebUrl.InnerText = Datas[index].paintingWebUrl;
        for (int k = 0; k < elements.Count; k++)
        {
            paintingArrayName.AppendChild(elements[k]);
        }
        paintingModule.AppendChild(paintingName);
        paintingModule.AppendChild(id);
        paintingModule.AppendChild(url);
        paintingModule.AppendChild(angle);
        paintingModule.AppendChild(quantity);
        paintingModule.AppendChild(resolution);
        paintingModule.AppendChild(isDownload);
        paintingModule.AppendChild(paintingVideoName);
        paintingModule.AppendChild(paintingWebUrl);
        paintingModule.AppendChild(paintingArrayName);
        localXmlDoc.ChildNodes[0].AppendChild(paintingModule);
        SaveXML();
    }

    public void SaveXML()
    {
        localXmlDoc.Save(Application.persistentDataPath + "/1" + "/XML/" + "MuseumPaintingData.xml");
    }

//#if UNITY_EDITOR
//    private static string staticURL;
//    private static List<PaintingModule> staticDatas = new List<PaintingModule>();
//    [MenuItem("AssetsBundle/Create XML")]
//    static void EditorCreateXML()
//    {
//        staticURL = Application.persistentDataPath + "/1" + "/";

//        PaintingModule pm1 = new PaintingModule();
//        pm1.paintingName = "01";
//        pm1.id = "1.00";
//        pm1.url = staticURL + "AssetBundlePM/01/";
//        pm1.angle = 360;
//        pm1.quantity = 41;
//        pm1.resolution = "4240_2832";
//        pm1.paintingVideoName = "1b7ef2211c557760a90c9bafad36c12b.mp4";
//        pm1.paintingWebUrl = "www.silkroad-museum.jp";
//        pm1.isDownload = true;

//        PaintingModule pm2 = new PaintingModule();
//        pm2.paintingName = "02";
//        pm2.id = "1.00";
//        pm2.url = staticURL + "AssetBundlePM/02/";
//        pm2.angle = 360;
//        pm2.quantity = 41;
//        pm2.resolution = "4240_2832";
//        pm2.paintingVideoName = "1b7ef2211c557760a90c9bafad36c12b.mp4";
//        pm2.paintingWebUrl = "www.silkroad-museum.jp";
//        pm2.isDownload = true;

//        PaintingModule pm3 = new PaintingModule();
//        pm3.paintingName = "03";
//        pm3.id = "1.00";
//        pm3.url = staticURL + "AssetBundlePM/03/";
//        pm3.angle = 360;
//        pm3.quantity = 45;
//        pm3.resolution = "2768_1848";
//        pm3.paintingVideoName = "1b7ef2211c557760a90c9bafad36c12b.mp4";
//        pm3.paintingWebUrl = "www.silkroad-museum.jp";
//        pm3.isDownload = true;

//        PaintingModule pm4 = new PaintingModule();
//        pm4.paintingName = "04";
//        pm4.id = "1.00";
//        pm4.url = staticURL + "AssetBundlePM/04/";
//        pm4.angle = 360;
//        pm4.quantity = 43;
//        pm4.resolution = "2768_1848";
//        pm4.paintingVideoName = "1b7ef2211c557760a90c9bafad36c12b.mp4";
//        pm4.paintingWebUrl = "www.silkroad-museum.jp";
//        pm4.isDownload = true;

//        PaintingModule pm5 = new PaintingModule();
//        pm5.paintingName = "05";
//        pm5.id = "1.00";
//        pm5.url = staticURL + "AssetBundlePM/05/";
//        pm5.angle = 360;
//        pm5.quantity = 44;
//        pm5.resolution = "2768_1848";
//        pm5.paintingVideoName = "1603c2f468625e7afab6bc218a105ce4.mp4";
//        pm5.paintingWebUrl = "www.silkroad-museum.jp";
//        pm5.isDownload = true;

//        PaintingModule pm6 = new PaintingModule();
//        pm6.paintingName = "06";
//        pm6.id = "1.00";
//        pm6.url = staticURL + "AssetBundlePM/06/";
//        pm6.angle = 360;
//        pm6.quantity = 46;
//        pm6.resolution = "2768_1848";
//        pm6.paintingVideoName = "1603c2f468625e7afab6bc218a105ce4.mp4";
//        pm6.paintingWebUrl = "www.silkroad-museum.jp";
//        pm6.isDownload = true;

//        PaintingModule pm7 = new PaintingModule();
//        pm7.paintingName = "07";
//        pm7.id = "1.00";
//        pm7.url = staticURL + "AssetBundlePM/07/";
//        pm7.angle = 360;
//        pm7.quantity = 46;
//        pm7.resolution = "2768_1848";
//        pm7.paintingVideoName = "1603c2f468625e7afab6bc218a105ce4.mp4";
//        pm7.paintingWebUrl = "www.silkroad-museum.jp";
//        pm7.isDownload = true;

//        PaintingModule pm8 = new PaintingModule();
//        pm8.paintingName = "08";
//        pm8.id = "1.00";
//        pm8.url = staticURL + "AssetBundlePM/08/";
//        pm8.angle = 360;
//        pm8.quantity = 47;
//        pm8.resolution = "2768_1848";
//        pm8.paintingVideoName = "1603c2f468625e7afab6bc218a105ce4.mp4";
//        pm8.paintingWebUrl = "www.silkroad-museum.jp";
//        pm8.isDownload = true;

//        PaintingModule pm9 = new PaintingModule();
//        pm9.paintingName = "09";
//        pm9.id = "1.00";
//        pm9.url = staticURL + "AssetBundlePM/09/";
//        pm9.angle = 360;
//        pm9.quantity = 34;
//        pm9.resolution = "2768_1848";
//        pm9.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
//        pm9.paintingWebUrl = "www.silkroad-museum.jp";
//        pm9.isDownload = true;

//        PaintingModule pm10 = new PaintingModule();
//        pm10.paintingName = "10";
//        pm10.id = "1.00";
//        pm10.url = staticURL + "AssetBundlePM/10/";
//        pm10.angle = 360;
//        pm10.quantity = 57;
//        pm10.resolution = "1376_920";
//        pm10.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
//        pm10.paintingWebUrl = "www.silkroad-museum.jp";
//        pm10.isDownload = true;

//        staticDatas.Add(pm1);
//        staticDatas.Add(pm2);
//        staticDatas.Add(pm3);
//        staticDatas.Add(pm4);
//        staticDatas.Add(pm5);
//        staticDatas.Add(pm6);
//        staticDatas.Add(pm7);
//        staticDatas.Add(pm8);
//        staticDatas.Add(pm9);
//        staticDatas.Add(pm10);

//        XmlDocument xml = new XmlDocument();
//        XmlElement museumPainting = xml.CreateElement("MuseumPainting");
//        XmlElement paintingModuleTexture = xml.CreateElement("PaintingModuleTexture");
//        for (int i = 0; i < staticDatas.Count; i++)
//        {
//            XmlElement paintingModule = null;
//            if (i < 10)
//            {
//                paintingModule = xml.CreateElement("PaintingModule_0" + i);
//            }
//            else
//            {
//                paintingModule = xml.CreateElement("PaintingModule_" + i);
//            }
//            XmlElement paintingName = xml.CreateElement("PaintingName");
//            XmlElement id = xml.CreateElement("ID");
//            XmlElement url = xml.CreateElement("URL");
//            XmlElement angle = xml.CreateElement("Angle");
//            XmlElement quantity = xml.CreateElement("Quantity");
//            XmlElement resolution = xml.CreateElement("Resolution");
//            XmlElement isDownload = xml.CreateElement("IsDownload");
//            XmlElement paintingVideoName = xml.CreateElement("PaintingVideoName");
//            XmlElement paintingWebUrl = xml.CreateElement("PaintingWebUrl");
//            XmlElement paintingArrayName = xml.CreateElement("PaintingArrayName");
//            List<XmlElement> elements = new List<XmlElement>();
//            DirectoryInfo dir = new DirectoryInfo(staticDatas[i].url + "Texture");
//            FileInfo[] fil = dir.GetFiles();
//            for (int j = 0; j < fil.Length; j++)
//            {
//                XmlElement ele = xml.CreateElement("Name" + j);
//                ele.InnerText = fil[j].Name;
//                elements.Add(ele);
//            }
//            paintingName.InnerText = staticDatas[i].paintingName;
//            id.InnerText = staticDatas[i].id;
//            url.InnerText = staticDatas[i].url;
//            angle.InnerText = staticDatas[i].angle.ToString();
//            quantity.InnerText = staticDatas[i].quantity.ToString();
//            resolution.InnerText = staticDatas[i].resolution;
//            isDownload.InnerText = staticDatas[i].isDownload.ToString();
//            paintingVideoName.InnerText = staticDatas[i].paintingVideoName;
//            paintingWebUrl.InnerText = staticDatas[i].paintingWebUrl;
//            for (int k = 0; k < elements.Count; k++)
//            {
//                paintingArrayName.AppendChild(elements[k]);
//            }
//            paintingModule.AppendChild(paintingName);
//            paintingModule.AppendChild(id);
//            paintingModule.AppendChild(url);
//            paintingModule.AppendChild(angle);
//            paintingModule.AppendChild(quantity);
//            paintingModule.AppendChild(resolution);
//            paintingModule.AppendChild(isDownload);
//            paintingModule.AppendChild(paintingVideoName);
//            paintingModule.AppendChild(paintingWebUrl);
//            paintingModule.AppendChild(paintingArrayName);
//            paintingModuleTexture.AppendChild(paintingModule);
//            museumPainting.AppendChild(paintingModuleTexture);
//        }
//        xml.AppendChild(museumPainting);
//        if (!Directory.Exists(Application.persistentDataPath + "/1" + "/XML"))
//        {
//            Debug.Log("未检测到xml文件夹,创建新的");
//            Directory.CreateDirectory(Application.persistentDataPath + "/1" + "/XML");
//        }
//        xml.Save(Application.persistentDataPath + "/1" + "/XML/" + "MuseumPaintingData.xml");
//    }
//#endif


}



