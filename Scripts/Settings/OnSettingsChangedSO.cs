using System;
using UnityEngine;

namespace Common
{
/// <summary>
/// Класс для реализация события из Unity Editor
/// </summary>
public abstract class OnSettingsChangedSO : ScriptableObject
{
  public abstract void InvokeOnSettingsChanged();
}  // class

}  // namespace