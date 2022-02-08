using System;
using System.Collections.Generic;

namespace Common
{
/// <summary>
/// Основная информация, собираемая модулем статистики
/// </summary>
[Serializable]
public class Information
{
  /// <summary>
  /// Создает список из строк, переданных в качестве параметров, исключая пустые значения
  /// </summary>
  protected List<string> CreateItem(string timeFormat, string s1 = "", string s2 = "",
                                    string s3 = "", string s4 = "")
  {
    string time = DateTime.Now.ToString(timeFormat);
    var item = new List<string>();

    TryAdd(item, time);
    TryAdd(item, s1);
    TryAdd(item, s2);
    TryAdd(item, s3);
    TryAdd(item, s4);

    return item;
  }

  private void TryAdd(List<string> item, string value)
  {
    if (!string.IsNullOrWhiteSpace(value)) item.Add(value);
  }

}  // class
}  // namespace
