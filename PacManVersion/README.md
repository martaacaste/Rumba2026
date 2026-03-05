# Versión Pac-Man del Proyecto

Esta versión implementa un simulador de Pac-Man utilizando arquitecturas modernas de sistemas distribuidos en C# con Avalonia UI.

## Arquitectura Implementada

### Fase 1: Estructura Base y API Gateway
- **InputController**: Controlador central que valida y enruta comandos de movimiento usando MediatR.
- Rate limiting simulado para evitar movimientos excesivos.

### Fase 2: Movimiento y CQRS
- **Comandos**: `MovePacManCommand`, `EatPillCommand` para writes.
- **Queries**: `GetGameStateQuery` para reads optimizados.
- Separación clara entre lógica de negocio y presentación.

### Fase 3: Inteligencia de los Fantasmas y Publish/Subscribe
- **EventBus**: Pac-Man publica `PacManMovedEvent`, fantasmas se suscriben y reaccionan.
- Desacoplamiento total: añadir/quitar fantasmas sin modificar Pac-Man.

### Fase 4: Lógica de Colisiones y Patrón Saga
- **Saga Pattern**: Al comer píldora de poder, secuencia asíncrona con compensación.
- Manejo de transacciones complejas (estado de fantasmas, temporizador).

### Fase 5: Monitoreo y Patrón Sidecar
- **SidecarLogger**: Servicio independiente para logs en JSON, sin interferir con el motor del juego.

## Tecnologías
- **Avalonia UI**: Cross-platform desktop.
- **MediatR**: Para CQRS.
- **EventBus**: Pub/Sub interno.
- **Async/Await**: Concurrencia.
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection.

## Requisitos
- .NET 9.0
- Sistema con display gráfico.

## Ejecución
1. `dotnet run` en entorno con GUI.
2. Usa botones para mover Pac-Man, observa reacción de fantasmas.

Esta implementación transforma Pac-Man en un sistema escalable y profesional, aplicando patrones de alto rendimiento.