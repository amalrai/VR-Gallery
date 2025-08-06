using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameControl : MonoBehaviour
{
    public GameObject m_FrameUIColliderInspector;
    public GameObject m_FrameUICanvasInspector;
    public GameObject m_TouchColliderGameObject;
    private GameObject m_EasyTouchControlsCanvas;
    private DragRotate m_dr;

    public Transform m_LookAtMainCamera;
    private Transform m_MainCamera;

    private void Awake()
    {
        m_FrameUICanvasInspector.SetActive(false);
        m_TouchColliderGameObject.SetActive(false);
        m_EasyTouchControlsCanvas = GameObject.Find("EasyTouchControlsCanvas");
        m_dr = m_TouchColliderGameObject.GetComponent<DragRotate>();
        m_dr.enabled = false;
        m_MainCamera = GameObject.Find("Main Camera").transform;
    }

    public void FramePlayEvent(int index)
    {
        StartCoroutine(framePlayEvent(index));
    }

    private IEnumerator framePlayEvent(int index)
    {
        m_TouchColliderGameObject.GetComponent<DragRotate>().ReturnBackScale();
        m_dr.frameIndex = index;
        m_dr.CorrectCurrentFrame();
        yield return new WaitForSeconds(0.3f);
        m_TouchColliderGameObject.SetActive(true);
        m_EasyTouchControlsCanvas.SetActive(false);
        m_FrameUICanvasInspector.SetActive(true);
        m_FrameUIColliderInspector.SetActive(false);
        m_dr.enabled = true;
        m_dr.FirstOpenEvent();
    }

    public void FrameExitEvent()
    {
        m_TouchColliderGameObject.SetActive(false);
        m_EasyTouchControlsCanvas.SetActive(true);
        m_FrameUICanvasInspector.SetActive(false);
        m_FrameUIColliderInspector.SetActive(true);
        m_dr.FirstOpenEvent();
        m_dr.enabled = false;



    }

    private void Update()
    {
        Transform lookatDir = m_LookAtMainCamera;
        lookatDir.LookAt(m_MainCamera);
        m_LookAtMainCamera.eulerAngles = new Vector3(lookatDir.eulerAngles.x, m_LookAtMainCamera.eulerAngles.y, m_LookAtMainCamera.eulerAngles.z);

    }
}
