using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfApp1.DataModel;

public class GameTimer {
    private TextBlock timeTextBlock;
    private DispatcherTimer dispatcherTimer = new();
    private int duration = 0;
    
    public GameTimer (TextBlock timeTextBlock)
    {
        this.timeTextBlock = timeTextBlock;
        timeTextBlock.Text = "0";
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0,0,1);
        dispatcherTimer.Start();
    }
    
    private void dispatcherTimer_Tick(object? sender, EventArgs e)
    {
        duration++;
        timeTextBlock.Text = duration.ToString();
    }
    
}