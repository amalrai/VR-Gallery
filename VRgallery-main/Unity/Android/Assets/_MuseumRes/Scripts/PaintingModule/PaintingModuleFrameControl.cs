using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRubyShared;
using TouchPhase = DigitalRubyShared.TouchPhase;

public class PaintingModuleFrameControl : UnitySingleton<PaintingModuleFrameControl>
{
    public Camera m_Camera;

    public GameObject m_Cube;

    public bool m_isRotate = false;

    public bool isDragging = false;

    public float m_lastMouseX = float.MaxValue;

    public float m_lastSpeed = 0f;

    public bool m_isMoving = false;

    public float m_speed;

    public float m_rotateCoefficient;

    public RawImage frameTexture;

    public RawImage introduceTexture;

    public bool isFrameFingers;

    public Collider coll;

    private void Awake()
    {

    }

    public void Initial()
    {
        isFrameFingers = true;
        coll = this.GetComponent<Collider>();
    }

    public bool isCalibration = false;
    void Update()
    {
        if (!m_isRotate)
            return;

        int eulerindex;
        eulerindex = (int)(m_Cube.transform.localEulerAngles.y / m_rotateCoefficient);

        //frameTexture.sprite = frameSprites[frameIndex].m_frameSprite[eulerindex];
        //frameTexture.SetTexture("_MainTex", frameSprites[frameIndex].m_frameSprite[eulerindex]);
        //替换贴图方法
        PaintingModuleFrameCanvas.Instance.PaintingModuleFrameRawImageReplace(eulerindex);

        if (FingersScript.Instance.CurrentTouches.Count == 1)
        {
            if (FingersScript.Instance.CurrentTouches[0].TouchPhase == TouchPhase.Began)
            {
                if (m_Camera != null)
                {
                    //RaycastHit hit;
                    //Ray m_ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                    //if (Physics.Raycast(m_ray, out hit, 1000f, m_Camera.cullingMask) && (hit.collider.gameObject == base.gameObject))
                    //{
                    //    //选中物体
                    //    Debug.Log("选中物体");

                    //}
                    Debug.Log("选中物体");
                    this.isDragging = true;
                    isBoth = false;
                }
            }
            else if (FingersScript.Instance.CurrentTouches[0].TouchPhase == TouchPhase.Ended)
            {
                Debug.Log("抬起物体");
                if (FingersScript.Instance.CurrentTouches.Count <= 1)
                {

                    isCalibration = false;
                    bool isDrag = false;
                    //抬起
                    if (isDragging)
                        isDrag = true;
                    this.isDragging = false;


                    ////抬起后缓慢转动 
                    //if (isDrag)
                    //{
                    //    this.m_isMoving = true;
                    //    this.m_lastSpeed = Input.mousePosition.x - this.m_lastMouseX;
                    //    this.m_lastMouseX = float.MaxValue;
                    //}
                }
            }

            if (this.isDragging)
            {
                Debug.Log("拖拽物体");
                if (!isCalibration)
                {
                    isCalibration = true;
                    m_Cube.transform.localEulerAngles -= new Vector3(0, 0, 0);
                }
                else
                {
                    m_Cube.transform.localEulerAngles -= new Vector3(0, FingersScript.Instance.CurrentTouches[0].DeltaX * m_speed, 0);
                }

                //float num = 0;
                //if (this.m_lastMouseX != float.MaxValue)
                //{
                //    num = Input.mousePosition.x - this.m_lastMouseX;
                //}
                //this.m_lastMouseX = Input.mousePosition.x;


                //Vector3 cube_localEulerAngles = m_Cube.transform.localEulerAngles;
                //m_Cube.transform.localEulerAngles = new Vector3(cube_localEulerAngles.x, cube_localEulerAngles.y -= num * m_speed, cube_localEulerAngles.z);



            }
        }

        //if (Input.touchCount == 1)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        if (m_Camera != null)
        //        {
        //            //RaycastHit hit;
        //            //Ray m_ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        //            //if (Physics.Raycast(m_ray, out hit, 1000f, m_Camera.cullingMask) && (hit.collider.gameObject == base.gameObject))
        //            //{
        //            //    //选中物体
        //            //    Debug.Log("选中物体");

        //            //}

        //            this.isDragging = true;
        //            isBoth = false;
        //        }
        //    }
        //    else if (Input.GetMouseButtonUp(0))
        //    {
        //        if (Input.touchCount <= 1)
        //        {

        //            bool isDrag = false;
        //            //抬起
        //            if (isDragging)
        //                isDrag = true;
        //            this.isDragging = false;


        //            //抬起后缓慢转动 
        //            if (isDrag)
        //            {
        //                this.m_isMoving = true;
        //                this.m_lastSpeed = Input.mousePosition.x - this.m_lastMouseX;
        //                this.m_lastMouseX = float.MaxValue;
        //            }
        //        }
        //    }

        //    if (this.isDragging)
        //    {

        //        float num = 0;
        //        if (this.m_lastMouseX != float.MaxValue)
        //        {
        //            num = Input.mousePosition.x - this.m_lastMouseX;
        //        }
        //        this.m_lastMouseX = Input.mousePosition.x;


        //        Vector3 cube_localEulerAngles = m_Cube.transform.localEulerAngles;
        //        m_Cube.transform.localEulerAngles = new Vector3(cube_localEulerAngles.x, cube_localEulerAngles.y -= num * m_speed, cube_localEulerAngles.z);



        //    }
        //}
        //else if (Input.touchCount > 1)
        //{
        //    isBoth = true;

        //    this.m_isMoving = false;
        //    this.isDragging = false;


        //    if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
        //    {
        //        Vector2 tempPosition1 = Input.GetTouch(0).position;
        //        Vector2 tempPosition2 = Input.GetTouch(1).position;

        //        if (IsEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
        //        {
        //            if (isRange)
        //            {
        //                distance += 0;
        //                moveSpeed = 0;
        //                flag = true;
        //                isRange = false;
        //            }
        //            else
        //            {
        //                if (distance < 12)
        //                {
        //                    distance += 0.1f;
        //                    moveSpeed = 1;
        //                    flag = true;
        //                    //Debug.Log("zoom+");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (isRange)
        //            {
        //                distance += 0;
        //                moveSpeed = 0;
        //                flag = true;
        //                isRange = false;
        //            }
        //            else
        //            {
        //                if (distance > 1)
        //                {
        //                    distance -= 0.1f;
        //                    moveSpeed = -1;
        //                    flag = true;
        //                    //Debug.Log("zoom-");
        //                }
        //            }
        //        }

        //        if (IsMove(Input.GetTouch(0).deltaPosition, Input.GetTouch(1).deltaPosition))
        //        {
        //            move = true;
        //            moveV2 = (Input.GetTouch(0).deltaPosition + Input.GetTouch(1).deltaPosition) / 2;
        //        }

        //        oldPosition1 = tempPosition1;
        //        oldPosition2 = tempPosition2;
        //    }
        //}
    }
    //void LateUpdate()
    //{

