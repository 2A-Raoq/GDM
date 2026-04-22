using Gdm.Application;
using Gdm.Infrastructure;
using Gdm.UI.ViewModels;
using Gdm.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Gdm.UI;

public static class CompositionRoot
{
    public static MainWindow BuildMainWindow()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var provider = services.BuildServiceProvider();
        return new MainWindow
        {
            DataContext = provider.GetRequiredService<MainWindowViewModel>()
        };
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        var vaultPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GDM",
            "vault.gdm");

        services.AddSingleton<IVaultRepository>(_ => new FileVaultRepository(vaultPath));
        services.AddSingleton<ICryptoService, AesGcmCryptoService>();
        services.AddSingleton<IPasswordGenerator, PasswordGenerator>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddSingleton<VaultSessionService>();
        services.AddSingleton<MainWindowViewModel>();
    }
}
