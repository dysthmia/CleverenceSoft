using System.Globalization;
using LogStandartApp.Models;

namespace LogStandartApp.Services;

public class LogStandardizer
{
    private readonly LogParser _parser = new();
    private const string OutputDateFormat = "yyyy-MM-dd";

    public void ProcessFile(string inputPath, string outputPath, string problemsPath)
    {
        if (!File.Exists(inputPath))
            throw new FileNotFoundException("Входной файл не найден.", inputPath);

        using var reader = new StreamReader(inputPath);
        using var outputWriter = new StreamWriter(outputPath,false);
        using var problemsWriter = new StreamWriter(problemsPath, false);

        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            if (_parser.TryParse(line, out var record) && record != null)
            {
                outputWriter.WriteLine(ToOutputLine(record));
            }
            else
            {
                problemsWriter.WriteLine(line);
            }
        }
    }

    private string ToOutputLine(LogRecord record)
    {
        return string.Join('\t',
            record.Date.ToString(OutputDateFormat, CultureInfo.InvariantCulture),
            record.Time,
            record.Level,
            record.Method,
            record.Message);
    }
}