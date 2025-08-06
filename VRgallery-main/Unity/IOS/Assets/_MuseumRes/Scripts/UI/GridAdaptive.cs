using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridAdaptive : MonoBehaviour
{

    public GridLayoutGroup grid;

    public void Start()
    {
        CanvasScaler canvasScaler = this.GetComponentInParent<CanvasScaler>();
        grid = this.GetComponent<GridLayoutGroup>();


        float standard_width = canvasScaler.referenceResolution.x;
        float standard_height = canvasScaler.referenceResolution.y;

        float device_width = Screen.width;
        float device_height = Screen.height;

        float cellx;
        float celly;

        cellx = grid.cellSize.x;
        celly = grid.cellSize.y;

        if (device_width == 1920 && device_height == 1080)
        {
            return;

        }
        else
        {
            float standard_asoect = standard_width / standard_height;
            float device_asoect = device_width / device_height;

            if (device_asoect < standard_asoect)
            {
                //cellx = (device_width / 1920) * grid.cellSize.x;
                celly = device_height / (1080 * (device_width / 1920)) * grid.cellSize.y;
            }
            else
            {
                cellx = device_width / (1920 * (device_height / 1080)) * grid.cellSize.x;
                //celly = (device_height / 1080) * grid.cellSize.y;
            }
        }


        grid.cellSize = new Vector2(cellx, celly);

    }
}
