using System.Text.Json;

namespace PacManVersion.Services;

public class SidecarLogger
{
    private readonly string _logFile = "pacman_log.json";

    public void Log(string message)
    {
        var logEntry = new { Timestamp = DateTime.Now, Message = message };
        var json = JsonSerializer.Serialize(logEntry);
        File.AppendAllText(_logFile, json + Environment.NewLine);
    }
}