using System;
using System.Collections.Generic;

namespace Common
{
/// <summary>
/// Информация о пользователях, собираемая модулем статистики
/// </summary>
[Serializable]
internal class UserInformation : Information
{
  public int id;
  public string gender = "none";
  public string start;
  public string end;
  public List<List<string>> GuiInfo = new List<List<string>>();
  public List<List<string>> ClothesInfo = new List<List<string>>();
  public List<List<string>> ModelInfo = new List<List<string>>();
  public List<List<string>> SkelInfo = new List<List<string>>();
  public List<List<string>> CommonInfo = new List<List<string>>();

  /// <summary>
  /// Упрощенная запись данных
  /// </summary>
  public void Add(string timeFormat, TypeOfLog type, string key, string value)
  {
    var item = CreateItem(timeFormat, key, value);

    switch (type)
    {
      case TypeOfLog.GuiInfo:
        GuiInfo.Add(item);
        break;

      case TypeOfLog.ClothesInfo:
        ClothesInfo.Add(item);
        break;

      case TypeOfLog.ModelInfo:
        ModelInfo.Add(item);
        break;

      case TypeOfLog.SkeletonTracking:
        SkelInfo.Add(item);
        break;

      default:
        item = CreateItem(timeFormat, type.ToString(), key, value);
        CommonInfo.Add(item);
        break;
    }
  }
}  // class
}