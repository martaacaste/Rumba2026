using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using AvaloniaVersion.Commands;
using AvaloniaVersion.Handlers;
using AvaloniaVersion.Models;
using AvaloniaVersion.Services;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace AvaloniaVersion.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly CalcularAreasCommandHandler _calcularHandler;
    private readonly ExportService _exportService;

    public ObservableCollection<Zona> Zonas { get; } = new();
    private double _superficieTotal;
    private double _tiempoEstimado;

    public double SuperficieTotal
    {
        get => _superficieTotal;
        set => SetProperty(ref _superficieTotal, value);
    }

    public double TiempoEstimado
    {
        get => _tiempoEstimado;
        set => SetProperty(ref _tiempoEstimado, value);
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
            // TODO: Mostrar dialog
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task ExportarAsync()
    {
        try
        {
            // Usar storage provider para dialog
            var topLevel = Avalonia.Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
            if (topLevel?.MainWindow is Window window)
            {
                var file = await window.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    Title = "Guardar JSON",
                    DefaultExtension = "json",
                    FileTypeChoices = new List<FilePickerFileType>
                    {
                        new FilePickerFileType("JSON Files") { Patterns = new[] { "*.json" } }
                    }
                });

                if (file != null)
                {
                    var resultado = new Resultado
                    {
                        Zonas = Zonas.ToList(),
                        SuperficieTotal = SuperficieTotal,
                        TiempoEstimado = TiempoEstimado
                    };
                    await _exportService.ExportarAsync(resultado, file.Path.LocalPath);
                    // TODO: Mostrar éxito
                    Console.WriteLine("Exportado exitosamente.");
                }
            }
        }
        catch (Exception ex)
        {
            // TODO: Mostrar error
            Console.WriteLine($"Error al exportar: {ex.Message}");
        }
    }

    private async Task ShowMessage(string title, string message)
    {
        // TODO: Implementar dialog
        Console.WriteLine($"{title}: {message}");
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