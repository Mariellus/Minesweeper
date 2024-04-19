using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.Command;
using WpfApp1.Commands;
using WpfApp1.DataModel;

namespace WpfApp1;

public class GameWindow : Window
{
    public Grid GameGrid { get; }
    public Grid RootGrid { get; }
    public Grid TopBarGrid { get; }

    public GameWindow(int gameWidth, int gameHeight, int numberOfMines)
    {
        RootGrid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition()
            }
        };

        TopBarGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition()
            },
        };

        Grid.SetRow(TopBarGrid, 0);
        RootGrid.Children.Add(TopBarGrid);

        Thickness topElementMargin = new Thickness();
        topElementMargin.Bottom = 5;
        topElementMargin.Left = 5;
        topElementMargin.Right = 5;
        topElementMargin.Top = 5;

        TextBlock time = new TextBlock
        {
            Margin = topElementMargin,
            FontSize = 14,
            Foreground = Brushes.Red,
            FontFamily = new FontFamily("DSEG7 Modern"),
        };
        TopBarGrid.Children.Add(time);

        TextBlock remainingMines = new TextBlock
        {
            Margin = topElementMargin,
            FontSize = 14,
            Foreground = Brushes.Red,
            FontFamily = new FontFamily("DSEG7 Modern"),
            Text = numberOfMines.ToString()
        };
        Grid.SetColumn(remainingMines, 2);
        TopBarGrid.Children.Add(remainingMines);

        GameGrid = new Grid();
        GameViewModel gameViewModel = new(gameWidth, gameHeight, numberOfMines, time);

        for (int i = 0; i < gameWidth; i++)
        {
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int i = 0; i < gameHeight; i++)
        {
            GameGrid.RowDefinitions.Add(new RowDefinition());
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
                    Command = new GameButtonRightClicked(button, remainingMines)
                };
                button.InputBindings.Add(rightClick);
                gameViewModel.Game.Buttons[x, y] = button;
                Grid.SetColumn(button, x);
                Grid.SetRow(button, y);
                GameGrid.Children.Add(button);
            }
        }

        Grid.SetRow(GameGrid, 1);
        RootGrid.Children.Add(GameGrid);
        // Add the RootGrid to the content of the window
        Content = RootGrid;

        // fit the window size to the size of the RootGrid
        SizeToContent = SizeToContent.WidthAndHeight;

    }
}