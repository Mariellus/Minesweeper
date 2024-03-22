using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Command;
using WpfApp1.Commands;
using WpfApp1.DataModel;

namespace WpfApp1
{
    public class GameWindow : Window
    {
        public Grid RootGrid { get; }
        private GameViewModel _gameViewModel;

        public GameWindow(int gameWidth, int gameHeight, int numberOfMines)
        {
            RootGrid = new Grid();
            _gameViewModel = new(gameWidth, gameHeight, numberOfMines);

            for (int i = 0; i < gameWidth; i++)
            {
                this.RootGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < gameHeight; i++)
            {
                this.RootGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int x = 0; x < gameWidth; x++)
            {
                for (int y = 0; y < gameHeight; y++)
                {
                    Button button = new Button() { Name = "Button" + x + "Z" + y, MinHeight = 30, MinWidth = 30 };
                    MouseBinding lefClick = new MouseBinding
                    {
                        MouseAction = MouseAction.LeftClick,
                        Command = new GameButtonLeftClicked(button, _gameViewModel)
                    };
                    button.InputBindings.Add(lefClick);
                    MouseBinding rightClick = new MouseBinding
                    {
                        MouseAction = MouseAction.RightClick,
                        Command = new GameButtonRightClicked(button)
                    };
                    button.InputBindings.Add(rightClick);
                    _gameViewModel.Game.Buttons[x, y] = button;
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    RootGrid.Children.Add(button);
                }
            }

            /* Create a new Textbox and place it in the middle of the root grid
            TextBox TextBox_Test = new TextBox()
            { Text = "ABC", Background = Brushes.Yellow, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Top };

            Grid.SetColumn(TextBox_Test, 1);
            Grid.SetRow(TextBox_Test, 1);
            this.RootGrid.Children.Add(TextBox_Test);

            Grid GridForButtons = new Grid()
            { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };

            Button Button_Close = new Button() { Content = "Close" };
            Button_Close.Click += Button_Close_Click;

            // Add the button to the grid which has one cell by default
            Grid.SetColumn(Button_Close, 0);
            Grid.SetRow(Button_Close, 0);
            GridForButtons.Children.Add(Button_Close);

            // add the button grid to the RootGrid
            Grid.SetRow(GridForButtons, 2);
            Grid.SetColumn(GridForButtons, 1);
            this.RootGrid.Children.Add(GridForButtons);*/

            // Add the RootGrid to the content of the window
            this.Content = this.RootGrid;

            // fit the window size to the size of the RootGrid
            this.SizeToContent = SizeToContent.WidthAndHeight;
            
        }
    }
}
