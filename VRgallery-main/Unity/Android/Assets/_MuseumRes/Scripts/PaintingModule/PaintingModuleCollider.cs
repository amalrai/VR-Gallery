using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingModuleCollider : MonoBehaviour
{
    private PaintingModule module;

    private void Awake()
    {
        module = this.GetComponentInParent<PaintingModule>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (module.colliderEnterEvent != null)
            {
                Debug.Log("开始碰撞器事件执行");
                module.colliderEnterEvent();
            }
            else
            {
                Debug.Log("开始碰撞器没有事件添加");
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (module.colliderEnterEvent != null)
            {
                Debug.Log("结束碰撞器事件执行");
                module.colliderExitEvent();
            }
            else
            {
                Debug.Log("结束碰撞器没有事件添加");
            }
        }
    }

}
