using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Common
{
public static class Extension
{
  public static T GetOrAddComponent<T>(this GameObject m)
    where T : Component { return m.GetComponent<T>() ?? m.gameObject.AddComponent<T>(); }

  public static void Destroy(this Object obj, bool immediate = false)
  {
    if (obj != null)
    {
#if UNITY_EDITOR
      EditorApplication.delayCall += () =>
      { UnityEngine.Object.DestroyImmediate(obj); };
#else
      if (immediate)
        UnityEngine.Object.DestroyImmediate(obj);
      else
        UnityEngine.Object.Destroy(obj);
#endif
    }
  }
}  // class
}
