using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerManager : MonoBehaviour
{
    [SerializeField] GameObject tvPanel, menuPanel, videoPlayerList, mainVideoPlayerScreen;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] VideoClip[] videos;
    [SerializeField] GameObject videoThumbnailPrefab;
    [SerializeField] Transform thumbnailParent;
    [SerializeField] Slider videoSeekBar;
    [SerializeField] Image playPauseImg, powerOnOffImg;
    [SerializeField] Sprite playSprite, pauseSprite, powerOnSprite, powerOffSprite;
    [SerializeField] TMP_Text playingVideoName;
    [SerializeField] GameObject headerPlayer, footerPlayer;

    int currPlayingVideoIndex = 0;
    long videoPlayerFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < videos.Length; i++)
        {
            GameObject videoTile = Instantiate(videoThumbnailPrefab);
            videoTile.transform.SetParent(thumbnailParent);
            videoTile.GetComponentInChildren<TMP_Text>().text = videos[i].name;
            videoTile.transform.localPosition = Vector3.zero;
            videoTile.transform.localScale = videoThumbnailPrefab.transform.localScale;
            videoTile.transform.localEulerAngles = videoThumbnailPrefab.transform.localEulerAngles;
            videoTile.name = i.ToString(); ;
            videoTile.GetComponent<Button>().onClick.AddListener(() => { PlayVideo(int.Parse(videoTile.name)); });
        }
    }

    private void LateUpdate()
    {
        videoSeekBar.value = videoPlayer.frame;
    }

    public void ToggleTV()
    {
        if (tvPanel.activeSelf)
        {
            tvPanel.SetActive(false);
            powerOnOffImg.sprite = powerOnSprite;
            videoPlayer.Stop();
        }
        else
        {
            tvPanel.SetActive(true);
            powerOnOffImg.sprite = powerOffSprite;
        }
    }

    public void PlayVideo(int videoIndex)
    {
        videoPlayerList.SetActive(false);
        currPlayingVideoIndex = videoIndex;
        playingVideoName.text = videos[currPlayingVideoIndex].name;
        videoPlayer.Stop();
        videoPlayer.clip = videos[videoIndex];
        videoPlayer.Play();
        SetVideoFrame();
        mainVideoPlayerScreen.SetActive(true);
    }

    public void PlayNext()
    {
        if(currPlayingVideoIndex == (videos.Length - 1))
        {
            currPlayingVideoIndex = 0;
        }
        else
        {
            currPlayingVideoIndex++;
        }
        videoPlayer.Stop();
        videoPlayer.clip = videos[currPlayingVideoIndex];
        playingVideoName.text = videos[currPlayingVideoIndex].name;
        videoPlayer.Play();
        SetVideoFrame();
    }

    public void PlayPrevious()
    {
        if (currPlayingVideoIndex == 0)
        {
            currPlayingVideoIndex = videos.Length - 1;
        }
        else
        {
            currPlayingVideoIndex--;
        }
        videoPlayer.Stop();
        videoPlayer.clip = videos[currPlayingVideoIndex];
        playingVideoName.text = videos[currPlayingVideoIndex].name;
        videoPlayer.Play();
        SetVideoFrame();
    }

    public void VideoPlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            playPauseImg.sprite = playSprite;
        }
        else
        {
            videoPlayer.Play();
            playPauseImg.sprite = pauseSprite;
        }
    }

    public void SeekVideo()
    {
        videoPlayer.frame = (long)videoSeekBar.value;
    }

    void SetVideoFrame()
    {
        videoPlayer.Pause();
        videoPlayerFrames = (int)videoPlayer.frameCount;
        videoSeekBar.minValue = 0;
        videoSeekBar.maxValue = videoPlayerFrames;
        videoPlayer.Play();
    }

    public void TogglePlayerControls()
    {
        if (headerPlayer.activeSelf)
        {
            headerPlayer.SetActive(false);
            footerPlayer.SetActive(false);
        }
        else
        {
            headerPlayer.SetActive(true);
            footerPlayer.SetActive(true);
        }

    }
}
