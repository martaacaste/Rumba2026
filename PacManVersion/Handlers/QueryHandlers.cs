using MediatR;
using PacManVersion.Queries;
using PacManVersion.Models;

namespace PacManVersion.Handlers;

public class GetGameStateQueryHandler : IRequestHandler<GetGameStateQuery, GameState>
{
    private readonly GameEngine _gameEngine;

    public GetGameStateQueryHandler(GameEngine gameEngine)
    {
        _gameEngine = gameEngine;
    }

    public async Task<GameState> Handle(GetGameStateQuery request, CancellationToken cancellationToken)
    {
        return await _gameEngine.GetGameState();
    }
}