    //    if (flag)
    //    {
    //        Vector3 body = bg.transform.localScale;
    //        bg.transform.localScale += new Vector3(body.x * Time.deltaTime * moveSpeed, body.y * Time.deltaTime * moveSpeed, 0);
    //        flag = false;
    //    }


    //    if (move)
    //    {
    //        if (Mathf.Abs(bg.transform.localPosition.x) < 450)
    //        {
    //            bg.transform.Translate(new Vector3(moveV2.x * transSpeed, 0, 0f), Space.Self);
    //        }
    //        else
    //        {
    //            if (bg.transform.localPosition.x < -450)
    //            {
    //                bg.transform.localPosition = new Vector3(-450, bg.transform.localPosition.y, bg.transform.localPosition.z);
    //            }
    //            else if (bg.transform.localPosition.x > 450)
    //            {
    //                bg.transform.localPosition = new Vector3(450, bg.transform.localPosition.y, bg.transform.localPosition.z);
    //            }
    //        }


    //        if (Mathf.Abs(bg.transform.localPosition.y) < 250)
    //        {
    //            bg.transform.Translate(new Vector3(0, moveV2.y * transSpeed, 0f), Space.Self);
    //        }
    //        else
    //        {
    //            if (bg.transform.localPosition.y < -250)
    //            {
    //                bg.transform.localPosition = new Vector3(bg.transform.localPosition.x, -250, bg.transform.localPosition.z);
    //            }
    //            else if (bg.transform.localPosition.y > 250)
    //            {
    //                bg.transform.localPosition = new Vector3(bg.transform.localPosition.x, 250, bg.transform.localPosition.z);
    //            }
    //        }

