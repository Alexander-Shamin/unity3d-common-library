using System.Reflection;
using System.Text;
using UnityEngine;

namespace SM
{
public class _
{
  public static string LogMatrix(string name, Matrix4x4 mat)
  {
    StringBuilder str = new StringBuilder(500);
    str.Append(name);
    str.Append("\n");

    str.Append(mat.m00);
    str.Append("\t");
    str.Append(mat.m01);
    str.Append("\t");
    str.Append(mat.m02);
    str.Append("\t");
    str.Append(mat.m03);
    str.Append("\n");

    str.Append(mat.m10);
    str.Append("\t");
    str.Append(mat.m11);
    str.Append("\t");
    str.Append(mat.m12);
    str.Append("\t");
    str.Append(mat.m13);
    str.Append("\n");

    str.Append(mat.m20);
    str.Append("\t");
    str.Append(mat.m21);
    str.Append("\t");
    str.Append(mat.m22);
    str.Append("\t");
    str.Append(mat.m23);
    str.Append("\n");

    str.Append(mat.m30);
    str.Append("\t");
    str.Append(mat.m31);
    str.Append("\t");
    str.Append(mat.m32);
    str.Append("\t");
    str.Append(mat.m33);
    str.Append("\n");

    return str.ToString();
  }

  public static void DebugErrorException(System.Exception exc, string str = "Exception! ")
  {
    Debug.LogError(str + exc.Message + exc.StackTrace);
  }

  static public void LogError(string message, MethodBase mb)
  {
    Debug.LogError($"{mb.Name} :: {mb.DeclaringType} :: {message}");
  }
  static public void LogError(string message) { Debug.LogError($"{message}"); }

  static public void Log(string message, MethodBase mb)
  {
    Debug.Log($"{mb.Name} :: {mb.DeclaringType} :: {message}");
  }
  static public void Log(string message) { Debug.Log($"{message}"); }

  static public void LogWarning(string message, MethodBase mb)
  {
    Debug.LogWarning($"{mb.Name} :: {mb.DeclaringType} :: {message}");
  }

  static public void LogWarning(string message) { Debug.LogWarning($"{message}"); }

  static public void LogException(System.Exception exception, MethodBase mb)
  {
    Debug.LogError(
      $"{mb.Name} :: {mb.DeclaringType} :: Exception \n {exception.Message} \n {exception.StackTrace}");
  }

  static public void LogException(System.Exception exception)
  {
    Debug.LogError($"Exception \n {exception.Message} \n {exception.StackTrace}");
  }

  /// <summary>
  /// if object == null LogError(message, mb) => false
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="message"></param>
  /// <param name="mb"></param>
  static public bool CheckNull(object obj, string message, MethodBase mb)
  {
    if (obj == null)
    {
      _.LogError(message, mb);
      return true;
    }
    return false;
  }

  /// <summary>
  /// if object == null LogError(message)
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="message"></param>
  static public bool CheckNull(object obj, string message)
  {
    if (obj == null)
    {
      _.LogError(message);
      return true;
    }
    return false;
  }

  /// <summary>
  /// if object == null Debug.LogWarning(message, context);
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="message"></param>
  /// <param name="context"></param>
  static public bool CheckNull(object obj, string message, Object context)
  {
    if (obj == null)
    {
      Debug.LogWarning(message, context);
      return true;
    }
    return false;
  }
}
}
