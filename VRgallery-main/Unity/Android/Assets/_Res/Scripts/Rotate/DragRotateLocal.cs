using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DragRotateLocal : MonoBehaviour
{
    public Camera m_Camera;

    public GameObject m_Cube;


    private bool isDragging = false;


    private float m_lastMouseX = float.MaxValue;


    private float m_lastSpeed = 0f;


    private bool m_isMoving = false;

    public float m_speed;

    //public Image frameTexture;
    public Material frameTexture;
    public Text textureNameText;
    public string[] textureNames;

    [SerializeField]
    public List<frameArray> frameSprites = new List<frameArray>();

    public int frameIndex;
    // public Sprite[] frameSprites;
    //public string[] fileNames;
    public bool m_isRotate = false;

    public string[] urls;

    public Button button1;
    public Button button2;

    public Transform Quad;

    // Use this for initialization
    void Start()
    {

        button1.interactable = false;
        button2.interactable = false;

        //FileDetectionMethod();

        string ur = "jar:file://" + Application.dataPath + "!/assets/" + "20220307_DefaultData/04";
        //if (Directory.Exists(ur))
        //{
        //    DirectoryInfo direction = new DirectoryInfo(ur);
        //    FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        //    int temp = 0;
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        if (files[i].Name.EndsWith(".meta"))
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            temp++;
        //            fileNames[i] = files[i].Name;
        //            Debug.Log("Name:" + files[i].Name);  //打印出来这个文件夹下的所有文件
        //        }
        //    }
        //}

        //frameArray frame = new frameArray();
        //frame.m_frameSprite = Resources.LoadAll<Sprite>("20220307_DefaultData/04");
        //frameSprites.Add(frame);


        StartCoroutine(ContentLoad(urls[0]));
        //ContentLoad(urls[0]);
    }

    private frameArray frames;
    [ContextMenu("添加名字")]
    public void CEshjoi()
    {

        for (int i = 0; i < urls.Length; i++)
        {
            frames = new frameArray();
            frames.m_frameSprite = new Texture[120];
            frames.fileNames = new string[120];

            Debug.Log("A");

            string ur = Application.streamingAssetsPath + "/" + urls[i];
            Debug.Log(ur);
            //string ur = "jar:file://" + Application.dataPath + "!/assets/" + "20220307_DefaultData/04";
            if (Directory.Exists(ur))
            {
                Debug.Log("B");
                DirectoryInfo direction = new DirectoryInfo(ur);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                for (int j = 0; j < files.Length; j++)
                {
                    Debug.Log("Name:______" + files[j].Name);  //打印出来这个文件夹下的所有文件
                    if (files[j].Name.EndsWith(".meta"))
                    {
                        continue;
                    }
                    else
                    {
                        frames.fileNames[j] = files[j].Name;
                        Debug.Log("Name:" + files[j].Name);  //打印出来这个文件夹下的所有文件
                    }
                }
            }
            frameSprites.Add(frames);
        }


    }


    private IEnumerator ContentLoad(string path)
    //private void ContentLoad(string path)
    {
        string ur = "";
#if UNITY_EDITOR
        ur = Application.streamingAssetsPath + "/" + path;
#elif UNITY_ANDROID
        ur = "jar:file://" + Application.dataPath + "!/assets/" + path;
#elif UNITY_IOS|| UNITY_IPHONE
        ur = "file://" + Application.dataPath + "!/assets/" + path;
#endif

        //string ur = Application.persistentDataPath + "/1" + "/" + path;
        //string ur = Application.streamingAssetsPath + "/" + path;

        Debug.Log(ur);
        for (int l = 0; l < 120; l++)
        {
            Debug.Log("a");
            WWW www = new WWW(ur + "/" + frameSprites[frameIndex].fileNames[l]); //只能放URl 

            yield return www;
            if (www != null && string.IsNullOrEmpty(www.error))
            {
                if (l == 0)
                {
                    Quad.localScale = new Vector3(0.9f * www.texture.width / www.texture.height, 1);
                    Debug.Log(www.texture.width);
                    Debug.Log(www.texture.height);
                }

                Debug.Log(www.texture.name);
                frameSprites[frameIndex].m_frameSprite[l] = new Texture2D(64, 64, TextureFormat.DXT1, false);
                www.LoadImageIntoTexture((Texture2D)frameSprites[frameIndex].m_frameSprite[l]);


                //    Texture2D texture = www.texture;
                //    frameSprites[frameIndex].m_frameSprite[l] = texture;

                if (l == frameSprites[frameIndex].fileNames.Length - 1)
                {
                    m_isRotate = true;

                    button1.interactable = true;
                    button2.interactable = true;
                }
            }

            //UnityWebRequest wr = new UnityWebRequest(ur + "/" + frameSprites[frameIndex].fileNames[l]);
            //DownloadHandlerTexture texdl = new DownloadHandlerTexture(true);
            //wr.downloadHandler = texdl;
            //yield return wr.SendWebRequest();
            //if (!wr.isNetworkError)
            //{
            //    if (l == 0)
            //    {
            //        Quad.localScale = new Vector3(0.9f * texdl.texture.width / texdl.texture.height, 1);
            //        Debug.Log(texdl.texture.width);
            //        Debug.Log(texdl.texture.height);
            //    }
            //    Texture2D t = texdl.texture;
            //    frameSprites[frameIndex].m_frameSprite[l] = t;
            //    if (l == frameSprites[frameIndex].fileNames.Length - 1)
            //    {
            //        m_isRotate = true;

            //        button1.interactable = true;
            //        button2.interactable = true;
            //    }
            //}
            //if (l == 0)
            //{
            //    Quad.transform.localScale = new Vector3(0.9f * 997 / 561, 1);
            //}
            //Debug.Log(ur + "/" + frameSprites[frameIndex].fileNames[l]);
            //FileStream fileStream = new FileStream(ur + "/" + frameSprites[frameIndex].fileNames[l], FileMode.Open, FileAccess.Read);
            //fileStream.Seek(0, SeekOrigin.Begin);
            //byte[] bytes = new byte[fileStream.Length];
            //fileStream.Read(bytes, 0, (int)fileStream.Length);
            //fileStream.Close();
            //fileStream.Dispose();
            //fileStream = null;

            //int width = 997;
            //int height = 561;
            //Texture2D texture1 = new Texture2D(width, height);
            //texture1.LoadImage(bytes);
            //frameSprites[frameIndex].m_frameSprite[l] = texture1;

            //if (l == frameSprites[frameIndex].fileNames.Length - 1)
            //{
            //    m_isRotate = true;

            //    button1.interactable = true;
            //    button2.interactable = true;
            //}

            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }
    }

    public void FirstOpenEvent()
    {
        this.isDragging = false;
    }


    private float num0;
    void FixedUpdate()
    {
        if (this.m_isMoving)
        {
            this.m_lastSpeed *= 0.9f;//递减
            Vector3 localEulerAngles = m_Cube.transform.localEulerAngles;
            m_Cube.transform.localEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y -= m_lastSpeed * m_speed, localEulerAngles.z);
            if (Mathf.Abs(this.m_lastSpeed) < 0.01)
            {
                this.m_isMoving = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isRotate)
            return;
        Debug.Log(m_Cube.transform.localEulerAngles.y);
        int eulerindex;
        eulerindex = (int)m_Cube.transform.localEulerAngles.y / 3;
        //frameTexture.sprite = frameSprites[frameIndex].m_frameSprite[eulerindex];
        frameTexture.SetTexture("_MainTex", frameSprites[frameIndex].m_frameSprite[eulerindex]);

        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_Camera != null)
                {
                    RaycastHit hit;
                    Ray m_ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(m_ray, out hit, 1000f, m_Camera.cullingMask) && (hit.collider.gameObject == base.gameObject))
                    {
                        //选中物体
                        Debug.Log("选中物体");
                        this.isDragging = true;
                        isBoth = false;
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (Input.touchCount <= 1)
                {

                    bool isDrag = false;
                    //抬起
                    if (isDragging)
                        isDrag = true;
                    this.isDragging = false;


                    //抬起后缓慢转动 
                    if (isDrag)
                    {
                        this.m_isMoving = true;
                        this.m_lastSpeed = Input.mousePosition.x - this.m_lastMouseX;
                        this.m_lastMouseX = float.MaxValue;
                    }
                }
            }

            if (this.isDragging)
            {

                float num = 0;
                if (this.m_lastMouseX != float.MaxValue)
                {
                    num = Input.mousePosition.x - this.m_lastMouseX;
                }
                this.m_lastMouseX = Input.mousePosition.x;


                Vector3 cube_localEulerAngles = m_Cube.transform.localEulerAngles;
                m_Cube.transform.localEulerAngles = new Vector3(cube_localEulerAngles.x, cube_localEulerAngles.y -= num * m_speed, cube_localEulerAngles.z);



            }
        }
        else if (Input.touchCount > 1)
        {
            isBoth = true;

            this.m_isMoving = false;
            this.isDragging = false;
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Vector2 tempPosition1 = Input.GetTouch(0).position;
                Vector2 tempPosition2 = Input.GetTouch(1).position;

                if (IsEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                {
                    if (distance < 12)
                    {
                        distance += 0.1f;
                        moveSpeed = 1;
                        flag = true;
                        Debug.Log("zoom+");
                    }
                }
                else
                {
                    if (distance > 1)
                    {
                        distance -= 0.1f;
                        moveSpeed = -1;
                        flag = true;
                        Debug.Log("zoom-");
                    }
                }

                oldPosition1 = tempPosition1;
                oldPosition2 = tempPosition2;
            }
        }
    }

    void LateUpdate()
    {
        if (flag)
        {
            Vector3 body = bg.transform.localScale;
            bg.transform.localScale += new Vector3(body.x * Time.deltaTime * moveSpeed, body.y * Time.deltaTime * moveSpeed, 0);
            flag = false;
        }
    }

    public float moveSpeed = 1;//物体移动速度

    public GameObject bg;

    private Vector2 oldPosition;
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;

    private float distance = 5;
    private bool flag = false;
    private bool isBoth = false;
    bool IsEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //old distance
        float oldDistance = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        //new distance
        float newDistance = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));

        if (oldDistance < newDistance)
        {
            //zoom+
            return true;
        }
        else
        {
            //zoom-
            return false;
        }
    }

    public void ToggleSprites()
    {
        if (frameIndex == 0)
            frameIndex = 1;
        else
            frameIndex = 0;
    }

    public void ToggleSpritesArray()
    {
        button1.interactable = false;
        button2.interactable = false;
        m_isRotate = false;

        int index = frameIndex;
        for (int i = 0; i < frameSprites[index].m_frameSprite.Length; i++)
        {
            Destroy(frameSprites[index].m_frameSprite[i]);
            frameSprites[index].m_frameSprite[i] = null;
        }
        frameTexture.SetTexture("_MainTex", null);
        Resources.UnloadUnusedAssets();
        System.GC.Collect();



        if (frameIndex == frameSprites.Count - 1)
        {
            frameIndex = 0;
        }
        else
        {
            frameIndex++;
        }

        textureNameText.text = "Texture Name : " + textureNames[frameIndex];
        StartCoroutine(ContentLoad(urls[frameIndex]));
        //ContentLoad(urls[frameIndex]);


    }

    public void ToggleSpritesArrayLast()
    {
        button1.interactable = false;
        button2.interactable = false;
        m_isRotate = false;

        int index = frameIndex;
        for (int i = 0; i < frameSprites[index].m_frameSprite.Length; i++)
        {
            Destroy(frameSprites[index].m_frameSprite[i]);
            frameSprites[index].m_frameSprite[i] = null;
        }
        frameTexture.SetTexture("_MainTex", null);
        Resources.UnloadUnusedAssets();
        System.GC.Collect();


        if (frameIndex == 0)
        {
            frameIndex = frameSprites.Count - 1;
        }
        else
        {
            frameIndex--;
        }

        textureNameText.text = "Texture Name : " + textureNames[frameIndex];
        StartCoroutine(ContentLoad(urls[frameIndex]));
        //ContentLoad(urls[frameIndex]);

    }


    public void CorrectCurrentFrame()
    {
        int eulerindex = (int)m_Cube.transform.localEulerAngles.y / 3;
        //frameTexture.sprite = frameSprites[frameIndex].m_frameSprite[eulerindex];
        frameTexture.SetTexture("_MainTex", frameSprites[frameIndex].m_frameSprite[eulerindex]);
    }

    public void ReturnBackScale()
    {
        bg.transform.localScale = Vector3.one;
    }

    //public void FileDetectionMethod()
    //{
    //    //#if UNITY_EDITOR
    //    //        string ur = Application.persistentDataPath + "/1" + "/";
    //    //#elif UNITY_ANDROID
    //    //        string ur = "jar:file://" + Application.dataPath + "!/assets/";
    //    //#endif
    //    string ur = Application.persistentDataPath + "/1" + "/";

    //    if (!Directory.Exists(Application.persistentDataPath + "/1" + "/" + urls[0].Split('/')[0]))
    //    {
    //        fileDetectionButton.gameObject.SetActive(true);
    //        Debug.Log("文件夹没有创建，准备移动文件");
    //        return;
    //    }

    //    for (int i = 0; i < frameSprites.Count; i++)
    //    {
    //        for (int j = 0; j < frameSprites[i].fileNames.Length; j++)
    //        {
    //            if (!File.Exists(ur + urls[i] + "/" + frameSprites[i].fileNames[j]))
    //            {
    //                Debug.Log(ur + urls[i] + "/" + frameSprites[i].fileNames[j]);
    //                fileDetectionButton.gameObject.SetActive(true);
    //                Debug.Log("文件没有移动到本地，准备移动文件");
    //                return;
    //            }
    //            else
    //            {
    //                if (i == frameSprites.Count - 1 && j == 119)
    //                {
    //                    Debug.Log("文件完整");
    //                    Quad.gameObject.SetActive(true);
    //                    fileDetectionButton.gameObject.SetActive(false);
    //                    textureNameText.text = "Texture Name : " + textureNames[frameIndex];
    //                    //StartCoroutine(ContentLoad(urls[0]));
    //                    ContentLoad(urls[frameIndex]);
    //                }
    //            }
    //        }
    //    }
    //}

    //    public void MoveFileStorageLocationMethod()
    //    {
    //        StartCoroutine(MoveFileStorageLocation());
    //    }

    //    IEnumerator MoveFileStorageLocation()
    //    {
    //#if UNITY_EDITOR
    //        string ur = Application.streamingAssetsPath + "/";
    //#elif UNITY_ANDROID
    //       string ur = "jar:file://" + Application.dataPath + "!/assets/";
    //#endif
    //        for (int i = 0; i < frameSprites.Count; i++)
    //        {
    //            Debug.Log(Application.persistentDataPath + "/1" + "/" + urls[i]);


    //            if (!Directory.Exists(Application.persistentDataPath + "/1" + "/" + urls[i]))  // 检查有无指定文件夹
    //                Directory.CreateDirectory(Application.persistentDataPath + "/1" + "/" + urls[i]);  // 创建文件夹


    //            for (int j = 0; j < frameSprites[i].fileNames.Length; j++)
    //            {
    //                WWW www = new WWW(ur + urls[i] + "/" + frameSprites[i].fileNames[j]); //只能放URl 
    //                Debug.Log(ur + urls[i] + "/" + frameSprites[i].fileNames[j]);
    //                yield return www;
    //                if (www.isDone)
    //                {

    //                    Debug.Log(Application.persistentDataPath + "/1" + "/" + urls[i] + "/" + frameSprites[i].fileNames[j]);
    //                    //拷贝数据库到指定路径
    //                    string path = Application.persistentDataPath + "/1" + "/" + urls[i] + "/" + frameSprites[i].fileNames[j];

    //                    File.WriteAllBytes(path, www.bytes);



    //                    if (i == frameSprites.Count - 1 && j == 119)
    //                    {
    //                        yield return new WaitForSeconds(1f);

    //                        Quad.gameObject.SetActive(true);
    //                        fileDetectionButton.gameObject.SetActive(false);
    //                        textureNameText.text = "Texture Name : " + textureNames[frameIndex];
    //                        //StartCoroutine(ContentLoad(urls[0]));
    //                        ContentLoad(urls[frameIndex]);
    //                    }

    //                }
    //            }
    //        }
    //    }
}
