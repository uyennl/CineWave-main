using System.Diagnostics;
using CineWave.Helpers;
using CineWave.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Login;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty] private bool _canLogin;
    [ObservableProperty] private bool _isLoginFormVisible = true;
    private string? _password;
    private string? _username;

    public string? Username
    {
        get => _username;
        set
        {
            SetProperty(ref _username, value);
            UpdateCanLogin();
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value);
            UpdateCanLogin();
        }
    }


    private bool CanExecuteLogin()
    {
        return CanLogin;
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedMember.Global
    public void Login()
    {
        //var isAuthenticated = Password != null && Username == PseudoRandomEncryptor.Decrypt("YWRtaW4=") && Password == PseudoRandomEncryptor.Decrypt("bmltZGE=");
        var isAuthenticated = Password != null && Username == "admin" && Password == "123";
        if (!isAuthenticated) return;
        IsLoginFormVisible = false;
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        App.ServiceProvider.GetRequiredService<MainWindow>().Show(); 
        App.ServiceProvider.GetRequiredService<MainViewModel>().NavigateToHome();
    }

    private void UpdateCanLogin()
    {
        CanLogin = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
    }
}