using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.DataModel;

namespace WpfApp1.Commands
{
    public class MainMiddleGame : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            GameWindow gameWindow = new GameWindow(16, 16, 40);
            gameWindow.ShowDialog();
        }
    }
}
