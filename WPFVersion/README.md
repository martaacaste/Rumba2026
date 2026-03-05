# Versión WPF del Robot Aspirador

Esta versión implementa una aplicación de escritorio WPF que simula el procesamiento de datos de un robot aspirador, siguiendo los principios de arquitectura moderna.

## Arquitectura

- **CQRS**: Separación entre comandos (cálculos) y queries (visualización).
- **Concurrencia Asíncrona**: Uso de async/await y TPL para procesar 4 zonas en paralelo con Task.Run.
- **Gestión de Estado Reactiva**: La GUI se actualiza automáticamente mediante INotifyPropertyChanged.
- **Thread Safety**: Suma de superficies después de completar todas las tareas.
- **Manejo de Errores**: Try-catch global en operaciones asíncronas.
- **RAII**: Recursos manejados automáticamente por .NET.

## Funcionalidades

- Cálculo paralelo de áreas para 4 zonas predefinidas.
- Estimación de tiempo basada en 1000 cm²/s.
- Exportación de resultados a JSON usando System.Text.Json.

## Requisitos

- .NET 8.0 o superior
- Windows (WPF es específico de Windows)

## Ejecución

1. Abrir en Visual Studio en Windows.
2. Compilar y ejecutar.

Nota: Dado que el entorno actual es Linux, esta versión requiere Windows para ejecutarse. El código está preparado para desarrollo en Windows.