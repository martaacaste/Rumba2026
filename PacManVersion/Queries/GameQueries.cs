using MediatR;
using PacManVersion.Models;

namespace PacManVersion.Queries;

public class GetGameStateQuery : IRequest<GameState>
{
}