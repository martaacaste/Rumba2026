using System.Collections.Generic;

namespace AvaloniaVersion.Models;

public class Resultado
{
    public List<Zona> Zonas { get; set; } = new();
    public double SuperficieTotal { get; set; }
    public double TiempoEstimado { get; set; }
}