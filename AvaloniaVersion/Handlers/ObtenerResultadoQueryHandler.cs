using AvaloniaVersion.Queries;
using AvaloniaVersion.Models;

namespace AvaloniaVersion.Handlers;

public class ObtenerResultadoQueryHandler
{
    public Resultado Handle(ObtenerResultadoQuery query)
    {
        return query.Resultado;
    }
}