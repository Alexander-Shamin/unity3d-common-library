using System;
using System.Collections.Generic;

namespace Common
{
/// <summary>
/// Сбор и объединение информации для записи
///
/// Предполагается, что фиксируется действия пользователя перед зеркалом,
/// а также записывается общая информация
/// </summary>
/// <remarks>
/// Все непользовательские действия имеют отрицательные id и обрабатываются отдельно
/// </remarks>
internal class LogCollector : ILogCollector
{
#region ILogCollector
  public LogCollector(ILogWriter logWriter)
  {
    LogWriter = logWriter;
    LogWriter.TimeFormat = _timeFormat;
    LogWriter.DateFormat = _dateFormat;
    LogWriter.OnUpdateOrdinalValues += () =>
    {
      WriteNonUserActions(usersInfo);
      _ordinalUserId = 0;
    };
  }

  public ILogWriter LogWriter { get; set; }

  public void FinishRecording()
  {
    List<int> keys = new List<int>(usersInfo.Keys);
    foreach (int id in keys)
    {
      WriteUserInfo(usersInfo, id);
    }
    LogWriter?.Close();
  }

  public void Log(TypeOfLog type, int? id = null, string message = "", string addMessage = "")
  {
    try
    {
      if (!id.HasValue)
      {
        // Записать лог без id, не обращаясь к словарю пользователей
        WriteInfo(type, message, addMessage);
      }
      else
      {
        int _id = id.Value;
        switch (type)
        {
          case TypeOfLog.NewUser:
          {
            AddUserInfo(usersInfo, _id);
            break;
          }

          case TypeOfLog.LostUser:
          {
            WriteUserInfo(usersInfo, _id);
            break;
          }

          case TypeOfLog.Gender:
          {
            var st = GetUserInfo(usersInfo, _id);
            st.gender = message;
            break;
          }

          default:
          {
            var st = GetUserInfo(usersInfo, _id);
            st?.Add(_timeFormat, type, message, addMessage);
            break;
          }
        }
      }
    }
    catch (Exception ex)
    {
      UnityEngine.Debug.LogException(ex);
    }
  }

#endregion  // ILogCollector

  private readonly string _timeFormat = "hh:mm:ss.ff";
  private readonly string _dateFormat = "dd-MM-yy";

  private Dictionary<int, UserInformation> usersInfo = new Dictionary<int, UserInformation>();

  // Порядковый номер пользователя
  private int _ordinalUserId = 0;

  private UserInformation GetUserInfo(Dictionary<int, UserInformation> dict, int id)
  {
    if (!dict.ContainsKey(id))
    {
      AddUserInfo(dict, id);
    }

    return dict[id];
  }

  private void AddUserInfo(Dictionary<int, UserInformation> dict, int id)
  {
    if (!dict.ContainsKey(id))
    {
      var st = new UserInformation { start = DateTime.Now.ToString(_timeFormat) };

      if (id < 0)
      {
        st.id = id;
      }
      else
      {
        _ordinalUserId++;
        st.id = _ordinalUserId;
      }

      dict.Add(id, st);
    }
  }

  /// <summary>
  /// Запись полученной информации
  /// </summary>
  private void WriteInfo(TypeOfLog type, string message, string addMessage)
  {
    var commonInfo = new CommonInformation();
    commonInfo.Add(_timeFormat, type, message, addMessage);
    LogWriter?.Append(commonInfo);
  }

  /// <summary>
  /// Закрытие сессии пользователя и запись полученной информации
  /// </summary>
  private void WriteUserInfo(Dictionary<int, UserInformation> dict, int id)
  {
    var st = GetUserInfo(dict, id);
    st.end = DateTime.Now.ToString(_timeFormat);
    LogWriter?.Append(st);
    dict.Remove(id);
  }

  /// <summary>
  /// Запись непользовательских действий (с отрицательными id)
  /// </summary>
  private void WriteNonUserActions(Dictionary<int, UserInformation> dict)
  {
    List<int> keys = new List<int>(dict.Keys);
    foreach (int id in keys)
    {
      if (id < 0) WriteUserInfo(dict, id);
    }
  }
}  // class
}
