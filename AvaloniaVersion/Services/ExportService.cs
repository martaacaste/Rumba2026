using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using AvaloniaVersion.Models;

namespace AvaloniaVersion.Services;

public class ExportService
{
    public async Task ExportarAsync(Resultado resultado, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(resultado, options);
        await File.WriteAllTextAsync(filePath, json);
    }
}