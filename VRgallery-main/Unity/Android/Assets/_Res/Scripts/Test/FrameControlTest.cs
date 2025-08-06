using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameControlTest : MonoBehaviour
{
    public Transform m_LookAtMainCamera;
    private Transform m_MainCamera;
    void Start()
    {

        m_MainCamera = GameObject.Find("Main Camera").transform;
    }

    private void Update()
    {
        Transform lookatDir = m_LookAtMainCamera;
        lookatDir.LookAt(m_MainCamera);
        m_LookAtMainCamera.eulerAngles = new Vector3(lookatDir.eulerAngles.x, m_LookAtMainCamera.eulerAngles.y, m_LookAtMainCamera.eulerAngles.z);

    }
}
