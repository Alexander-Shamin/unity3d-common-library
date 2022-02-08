using Common;
using UnityEngine;
using System.Collections.Generic;

namespace Common
{
/// <summary>
/// Настройки ведения журнала логов
/// </summary>
[CreateAssetMenu(fileName = "Settings Log", menuName = "SmartMirror/Common/SettingsLog")]
public class SettingsLog : AbstractSettingSO<SettingsLog>
{
  public bool isLogEnabled = true;

  private readonly string _moduleName = "Log";
  protected override bool GetSettings(AbstractSettingsStorageSO s)
  {
    isLogEnabled =
      s.GetValue<bool>(nameof(isLogEnabled), module: _moduleName, scope: SettingPolicy.Global);

    return true;
  }

  protected override void SetSettings(AbstractSettingsStorageSO s)
  {
    s.SetValue<bool>(nameof(isLogEnabled), module: _moduleName, isLogEnabled,
                     scope: SettingPolicy.Global);
  }

  public override void InvokeOnSettingsChanged() { InvokeOnSettingsChanged(this); }
}  // class
}
