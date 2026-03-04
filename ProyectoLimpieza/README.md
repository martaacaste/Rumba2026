# Proyecto: Cálculo de áreas y estimación de limpieza

Este proyecto muestra varias implementaciones para calcular áreas de zonas en paralelo y estimar el tiempo de limpieza.

Contenido:
- Código base (Task.Run)
- Versión con Parallel.ForEach
- Versión con Threads manuales
- Versión con TPL Dataflow
- Versión GUI (WinForms)

Fórmula usada:
- Área = largo * ancho
- Tiempo (s) = Superficie total (cm²) / Tasa de limpieza (cm²/s)

Instrucciones para generar PDF:
1. Instala pandoc (https://pandoc.org) o abre el archivo en un editor y imprime a PDF.
2. Ejecuta: `pandoc README.md -o ProyectoLimpieza.pdf`

Licencia: contenido propio.