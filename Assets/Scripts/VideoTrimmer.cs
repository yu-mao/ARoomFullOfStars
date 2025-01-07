using UnityEngine;
using UnityEngine.Video;

public class VideoTrimmer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private float startTime;
    [SerializeField] private float endTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.time = startTime;
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.time >= endTime) videoPlayer.time = startTime;
    }
}
