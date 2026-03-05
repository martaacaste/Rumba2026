using System.ComponentModel;

namespace WPFVersion.Models;

public class Zona : INotifyPropertyChanged
{
    private string _nombre;
    private double _largo;
    private double _ancho;
    private double _area;
    private string _estado;

    public string Nombre
    {
        get => _nombre;
        set { _nombre = value; OnPropertyChanged(); }
    }

    public double Largo
    {
        get => _largo;
        set { _largo = value; OnPropertyChanged(); }
    }

    public double Ancho
    {
        get => _ancho;
        set { _ancho = value; OnPropertyChanged(); }
    }

    public double Area
    {
        get => _area;
        set { _area = value; OnPropertyChanged(); }
    }

    public string Estado
    {
        get => _estado;
        set { _estado = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}