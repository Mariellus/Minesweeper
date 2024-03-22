using System.ComponentModel;
using WpfApp1.Commands;

namespace WpfApp1.DataModel;

public class MainViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel()
    {
        Close = new MainClose(this);
        EasyGame = new MainEasyGame();
        MiddleGame = new MainMiddleGame(this);
        HardGame = new MainHardGame();
        
    }
    public MainClose Close { get; set; }
    public MainEasyGame EasyGame { get; set; }
    public MainMiddleGame MiddleGame { get; set; }
    public MainHardGame HardGame { get; set; }
}