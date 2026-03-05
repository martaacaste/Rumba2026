# Versión Avalonia del Robot Aspirador

Esta versión implementa una aplicación de escritorio cross-platform usando Avalonia UI, que simula el procesamiento de datos de un robot aspirador, siguiendo los principios de arquitectura moderna.

## Arquitectura

- **CQRS**: Separación entre comandos (cálculos) y queries (visualización).
- **Concurrencia Asíncrona**: Uso de async/await y TPL para procesar 4 zonas en paralelo con Task.Run.
- **Gestión de Estado Reactiva**: La GUI se actualiza automáticamente mediante ViewModelBase con INotifyPropertyChanged.
- **Thread Safety**: Suma de superficies después de completar todas las tareas.
- **Manejo de Errores**: Try-catch global en operaciones asíncronas.
- **RAII**: Recursos manejados automáticamente por .NET.

## Funcionalidades

- Cálculo paralelo de áreas para 4 zonas predefinidas.
- Estimación de tiempo basada en 1000 cm²/s.
- Exportación de resultados a JSON usando System.Text.Json.

## Requisitos

- .NET 9.0 o superior
- Sistema con soporte gráfico (X11 en Linux, etc.)

## Ejecución

1. En un entorno con GUI: `dotnet run`
2. La app mostrará una ventana con DataGrid de zonas, botones para calcular y exportar.

Nota: En entornos headless como este contenedor, requiere configuración adicional para ejecución sin display. El código está preparado para ejecución en sistemas con GUI.