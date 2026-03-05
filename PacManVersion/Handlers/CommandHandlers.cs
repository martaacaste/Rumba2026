using MediatR;
using PacManVersion.Commands;
using PacManVersion.Models;

namespace PacManVersion.Handlers;

public class MovePacManCommandHandler : IRequestHandler<MovePacManCommand>
{
    private readonly GameEngine _gameEngine;

    public MovePacManCommandHandler(GameEngine gameEngine)
    {
        _gameEngine = gameEngine;
    }

    public async Task Handle(MovePacManCommand request, CancellationToken cancellationToken)
    {
        // API Gateway logic: validate and route
        await _gameEngine.MovePacMan(request.Direction);
    }
}

public class EatPillCommandHandler : IRequestHandler<EatPillCommand>
{
    private readonly GameEngine _gameEngine;

    public EatPillCommandHandler(GameEngine gameEngine)
    {
        _gameEngine = gameEngine;
    }

    public async Task Handle(EatPillCommand request, CancellationToken cancellationToken)
    {
        // Saga pattern: handle power pill effects
        await _gameEngine.EatPill(request.X, request.Y);
    }
}

public class UpdateGhostPositionsCommandHandler : IRequestHandler<UpdateGhostPositionsCommand>
{
    private readonly GameEngine _gameEngine;

    public UpdateGhostPositionsCommandHandler(GameEngine gameEngine)
    {
        _gameEngine = gameEngine;
    }

    public async Task Handle(UpdateGhostPositionsCommand request, CancellationToken cancellationToken)
    {
        await _gameEngine.UpdateGhosts();
    }
}