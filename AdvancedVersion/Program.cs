using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AdvancedVersion.Services;
using AdvancedVersion.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

class Program
{
    [STATHREAD]
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=robot.db"));
                services.AddMediatR(typeof(Program).Assembly);
                services.AddSingleton<EventBus>();
                services.AddSingleton<SidecarLogger>();
                services.AddSingleton<AntiCorruptionLayer>();
                services.AddSingleton<ServiceDiscovery>();
                services.AddHostedService<OutboxProcessor>();
                services.AddHostedService<ReplicationService>();
                services.AddTransient<GameEngine>();
            })
            .Build();

        // Ensure database is created
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        }

        // Run Avalonia app
        AvaloniaApp.Run(host.Services);
    }
}

public class AvaloniaApp
{
    public static void Run(IServiceProvider services)
    {
        var app = new App(services);
        app.Run();
    }
}