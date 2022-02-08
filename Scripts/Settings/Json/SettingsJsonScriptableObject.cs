using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common
{
/// <summary>
///	Класс реализующий хранение данных в Json файлах (default name: local_settings.json (локальное),
/// settings.json (глобальное))
///   что позволяет реализовать контекст переключения операций
/// </summary>
/// <remarks>
/// <para>
/// Класс реализован в виде ScriptableObject и независим от сцены использования.
/// Класс представляет хранение, согласно интерфейсу AbstractSettings
/// Класс реализует замещение глобальных настроек локальными - для этого необходимо изменить
/// параметр 	dominationLocalSettings = на true. В этом случае при обращении к глобальному хранилищу
/// сначала будет производится поиск в локальном.
/// </para>
/// <para>
/// SO загружается раньше всех других классов, но в итоге, событие загрузки высылается слишком рано.
/// Чтобы не вмешиваться в эту логику - первичную инициализацию нужно производить вручную
/// </para>
/// </remarks>
[CreateAssetMenu(fileName = "SettingsJson", menuName = "Common/Settings Provider/SettingsJson")]
public class SettingsJsonScriptableObject : AbstractSettingsStorageSO
{
  protected JsonFileStorage localStorage = null;
  protected JsonFileStorage globalStorage = null;

  protected JsonFileStorage GetStorage(SettingPolicy scope)
  {
    switch (scope)
    {
      case SettingPolicy.Local:
      {
        if (localStorage == null)
        {
          localStorage = new JsonFileStorage(Application.streamingAssetsPath, localFilename,
                                             JsonSerializer, converters);
        }
        return localStorage;
      }

      default:
      case SettingPolicy.Global:
      {
        if (globalStorage == null)
        {
          globalStorage = new JsonFileStorage(Application.streamingAssetsPath, globalFilename,
                                              JsonSerializer, converters);
        }
        return globalStorage;
      }
    }
  }

  [SerializeField]
  private bool? _dominationLocalStorage = null;
  protected bool DominationLocalStorage
  {
    get {
      if (!_dominationLocalStorage.HasValue)
      {
        _dominationLocalStorage =
          GetStorage(SettingPolicy.Local).GetValue<bool>("dominationLocalSettings", false);
      }

      return _dominationLocalStorage.Value;
    }
  }

  [SerializeField] protected string globalFilename = "settings.json";

  [SerializeField]
  protected string localFilename = "local_settings.json";

  /// <summary>
  /// JsonSerializer with our converters
  /// </summary>
  private JsonSerializer _jsonSerializer = null;
  /// <summary>
  /// JsonSerializer properties - control time creation
  /// </summary>
  protected JsonSerializer JsonSerializer
  {
    get {
      if (_jsonSerializer != null)
      {
        return _jsonSerializer;
      }
      else
      {
        _jsonSerializer = new JsonSerializer {
          Formatting = Formatting.Indented,
        };
        return _jsonSerializer;
      }
    }
  }

  private readonly JsonConverter[] converters = {
    new ColorJsonConverter(),      new Matrix4x4JsonConverter(), new Vector2JsonConverter(),
    new Vector2IntJsonConverter(), new Vector3JsonConverter(),   new Vector3IntJsonConverter(),
    new Vector3JsonConverter(),    new StringEnumConverter(),    new Vector4JsonConverter()
  };

  public override T GetValue<T>(string name, T defaultValue = default,
                                SettingPolicy scope = SettingPolicy.Global)
  {
    try
    {
      if (DominationLocalStorage)
      {
        if (GetStorage(SettingPolicy.Local).ContainstKey(name))
        {
          return GetStorage(SettingPolicy.Local).GetValue<T>(name, defaultValue);
        }
        else
        {
          return GetStorage(SettingPolicy.Global).GetValue<T>(name, defaultValue);
        }
      }
      else
      {
        return GetStorage(scope).GetValue<T>(name, defaultValue);
      }
    }
    catch (InvalidCastException exception)
    {
      Debug.LogException(exception, this);
    }
    return defaultValue;
  }

  public override void SetValue<T>(string name, T value, SettingPolicy scope = SettingPolicy.Global)
  {
    GetStorage(scope).SetValue(name, value);
  }

  public override void Load()
  {
    GetStorage(SettingPolicy.Local).Deserialize();
    GetStorage(SettingPolicy.Global).Deserialize();
    _dominationLocalStorage =
      GetValue<bool>("dominationLocalSettings", false, scope: SettingPolicy.Local);
    InvokeOnSettingsChanged();
  }

  public override void Save()
  {
    SetValue<bool>("dominationLocalSettings", DominationLocalStorage, scope: SettingPolicy.Local);
    GetStorage(SettingPolicy.Local).Serialize();
    GetStorage(SettingPolicy.Global).Serialize();
  }
  protected virtual void OnEnable() { Load(); }
  protected virtual void OnDisable() { Save(); }
}  // class
}  // namespace
