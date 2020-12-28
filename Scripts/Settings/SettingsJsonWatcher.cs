using System.IO;
using UnityEngine;

namespace Common
{
	[CreateAssetMenu(menuName = "Common/Settings/SettingsJsonWatcher")]
	public class SettingsJsonWatcher : SettingsJson
	{
		FileSystemWatcher fileSystemWatcher;

		private void OnEnable()
		{
			Debug.Log("SettingsJsonWatcher");
			try
			{
				fileSystemWatcher = new FileSystemWatcher(Path.GetFullPath("."))
				{
					NotifyFilter = NotifyFilters.LastWrite,
					Filter = _filename
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
			Debug.Log("Hello!");
			InvokeOnSettingsScriptableObjectChanged();
		}

		private void OnDestroy()
		{
			fileSystemWatcher.EnableRaisingEvents = false;
			fileSystemWatcher?.Dispose();
		}
	}
}
