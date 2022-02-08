using UnityEngine.UI;

namespace Common
{
/// <summary>
/// Функции учета отличий графических API, влияющих на рендеринг
/// </summary>
public class GraphicsDifference
{
  /// <summary>
  /// Учет платформозависимого рендера
  /// </summary>
  /// <param name="isFlip">Перевернуть изображение</param>
  /// <param name="rawImage">Изображение для обработки</param>
  public static void CheckPlatformSpecificRenderingDifferences(bool isFlip, RawImage rawImage)
  {
    if (isFlip)
    {
      var uvRect = rawImage.uvRect;
      uvRect.height = -1.0f;
      uvRect.y = 1.0f;
      rawImage.uvRect = uvRect;
    }
    else
    {
      var uvRect = rawImage.uvRect;
      uvRect.height = 1.0f;
      uvRect.y = 0.0f;
      rawImage.uvRect = uvRect;
    }
  }

  /// <summary>
  /// Учет платформозависимого рендера
  /// </summary>
  /// <param name="isFlip">Перевернуть изображение</param>
  /// <param name="isReflect">Отразить</param>
  /// <param name="rawImage">Изображение для обработки</param>
  public static void CheckPlatformSpecificRenderingDifferences(bool isFlip, bool isReflect,
                                                               RawImage rawImage)
  {
    if (isFlip)
    {
      var uvRect = rawImage.uvRect;
      uvRect.height = -1.0f;
      uvRect.y = 1.0f;
      rawImage.uvRect = uvRect;
    }
    else
    {
      var uvRect = rawImage.uvRect;
      uvRect.height = 1.0f;
      uvRect.y = 0.0f;
      rawImage.uvRect = uvRect;
    }

    if (isReflect)
    {
      var uvRect = rawImage.uvRect;
      uvRect.width = -1.0f;
      rawImage.uvRect = uvRect;
    }
    else
    {
      var uvRect = rawImage.uvRect;
      uvRect.width = 1.0f;
      rawImage.uvRect = uvRect;
    }
  }
}
}