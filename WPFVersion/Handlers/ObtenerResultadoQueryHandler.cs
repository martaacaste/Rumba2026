using WPFVersion.Queries;
using WPFVersion.Models;

namespace WPFVersion.Handlers;

public class ObtenerResultadoQueryHandler
{
    public Resultado Handle(ObtenerResultadoQuery query)
    {
        return query.Resultado;
    }
}