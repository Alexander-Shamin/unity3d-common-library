using System.IO;
using UnityEngine;

namespace Common
{
/// <summary>
/// Упрощение загрузки файлов (не через Unity)
/// </summary>
public class FileLoader
{
  /// <summary>
  /// Из-за разного расположения файлов - необходим простой инструмент
  /// для их загрузки.
  /// В Editor path рассчитвается от Asset
  /// В Release от exe
  /// </summary>
  public static string DataPath()
  {
#if UNITY_EDITOR
    return Application.dataPath;
#else
    return Path.GetFullPath(".");
#endif
  }

  public static string GetUrlFilePrefix() { return "file://"; }

  /// <summary>
  /// Расширение DataPath для формирования пути к файлу
  /// </summary>
  /// <param name="path"> Путь к файлу от папки Assets без начального символа
  /// разделителя пути </param>
  /// <returns></returns>
  public static string DataPath(string path)
  {
    return DataPath() + Path.DirectorySeparatorChar + path;
  }

  /// <summary>
  /// Загрузить Sprite из файла
  /// </summary>
  /// <param name="path">Путь к файлу от папки Assets</param>
  /// <returns></returns>
  public static Sprite LoadSprite(string path)
  {
    Sprite sprite = null;
    Texture2D tex = LoadTextureFromFile(path);

    if (tex != null)
    {
      sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }

    return sprite;
  }

  /// <summary>
  /// Загрузка Texture2D из файла
  /// </summary>
  /// <param name="path">Путь к файлу от папки Assets</param>
  public static Texture2D LoadTextureFromFile(string path)
  {
    Texture2D tex = null;
    string absPath = DataPath(path);

    if (File.Exists(absPath))
    {
      byte[] fileData = File.ReadAllBytes(absPath);

      // LoadImage will auto-resize the texture dimensions
      tex = new Texture2D(2, 2);
      tex.LoadImage(fileData);
    }
    else
    {
      Debug.LogError($"File not found at path = {absPath}.");
    }

    return tex;
  }

  /// <summary>
  /// Конвертация текстуры типа Texture к Texture2D
  /// </summary>
  /// <param name="texture">Текстура</param>
  public static Texture2D TextureToTexture2D(Texture texture)
  {
    Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
    RenderTexture currentRenderTexture = RenderTexture.active;
    RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 24);
    Graphics.Blit(texture, renderTexture);

    RenderTexture.active = renderTexture;
    texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
    texture2D.Apply();

    RenderTexture.active = currentRenderTexture;
    RenderTexture.ReleaseTemporary(renderTexture);
    return texture2D;
  }

}  // class
}
