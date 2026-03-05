using System.Text.Json;
using WPFVersion.Models;

namespace WPFVersion.Services;

public class ExportService
{
    public async Task ExportarAsync(Resultado resultado, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(resultado, options);
        await File.WriteAllTextAsync(filePath, json);
    }
}