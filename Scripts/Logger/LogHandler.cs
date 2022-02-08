namespace Common
{
internal class LogHandler : ILogHandler
{
  public bool IsLogEnable { get; set; }

  public ILogCollector LogCollector { get; set; }

  public LogHandler(ILogCollector collector) { LogCollector = collector; }

  void ILogHandler.Log(TypeOfLog type, string message, string addMessage)
  {
    if (IsLogEnable) LogCollector?.Log(type, null, message, addMessage);
  }

  void ILogHandler.Log(TypeOfLog type, int? id, string message, string addMessage)
  {
    if (IsLogEnable) LogCollector?.Log(type, id, message, addMessage);
  }
}
}