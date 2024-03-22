using System.ComponentModel;
using System.Windows.Controls;

namespace WpfApp1.DataModel;

public class GameViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public Game Game {get; private set;} = new Game();
}