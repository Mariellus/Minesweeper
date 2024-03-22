using System;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1.Commands;

public class GameButtonRightClicked : ICommand {
    private readonly Button _button;
    
    public GameButtonRightClicked(Button button)
    {
        _button = button;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (_button.Content is string and "🚩")
        {
            _button.Content = null;
        }
        else if (_button.Content == null)
        {
            _button.Content = "🚩";
        }
    }

    public event EventHandler? CanExecuteChanged;
}