using System.Globalization;
using System.Text.RegularExpressions;
using LogStandartApp.Models;

namespace LogStandartApp.Services;

public class LogParser
{
    private static readonly Regex Format1Regex = new(
        @"^(?<date>\d{2}\.\d{2}\.\d{4})\s+(?<time>\d{2}:\d{2}:\d{2}(?:\.\d+)?)\s+(?<level>[A-Z]+)\s+(?<message>.+)$",
        RegexOptions.Compiled);

    private static readonly Regex Format2Regex = new(
        @"^(?<date>\d{4}-\d{2}-\d{2})\s+(?<time>\d{2}:\d{2}:\d{2}(?:\.\d+)?)\s*\|\s*(?<level>[A-Z]+)\s*\|\s*[^|]*\s*\|\s*(?<method>[^|]+)\s*\|\s*(?<message>.*)$",
        RegexOptions.Compiled);

    public bool TryParse(string line, out LogRecord? record)
    {
        record = null;

        if (string.IsNullOrWhiteSpace(line))
            return false;

        return TryParseFormat1(line, out record) || TryParseFormat2(line, out record);
    }

    private bool TryParseFormat1(string line, out LogRecord? record)
    {
        record = null;
        var match = Format1Regex.Match(line);

        if (!match.Success)
            return false;

        if (!DateTime.TryParseExact(
                match.Groups["date"].Value,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
        {
            return false;
        }

        if (!TryNormalizeLevel(match.Groups["level"].Value, out var level))
            return false;

        record = new LogRecord
        (
            date,
            match.Groups["time"].Value,
            level,
            "DEFAULT",
            match.Groups["message"].Value.Trim()
        );

        return true;
    }

    private bool TryParseFormat2(string line, out LogRecord? record)
    {
        record = null;
        var match = Format2Regex.Match(line);

        if (!match.Success)
            return false;

        if (!DateTime.TryParseExact(
                match.Groups["date"].Value,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
        {
            return false;
        }

        if (!TryNormalizeLevel(match.Groups["level"].Value, out var level))
            return false;

        var method = match.Groups["method"].Value.Trim();

        if (string.IsNullOrWhiteSpace(method))
            method = "DEFAULT";

        record = new LogRecord
        (
            date,
            match.Groups["time"].Value,
            level,
            method,
            match.Groups["message"].Value.Trim()
        );

        return true;
    }

    private bool TryNormalizeLevel(string sourceLevel, out string level)
    {
        level = sourceLevel.Trim().ToUpperInvariant() switch
        {
            "INFO" or "INFORMATION" => "INFO",
            "WARN" or "WARNING" => "WARN",
            "ERROR" => "ERROR",
            "DEBUG" => "DEBUG",
            _ => string.Empty
        };

        return level != string.Empty;
    }
}