using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using PacManVersion.ViewModels;
using PacManVersion.Views;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PacManVersion;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        services.AddMediatR(typeof(App).Assembly);
        services.AddSingleton<GameEngine>();
        services.AddSingleton<EventBus>();
        services.AddSingleton<SidecarLogger>();
        var serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(serviceProvider.GetRequiredService<IMediator>(), serviceProvider.GetRequiredService<GameEngine>(), serviceProvider.GetRequiredService<EventBus>())
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}