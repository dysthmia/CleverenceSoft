namespace LogStandartApp.Models;

public class LogRecord
{
    public DateTime Date {get; }
    public string Time {get; }
    public string Level {get; }
    public string Method {get; }
    public string Message {get; }

    public LogRecord (DateTime date, 
                    string time, 
                    string level, 
                    string method, 
                    string message)
    {
        Date = date;
        Time = time;
        Level = level;
        Method = method;
        Message = message;
    }
}