namespace Common
{
/// <summary>
/// Сбор данных о работе приложения, взаимодействии с пользователем
/// </summary>

public interface ILogHandler
{
  bool IsLogEnable { get; set; }
  ILogCollector LogCollector { get; set; }

  /// <param name="type">Тип собираемой информации</param>
  /// <param name="id">ID пользователя</param>
  /// <param name="message">Информация для записи</param>
  void Log(TypeOfLog type, string message, string addMessage = "");
  void Log(TypeOfLog type, int? id, string message = "", string addMessage = "");
}
}