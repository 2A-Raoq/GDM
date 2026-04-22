using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gdm.UI.ViewModels;

public sealed class RelayCommand : ICommand
{
    private readonly Func<Task> _execute;

    public RelayCommand(Func<Task> execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        await _execute();
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}