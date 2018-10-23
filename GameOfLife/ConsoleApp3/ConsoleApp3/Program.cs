using System;
using System.Text;


class GameOfLife
{
    bool[,] field;

    public GameOfLife(int width, int high)
    {
        field = new bool[high, width];
        StartCond();
    }

    void StartCond()
    {
        Random rand = new System.Random();

        for (int line = 0; line < field.GetLength(0); ++line)
        {
            for (int col = 0; col < field.GetLength(1); ++col)
            {
                field[line, col] = rand.Next(1000) < 300 ? true : false;
            }
        }
    }

    public void ViewField()
    {
        StringBuilder sb = new StringBuilder();

        for (int line = 0; line < field.GetLength(0); ++line)
        {
            for (int col = 0; col < field.GetLength(1); ++col)
            {
                if (field[line, col])
                    sb.Append('X');
                else
                    sb.Append(' ');
            }
        }

        Console.Write(sb);
    }

    public void Refresh()
    {
        bool[,] newField = new bool[field.GetLength(0), field.GetLength(1)];

        for (int line = 0; line < field.GetLength(0); ++line)
        {
            for (int col = 0; col < field.GetLength(1); ++col)
            {
                newField[line, col] = CellIsLive(line, col);
            }
        }

        field = newField;

        Console.SetCursorPosition(0, 0);
        ViewField();
    }

    bool CellIsLive(int line, int col)
    {
        int numLiveNb = NumOfLiveNeigbors(line, col);

        if (field[line, col])
            return (numLiveNb >= 2 && numLiveNb <= 3);
        else
            return (numLiveNb == 3);
    }

    int NumOfLiveNeigbors(int line, int col)
    {
        int numLive = 0;

        for (int l = Math.Max(line - 1, 0); l <= Math.Min(line + 1, field.GetLength(0) - 1); ++l)
        {
            for (int c = Math.Max(col - 1, 0); c <= Math.Min(col + 1, field.GetLength(1) - 1); ++c)
            {
                if (field[l, c])
                    ++numLive;
            }
        }

        return numLive - (field[line, col]? 1 : 0);
    }
}


class InPoint
{
    static void Main()
    {
        int prevWidth = 0;
        int prevHeight = 0;
        GameOfLife game = null;

        while (true)
        {
            int width = Console.WindowWidth;
            int height = (Console.WindowHeight - 1);

            if (width != prevWidth || height != prevHeight || game == null)
            {
                Console.Clear();
                game = new GameOfLife(width, height);
                game.ViewField();

                prevWidth = width;
                prevHeight = height;
            }
            else
            {
                game.Refresh();
            }
        }
    }
}
