using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragRotateModel : MonoBehaviour
{
    public Camera m_Camera;


    public GameObject m_Cube;


    private bool isDragging = false;


    private float m_lastMouseX = float.MaxValue;


    private float m_lastSpeed = 0f;

    private bool m_isMoving = false;

    public float m_speed;

    public GameObject rotateModel;


    [SerializeField]
    public List<GameObject> rotateModels = new List<GameObject>();

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

    void Update()
    {
        if (rotateModel != null)
            rotateModel.transform.localEulerAngles = m_Cube.transform.localEulerAngles;

        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_Camera != null)
                {
                    RaycastHit hit;
                    Ray m_ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(m_ray, out hit, 1000f, m_Camera.cullingMask) &&
                        (hit.collider.gameObject == base.gameObject))
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
                m_Cube.transform.localEulerAngles = new Vector3(cube_localEulerAngles.x,
                    cube_localEulerAngles.y -= num * m_speed, cube_localEulerAngles.z);
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
            Vector3 body = rotateModel.transform.localScale;
            rotateModel.transform.localScale += new Vector3(body.x * Time.deltaTime * moveSpeed, body.y * Time.deltaTime * moveSpeed, 0);
            flag = false;
        }
    }

    public float moveSpeed = 1;//物体移动速度

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
}
