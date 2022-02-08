using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Video;

namespace Common
{
[RequireComponent(typeof(VideoPlayer))]
public class LogoVideoPlayer : MonoBehaviour
{
  public UnityEvent OnVideoEnd;
  private VideoPlayer _videoPlayer = null;
  [SerializeField]
  private float _minimalVideoTime = 0.5f;
  private void Start()
  {
    _videoPlayer = this.GetComponent<VideoPlayer>();
    Assert.IsNotNull(_videoPlayer, "LogoVideoPlayer :: Start :: Not found component VideoPlayer.");

    if (_videoPlayer.length < _minimalVideoTime)
    {
      OnVideoEnd?.Invoke();
    }
    _videoPlayer.playOnAwake = false;
    _videoPlayer.loopPointReached += VideoPlayer_OnVideoEnd;
    _videoPlayer.errorReceived += VideoPlayer_ErrorReceived;
    _videoPlayer.Play();
  }
  private void OnDisable()
  {
    _videoPlayer.loopPointReached -= VideoPlayer_OnVideoEnd;
    _videoPlayer.errorReceived -= VideoPlayer_ErrorReceived;
  }

  private void VideoPlayer_ErrorReceived(VideoPlayer source, string message)
  {
    OnVideoEnd?.Invoke();
  }

  private void VideoPlayer_OnVideoEnd(UnityEngine.Video.VideoPlayer source)
  {
    source.Stop();
    OnVideoEnd?.Invoke();
  }
} // class
}  // namespace
