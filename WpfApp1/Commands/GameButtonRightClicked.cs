using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1.Commands;

public class GameButtonRightClicked : ICommand {
    private readonly Button _button;
    private readonly TextBlock _remainingMines;
    
    public GameButtonRightClicked(Button button, TextBlock remainingMines)
    {
        _button = button;
        _remainingMines = remainingMines;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        int numberOfMines = int.Parse(_remainingMines.Text);

        if (_button.Content is string and "🚩")
        {
            _button.Content = null;
            numberOfMines++;
        }
        else if (_button.Content == null)
        {
            _button.Content = "🚩";
            numberOfMines--;
        }
        _remainingMines.Text = numberOfMines.ToString();
    }

    public event EventHandler? CanExecuteChanged;
}