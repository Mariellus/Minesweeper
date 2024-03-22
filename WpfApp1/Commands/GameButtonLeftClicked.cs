using System;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.DataModel;

namespace WpfApp1.Command;

public class GameButtonLeftClicked : ICommand {
    private Button _button;
    private GameViewModel _gameViewModel;
    
    public GameButtonLeftClicked(Button button, GameViewModel gameViewModel)
    {
        _button = button;
        _gameViewModel = gameViewModel;
    }
    
    public bool CanExecute(object? parameter)
    {
        return !(_button.Content is string and "🚩");
    }

    public void Execute(object? parameter)
    {
        _gameViewModel.Game.MineButtonLeftClicked(_button);
    }

    public event EventHandler? CanExecuteChanged;
}