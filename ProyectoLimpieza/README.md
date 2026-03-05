# Proyecto: Cálculo de áreas y estimación de limpieza

Este proyecto muestra varias implementaciones para calcular áreas de zonas en paralelo y estimar el tiempo de limpieza.

Contenido:
- Código base (Task.Run)
- Versión con Parallel.ForEach
- Versión con Threads manuales
- Versión con TPL Dataflow
- Versión GUI (WinForms)
- Versión WPF (con CQRS, async/await, TPL, reactiva, JSON export)
- Versión Avalonia (cross-platform con Avalonia UI, CQRS, async/await, TPL, reactiva, JSON export)

Fórmula usada:
- Área = largo * ancho
- Tiempo (s) = Superficie total (cm²) / Tasa de limpieza (cm²/s)
