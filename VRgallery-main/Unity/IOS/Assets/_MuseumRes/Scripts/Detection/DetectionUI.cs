using UnityEngine;
using UnityEngine.UI;

public class DetectionUI : UnitySingleton<DetectionUI>
{
    public GameObject detectionUICanvas;
    public Text detectionUICanvasDebug_Text;
    public Button detectionUICanvasDebug_Button;

    public void Awake()
    {
        detectionUICanvas = GameObject.Find("DebugCanvas").gameObject;
        detectionUICanvasDebug_Text = GameObject.Find("Debug_Text").GetComponent<Text>();
        detectionUICanvasDebug_Button = GameObject.Find("Debug_Button").GetComponent<Button>();
        detectionUICanvasDebug_Button.onClick.AddListener(DebugButtonClose);


    }

    public void Start()
    {
        detectionUICanvas.SetActive(false);

    }


    /// <summary>
    /// 打开button，提示问题
    /// </summary>
    /// <param name="text"></param>
    public void DebugButtonOpen(string text)
    {
        detectionUICanvasDebug_Text.text = text;
        detectionUICanvas.SetActive(true);
    }
    /// <summary>
    /// 关闭button
    /// </summary>
    public void DebugButtonClose()
    {
        detectionUICanvasDebug_Text.text = "";
        detectionUICanvas.SetActive(false);

        switch (HttpManager.Instance.httpState)
        {
            case HttpState.None:
                break;

            case HttpState.GetRoomDataWebRequest:
                HttpManager.Instance.GetHttpRoomData();
                break;

            case HttpState.GetRoomDataWeb:
                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.GalleryIDGetAllRoomURL, null);
                break;

            case HttpState.GetRoomPMDataWebRequest:
                HttpManager.Instance.GetHttpRoomPMData();
                break;

            case HttpState.GetRoomPMDataWeb:
                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.GetRoomAllExhibitsData, null);
                break;

            case HttpState.SendRegisterCode:
                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.SendRegisterCodeURL, null);
                break;

            case HttpState.Register:
                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.RegisterURL, null);
                break;

            case HttpState.DeleteUser:
                HttpManager.Instance.NetWorkUpdate(DataJsonClass.NetTypeListen.DeleteUserByIdURL, null);
                break;
        }
    }
}
