using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.RemoteConfig;
using System.Text.RegularExpressions;

namespace Common
{
	[CreateAssetMenu(fileName = "SettingsRemoteConfigJson", menuName = "Common/Settings Provider/SettingsRemoteConfigJson")]
	/// <summary>
	/// Реализация системы доступа к удаленным настройкам, с реализацией кеш данных в json файлах
	/// </summary>
	public class RemoteConfigScriptableObject : SettingsJsonScriptableObject
	{
		public UnityAction<bool> OnSettingsUpdateSuccessfully;

		[SerializeField]
		private bool _disableRemoteUpdate = false;

		public struct userAttributes
		{
		}

		public struct appAttributes
		{
			public string customerInformation;
			public string locationInformation;
			public string versionInformation;
		}


		private void OnEnable()
		{
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

		private void OnDisable()
		{
			if (!_disableRemoteUpdate)
			{
				ConfigManager.FetchCompleted -= OnFetchRemoteSettings;
			}
		}

		private void OnFetchRemoteSettings(ConfigResponse configResponse)
		{
			if (configResponse.status == ConfigRequestStatus.Success)
			{
				switch (configResponse.requestOrigin)
				{
					case ConfigOrigin.Remote:
					{
						var storage = GetStorage(TypeScopeSettings.Global).Storage;
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

		[ContextMenu("FetchRemoteSettings")]
		public void FetchRemoteSettings()
		{
			ConfigManager.SetCustomUserID("none");
			var attr = new appAttributes()
			{
				customerInformation = GetStorage(TypeScopeSettings.Local).GetValue("CustomerInformation", "none"),
				locationInformation = GetStorage(TypeScopeSettings.Local).GetValue("LocationInformation", "none"),
				versionInformation = GetStorage(TypeScopeSettings.Global).GetValue("VersionInformation", "none"),
			};
			ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), attr);
		}

	} // class
}
