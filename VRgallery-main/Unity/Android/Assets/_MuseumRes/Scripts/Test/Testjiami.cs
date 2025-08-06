using System.Collections;
using System.Collections.Generic;
using System.IO;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class Testjiami : MonoBehaviour
{
    private VideoPlayer vp;
    public RawImage raw;
    public MediaPlayer media;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(aaa());
        //StartCoroutine(bbb());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator aaa()
    {

        yield return null;
        //UnityWebRequest wr = new UnityWebRequest(Application.streamingAssetsPath + "/AS_08557_1657505128961.jpg");

        //DownloadHandlerTexture texdl = new DownloadHandlerTexture(true);

        //wr.downloadHandler = texdl;

        //yield return wr.SendWebRequest();

        //Debug.Log(texdl.texture.width.ToString());
        //Debug.Log(texdl.texture.height.ToString());


        FileStream fileStream = new FileStream(Application.streamingAssetsPath + "/AS_03129.jpg", FileMode.Open, FileAccess.Read);
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
            for (int i = 0; i < index - 1; i++)
            {
                bytes[(butsize * i) + 3] = (byte)~bytes[(butsize * i) + 3];
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

        Debug.Log("texture1:" + texture1.width);
        Debug.Log("texture1:" + texture1.height);

        raw.texture = texture1;
    }

    public IEnumerator bbb()
    {
        yield return new WaitForSeconds(2f);

        //FileStream file = File.OpenRead(Application.dataPath + "/Test/02_1655621299467_ecode.mp4");
        //Debug.Log(file.Length);
        //media.StartOpenChunkedVideoFromBuffer((ulong)file.Length);
        //byte[] data = new byte[1024 * 1024];
        //ulong filePos = 0;
        //int isset = 0;
        //while (file.Position < file.Length)
        //{

        //    file.Read(data, 0, data.Length);
        //    if (data.Length > 3)
        //    {
        //        data[3] = (byte)~data[3];
        //    }
        //    //if (isset % 3 == 1)
        //    //{
        //    //    System.Array.Reverse(data);
        //    //}
        //    isset++;
        //    media.AddChunkToVideoBuffer(data, filePos, (ulong)data.Length);
        //    filePos = filePos + (ulong)data.Length;
        //}
        //media.EndOpenChunkedVideoFromBuffer();
        //Debug.Log("loadOver");
        //media.Play();

        byte[] bytes = File.ReadAllBytes(Application.dataPath + "/Test/AS_03129.mp4");

        ////FileStream fileStream = new FileStream(Application.dataPath + "/Test/2022-06-2918-25-57_x264_1658370895963.mp4", FileMode.Open, FileAccess.ReadWrite);
        ////fileStream.Seek(0, SeekOrigin.Begin);
        ////byte[] bytes = new byte[fileStream.Length];
        ////fileStream.Read(bytes, 0, (int)fileStream.Length);
        ////fileStream.Close();
        ////fileStream.Dispose();
        ////fileStream = null;

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        int butsize = 1048576;
        int index = 0;

        Debug.Log("bytes:" + bytes.Length);

        if ((bytes.Length <= butsize) && (bytes.Length > 3))
        {
            bytes[3] = (byte)~bytes[3];
        }
        else
        {
            index = bytes.Length / butsize;

            Debug.Log("index0:" + index);


            for (int i = 0; i < index; i++)
            {

                Debug.Log("indexCC:" + i);
                if (i == 0)
                {
                    bytes[3] = (byte)~bytes[3];
                }
                else
                {
                    Debug.Log("indexAA:" + ((butsize * i) + 3));
                    bytes[(butsize * i) + 3] = (byte)~bytes[(butsize * i) + 3];
                }
            }

            if (bytes.Length % butsize > 3)
            {
                Debug.Log("indexBB:" + ((index * butsize) + 3));

                bytes[index * butsize + 3] = (byte)~bytes[index * butsize + 3];
            }
        }

        //FileStream fs = File.Create(Application.dataPath + "/Test/02_1655621299467_ecode.mp4"); //path为你想保存文件的路径。
        //fs.Write(bytes, 0, bytes.Length);
        //fs.Close();

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        media.OpenVideoFromBuffer(bytes);
        //media.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, Application.dataPath + "/Test/02_1655621299467_ecode.mp4");

        //media.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL,
        //    //Application.dataPath + "/Test/2022-06-2918-25-57_x264_1658370895963.mp4");
        //    Application.dataPath + "/Test/02_1655621299467_ecode.mp4");
        //media.AddChunkToVideoBuffer(bytes, 0, 0);
        //media.StartOpenChunkedVideoFromBuffer()
        //media.OpenVideoFromBuffer()
    }
}
