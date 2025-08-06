using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAdaptive : MonoBehaviour
{
    public float ScreenWidth;
    public float ScreenHeight;

    public CanvasScaler scaler;

    public void Awake()
    {
        scaler = this.GetComponent<CanvasScaler>();
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        scaler.referenceResolution = new Vector2(ScreenWidth, ScreenHeight);
    }

}
