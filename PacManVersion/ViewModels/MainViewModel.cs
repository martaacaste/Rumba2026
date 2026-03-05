using MediatR;
using PacManVersion.Commands;
using PacManVersion.Queries;
using PacManVersion.Services;
using PacManVersion.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacManVersion.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IMediator _mediator;
    private readonly GameEngine _gameEngine;
    private readonly EventBus _eventBus;

    public ObservableCollection<Entity> Entities { get; } = new();

    public ICommand MoveUpCommand { get; }
    public ICommand MoveDownCommand { get; }
    public ICommand MoveLeftCommand { get; }
    public ICommand MoveRightCommand { get; }

    public MainViewModel(IMediator mediator, GameEngine gameEngine, EventBus eventBus)
    {
        _mediator = mediator;
        _gameEngine = gameEngine;
        _eventBus = eventBus;
        MoveUpCommand = new RelayCommand(async () => await MoveUp());
        MoveDownCommand = new RelayCommand(async () => await MoveDown());
        MoveLeftCommand = new RelayCommand(async () => await MoveLeft());
        MoveRightCommand = new RelayCommand(async () => await MoveRight());
        LoadGame();
    }

    private async void LoadGame()
    {
        var state = await _mediator.Send(new GetGameStateQuery());
        Entities.Add(state.PacMan);
        foreach (var ghost in state.Ghosts)
        {
            Entities.Add(ghost);
        }
    }

    public async Task MoveUp() => await _mediator.Send(new MovePacManCommand { Direction = "Up" });
    public async Task MoveDown() => await _mediator.Send(new MovePacManCommand { Direction = "Down" });
    public async Task MoveLeft() => await _mediator.Send(new MovePacManCommand { Direction = "Left" });
    public async Task MoveRight() => await _mediator.Send(new MovePacManCommand { Direction = "Right" });
}

public class RelayCommand : ICommand
{
    private readonly Func<Task> _execute;

    public RelayCommand(Func<Task> execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter) => await _execute();

    public event EventHandler? CanExecuteChanged;
}