using UnityEngine;
using System.Collections;

namespace SM
{
// An FPS counter.
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
public class FPS : MonoBehaviour
{
  public float updateInterval = 0.5F;
  private double lastInterval;
  private int frames = 0;
  [SerializeField]
  private float fps;
  public bool _enable = false;
  public void Start()
  {
    lastInterval = Time.realtimeSinceStartup;
    frames = 0;
  }

  public void OnGUI()
  {
    if (_enable) GUILayout.Label("" + fps.ToString("f2"));
  }

  public void Update()
  {
    ++frames;
    float timeNow = Time.realtimeSinceStartup;
    if (timeNow > lastInterval + updateInterval)
    {
      fps = (float)(frames / (timeNow - lastInterval));
      frames = 0;
      lastInterval = timeNow;
    }
  }
}
}