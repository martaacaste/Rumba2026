using PacManVersion.Models;

namespace PacManVersion.Services;

public class GameEngine
{
    private readonly EventBus _eventBus;
    private readonly SidecarLogger _logger;
    public GameState GameState { get; } = new();

    public GameEngine(EventBus eventBus, SidecarLogger logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        InitializeGame();
        _eventBus.Subscribe<PacManMovedEvent>(OnPacManMoved);
    }

    private void InitializeGame()
    {
        GameState.PacMan = new PacMan { X = 5, Y = 5 };
        GameState.Ghosts.Add(new Ghost { X = 1, Y = 1, Color = "Red" });
        GameState.Ghosts.Add(new Ghost { X = 8, Y = 1, Color = "Blue" });
    }

    public async Task MovePacMan(string direction)
    {
        // API Gateway: validate input
        if (!IsValidMove(direction)) return;

        // Update position
        switch (direction)
        {
            case "Up": GameState.PacMan.Y -= 1; break;
            case "Down": GameState.PacMan.Y += 1; break;
            case "Left": GameState.PacMan.X -= 1; break;
            case "Right": GameState.PacMan.X += 1; break;
        }

        // Publish event for Pub/Sub
        await _eventBus.Publish(new PacManMovedEvent { X = GameState.PacMan.X, Y = GameState.PacMan.Y });
    }

    public async Task EatPill(int x, int y)
    {
        // Saga: power up sequence
        GameState.PacMan.IsPowered = true;
        GameState.PacMan.Score += 10;
        foreach (var ghost in GameState.Ghosts)
        {
            ghost.IsVulnerable = true;
        }
        // Compensate if needed
        await Task.Delay(5000); // Power duration
        GameState.PacMan.IsPowered = false;
        foreach (var ghost in GameState.Ghosts)
        {
            ghost.IsVulnerable = false;
        }
    }

    public async Task UpdateGhosts()
    {
        foreach (var ghost in GameState.Ghosts)
        {
            // Simple AI
            if (ghost.X < GameState.PacMan.X) ghost.X += 0.1;
            else ghost.X -= 0.1;
            if (ghost.Y < GameState.PacMan.Y) ghost.Y += 0.1;
            else ghost.Y -= 0.1;
        }
    }

    public async Task<GameState> GetGameState()
    {
        return GameState;
    }

    private bool IsValidMove(string direction)
    {
        // Simplified validation
        return true;
    }

    private async Task OnPacManMoved(PacManMovedEvent evt)
    {
        // Ghosts react to event
        await UpdateGhosts();
        _logger.Log($"Pac-Man moved to {evt.X}, {evt.Y}");
    }
}

public class PacManMovedEvent
{
    public double X { get; set; }
    public double Y { get; set; }
}