using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideoURL : MonoBehaviour
{
    public string url;
    VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = url;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            videoPlayer.Play();
        }
    }
}
