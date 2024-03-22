using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Command;
using WpfApp1.Commands;
using WpfApp1.DataModel;

namespace WpfApp1;

public class GameWindow : Window
{
    public Grid RootGrid { get; }

    public GameWindow(int gameWidth, int gameHeight, int numberOfMines)
    {
        RootGrid = new Grid();
        GameViewModel gameViewModel = new(gameWidth, gameHeight, numberOfMines);

        for (int i = 0; i < gameWidth; i++)
        {
            RootGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int i = 0; i < gameHeight; i++)
        {
            RootGrid.RowDefinitions.Add(new RowDefinition());
        }

        for (int x = 0; x < gameWidth; x++)
        {
            for (int y = 0; y < gameHeight; y++)
            {
                Button button = new() { Name = "Button" + x + "Z" + y, MinHeight = 30, MinWidth = 30 };
                MouseBinding lefClick = new MouseBinding
                {
                    MouseAction = MouseAction.LeftClick,
                    Command = new GameButtonLeftClicked(button, gameViewModel)
                };
                button.InputBindings.Add(lefClick);
                MouseBinding rightClick = new MouseBinding
                {
                    MouseAction = MouseAction.RightClick,
                    Command = new GameButtonRightClicked(button)
                };
                button.InputBindings.Add(rightClick);
                gameViewModel.Game.Buttons[x, y] = button;
                Grid.SetColumn(button, x);
                Grid.SetRow(button, y);
                RootGrid.Children.Add(button);
            }
        }

        // Add the RootGrid to the content of the window
        Content = RootGrid;

        // fit the window size to the size of the RootGrid
        SizeToContent = SizeToContent.WidthAndHeight;

    }
}