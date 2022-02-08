using System;
using UnityEngine;

namespace Common
{
/// <summary>
/// Абстрактный класс настроек, хранимый в виде ScriptableObject
/// </summary>
/// <remarks>
/// <para>
/// Для использования необходимо:
/// - наследоваться от данного класса;
/// - добавить необходимые значения;
/// - реализовать метод UpdateSettings(AbstractSettings s) и реализовать механизм обновления данных
/// через s
/// - добавить создание через [CreateAssetMenu(fileName = "Name", menuName =
/// "Common/Settings/Name")]
/// - создать scriptableObject через меню
/// </para>
/// <para>
/// Общая логика настроек следующая:
/// - сохранение всех настроек в отдельном хранилище (scriptableObject)
/// - простой доступ из билда и editor
/// - при старте, проект сам читает настройки из start, плюс есть возможность среагировать на
/// изменение настроек (каллбек)
/// </para>
/// </remarks>
public abstract class AbstractSettingSO<T> : OnSettingsChangedSO
{
  [SerializeField]
  protected AbstractSettingsStorageSO settings = null;

  /// <summary>
  /// Загрузка данных.
  /// </summary>
  /// <param name="s"></param>
  /// <returns>Если данные изменились - необходимо вернуть true</returns>
  protected abstract bool GetSettings(AbstractSettingsStorageSO s);
  protected abstract void SetSettings(AbstractSettingsStorageSO s);

  public event Action<T> OnSettingsChanged;
  public void RegisterListener(Action<T> handler) { OnSettingsChanged += handler; }
  public void UnregisterListener(Action<T> handler) { OnSettingsChanged -= handler; }
  protected void InvokeOnSettingsChanged(T value) { OnSettingsChanged?.Invoke(value); }

  protected void OnEnable()
  {
    if (settings != null)
    {
      settings.OnSettingsChanged += AbstractSettings_OnSettingsChanged;
      GetSettings(settings);
      Application.quitting += Save;
    }
  }

  protected void OnDisable()
  {
    if (settings != null)
    {
      settings.OnSettingsChanged -= AbstractSettings_OnSettingsChanged;
      Save();
    }
  }

  protected void AbstractSettings_OnSettingsChanged()
  {
    if (GetSettings(settings))
    {
#if UNITY_EDITOR
      UnityEditor.EditorUtility.SetDirty(this);
#endif
      InvokeOnSettingsChanged();
    }
  }

  public void Save()
  {
    if (settings != null)
    {
      SetSettings(settings);
#if UNITY_EDITOR
      UnityEditor.EditorUtility.SetDirty(this);
#endif
      settings?.Save();
    }
  }

  public void Load()
  {
    settings?.Load();

#if UNITY_EDITOR
    UnityEditor.EditorUtility.SetDirty(this);
#endif
  }

  protected void OnValidate() { Save(); }
}  // class
}  // namespace
