using System;
using System.Windows.Controls;
using System.Windows;

namespace WpfApp1
{
    public class GameWindow : Window
    {
        Button[,] buttons = new Button[16, 16];
        bool[,] mines = new bool[16, 16];
        private Random random = new Random();
        private bool gameOver = false;
        private int numOfRevealedTiles = 0;

        public Grid RootGrid { get; private set; }

        public GameWindow()
        {
            for (int i = 0; i < 40; i++)
            {
                int x = random.Next(0, 16);
                int y = random.Next(0, 16);
                while (mines[x, y])
                {
                    x = random.Next(0, 16);
                    y = random.Next(0, 16);
                }
                mines[x, y] = true;
            }

            RootGrid = new Grid();

            for (int i = 0; i < 16; i++)
            {
                this.RootGrid.RowDefinitions.Add(new RowDefinition());
                this.RootGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Button button = new Button() { Name = "Button" + i + "Z" + j, MinHeight = 30, MinWidth = 30 };
                    button.Click += ButtonLeftClick;
                    button.MouseRightButtonDown += ButtonRightClick;
                    buttons[i, j] = button;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    this.RootGrid.Children.Add(button);
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

        private void ButtonLeftClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button button = (Button)sender;
                if (button.Content is string && (string)button.Content == "🚩") return;
                var coords = button.Name.Replace("Button", "").Split("Z");
                var x = int.Parse(coords[0]);
                int y = int.Parse(coords[1]);
                var hit = mines[x, y];
                if (hit)
                {
                    if (gameOver)
                    {
                        button.Content = "💣";
                    }
                    else
                    {
                        button.Content = "💥";
                    }
                    if (!gameOver)
                    {
                        gameOver = true;
                        foreach (var item in buttons)
                        {
                            if (item.Content == null)
                            {
                                ButtonLeftClick(item, e);
                            }
                        }
                    }
                }
                else
                {
                    var minesCount = GetSurroundingMines(x, y);
                    button.Content = minesCount;
                    if (minesCount == 0)
                    {
                        ClearSurrounding0Fields(x, y);
                    }
                    if (++numOfRevealedTiles == 16 * 16 - 40)
                    {
                        button.Content = "You win";
                    }
                }
            }
        }

        private void ButtonRightClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button button = (Button)sender;
                if (button.Content is string && (string)button.Content == "🚩")
                {
                    button.Content = null;
                }
                else if (button.Content == null)
                {
                    button.Content = "🚩";
                }
            }
        }

        private int GetSurroundingMines(int x, int y)
        {
            int minesAround = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    try
                    {
                        if (mines[x + i, y + j])
                        {
                            minesAround++;
                        }
                    }
                    catch { }
                }
            }
            return minesAround;
        }
        private void ClearSurrounding0Fields(int x, int y)
        {

            //Check above
            if (x > 0 && !mines[x - 1, y] && buttons[x - 1, y].Content is not 0)
            {
                var surroundingMines = GetSurroundingMines(x - 1, y);
                buttons[x - 1, y].Content = surroundingMines;
                if (surroundingMines == 0)
                {
                    ClearSurrounding0Fields(x - 1, y);
                }

            }
            //Check below
            if (x < mines.GetLength(0) - 1 && !mines[x + 1, y] && buttons[x + 1, y].Content is not 0)
            {
                var surroundingMines = GetSurroundingMines(x + 1, y);
                buttons[x + 1, y].Content = surroundingMines;
                if (surroundingMines == 0)
                {
                    ClearSurrounding0Fields(x + 1, y);
                }
            }
            //Check right
            if (y < mines.GetLength(1) - 1 && !mines[x, y + 1] && buttons[x, y + 1].Content is not 0)
            {
                var surroundingMines = GetSurroundingMines(x, y + 1);
                buttons[x, y + 1].Content = surroundingMines;
                if (surroundingMines == 0)
                {
                    ClearSurrounding0Fields(x, y + 1);
                }
            }
            //Check left
            if (y > 0 && !mines[x, y - 1] && buttons[x, y - 1].Content is not 0)
            {
                var surroundingMines = GetSurroundingMines(x, y - 1);
                buttons[x, y - 1].Content = surroundingMines;
                if (surroundingMines == 0)
                {
                    ClearSurrounding0Fields(x, y - 1);
                }
            }
        }
    }
}
