using UnityEngine;
using UnityEngine.Assertions;

namespace Common
{
/// <summary>
/// Сбор статистики о работе приложения и взаимодействии пользователей с системой
/// </summary>
public sealed class LogManager : Singelton<LogManager, DontDestroyOnLoadEnable>
{
  [SerializeField, Required]
  private SettingsLog settings;

  private readonly ILogHandler LogHandler = new LogHandler(new LogCollector(new LogWriterJson()));

  private static ILogHandler LH
  {
    get {
      return IsAlive ? Instance.LogHandler : null;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    Assert.IsNotNull(settings,
                     "LogManager :: Awake :: SettingsLog not found. " +
                       "Add SettingsLog ScriptableObject to script <LogManager> in inspector.");

    settings?.RegisterListener(SettingsLog_OnSettingsChanged);

    if (LH != null) LH.IsLogEnable = settings?.isLogEnabled ?? true;
  }

  protected override void OnApplicationQuit()
  {
    LH?.LogCollector.FinishRecording();
    settings?.UnregisterListener(SettingsLog_OnSettingsChanged);
    base.OnApplicationQuit();
  }

  private void SettingsLog_OnSettingsChanged(SettingsLog value)
  {
    if (LH != null) LH.IsLogEnable = settings?.isLogEnabled ?? true;
  }

#region Static wrappers for logs
  public static void Log(TypeOfLog type, int? id, string message) { LH?.Log(type, id, message); }

  public static void Log(TypeOfLog type, int? id, string message, string addMessage)
  {
    LH?.Log(type, id, message, addMessage);
  }

  public static void Log(TypeOfLog type, string message) { LH?.Log(type, message); }

  public static void Log(TypeOfLog type, string message, string addMessage)
  {
    LH?.Log(type, message, addMessage);
  }

  public static void LogException(string message, int? id = null)
  {
    LH?.Log(TypeOfLog.Exception, id, message);
  }

  public static void LogFatalError(string message, int? id = null)
  {
    LH?.Log(TypeOfLog.FatalError, id, message);
  }

  public static void LogProject(string message, int? id = null)
  {
    LH?.Log(TypeOfLog.ProjectInfo, id, message);
  }

  public static void LogSkeletonTracking(string message, int? id = null)
  {
    LH?.Log(TypeOfLog.SkeletonTracking, id, message);
  }

  public static void LogModel(string message, int? id = null)
  {
    LH?.Log(TypeOfLog.ModelInfo, id, message);
  }

  public static void LogGui(string message, int? id = null)
  {
    LH?.Log(TypeOfLog.GuiInfo, id, message);
  }

  public static void LogClothesInfo(string message, int? id)
  {
    LH?.Log(TypeOfLog.ClothesInfo, id, message);
  }

  public static void LogClothesInfo(string clothesType, string textureName, int? id)
  {
    LH?.Log(TypeOfLog.ClothesInfo, id, clothesType, textureName);
  }

  public static void LogNewUser(int? id) { LH?.Log(TypeOfLog.NewUser, id); }

  public static void LogLostUser(int? id) { LH?.Log(TypeOfLog.LostUser, id); }

  public static void LogGender(string gender, int? id) { LH?.Log(TypeOfLog.Gender, id, gender); }

  public static void LogUserCommon(string message, int? id = null)
  {
    LH?.Log(TypeOfLog.UserCommon, id, message);
  }
#endregion  // Static wrappers for logs
}  // class
}  // namespace