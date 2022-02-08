using UnityEngine;

namespace Common
{
public static class TransformExtension
{
  public static void SetLayersRecursively(this Transform transform, LayerMask layer)
  {
    transform.gameObject.layer = layer;
    foreach (Transform child in transform)
    {
      child.SetLayersRecursively(layer);
    }
  }
}
}  // namespace