    //        move = false;
    //    }
    //}


    public float moveSpeed = 1;//物体移动速度
    public float transSpeed = 1;//物体移动速度

    //public GameObject bg;

    private Vector2 oldPosition;
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;

    private Vector2 moveV2;

    private float distance = 5;
    private bool flag = false;
    private bool move = false;
    private bool isBoth = false;
    private bool isRange = false;
    bool IsEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //old distance
        float oldDistance = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        //new distance
        float newDistance = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));

        Debug.Log("newDistance - oldDistance:" + (newDistance - oldDistance));

        //if ((newDistance - oldDistance) < 0.5f)
        //{
        //    return false;
        //}
        //else
        //{
        if (oldDistance < newDistance)
        {
            //zoom+
            if ((newDistance - oldDistance) < 0.5f)
            {
                isRange = true;
            }
            return true;
        }
        else
        {
            //zoom-
            if ((newDistance - oldDistance) > -0.5f)
            {
                isRange = true;
            }
            return false;
        }
        //}

    }

    bool IsMove(Vector2 oP1, Vector2 oP2)
    {
        if (oP1 != oP2)
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



    public void PaintingModuleFrameControlStart()
    {
        coll.enabled = true;
        m_isRotate = true;
    }

    public void PaintingModuleFrameControlStop()
    {
        coll.enabled = false;
        m_isRotate = false;
    }

    public void PaintingModuleDistanceInitial()
    {
        //frameTexture.transform.localPosition = Vector3.zero;
        frameTexture.transform.localPosition = new Vector3(0, -50, 0);
        frameTexture.transform.localScale = Vector3.one;
        distance = 5;
    }

    public void PaintingModuleLocalPositionInitial()
    {
        //frameTexture.transform.localPosition = Vector3.zero;
        frameTexture.transform.localPosition = new Vector3(0, -50, 0);
    }

    public void PaintingModuleIntroduceTextureInitial()
    {
        introduceTexture.transform.localPosition = Vector3.zero;
    }


    private TapGestureRecognizer doubleTapGesture;
    private SwipeGestureRecognizer swipeGesture;
    private PanGestureRecognizer panGesture;
    private ScaleGestureRecognizer scaleGesture;
    //private RotateGestureRecognizer rotateGesture;
    private float nextAsteroid = float.MinValue;
    private GameObject draggingAsteroid;

    private readonly List<Vector3> swipeLines = new List<Vector3>();

    private void HandleSwipe(float endX, float endY)
    {
        Vector2 start = new Vector2(swipeGesture.StartFocusX, swipeGesture.StartFocusY);
        Vector3 startWorld = Camera.main.ScreenToWorldPoint(start);
        Vector3 endWorld = Camera.main.ScreenToWorldPoint(new Vector2(endX, endY));
        float distance = Vector3.Distance(startWorld, endWorld);
        startWorld.z = endWorld.z = 0.0f;

        swipeLines.Add(startWorld);
        swipeLines.Add(endWorld);

        if (swipeLines.Count > 4)
        {
            swipeLines.RemoveRange(0, swipeLines.Count - 4);
        }

        RaycastHit2D[] collisions = Physics2D.CircleCastAll(startWorld, 10.0f, (endWorld - startWorld).normalized, distance);

        if (collisions.Length != 0)
        {
            Debug.Log("Raycast hits: " + collisions.Length + ", start: " + startWorld + ", end: " + endWorld + ", distance: " + distance);

            Vector3 origin = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(swipeGesture.VelocityX, swipeGesture.VelocityY, Camera.main.nearClipPlane));
            Vector3 velocity = (end - origin);
            Vector2 force = velocity * 500.0f;

            foreach (RaycastHit2D h in collisions)
            {
                h.rigidbody.AddForceAtPosition(force, h.point);
            }
        }
    }

    private void CreateDoubleTapGesture()
    {
        doubleTapGesture = new TapGestureRecognizer();
        doubleTapGesture.NumberOfTapsRequired = 2;
        FingersScript.Instance.AddGesture(doubleTapGesture);
    }

    private void SwipeGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            HandleSwipe(gesture.FocusX, gesture.FocusY);
        }
    }

    private void CreateSwipeGesture()
    {
        swipeGesture = new SwipeGestureRecognizer();
        swipeGesture.Direction = SwipeGestureRecognizerDirection.Any;
        swipeGesture.StateUpdated += SwipeGestureCallback;
        swipeGesture.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
        FingersScript.Instance.AddGesture(swipeGesture);
    }

    private void PanGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {


            float deltaX = panGesture.DeltaX;
            float deltaY = panGesture.DeltaY;
            if (isFrameFingers)
            {
                Vector3 pos = frameTexture.transform.localPosition;
                if ((pos.x + deltaX) > 900)
                {
                    pos.x = 900;
                }
                else if ((pos.x + deltaX) < -900)
                {
                    pos.x = -900;
                }
                else
                {
                    pos.x += deltaX;
                }

                if ((pos.y + deltaY) > 500)
                {
                    pos.y = 500;
                }
                else if ((pos.y + deltaY) < -500)
                {
                    pos.y = -500;
                }
                else
                {
                    pos.y += deltaY;
                }

                frameTexture.transform.localPosition = pos;
            }
            else
            {
                Vector3 pos = introduceTexture.transform.localPosition;
                if ((pos.x + deltaX) > 900)
                {
                    pos.x = 900;
                }
                else if ((pos.x + deltaX) < -900)
                {
                    pos.x = -900;
                }
                else
                {
                    pos.x += deltaX;
                }

                if ((pos.y + deltaY) > 500)
                {
                    pos.y = 500;
                }
                else if ((pos.y + deltaY) < -500)
                {
                    pos.y = -500;
                }
                else
                {
                    pos.y += deltaY;
                }

                introduceTexture.transform.localPosition = pos;
            }
        }
    }

    private void CreatePanGesture()
    {
        panGesture = new PanGestureRecognizer();
        panGesture.MinimumNumberOfTouchesToTrack = 2;
        panGesture.StateUpdated += PanGestureCallback;
        FingersScript.Instance.AddGesture(panGesture);
    }

    private void ScaleGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            if (isFrameFingers)
            {
                if ((frameTexture.transform.localScale.x * scaleGesture.ScaleMultiplier) > 3)
                {
                    return;
                    //frameTexture.transform.localScale = new Vector3(3, 3, 3);
                }

                if ((frameTexture.transform.localScale.x * scaleGesture.ScaleMultiplier) < 0.2f)
                {
                    return;
                }

                frameTexture.transform.localScale *= scaleGesture.ScaleMultiplier;
            }
            else
            {
                if ((introduceTexture.transform.localScale.x * scaleGesture.ScaleMultiplier) > 3)
                {
                    return;
                    //frameTexture.transform.localScale = new Vector3(3, 3, 3);
                }

                if ((introduceTexture.transform.localScale.x * scaleGesture.ScaleMultiplier) < 0.2f)
                {
                    return;
                }

                introduceTexture.transform.localScale *= scaleGesture.ScaleMultiplier;
            }

        }
    }

    private void CreateScaleGesture()
    {
        scaleGesture = new ScaleGestureRecognizer();
        scaleGesture.StateUpdated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(scaleGesture);
    }

    private static bool? CaptureGestureHandler(GameObject obj)
    {
        // I've named objects PassThrough* if the gesture should pass through and NoPass* if the gesture should be gobbled up, everything else gets default behavior
        if (obj.name.StartsWith("PassThrough"))
        {
            // allow the pass through for any element named "PassThrough*"
            return false;
        }
        else if (obj.name.StartsWith("NoPass"))
        {
            // prevent the gesture from passing through, this is done on some of the buttons and the bottom text so that only
            // the triple tap gesture can tap on it
            return true;
        }

        // fall-back to default behavior for anything else
        return null;
    }


    //private void RotateGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
    //{
    //    Vector3 cube_localEulerAngles = m_Cube.transform.localEulerAngles;
    //    m_Cube.transform.localEulerAngles = new Vector3(0, cube_localEulerAngles.y+= scaleGesture.ScaleMultiplierY, 0);
    //}


    //private void CreateRotateGesture()
    //{
    //    rotateGesture = new RotateGestureRecognizer();
    //    rotateGesture.StateUpdated += RotateGestureCallback;
    //    FingersScript.Instance.AddGesture(rotateGesture);
    //}

    private void Start()
    {

        // don't reorder the creation of these :)

        CreateDoubleTapGesture();
        CreateSwipeGesture();
        CreatePanGesture();
        CreateScaleGesture();
        //CreateRotateGesture();

        // pan, scale and rotate can all happen simultaneously
        panGesture.AllowSimultaneousExecution(scaleGesture);


        // prevent the one special no-pass button from passing through,
        //  even though the parent scroll view allows pass through (see FingerScript.PassThroughObjects)
        FingersScript.Instance.CaptureGestureHandler = CaptureGestureHandler;


        FingersScript.Instance.enabled = false;

        // show touches, only do this for debugging as it can interfere with other canvases
        //FingersScript.Instance.ShowTouches = true;
    }

    private void LateUpdate()
    {
        if (!m_isRotate)
            return;

        //if (FingersScript.Instance.MousePresent)
        //{
        //    if (FingersScript.Instance.TouchCount > 0 && FingersScript.Instance.TouchCount < 1)
        //    {
        //        Debug.Log("MousePresent:y=" + (FingersScript.Instance.MousePosition - offset).x);
        //        Vector3 cube_localEulerAngles = m_Cube.transform.localEulerAngles;
        //        m_Cube.transform.localEulerAngles = new Vector3(0, (FingersScript.Instance.MousePosition - offset).x, 0);
        //    }
        //    //    if (FingersScript.Instance.IsMouseDownThisFrame(0))
        //    //{
        //    //    CheckOffset(FingersScript.Instance.MousePosition);
        //    //}
        //    //else if (FingersScript.Instance.IsMouseDown(0))
        //    //{
        //    //    Debug.Log("MousePresent:y=" + (FingersScript.Instance.MousePosition - offset).x);
        //    //    Vector3 cube_localEulerAngles = m_Cube.transform.localEulerAngles;
        //    //    m_Cube.transform.localEulerAngles = new Vector3(0, (FingersScript.Instance.MousePosition - offset).x, 0);
        //    //    //transform.position = FingersScript.Instance.MousePosition - offset;
        //    //}
        //    //else
        //    //{
        //    //    offset = new Vector2(transform.localEulerAngles.x,0);
        //    //}
        //}
        //else if (FingersScript.Instance.TouchCount > 0)
        //{
        //    Debug.Log("AAAA:A");
        //    DigitalRubyShared.GestureTouch touch = FingersScript.Instance.GetTouch(0);
        //    Vector2 pos = new Vector2(touch.X, touch.Y);
        //    if (touch.TouchPhase == DigitalRubyShared.TouchPhase.Began)
        //    {
        //        CheckOffset(pos);
        //    }
        //    Vector3 cube_localEulerAngles = m_Cube.transform.localEulerAngles;
        //    m_Cube.transform.localEulerAngles = new Vector3(cube_localEulerAngles.x, (pos - offset).x, cube_localEulerAngles.z);

        //    //transform.position = pos - offset;
        //}
        //else
        //{
        //    Debug.Log("AAAA:B");
        //    offset = new Vector2(transform.localEulerAngles.x, 0);
        //}

        if (Time.timeSinceLevelLoad > nextAsteroid)
        {
            nextAsteroid = Time.timeSinceLevelLoad + UnityEngine.Random.Range(1.0f, 4.0f);
        }

        int touchCount = FingersScript.Instance.TouchCount;
        if (FingersScript.Instance.TreatMousePointerAsFinger && FingersScript.Instance.MousePresent)
        {
            touchCount += (FingersScript.Instance.IsMouseDown(0) ? 1 : 0);
            touchCount += (FingersScript.Instance.IsMouseDown(1) ? 1 : 0);
            touchCount += (FingersScript.Instance.IsMouseDown(2) ? 1 : 0);
        }
        string touchIds = string.Empty;
        int gestureTouchCount = 0;
        foreach (DigitalRubyShared.GestureRecognizer g in FingersScript.Instance.Gestures)
        {
            gestureTouchCount += g.CurrentTrackedTouches.Count;
        }
        foreach (GestureTouch t in FingersScript.Instance.CurrentTouches)
        {
            touchIds += ":" + t.Id + ":";
        }

    }
}
