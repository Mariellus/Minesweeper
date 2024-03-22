using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.DataModel;

namespace WpfApp1.Commands
{
    public class MainEasyGame : ICommand
    {
        private MainViewModel viewModel;
        public MainEasyGame(MainViewModel mainViewModel)
        {
            this.viewModel = mainViewModel;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            GameWindow gameWindow = new GameWindow(8, 8, 10);
            gameWindow.ShowDialog();
        }
    }
}
