using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace SM
{
public class ProgressBar : MonoBehaviour
{
  public Image FilledImage = null;
  public Text TextOnFilledImage = null;
  public float Delay = 10.0f;

  public UnityEvent OnFilled;

  private float _time = 0.0f;

  private void Start()
  {
    const string prefix = "ProgressBar :: Start :: ";
    Assert.IsNotNull(FilledImage, prefix + "Not found FilledImage object");
    Assert.IsNotNull(TextOnFilledImage, prefix + "Not found TextuOnFilledImage object");

    StartCoroutine(FilledProgressBar());
  }

  private IEnumerator FilledProgressBar()
  {
    FilledImage.fillAmount = 0;
    TextOnFilledImage.text = "0";
    _time = 0.0f;

    float delta = 0.1f;
    while (_time < Delay)
    {
      _time += delta;
      float perсent = Mathf.Clamp(_time / Delay, 0.0f, 1.0f);
      FilledImage.fillAmount = perсent;
      TextOnFilledImage.text = (perсent * 100.0f).ToString("0");

      yield return new WaitForSeconds(delta);
    }
    OnFilled?.Invoke();
    yield break;
  }
}  // class
}  // namespace
