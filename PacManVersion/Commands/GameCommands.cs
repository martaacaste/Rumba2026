using MediatR;

namespace PacManVersion.Commands;

public class MovePacManCommand : IRequest
{
    public string Direction { get; set; } = "Up";
}

public class EatPillCommand : IRequest
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class UpdateGhostPositionsCommand : IRequest
{
}