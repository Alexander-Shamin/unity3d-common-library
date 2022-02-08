using UnityEngine;
using UnityEngine.Assertions;
namespace Common
{
public class AbstractContainerSO<T> : ScriptableObject
  where T : class
{
  public delegate void ContainerUpdate(T previousHolder, T newHolder);

  [SerializeField, Required]
  private T holder;

  private T prevHolder;
  public T _
  {
    get {
      return holder;
    }
    set {
      prevHolder = holder;
      holder = value;
      if (prevHolder != holder) OnContainerUpdate?.Invoke(prevHolder, holder);
    }
  }

  public event ContainerUpdate OnContainerUpdate;
  private void OnEnable()
  {
    if (holder == null) Debug.LogWarning($"Not found stored object {typeof(T)}");
    prevHolder = holder;
  }
  private void OnValidate()
  {
    if (prevHolder != holder)
    {
      OnContainerUpdate?.Invoke(prevHolder, holder);
      prevHolder = holder;
    }
  }
}
}  // namespace