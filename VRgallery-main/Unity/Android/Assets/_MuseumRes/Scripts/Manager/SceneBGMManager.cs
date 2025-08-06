using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneBGMManager : UnitySingleton<SceneBGMManager>
{
    public AudioSource _audio;

    public bool isMute = false;

    public void Awake()
    {
        _audio = this.GetComponent<AudioSource>();
    }

    public void GetSceneBGM(string url)
    {
        if (PlayerPrefs.HasKey("BGMMute"))
        {
            if (PlayerPrefs.GetString("BGMMute") == "True")
            {
                _audio.mute = true;
                isMute = true;
            }
            else
            {
                _audio.mute = false;
                isMute = false;
            }
        }
        else
        {
            _audio.mute = false;
            isMute = false;
        }
        if (url != " ")
            StartCoroutine(GetAudioClip(url));
    }

    IEnumerator GetAudioClip(string path)
    {
        path = "file://" + path;
        //（目录如果为Application.persistentDataPath + "/1" 下必须添加“file://”，这里可以写个宏）
        using (var uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError)
            {
                Debug.LogError("uwrERROR:" + uwr.error
                               );
            }
            else
            {
                _audio.clip = DownloadHandlerAudioClip.GetContent(uwr);
            }
        }

        GetLoadingProgress.Instance.isFake = true;
        GetLoadingProgress.Instance.currentAlreadyIndex += 1;



        //_audio.Play();
    }

    public void PlaySceneBGM()
    {
        _audio.Play();
    }

    public void PlayIntroduceSceneBGM()
    {
        if (!_audio.isPlaying)
            _audio.Play();
    }

    public void PauseSceneBGM()
    {
        _audio.Pause();
    }

    public void UnPauseSceneBGM()
    {
        _audio.UnPause();
    }

    public void MuteSceneBGM()
    {
        if (isMute)
        {
            _audio.mute = false;
        }
        else
        {
            _audio.mute = true;
        }

        isMute = !isMute;
    }
}
