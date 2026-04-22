using System.Collections.ObjectModel;
using Gdm.Application;

namespace Gdm.UI.ViewModels;

public sealed class MainWindowViewModel : ObservableObject
{
    private readonly VaultSessionService _session;
    private readonly IPasswordGenerator _generator;

    private string _masterPassword = string.Empty;
    private string _searchQuery = string.Empty;
    private string _editServiceName = string.Empty;
    private string _editLogin = string.Empty;
    private string _editPassword = string.Empty;
    private string _editNote = string.Empty;
    private string _message = "Mode local hors ligne";
    private VaultEntryDto? _selectedEntry;

    public MainWindowViewModel(VaultSessionService session, IPasswordGenerator generator)
    {
        _session = session;
        _generator = generator;
        Entries = new ObservableCollection<VaultEntryDto>();

        CreateCommand = new RelayCommand(CreateAsync);
        UnlockCommand = new RelayCommand(UnlockAsync);
        LockCommand = new RelayCommand(LockAsync);
        SearchCommand = new RelayCommand(SearchAsync);
        AddCommand = new RelayCommand(AddAsync);
        UpdateCommand = new RelayCommand(UpdateAsync);
        GeneratePasswordCommand = new RelayCommand(GenerateAsync);
    }

    public ObservableCollection<VaultEntryDto> Entries { get; }
    public RelayCommand CreateCommand { get; }
    public RelayCommand UnlockCommand { get; }
    public RelayCommand LockCommand { get; }
    public RelayCommand SearchCommand { get; }
    public RelayCommand AddCommand { get; }
    public RelayCommand UpdateCommand { get; }
    public RelayCommand GeneratePasswordCommand { get; }

    public string MasterPassword
    {
        get => _masterPassword;
        set => SetProperty(ref _masterPassword, value);
    }

    public string SearchQuery
    {
        get => _searchQuery;
        set => SetProperty(ref _searchQuery, value);
    }

    public string EditServiceName
    {
        get => _editServiceName;
        set => SetProperty(ref _editServiceName, value);
    }

    public string EditLogin
    {
        get => _editLogin;
        set => SetProperty(ref _editLogin, value);
    }

    public string EditPassword
    {
        get => _editPassword;
        set => SetProperty(ref _editPassword, value);
    }

    public string EditNote
    {
        get => _editNote;
        set => SetProperty(ref _editNote, value);
    }

    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public VaultEntryDto? SelectedEntry
    {
        get => _selectedEntry;
        set
        {
            if (!SetProperty(ref _selectedEntry, value) || value is null)
            {
                return;
            }

            EditServiceName = value.ServiceName;
            EditLogin = value.Login;
            EditPassword = value.Password;
            EditNote = value.Note ?? string.Empty;
        }
    }

    private async Task CreateAsync()
    {
        await ExecuteSafe(async () =>
        {
            await _session.CreateVaultAsync(MasterPassword, CancellationToken.None);
            Message = "Coffre créé.";
            await SearchAsync();
        });
    }

    private async Task UnlockAsync()
    {
        await ExecuteSafe(async () =>
        {
            await _session.UnlockAsync(MasterPassword, CancellationToken.None);
            Message = "Coffre déverrouillé.";
            await SearchAsync();
        });
    }

    private async Task LockAsync()
    {
        LockOnExit();
        Message = "Coffre verrouillé.";
        await Task.CompletedTask;
    }

    public void LockOnExit()
    {
        _session.Lock();
        Entries.Clear();
    }

    private async Task SearchAsync()
    {
        await ExecuteSafe(() =>
        {
            var results = _session.Search(SearchQuery);
            Entries.Clear();
            foreach (var item in results)
            {
                Entries.Add(item);
            }

            return Task.CompletedTask;
        });
    }

    private async Task AddAsync()
    {
        await ExecuteSafe(async () =>
        {
            await _session.AddEntryAsync(
                EditServiceName,
                EditLogin,
                EditPassword,
                EditNote,
                CancellationToken.None);
            Message = "Entrée ajoutée.";
            await SearchAsync();
        });
    }

    private async Task UpdateAsync()
    {
        await ExecuteSafe(async () =>
        {
            if (SelectedEntry is null)
            {
                Message = "Sélectionnez une entrée.";
                return;
            }

            await _session.UpdateEntryAsync(
                SelectedEntry.Id,
                EditServiceName,
                EditLogin,
                EditPassword,
                EditNote,
                CancellationToken.None);
            Message = "Entrée modifiée.";
            await SearchAsync();
        });
    }

    private async Task GenerateAsync()
    {
        await ExecuteSafe(() =>
        {
            EditPassword = _generator.Generate(PasswordPolicy.Default);
            Message = "Mot de passe généré.";
            return Task.CompletedTask;
        });
    }

    private async Task ExecuteSafe(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }
}
