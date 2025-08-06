using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingModuleMapAnchor : MonoBehaviour
{
    public RectTransform rect;

    [ContextMenu("设置锚点")]
    public void SetAnchor()
    {
        GameObject AnchorMinX = new GameObject(rect.anchorMin.x.ToString());
        AnchorMinX.transform.parent = this.transform;
        GameObject AnchorMinY = new GameObject(rect.anchorMin.y.ToString());
        AnchorMinY.transform.parent = this.transform;
        GameObject AnchorMaxX = new GameObject(rect.anchorMax.x.ToString());
        AnchorMaxX.transform.parent = this.transform;
        GameObject AnchorMaxY = new GameObject(rect.anchorMax.y.ToString());
        AnchorMaxY.transform.parent = this.transform;
    }
}
