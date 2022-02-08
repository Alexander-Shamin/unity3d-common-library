using System;
using System.Collections.Generic;

namespace Common
{
/// <summary>
/// Основная информация, собираемая модулем статистики
/// </summary>
[Serializable]
public class CommonInformation : Information
{
  public List<string> Info = new List<string>();

  /// <summary>
  /// Упрощенная запись данных
  /// </summary>
  public void Add(string timeFormat, TypeOfLog type, string key, string value)
  {
    Info = CreateItem(timeFormat, type.ToString(), key, value);
  }

}  // class
}  // namespace
