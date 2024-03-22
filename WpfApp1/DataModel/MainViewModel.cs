using System.ComponentModel;
using WpfApp1.Commands;

namespace WpfApp1.DataModel;

public class MainViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel()
    {
        Close = new MainClose(this);
    }
    public MainClose Close { get; set; }
}