using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           

public class AutoCavasScalerAdjustor : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        CanvasScaler canvasScaler = this.GetComponent<CanvasScaler>();

        float standard_width = canvasScaler.referenceResolution.x;
        float standard_height = canvasScaler.referenceResolution.y;

        float device_width = Screen.width;
        float device_height = Screen.height;
        

        float standard_asoect = standard_width / standard_height;
        float device_asoect = device_width / device_height;

        if (device_asoect < standard_asoect)
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 1;
        }

        //canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
    }

}
