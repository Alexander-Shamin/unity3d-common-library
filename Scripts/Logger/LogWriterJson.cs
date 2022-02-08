using System;
using System.IO;

using UnityEngine;
using Newtonsoft.Json;

namespace Common
{
/// <summary>
/// Класс записи данных в файл json
///
/// Json файл должен содержать заголовок в виде заполенного класса LogHeader
/// </summary>
internal class LogWriterJson : ILogWriter
{
#region ILogWriter
  public event Action OnUpdateOrdinalValues;

  public void Append(Information info)
  {
    try
    {
      if (_writer == null || _currentDay.Date != DateTime.Now.Date)
      {
        var dir = Directory.CreateDirectory(DirectoryPath);
        RecreateFile(ref _writer, FileFullPath());
      }
      _jsonSerializer.Serialize(_writer, info);
    }
    catch (Exception exception)
    {
      Debug.LogException(exception);
    }
  }

  public void Close() { CloseFile(_writer); }

  public string TimeFormat { get; set; }
  public string DateFormat { get; set; }

#endregion

  private JsonTextWriter _writer;
  private readonly JsonSerializer _jsonSerializer =
    new JsonSerializer() { Formatting = Formatting.None };

  private readonly string _folderName = "Statistics";
  private readonly string _baseFileName = "stat_";
  private readonly string _fileExtension = ".json";

  private DateTime _currentDay;

  private string DirectoryPath
  {
    get {
      return Environment.CurrentDirectory + Path.DirectorySeparatorChar + _folderName;
    }
  }

  private string FileFullPath()
  {
    _currentDay = DateTime.Now;
    string name = DirectoryPath + Path.DirectorySeparatorChar + _baseFileName +
                  _currentDay.ToString(DateFormat) + _fileExtension;
    int revision = 1;
    while (File.Exists(name))
    {
      name = DirectoryPath + Path.DirectorySeparatorChar + _baseFileName +
             _currentDay.ToString(DateFormat) + "_rev" + revision.ToString() + _fileExtension;
      revision++;
    }
    return name;
  }

  private void RecreateFile(ref JsonTextWriter writer, string name)
  {
    if (writer != null)
    {
      OnUpdateOrdinalValues?.Invoke();
      CloseFile(writer);
    }

    writer = new JsonTextWriter(new StreamWriter(name, append: true));
    writer.WriteStartObject();

    // Заполнение заголовка файла
    var header = new LogHeader(DateFormat, TimeFormat);
    header.WriteHeader(writer);

    writer.WritePropertyName("Data");
    writer.WriteStartArray();
  }

  private void CloseFile(JsonTextWriter writer)
  {
    writer?.WriteEndArray();
    WritePair(writer, "DateEndWriting", DateTime.Now.Date.ToString(DateFormat));
    WritePair(writer, "TimeEndWriting", DateTime.Now.ToString(TimeFormat));
    writer?.WriteEndObject();
    writer?.Flush();
    writer?.Close();
  }

  public static void WritePair(JsonTextWriter writer, string key, string value)
  {
    writer?.WritePropertyName(key);
    writer?.WriteValue(value);
  }
}  // class
}
