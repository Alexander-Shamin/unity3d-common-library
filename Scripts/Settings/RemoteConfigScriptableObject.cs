using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.RemoteConfig;

namespace Common
{
	[CreateAssetMenu(fileName = "SettingsRemoteConfigJson", menuName = "Common/Settings Provider/SettingsRemoteConfigJson")]
	/// <summary>
	/// Реализация системы доступа к удаленным настройкам, с реализацией кеш данных в json файлах
	/// </summary>
	public class RemoteConfigScriptableObject : SettingsJsonWatcherScriptableObject
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
							List<string> namesMap = new List<string>();

							foreach (var name in storage.Keys)
								if (ConfigManager.appConfig.HasKey(name))
									namesMap.Add(name);

							int updatedValue = 0;
							foreach (var name in namesMap)
							{
								if (storage[name] != ConfigManager.appConfig.GetJson(name))
								{
									storage[name] = ConfigManager.appConfig.GetJson(name);
									updatedValue++;
								}
							}

							if (updatedValue != 0)
								InvokeOnSettingsChanged();

							OnSettingsUpdateSuccessfully?.Invoke(true);
							break;
						}
				}
			}
			else
			{
				Debug.LogError($"Error response remote settings = {configResponse.status}");
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
				versionInformation = GetStorage(TypeScopeSettings.Local).GetValue("VersionInformation", "none"),
			};
			ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), attr);
		}

	} // class
}
