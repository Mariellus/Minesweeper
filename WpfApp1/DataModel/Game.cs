using System;
using System.Net.NetworkInformation;
using System.Windows.Controls;

namespace WpfApp1.DataModel;

public class Game {
    bool[,] mines;
    private Random random = new Random();
    private bool gameOver = false;
    private int numOfRevealedTiles = 0;
    private int _fieldsRequiredToWin;
    public Button[,] Buttons {get; private set;}
    
    public Game(int gameWidth, int gameHeight, int numberOfMines)
    {
        Buttons = new Button[gameWidth, gameHeight];
        mines = new bool[gameWidth,gameHeight];
        for (int i = 0; i < numberOfMines; i++)
        {
            int x = random.Next(0, gameWidth);
            int y = random.Next(0, gameHeight);
            while (mines[x, y])
            {
                x = random.Next(0, gameWidth);
                y = random.Next(0, gameHeight);
            }
            mines[x, y] = true;
            _fieldsRequiredToWin = gameWidth * gameHeight - numberOfMines;
        }
    }
    
    public void MineButtonLeftClicked(Button button)
    {
        var coords = button.Name.Replace("Button", "").Split("Z");
        var x = int.Parse(coords[0]);
        int y = int.Parse(coords[1]);
        var hit = mines[x, y];
        if (hit)
        {
            button.Content = gameOver ? "💣" : "💥";
            if (!gameOver)
            {
                gameOver = true;
                foreach (var item in Buttons)
                {
                    if (item.Content == null)
                    {
                        MineButtonLeftClicked(item);
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
            if (++numOfRevealedTiles == _fieldsRequiredToWin)
            {
                button.Content = "You win";
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
        if (x > 0 && !mines[x - 1, y] && Buttons[x - 1, y].Content is not 0)
        {
            var surroundingMines = GetSurroundingMines(x - 1, y);
            Buttons[x - 1, y].Content = surroundingMines;
            if (surroundingMines == 0)
            {
                ClearSurrounding0Fields(x - 1, y);
            }

        }
        //Check below
        if (x < mines.GetLength(0) - 1 && !mines[x + 1, y] && Buttons[x + 1, y].Content is not 0)
        {
            var surroundingMines = GetSurroundingMines(x + 1, y);
            Buttons[x + 1, y].Content = surroundingMines;
            if (surroundingMines == 0)
            {
                ClearSurrounding0Fields(x + 1, y);
            }
        }
        //Check right
        if (y < mines.GetLength(1) - 1 && !mines[x, y + 1] && Buttons[x, y + 1].Content is not 0)
        {
            var surroundingMines = GetSurroundingMines(x, y + 1);
            Buttons[x, y + 1].Content = surroundingMines;
            if (surroundingMines == 0)
            {
                ClearSurrounding0Fields(x, y + 1);
            }
        }
        //Check left
        if (y > 0 && !mines[x, y - 1] && Buttons[x, y - 1].Content is not 0)
        {
            var surroundingMines = GetSurroundingMines(x, y - 1);
            Buttons[x, y - 1].Content = surroundingMines;
            if (surroundingMines == 0)
            {
                ClearSurrounding0Fields(x, y - 1);
            }
        }
    }
}