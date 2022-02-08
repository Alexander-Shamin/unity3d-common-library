using System;

using Newtonsoft.Json;

namespace Common
{
internal class LogHeader
{
  private string Version = "1.1.0";
  private string TimeZone = TimeZoneInfo.Local.DisplayName;
  private string TimeFormat;
  private string DateFormat;
  private string Date;
  private string Time;

  public LogHeader(string dateFormat, string timeFormat)
  {
    DateFormat = dateFormat;
    TimeFormat = timeFormat;
  }

  public void WriteHeader(JsonTextWriter writer)
  {
    Date = DateTime.Now.Date.ToString(DateFormat);
    Time = DateTime.Now.ToString(TimeFormat);

    LogWriterJson.WritePair(writer, nameof(Version), Version);
    LogWriterJson.WritePair(writer, nameof(TimeZone), TimeZone);
    LogWriterJson.WritePair(writer, nameof(DateFormat), DateFormat);
    LogWriterJson.WritePair(writer, nameof(TimeFormat), TimeFormat);
    LogWriterJson.WritePair(writer, nameof(Date), Date);
    LogWriterJson.WritePair(writer, nameof(Time), Time);
  }
}  // class
}  // namespace
