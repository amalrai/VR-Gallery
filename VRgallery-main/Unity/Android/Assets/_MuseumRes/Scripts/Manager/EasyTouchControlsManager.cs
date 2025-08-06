using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyTouchControlsManager : UnitySingleton<EasyTouchControlsManager>
{
    public ETCTouchPad etcTouchPad;

    public ETCJoystick eTCJoystick;

    public Image etcTouchPadImage;

    public Transform Player;

    public Transform PlayerPos;

    //private void Awake()
    //{
    //    Initial();
    //}

    /// <summary>
    /// 摇杆控制初始化
    /// </summary>
    public void Initial()
    {
        Debug.Log("摇杆控制初始化完成");

        etcTouchPad = this.GetComponentInChildren<ETCTouchPad>();

        eTCJoystick = this.GetComponentInChildren<ETCJoystick>();

        etcTouchPadImage = etcTouchPad.GetComponent<Image>();

        Player = GameObject.Find("Player").transform;

        PlayerPos = GameObject.Find("PlayerInitialPosition").transform;

        Player.transform.position = PlayerPos.position;
        Player.transform.eulerAngles = PlayerPos.eulerAngles;

        if (PlayerPrefs.HasKey("JoystickSpeed"))
        {
            SetETCJoystickSpeed(PlayerPrefs.GetFloat("JoystickSpeed"));
        }

        if (PlayerPrefs.HasKey("TouchPadSpeed"))
        {
            SetETCTouchPadSpeed(PlayerPrefs.GetFloat("TouchPadSpeed"));
        }

        GetLoadingProgress.Instance.isFake = true;
        GetLoadingProgress.Instance.currentAlreadyIndex += 1;

    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        eTCJoystick.axisX.speed = 1;
    //        eTCJoystick.axisY.speed = 1;
    //    }
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        eTCJoystick.axisX.speed = 5;
    //        eTCJoystick.axisY.speed = 5;
    //    }
    //}

    /// <summary>
    /// 摇杆控制重置
    /// </summary>
    public void Reset()
    {
        etcTouchPad.activated = false;
        eTCJoystick.activated = false;
        etcTouchPadImage.raycastTarget = false;
    }

    /// <summary>
    /// 摇杆控制启动
    /// </summary>
    public void EasyTouchControlsStart()
    {
        etcTouchPad.activated = true;
        eTCJoystick.activated = true;
        etcTouchPadImage.raycastTarget = true;
    }
    /// <summary>
    /// 摇杆控制关闭
    /// </summary>
    public void EasyTouchControlsStop()
    {
        etcTouchPad.activated = false;
        eTCJoystick.activated = false;
        etcTouchPadImage.raycastTarget = false;
    }
    /// <summary>
    /// 玩家传送到地图点
    /// </summary>
    /// <param name="v2"></param>
    public void PlayerTransferMap(Vector2 v2,Vector3 v3)
    {
        Debug.Log(v3);
        Player.transform.localPosition = new Vector3(v2.x, Player.transform.localPosition.y, v2.y);
        Player.transform.eulerAngles = v3;
        Player.transform.GetChild(0).localEulerAngles = Vector3.zero;
    }

    /// <summary>
    /// 玩家传送到地图点
    /// </summary>
    /// <param name="v2"></param>
    public void PlayerTransferMapNew(Transform point)
    {
        Player.transform.position = point.position;
        Player.transform.eulerAngles = point.eulerAngles;
        Player.transform.GetChild(0).localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// 设置摇杆速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetETCJoystickSpeed(float speed)
    {
        eTCJoystick.axisX.speed = speed;
        eTCJoystick.axisY.speed = speed;
    }
    /// <summary>
    /// 设置屏幕滑动速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetETCTouchPadSpeed(float speed)
    {
        etcTouchPad.axisX.speed = speed;
        etcTouchPad.axisY.speed = speed;
    }
}
