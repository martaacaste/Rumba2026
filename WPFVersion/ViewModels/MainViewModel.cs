using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFVersion.Commands;
using WPFVersion.Handlers;
using WPFVersion.Models;
using WPFVersion.Services;
using Microsoft.Win32;

namespace WPFVersion.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly CalcularAreasCommandHandler _calcularHandler;
    private readonly ExportService _exportService;

    public ObservableCollection<Zona> Zonas { get; } = new();
    private double _superficieTotal;
    private double _tiempoEstimado;

    public double SuperficieTotal
    {
        get => _superficieTotal;
        set { _superficieTotal = value; OnPropertyChanged(); }
    }

    public double TiempoEstimado
    {
        get => _tiempoEstimado;
        set { _tiempoEstimado = value; OnPropertyChanged(); }
    }

    public ICommand CalcularCommand { get; }
    public ICommand ExportarCommand { get; }

    public MainViewModel()
    {
        _calcularHandler = new CalcularAreasCommandHandler();
        _exportService = new ExportService();

        // Inicializar zonas
        Zonas.Add(new Zona { Nombre = "Zona 1", Largo = 500, Ancho = 150 });
        Zonas.Add(new Zona { Nombre = "Zona 2", Largo = 480, Ancho = 101 });
        Zonas.Add(new Zona { Nombre = "Zona 3", Largo = 309, Ancho = 480 });
        Zonas.Add(new Zona { Nombre = "Zona 4", Largo = 90, Ancho = 220 });

        CalcularCommand = new RelayCommand(async () => await CalcularAsync());
        ExportarCommand = new RelayCommand(async () => await ExportarAsync());
    }

    private async Task CalcularAsync()
    {
        try
        {
            var command = new CalcularAreasCommand { Zonas = Zonas.ToList() };
            var resultado = await _calcularHandler.Handle(command);
            SuperficieTotal = resultado.SuperficieTotal;
            TiempoEstimado = resultado.TiempoEstimado;
        }
        catch (Exception ex)
        {
            // Manejo de errores global
            System.Windows.MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private async Task ExportarAsync()
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "JSON files (*.json)|*.json",
            DefaultExt = "json"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                var resultado = new Resultado
                {
                    Zonas = Zonas.ToList(),
                    SuperficieTotal = SuperficieTotal,
                    TiempoEstimado = TiempoEstimado
                };
                await _exportService.ExportarAsync(resultado, saveFileDialog.FileName);
                System.Windows.MessageBox.Show("Exportado exitosamente.");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al exportar: {ex.Message}");
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

// RelayCommand simple
public class RelayCommand : ICommand
{
    private readonly Func<Task> _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    public async void Execute(object? parameter) => await _execute();

    public event EventHandler? CanExecuteChanged;
}