namespace Common
{
/// <summary>
/// Базовое поведение статистики
///
/// Предполагается, что фиксируется действия пользователя перед зеркалом,
/// а также записывается общая информация
/// </summary>
public interface ILogCollector
{
  ILogWriter LogWriter { get; set; }

  // void Log(TypeOfLog type, string key = "", string value = "");

  void Log(TypeOfLog type, int? id = null, string key = "", string value = "");
  void FinishRecording();
}
}