using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.DataModel;

namespace WpfApp1.Commands
{
    public class MainHardGame:ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            GameWindow gameWindow = new GameWindow(30, 16, 99);
            gameWindow.ShowDialog();
        }

    }
}
