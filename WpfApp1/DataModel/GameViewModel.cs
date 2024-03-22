using System.ComponentModel;
using System.Windows.Controls;

namespace WpfApp1.DataModel;

public class GameViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public GameViewModel(int gameWidth, int gameHeight, int numberOfMines)
    {
        Game = new Game(gameWidth, gameHeight, numberOfMines);
    }

    public Game Game {get; private set;}
}