using System.Collections.Concurrent;

namespace PacManVersion.Services;

public class EventBus
{
    private readonly ConcurrentDictionary<Type, List<Delegate>> _handlers = new();

    public async Task Publish<T>(T eventData) where T : class
    {
        if (_handlers.TryGetValue(typeof(T), out var handlers))
        {
            foreach (var handler in handlers)
            {
                await (Task)handler.DynamicInvoke(eventData);
            }
        }
    }

    public void Subscribe<T>(Func<T, Task> handler) where T : class
    {
        var eventType = typeof(T);
        _handlers.AddOrUpdate(eventType,
            new List<Delegate> { handler },
            (key, list) => { list.Add(handler); return list; });
    }
}