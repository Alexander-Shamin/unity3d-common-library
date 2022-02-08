using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.RemoteConfig;
using System.Text.RegularExpressions;

namespace Common
{
#if USE_REMOTE_CONFIG
[CreateAssetMenu(fileName = "SettingsRemoteConfigJson",
                 menuName = "Common/Settings Provider/SettingsRemoteConfigJson")]
/// <summary>
/// Реализация системы доступа к удаленным настройкам, с реализацией кеш данных в json файлах
/// </summary>
public class RemoteConfigScriptableObject : SettingsJsonWatcherScriptableObject
{
  public UnityAction<bool> OnSettingsUpdateSuccessfully;

  [SerializeField]
  private bool _disableRemoteUpdate = false;

  public struct UserAttributes
  {
  }

  public struct AppAttributes
  {
    public string customerInformation;
    public string locationInformation;
    public string versionInformation;
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    if (!_disableRemoteUpdate)
    {
      ConfigManager.FetchCompleted += OnFetchRemoteSettings;
      FetchRemoteSettings();
    }
    else
    {
      Debug.LogError("RemoteConfigScriptableObject :: Error! Disable remote settings update!");
    }
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (!_disableRemoteUpdate)
    {
      ConfigManager.FetchCompleted -= OnFetchRemoteSettings;
    }
  }

  private void OnFetchRemoteSettings(ConfigResponse configResponse)
  {
    try
    {
      if (configResponse.status == ConfigRequestStatus.Success)
      {
        switch (configResponse.requestOrigin)
        {
          case ConfigOrigin.Remote:
          {
            var storage = GetStorage(SettingPolicy.Global).Storage;
            int updatedValue = 0;

            foreach (var name in ConfigManager.appConfig.GetKeys())
            {
              if (storage.ContainsKey(name))
              {
                string value = ConfigManager.appConfig.GetJson(name);
                if (storage[name] != value)
                {
                  storage[name] = value;
                  updatedValue++;
                }
              }
            }
            if (updatedValue != 0)
            {
              Save();
              InvokeOnSettingsChanged();
            }

            OnSettingsUpdateSuccessfully?.Invoke(true);
            break;
          }
        }
      }
      else
      {
        Debug.LogWarning($"Error response remote settings = {configResponse.status}");
        OnSettingsUpdateSuccessfully?.Invoke(false);
      }
    }
    catch (System.Exception exception)
    {
      Debug.LogException(exception);
    }
  }

  [ContextMenu("FetchRemoteSettings")]
  public void FetchRemoteSettings()
  {
    try
    {
      ConfigManager.SetCustomUserID("none");
      var attr = new AppAttributes() {
        customerInformation =
          GetStorage(SettingPolicy.Local).GetValue("CustomerInformation", "none"),
        locationInformation =
          GetStorage(SettingPolicy.Local).GetValue("LocationInformation", "none"),
        versionInformation =
          GetStorage(SettingPolicy.Global).GetValue("VersionInformation", "none"),
      };
      ConfigManager.FetchConfigs<UserAttributes, AppAttributes>(new UserAttributes(), attr);
    }
    catch
    {
    }
  }

}  // class
#endif
}  // namespace
