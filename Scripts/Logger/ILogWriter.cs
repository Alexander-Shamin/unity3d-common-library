using System;

namespace Common
{
/// <summary>
/// Интерфейс класса, реализующего запись статистики
/// </summary>
public interface ILogWriter
{
  /// <summary>
  /// Добавление информации о текущей статистике
  /// </summary>
  /// <param name="info"></param>
  void Append(Information info);

  /// <summary>
  /// Закрыть запись
  /// </summary>
  void Close();

  /// <summary>
  /// Сигнал о начале нового дня или любого другого отрезка времени,
  /// чтобы в новом файле ID начинались с 1
  /// </summary>
  event Action OnUpdateOrdinalValues;

  string TimeFormat { get; set; }
  string DateFormat { get; set; }
}
}
