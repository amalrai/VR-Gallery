using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yoyohan;
using UnityEngine.UI;

public class GetLoadingProgress : UnitySingleton<GetLoadingProgress>
{
    public Text loadingProgressText;
    public Image loadingProgress;


    public void Awake()
    {
        DownloadProgressIntital();
    }

    public void DownloadProgressIntital()
    {
        loadingProgressText.text = "LOADING... 0%";
        loadingProgress.fillAmount = 0;
        fakeTimer = 0;
    }

    public bool isFake = false;
    public float fakeTimer = 0;
    public float currentLoadIndex = 1;
    public float totalLoadIndex = 6;
    public float currentAlreadyIndex = 0;

    public void Update()
    {
        if (isFake)
        {
            loadingProgress.fillAmount = fakeTimer;
            if (fakeTimer >= currentLoadIndex / totalLoadIndex)
            {
                if (currentLoadIndex / totalLoadIndex >= 1)
                {
                    isFake = false;
                    loadingProgressText.text = "LOADING... 100%";
                    loadingProgress.fillAmount = 1;
                    SceneBGMManager.Instance.PlaySceneBGM();

                    if (PlayerPrefs.GetString("SelectPM") == "Settsu")
                    {
                        PaintingModuleMap.Instance.PaintingModuleSceneModeEvent();
                    }


                    MaskManager.Instance.StartMask();
                    Destroy(this.gameObject);
                }
                else
                {
                    isFake = false;
                    if (currentLoadIndex < currentAlreadyIndex)
                    {
                        currentLoadIndex += 1;
                        isFake = true;
                    }
                }
            }
            else
            {
                fakeTimer += Time.deltaTime * 0.3f;
                if (fakeTimer >= 1f)
                {
                    loadingProgressText.text = "LOADING... 100%";
                }
                else
                {
                    loadingProgressText.text = "LOADING... " + (fakeTimer * 100).ToString("f1") + "%";
                }
            }
        }
    }
}
