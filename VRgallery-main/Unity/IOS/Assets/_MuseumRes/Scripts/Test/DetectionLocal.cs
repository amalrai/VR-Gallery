using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectionLocal : MonoBehaviour
{
    public GameObject 检测不到离线文件;
    public GameObject 检测不到XML文件;
    public GameObject 创建XML;

    public void Awake()
    {



        检测不到离线文件.SetActive(false);
        检测不到XML文件.SetActive(false);
        创建XML.SetActive(false);

        if (!Directory.Exists(Application.persistentDataPath + "/1" + "/AssetBundlePM"))
        {
            Debug.Log("未检测到AssetBundlePM文件夹,创建新的");
            检测不到离线文件.SetActive(true);
        }
        else
        {
            if (!Directory.Exists(Application.persistentDataPath + "/1" + "/XML"))
            {
                Debug.Log("未检测到XML文件夹,创建新的");
                检测不到XML文件.SetActive(true);
                创建XML.SetActive(true);
            }
            else
            {
                Debug.Log("数据完整，跳转场景");
                SceneManager.LoadScene("Main");

            }
        }
    }

    //private  string staticURL;
    //private  List<PaintingModule> staticDatas = new List<PaintingModule>();
    //public void EditorCreateXML()
    //{
    //    staticURL = Application.persistentDataPath + "/1" + "/";

    //    PaintingModule pm1 = new PaintingModule();
    //    pm1.paintingName = "01_xxxxx_印章";
    //    pm1.id = "1.00";
    //    pm1.url = staticURL + "AssetBundlePM/01_xxxxx_印章/";
    //    pm1.angle = 360;
    //    pm1.quantity = 69;
    //    pm1.resolution = "1376_920";
    //    pm1.paintingVideoName = "1b7ef2211c557760a90c9bafad36c12b.mp4";
    //    pm1.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm1.isDownload = true;

    //    PaintingModule pm2 = new PaintingModule();
    //    pm2.paintingName = "02_xxxxx_印章";
    //    pm2.id = "1.00";
    //    pm2.url = staticURL + "AssetBundlePM/02_xxxxx_印章/";
    //    pm2.angle = 360;
    //    pm2.quantity = 63;
    //    pm2.resolution = "1376_920";
    //    pm2.paintingVideoName = "1b7ef2211c557760a90c9bafad36c12b.mp4";
    //    pm2.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm2.isDownload = true;

    //    PaintingModule pm3 = new PaintingModule();
    //    pm3.paintingName = "03_xxxxx_印章";
    //    pm3.id = "1.00";
    //    pm3.url = staticURL + "AssetBundlePM/03_xxxxx_印章/";
    //    pm3.angle = 360;
    //    pm3.quantity = 63;
    //    pm3.resolution = "1376_920";
    //    pm3.paintingVideoName = "1b7ef2211c557760a90c9bafad36c12b.mp4";
    //    pm3.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm3.isDownload = true;

    //    PaintingModule pm4 = new PaintingModule();
    //    pm4.paintingName = "04_xxxxx_印章";
    //    pm4.id = "1.00";
    //    pm4.url = staticURL + "AssetBundlePM/04_xxxxx_印章/";
    //    pm4.angle = 360;
    //    pm4.quantity = 63;
    //    pm4.resolution = "1376_920";
    //    pm4.paintingVideoName = "1b7ef2211c557760a90c9bafad36c12b.mp4";
    //    pm4.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm4.isDownload = true;

    //    PaintingModule pm5 = new PaintingModule();
    //    pm5.paintingName = "05_xxxxx_印章";
    //    pm5.id = "1.00";
    //    pm5.url = staticURL + "AssetBundlePM/05_xxxxx_印章/";
    //    pm5.angle = 360;
    //    pm5.quantity = 64;
    //    pm5.resolution = "1376_920";
    //    pm5.paintingVideoName = "1603c2f468625e7afab6bc218a105ce4.mp4";
    //    pm5.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm5.isDownload = true;

    //    PaintingModule pm6 = new PaintingModule();
    //    pm6.paintingName = "06_xxxxx_印章";
    //    pm6.id = "1.00";
    //    pm6.url = staticURL + "AssetBundlePM/06_xxxxx_印章/";
    //    pm6.angle = 360;
    //    pm6.quantity = 64;
    //    pm6.resolution = "1376_920";
    //    pm6.paintingVideoName = "1603c2f468625e7afab6bc218a105ce4.mp4";
    //    pm6.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm6.isDownload = true;

    //    PaintingModule pm7 = new PaintingModule();
    //    pm7.paintingName = "07_xxxxx_印章";
    //    pm7.id = "1.00";
    //    pm7.url = staticURL + "AssetBundlePM/07_xxxxx_印章/";
    //    pm7.angle = 360;
    //    pm7.quantity = 63;
    //    pm7.resolution = "1376_920";
    //    pm7.paintingVideoName = "1603c2f468625e7afab6bc218a105ce4.mp4";
    //    pm7.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm7.isDownload = true;

    //    PaintingModule pm8 = new PaintingModule();
    //    pm8.paintingName = "08_xxxxx_印章";
    //    pm8.id = "1.00";
    //    pm8.url = staticURL + "AssetBundlePM/08_xxxxx_印章/";
    //    pm8.angle = 360;
    //    pm8.quantity = 63;
    //    pm8.resolution = "1376_920";
    //    pm8.paintingVideoName = "1603c2f468625e7afab6bc218a105ce4.mp4";
    //    pm8.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm8.isDownload = true;

    //    PaintingModule pm9 = new PaintingModule();
    //    pm9.paintingName = "09_xxxxx_印章";
    //    pm9.id = "1.00";
    //    pm9.url = staticURL + "AssetBundlePM/09_xxxxx_印章/";
    //    pm9.angle = 360;
    //    pm9.quantity = 68;
    //    pm9.resolution = "1376_920";
    //    pm9.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm9.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm9.isDownload = true;

    //    PaintingModule pm10 = new PaintingModule();
    //    pm10.paintingName = "10_xxxxx_印章";
    //    pm10.id = "1.00";
    //    pm10.url = staticURL + "AssetBundlePM/10_xxxxx_印章/";
    //    pm10.angle = 360;
    //    pm10.quantity = 69;
    //    pm10.resolution = "1376_920";
    //    pm10.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm10.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm10.isDownload = true;

    //    PaintingModule pm11 = new PaintingModule();
    //    pm11.paintingName = "11_xxxxx_印章";
    //    pm11.id = "1.00";
    //    pm11.url = staticURL + "AssetBundlePM/11_xxxxx_印章/";
    //    pm11.angle = 360;
    //    pm11.quantity = 69;
    //    pm11.resolution = "1376_920";
    //    pm11.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm11.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm11.isDownload = true;

    //    PaintingModule pm12 = new PaintingModule();
    //    pm12.paintingName = "12_xxxxx_印章";
    //    pm12.id = "1.00";
    //    pm12.url = staticURL + "AssetBundlePM/12_xxxxx_印章/";
    //    pm12.angle = 360;
    //    pm12.quantity = 71;
    //    pm12.resolution = "1376_920";
    //    pm12.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm12.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm12.isDownload = true;

    //    PaintingModule pm13 = new PaintingModule();
    //    pm13.paintingName = "13_xxxxx_印章";
    //    pm13.id = "1.00";
    //    pm13.url = staticURL + "AssetBundlePM/13_xxxxx_印章/";
    //    pm13.angle = 360;
    //    pm13.quantity = 72;
    //    pm13.resolution = "1376_920";
    //    pm13.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm13.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm13.isDownload = true;

    //    PaintingModule pm14 = new PaintingModule();
    //    pm14.paintingName = "14_xxxxx_印章";
    //    pm14.id = "1.00";
    //    pm14.url = staticURL + "AssetBundlePM/14_xxxxx_印章/";
    //    pm14.angle = 360;
    //    pm14.quantity = 70;
    //    pm14.resolution = "1376_920";
    //    pm14.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm14.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm14.isDownload = true;

    //    PaintingModule pm15 = new PaintingModule();
    //    pm15.paintingName = "15_xxxxx_印章";
    //    pm15.id = "1.00";
    //    pm15.url = staticURL + "AssetBundlePM/15_xxxxx_印章/";
    //    pm15.angle = 360;
    //    pm15.quantity = 70;
    //    pm15.resolution = "1376_920";
    //    pm15.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm15.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm15.isDownload = true;

    //    PaintingModule pm16 = new PaintingModule();
    //    pm16.paintingName = "16_xxxxx_印章";
    //    pm16.id = "1.00";
    //    pm16.url = staticURL + "AssetBundlePM/16_xxxxx_印章/";
    //    pm16.angle = 360;
    //    pm16.quantity = 71;
    //    pm16.resolution = "1376_920";
    //    pm16.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm16.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm16.isDownload = true;

    //    PaintingModule pm17 = new PaintingModule();
    //    pm17.paintingName = "17_xxxxx_コイン";
    //    pm17.id = "1.00";
    //    pm17.url = staticURL + "AssetBundlePM/17_xxxxx_コイン/";
    //    pm17.angle = 360;
    //    pm17.quantity = 70;
    //    pm17.resolution = "1376_920";
    //    pm17.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm17.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm17.isDownload = true;

    //    PaintingModule pm18 = new PaintingModule();
    //    pm18.paintingName = "18_xxxxx_コイン";
    //    pm18.id = "1.00";
    //    pm18.url = staticURL + "AssetBundlePM/18_xxxxx_コイン/";
    //    pm18.angle = 360;
    //    pm18.quantity = 69;
    //    pm18.resolution = "1376_920";
    //    pm18.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm18.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm18.isDownload = true;

    //    PaintingModule pm19 = new PaintingModule();
    //    pm19.paintingName = "19_xxxxx_コイン";
    //    pm19.id = "1.00";
    //    pm19.url = staticURL + "AssetBundlePM/19_xxxxx_コイン/";
    //    pm19.angle = 360;
    //    pm19.quantity = 69;
    //    pm19.resolution = "1376_920";
    //    pm19.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm19.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm19.isDownload = true;

    //    PaintingModule pm20 = new PaintingModule();
    //    pm20.paintingName = "20_xxxxx_コイン";
    //    pm20.id = "1.00";
    //    pm20.url = staticURL + "AssetBundlePM/20_xxxxx_コイン/";
    //    pm20.angle = 360;
    //    pm20.quantity = 69;
    //    pm20.resolution = "1376_920";
    //    pm20.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm20.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm20.isDownload = true;

    //    PaintingModule pm21 = new PaintingModule();
    //    pm21.paintingName = "21_xxxxx_";
    //    pm21.id = "1.00";
    //    pm21.url = staticURL + "AssetBundlePM/21_xxxxx_/";
    //    pm21.angle = 360;
    //    pm21.quantity = 70;
    //    pm21.resolution = "1376_920";
    //    pm21.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm21.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm21.isDownload = true;

    //    PaintingModule pm22 = new PaintingModule();
    //    pm22.paintingName = "22_xxxxx_化粧1";
    //    pm22.id = "1.00";
    //    pm22.url = staticURL + "AssetBundlePM/22_xxxxx_化粧1/";
    //    pm22.angle = 360;
    //    pm22.quantity = 69;
    //    pm22.resolution = "1376_920";
    //    pm22.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm22.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm22.isDownload = true;

    //    PaintingModule pm23 = new PaintingModule();
    //    pm23.paintingName = "23_xxxxx_化粧2";
    //    pm23.id = "1.00";
    //    pm23.url = staticURL + "AssetBundlePM/23_xxxxx_化粧2/";
    //    pm23.angle = 360;
    //    pm23.quantity = 70;
    //    pm23.resolution = "1376_920";
    //    pm23.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm23.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm23.isDownload = true;

    //    PaintingModule pm24 = new PaintingModule();
    //    pm24.paintingName = "24_xxxxx_";
    //    pm24.id = "1.00";
    //    pm24.url = staticURL + "AssetBundlePM/24_xxxxx_/";
    //    pm24.angle = 360;
    //    pm24.quantity = 68;
    //    pm24.resolution = "1376_920";
    //    pm24.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm24.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm24.isDownload = true;

    //    PaintingModule pm25 = new PaintingModule();
    //    pm25.paintingName = "25_xxxxx_ライオン";
    //    pm25.id = "1.00";
    //    pm25.url = staticURL + "AssetBundlePM/25_xxxxx_ライオン/";
    //    pm25.angle = 360;
    //    pm25.quantity = 66;
    //    pm25.resolution = "1376_920";
    //    pm25.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm25.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm25.isDownload = true;

    //    PaintingModule pm26 = new PaintingModule();
    //    pm26.paintingName = "26_xxxxx_";
    //    pm26.id = "1.00";
    //    pm26.url = staticURL + "AssetBundlePM/26_xxxxx_/";
    //    pm26.angle = 360;
    //    pm26.quantity = 63;
    //    pm26.resolution = "1376_920";
    //    pm26.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm26.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm26.isDownload = true;

    //    PaintingModule pm27 = new PaintingModule();
    //    pm27.paintingName = "27_xxxxx_";
    //    pm27.id = "1.00";
    //    pm27.url = staticURL + "AssetBundlePM/27_xxxxx_/";
    //    pm27.angle = 360;
    //    pm27.quantity = 63;
    //    pm27.resolution = "1376_920";
    //    pm27.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm27.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm27.isDownload = true;

    //    PaintingModule pm28 = new PaintingModule();
    //    pm28.paintingName = "28_xxxxx_";
    //    pm28.id = "1.00";
    //    pm28.url = staticURL + "AssetBundlePM/28_xxxxx_/";
    //    pm28.angle = 360;
    //    pm28.quantity = 65;
    //    pm28.resolution = "1376_920";
    //    pm28.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm28.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm28.isDownload = true;

    //    PaintingModule pm29 = new PaintingModule();
    //    pm29.paintingName = "29_xxxxx_";
    //    pm29.id = "1.00";
    //    pm29.url = staticURL + "AssetBundlePM/29_xxxxx_/";
    //    pm29.angle = 360;
    //    pm29.quantity = 63;
    //    pm29.resolution = "1376_920";
    //    pm29.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm29.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm29.isDownload = true;

    //    PaintingModule pm30 = new PaintingModule();
    //    pm30.paintingName = "30_xxxxx_";
    //    pm30.id = "1.00";
    //    pm30.url = staticURL + "AssetBundlePM/30_xxxxx_/";
    //    pm30.angle = 360;
    //    pm30.quantity = 64;
    //    pm30.resolution = "1376_920";
    //    pm30.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm30.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm30.isDownload = true;

    //    PaintingModule pm31 = new PaintingModule();
    //    pm31.paintingName = "31_xxxxx_";
    //    pm31.id = "1.00";
    //    pm31.url = staticURL + "AssetBundlePM/31_xxxxx_/";
    //    pm31.angle = 360;
    //    pm31.quantity = 64;
    //    pm31.resolution = "1376_920";
    //    pm31.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm31.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm31.isDownload = true;

    //    PaintingModule pm32 = new PaintingModule();
    //    pm32.paintingName = "32_xxxxx_";
    //    pm32.id = "1.00";
    //    pm32.url = staticURL + "AssetBundlePM/32_xxxxx_/";
    //    pm32.angle = 360;
    //    pm32.quantity = 63;
    //    pm32.resolution = "1376_920";
    //    pm32.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm32.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm32.isDownload = true;

    //    PaintingModule pm33 = new PaintingModule();
    //    pm33.paintingName = "33_xxxxx_";
    //    pm33.id = "1.00";
    //    pm33.url = staticURL + "AssetBundlePM/33_xxxxx_/";
    //    pm33.angle = 360;
    //    pm33.quantity = 63;
    //    pm33.resolution = "1376_920";
    //    pm33.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm33.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm33.isDownload = true;

    //    PaintingModule pm34 = new PaintingModule();
    //    pm34.paintingName = "34_xxxxx_";
    //    pm34.id = "1.00";
    //    pm34.url = staticURL + "AssetBundlePM/34_xxxxx_/";
    //    pm34.angle = 360;
    //    pm34.quantity = 65;
    //    pm34.resolution = "1376_920";
    //    pm34.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm34.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm34.isDownload = true;

    //    PaintingModule pm35 = new PaintingModule();
    //    pm35.paintingName = "35_xxxxx_";
    //    pm35.id = "1.00";
    //    pm35.url = staticURL + "AssetBundlePM/35_xxxxx_/";
    //    pm35.angle = 360;
    //    pm35.quantity = 64;
    //    pm35.resolution = "1376_920";
    //    pm35.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm35.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm35.isDownload = true;

    //    PaintingModule pm36 = new PaintingModule();
    //    pm36.paintingName = "36_xxxxx_";
    //    pm36.id = "1.00";
    //    pm36.url = staticURL + "AssetBundlePM/36_xxxxx_/";
    //    pm36.angle = 360;
    //    pm36.quantity = 62;
    //    pm36.resolution = "1376_920";
    //    pm36.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm36.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm36.isDownload = true;

    //    PaintingModule pm37 = new PaintingModule();
    //    pm37.paintingName = "37_xxxxx_";
    //    pm37.id = "1.00";
    //    pm37.url = staticURL + "AssetBundlePM/37_xxxxx_/";
    //    pm37.angle = 360;
    //    pm37.quantity = 64;
    //    pm37.resolution = "1376_920";
    //    pm37.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm37.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm37.isDownload = true;

    //    PaintingModule pm38 = new PaintingModule();
    //    pm38.paintingName = "38_xxxxx_";
    //    pm38.id = "1.00";
    //    pm38.url = staticURL + "AssetBundlePM/38_xxxxx_/";
    //    pm38.angle = 360;
    //    pm38.quantity = 63;
    //    pm38.resolution = "1376_920";
    //    pm38.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm38.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm38.isDownload = true;

    //    PaintingModule pm39 = new PaintingModule();
    //    pm39.paintingName = "39_後青調";
    //    pm39.id = "1.00";
    //    pm39.url = staticURL + "AssetBundlePM/39_後青調/";
    //    pm39.angle = 360;
    //    pm39.quantity = 41;
    //    pm39.resolution = "1696_1133";
    //    pm39.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm39.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm39.isDownload = true;

    //    PaintingModule pm40 = new PaintingModule();
    //    pm40.paintingName = "40_xxxxx_";
    //    pm40.id = "1.00";
    //    pm40.url = staticURL + "AssetBundlePM/40_xxxxx_/";
    //    pm40.angle = 360;
    //    pm40.quantity = 46;
    //    pm40.resolution = "1106_739";
    //    pm40.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm40.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm40.isDownload = true;

    //    PaintingModule pm41 = new PaintingModule();
    //    pm41.paintingName = "41_wappa";
    //    pm41.id = "1.00";
    //    pm41.url = staticURL + "AssetBundlePM/41_wappa/";
    //    pm41.angle = 360;
    //    pm41.quantity = 43;
    //    pm41.resolution = "1106_739";
    //    pm41.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm41.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm41.isDownload = true;

    //    PaintingModule pm42 = new PaintingModule();
    //    pm42.paintingName = "42_xxxxx_";
    //    pm42.id = "1.00";
    //    pm42.url = staticURL + "AssetBundlePM/42_xxxxx_/";
    //    pm42.angle = 360;
    //    pm42.quantity = 34;
    //    pm42.resolution = "1106_739";
    //    pm42.paintingVideoName = "5f50a13719a7d4867b183c4dd34cd53d.mp4";
    //    pm42.paintingWebUrl = "www.silkroad-museum.jp";
    //    pm42.isDownload = true;

    //    staticDatas.Add(pm1);
    //    staticDatas.Add(pm2);
    //    staticDatas.Add(pm3);
    //    staticDatas.Add(pm4);
    //    staticDatas.Add(pm5);
    //    staticDatas.Add(pm6);
    //    staticDatas.Add(pm7);
    //    staticDatas.Add(pm8);
    //    staticDatas.Add(pm9);
    //    staticDatas.Add(pm10);
    //    staticDatas.Add(pm11);
    //    staticDatas.Add(pm12);
    //    staticDatas.Add(pm13);
    //    staticDatas.Add(pm14);
    //    staticDatas.Add(pm15);
    //    staticDatas.Add(pm16);
    //    staticDatas.Add(pm17);
    //    staticDatas.Add(pm18);
    //    staticDatas.Add(pm19);
    //    staticDatas.Add(pm20);
    //    staticDatas.Add(pm21);
    //    staticDatas.Add(pm22);
    //    staticDatas.Add(pm23);
    //    staticDatas.Add(pm24);
    //    staticDatas.Add(pm25);
    //    staticDatas.Add(pm26);
    //    staticDatas.Add(pm27);
    //    staticDatas.Add(pm28);
    //    staticDatas.Add(pm29);
    //    staticDatas.Add(pm30);
    //    staticDatas.Add(pm31);
    //    staticDatas.Add(pm32);
    //    staticDatas.Add(pm33);
    //    staticDatas.Add(pm34);
    //    staticDatas.Add(pm35);
    //    staticDatas.Add(pm36);
    //    staticDatas.Add(pm37);
    //    staticDatas.Add(pm38);
    //    staticDatas.Add(pm39);
    //    staticDatas.Add(pm40);
    //    staticDatas.Add(pm41);
    //    staticDatas.Add(pm42);

    //    XmlDocument xml = new XmlDocument();
    //    XmlElement museumPainting = xml.CreateElement("MuseumPainting");
    //    XmlElement paintingModuleTexture = xml.CreateElement("PaintingModuleTexture");
    //    for (int i = 0; i < staticDatas.Count; i++)
    //    {
    //        XmlElement paintingModule = null;
    //        if (i < 10)
    //        {
    //            paintingModule = xml.CreateElement("PaintingModule_0" + i);
    //        }
    //        else
    //        {
    //            paintingModule = xml.CreateElement("PaintingModule_" + i);
    //        }
    //        XmlElement paintingName = xml.CreateElement("PaintingName");
    //        XmlElement id = xml.CreateElement("ID");
    //        XmlElement url = xml.CreateElement("URL");
    //        XmlElement angle = xml.CreateElement("Angle");
    //        XmlElement quantity = xml.CreateElement("Quantity");
    //        XmlElement resolution = xml.CreateElement("Resolution");
    //        XmlElement isDownload = xml.CreateElement("IsDownload");
    //        XmlElement paintingVideoName = xml.CreateElement("PaintingVideoName");
    //        XmlElement paintingWebUrl = xml.CreateElement("PaintingWebUrl");
    //        XmlElement paintingArrayName = xml.CreateElement("PaintingArrayName");
    //        List<XmlElement> elements = new List<XmlElement>();
    //        DirectoryInfo dir = new DirectoryInfo(staticDatas[i].url + "Texture");
    //        FileInfo[] fil = dir.GetFiles();
    //        for (int j = 0; j < fil.Length; j++)
    //        {
    //            XmlElement ele = xml.CreateElement("Name" + j);
    //            ele.InnerText = fil[j].Name;
    //            elements.Add(ele);
    //        }
    //        paintingName.InnerText = staticDatas[i].paintingName;
    //        id.InnerText = staticDatas[i].id;
    //        url.InnerText = staticDatas[i].url;
    //        angle.InnerText = staticDatas[i].angle.ToString();
    //        quantity.InnerText = staticDatas[i].quantity.ToString();
    //        resolution.InnerText = staticDatas[i].resolution;
    //        isDownload.InnerText = staticDatas[i].isDownload.ToString();
    //        paintingVideoName.InnerText = staticDatas[i].paintingVideoName;
    //        paintingWebUrl.InnerText = staticDatas[i].paintingWebUrl;
    //        for (int k = 0; k < elements.Count; k++)
    //        {
    //            paintingArrayName.AppendChild(elements[k]);
    //        }
    //        paintingModule.AppendChild(paintingName);
    //        paintingModule.AppendChild(id);
    //        paintingModule.AppendChild(url);
    //        paintingModule.AppendChild(angle);
    //        paintingModule.AppendChild(quantity);
    //        paintingModule.AppendChild(resolution);
    //        paintingModule.AppendChild(isDownload);
    //        paintingModule.AppendChild(paintingVideoName);
    //        paintingModule.AppendChild(paintingWebUrl);
    //        paintingModule.AppendChild(paintingArrayName);
    //        paintingModuleTexture.AppendChild(paintingModule);
    //        museumPainting.AppendChild(paintingModuleTexture);
    //    }
    //    xml.AppendChild(museumPainting);
    //    if (!Directory.Exists(Application.persistentDataPath + "/1" + "/XML"))
    //    {
    //        Debug.Log("未检测到xml文件夹,创建新的");
    //        Directory.CreateDirectory(Application.persistentDataPath + "/1" + "/XML");
    //    }
    //    xml.Save(Application.persistentDataPath + "/1" + "/XML/" + "MuseumPaintingData.xml");
    //    SceneManager.LoadScene("Main");
    //}
}
