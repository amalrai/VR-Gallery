using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;


public class TVControl : MonoBehaviour
{
    private MediaPlayer m_MediaPlayer;
    private GameObject m_MediaPlayerInspector;
    private GameObject m_EasyTouchControlsCanvas;
    public Slider _videoSeekSlider;

    private void Awake()
    {
        m_MediaPlayer = this.GetComponentInChildren<MediaPlayer>();
        m_MediaPlayerInspector = GameObject.Find("TVUICanvas");
        m_EasyTouchControlsCanvas = GameObject.Find("EasyTouchControlsCanvas");

        m_MediaPlayerInspector.SetActive(false);
    }

    public void MediaPlayerFirstOpenPlay(string videoName)
    {
        m_MediaPlayer.m_VideoPath = System.IO.Path.Combine("AVProVideoSamples/", videoName);
        m_MediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, m_MediaPlayer.m_VideoPath);

        m_MediaPlayerInspector.SetActive(true);
        m_EasyTouchControlsCanvas.SetActive(false);
       // _videoSeekSlider.value = 0;
        m_MediaPlayer.Play();
    }

    public void MediaPlayerExit()
    {
        m_MediaPlayer.Rewind(true);
        m_MediaPlayerInspector.SetActive(false);
        m_EasyTouchControlsCanvas.SetActive(true);
    }
}
