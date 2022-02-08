using System;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
/// <summary>
/// Абстрактный класс храннеия
/// </summary>
public abstract class AbstractSettingsStorageSO : ScriptableObject
{
  public event Action OnSettingsChanged;

  private const SettingPolicy DEFAULT_POLICY = SettingPolicy.Global;
  protected void InvokeOnSettingsChanged() { OnSettingsChanged?.Invoke(); }

  public abstract T GetValue<T>(string name, T defaultValue = default,
                                SettingPolicy scope = DEFAULT_POLICY);
  public T GetValue<T>(string name, ref T value, T defaultValue = default,
                       SettingPolicy scope = DEFAULT_POLICY)
  {
    value = GetValue<T>(name, default, scope);
    return value;
  }
  public T GetValue<T>(string name, string module, T defaultValue = default,
                       SettingPolicy scope = DEFAULT_POLICY)
  {
    return GetValue<T>(Join(name, module), default, scope);
  }
  public T GetValue<T>(string name, string module, ref T value, T defaultValue = default,
                       SettingPolicy scope = DEFAULT_POLICY)
  {
    return GetValue<T>(Join(name, module), ref value, default, scope);
  }
  public bool GetValueAndCheck<T>(string name, ref T value, T defaultValue = default,
                                  SettingPolicy scope = DEFAULT_POLICY)
  {
    T newValue = GetValue<T>(name, default, scope);
    bool changed = false;
    if (!Equals(value, newValue)) changed = true;
    value = newValue;
    return changed;
  }
  public bool GetValueAndCheck<T>(string name, string module, ref T value, T defaultValue = default,
                                  SettingPolicy scope = DEFAULT_POLICY)
  {
    return GetValueAndCheck<T>(Join(name, module), ref value, defaultValue, scope);
  }

  public abstract void SetValue<T>(string name, T value, SettingPolicy scope = DEFAULT_POLICY);
  public void SetValue<T>(string name, string module, T value, SettingPolicy scope = DEFAULT_POLICY)
  {
    SetValue<T>(Join(name, module), value, scope);
  }
  public abstract void Save();
  public abstract void Load();

  private string Join(string name, string module) { return module + "_" + name; }

}  // class
}
