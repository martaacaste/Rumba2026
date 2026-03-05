using WPFVersion.Commands;
using WPFVersion.Models;
using System.Threading.Tasks;

namespace WPFVersion.Handlers;

public class CalcularAreasCommandHandler
{
    public async Task<Resultado> Handle(CalcularAreasCommand command)
    {
        var resultado = new Resultado { Zonas = command.Zonas };
        var tasks = new List<Task>();

        foreach (var zona in command.Zonas)
        {
            tasks.Add(Task.Run(() =>
            {
                zona.Estado = "Calculando...";
                zona.Area = zona.Largo * zona.Ancho;
                zona.Estado = "Completado";
            }));
        }

        await Task.WhenAll(tasks);

        // Suma thread-safe
        resultado.SuperficieTotal = command.Zonas.Sum(z => z.Area);
        resultado.TiempoEstimado = resultado.SuperficieTotal / 1000.0; // 1000 cm²/s

        return resultado;
    }
}