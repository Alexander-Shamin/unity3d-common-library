using System.IO;
using UnityEngine;

namespace Common
{
	/// <summary>
	/// Реализация Json хранилища с функцией наблюдения за изменением файла
	/// </summary>
	[CreateAssetMenu(fileName = "SettingsJsonWatcher", menuName = "Common/Settings Provider/SettingsJsonWatcher")]
	public class SettingsJsonWatcherScriptableObject : SettingsJsonScriptableObject
	{
		FileSystemWatcher fileSystemWatcher;
		public string filenameFilter = "*settings.json";

		private void OnEnable()
		{
			try
			{
				fileSystemWatcher = new FileSystemWatcher(Application.streamingAssetsPath)
				{
					NotifyFilter = NotifyFilters.LastWrite,
					Filter = filenameFilter
				};

				fileSystemWatcher.Changed += FileSystemWatcher_Changed;
				fileSystemWatcher.EnableRaisingEvents = true;
			}
			catch (System.Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			Load();
		}

		private void OnDestroy()
		{
			if (fileSystemWatcher != null)
			{
				fileSystemWatcher.EnableRaisingEvents = false;
				fileSystemWatcher?.Dispose();
			}
		}
	} // class
}