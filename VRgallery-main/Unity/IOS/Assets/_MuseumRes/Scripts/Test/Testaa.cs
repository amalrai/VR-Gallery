using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testaa : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject c;

    private Vector3 oldPosition1;
    private Vector3 oldPosition2;

    public Vector3 pianyi;

    public RectTransform rect;

    private float distance;

    public void Awake()
    {
        //distance = Vector3.Distance(a.transform.position, b.transform.position);

    }

    public void Update()
    {
        //rect.transform.Translate(new Vector3(1 * 0.1f, 1 * 0.1f, 0f), Space.Self);

        //Vector3 tempPosition1 = a.transform.position;
        //Vector3 tempPosition2 = b.transform.position;
        Debug.Log(rect.transform.localPosition);

        //pianyi = IsEnlarge(GetBetweenPoint(tempPosition1, tempPosition2, distance), GetBetweenPoint(oldPosition1, oldPosition2, distance));

        //oldPosition1 = tempPosition1;
        //oldPosition2 = tempPosition2;

    }

    private Vector3 GetBetweenPoint(Vector3 start, Vector3 end, float distance)
    {
        Vector3 normal = (end - start).normalized;
        return normal * distance + start;
    }

    Vector3 IsEnlarge(Vector3 start, Vector3 end)
    {
        Vector3 ve3;

        ve3 = end - start;

        return ve3;
    }

    void LateUpdate()
    {

        //Vector3 body = c.transform.position;
        //c.transform.position = new Vector3(body.x + pianyi.x, body.y + pianyi.y, body.z);

    }
}

