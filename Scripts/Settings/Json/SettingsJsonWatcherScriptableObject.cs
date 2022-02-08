using System.IO;
using UnityEngine;

namespace Common
{
/// <summary>
/// Реализация Json хранилища с функцией наблюдения за изменением файла
/// </summary>
[CreateAssetMenu(fileName = "SettingsJsonWatcher",
                 menuName = "Common/Settings Provider/SettingsJsonWatcher")]
public class SettingsJsonWatcherScriptableObject : SettingsJsonScriptableObject
{
  private FileSystemWatcher fileSystemWatcher = null;
  public string filenameFilter = "*settings.json";
  protected override void OnEnable()
  {
    base.OnEnable();
    try
    {
      fileSystemWatcher = new FileSystemWatcher(Application.streamingAssetsPath) {
        NotifyFilter = NotifyFilters.LastWrite, Filter = filenameFilter
      };

      fileSystemWatcher.Changed += FileSystemWatcher_Changed;
      fileSystemWatcher.EnableRaisingEvents = true;
    }
    catch (System.Exception exception)
    {
      Debug.LogException(exception);
    }
  }

  private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e) { Load(); }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (fileSystemWatcher != null)
    {
      fileSystemWatcher.EnableRaisingEvents = false;
      fileSystemWatcher?.Dispose();
    }
  }
}  // class
}  // namespace