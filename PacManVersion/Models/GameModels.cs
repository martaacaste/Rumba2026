using System.ComponentModel;

namespace PacManVersion.Models;

public class Entity : INotifyPropertyChanged
{
    private double _x;
    private double _y;

    public double X
    {
        get => _x;
        set { _x = value; OnPropertyChanged(); }
    }

    public double Y
    {
        get => _y;
        set { _y = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class PacMan : Entity
{
    public int Score { get; set; }
    public bool IsPowered { get; set; }
}

public class Ghost : Entity
{
    public string Color { get; set; } = "Red";
    public bool IsVulnerable { get; set; }
}

public class GameState
{
    public PacMan PacMan { get; set; } = new();
    public List<Ghost> Ghosts { get; set; } = new();
    public int[][] Maze { get; set; } = new int[10][]; // Simplified maze
}