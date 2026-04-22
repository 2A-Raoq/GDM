using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Gdm.UI.ViewModels;

namespace Gdm.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);
        Closing += OnClosing;
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.LockOnExit();
        }
    }
}
