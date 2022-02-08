using UnityEngine;

namespace Common
{
/// <summary>
/// Базовая стратегия
/// </summary>
[ExecuteAlways]
public class BaseStrategy : MonoBehaviour
{
  [Multiline]
  public string DeveloperDescription = "Abstract Strategy";

  /// <summary>
  /// Удаление создаваемых объектов, необходимых для стратегии
  /// </summary>
  public virtual void DestroyCreatedObjects() {}
}
}
