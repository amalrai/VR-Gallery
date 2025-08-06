using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskManager : UnitySingleton<MaskManager>
{
    public Image image;

    public bool isStart = false;

    public float colorA = 1;

    public void Start()
    {

    }

    public void StartMask()
    {
        StartCoroutine(wait());
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);

        isStart = true;
    }

    public void Update()
    {
        if (isStart)
        {
            if (colorA <= 0)
            {
                Destroy(this.gameObject);
            }

            colorA -= (Time.deltaTime * 0.5f);

            image.color = new Color(0, 0, 0, colorA);
        }
    }
}
