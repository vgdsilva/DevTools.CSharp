namespace DevTools.Utils.Classes;

public class ProgressBar
{
    private const char ProgressChar = '#'; //'▒';

    private readonly int _total;
    private int _current;

    public ProgressBar(int total)
    {
        _total = total;
        _current = 0;
    }

    public void Update()
    {
        _current++;
        Draw();
    }

    private void Draw()
    {
        int progressWidth = Console.WindowWidth - 20;

        Console.CursorLeft = 0;
        Console.CursorLeft = progressWidth;

        int progress =  _current * progressWidth  / _total;

        Console.CursorLeft = 0;
        Console.Write(new string(ProgressChar, progress));

        Console.CursorLeft = progressWidth;
        Console.Write($" {progress * 100 / progressWidth}%");

        if ( _current == _total )
            Console.WriteLine();
    }
}
