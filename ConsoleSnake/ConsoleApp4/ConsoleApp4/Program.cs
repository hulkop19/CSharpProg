using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

// ToDo feed respaun only on free cels

struct Point
{
    public int X, Y;

    public Point(int x, int y)
    {
        X = x; Y = y;
    }

    public void Deconstruct(out int x, out int y)
    {
        x = X; y = Y;
    }

    public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

    public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Point a, Point b) => !(a == b);

    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
    // public bool Equals(Point a) => X == a.X && Y == a.Y;
}

class SnakeGame
{
    int width;
    int height;
    Point direction = (1, 0);
    Point feedPos;
    Queue<Point> prevFeedPos;
    Random rand = new Random();


    List<Point> snake = new List<Point>
    {
        (2, 1),
        (2, 2),
        (3, 2),
    };

    public int SnakeLength
    {
        get { return snake.Count + 1; }
    }

    public SnakeGame(int w, int h)
    {
        width = w;
        height = h;
        feedPos = (height / 2, width / 2);
        prevFeedPos = new Queue<Point>();
    }

    public void ViewField()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                if (snake.Last() == (i, j))
                {
                    sb.Append('0');
                }
                else if (snake.Contains((i, j)))
                {
                    sb.Append('X');
                }
                else if (feedPos == (i, j))
                {
                    sb.Append('@');
                } else
                {
                    sb.Append(' ');
                }
            }

            if (i == 10)
                sb.Append("//" + "   Score: " + snake.Count + "\n");
            else
                sb.Append("//\n");
        }

        Console.SetCursorPosition(0, 0);
        Console.Write(sb);
    }

    public void ChangeDirection(ConsoleKey key)
    {
        Point newDirect;

        if (key == ConsoleKey.UpArrow)
        {
            newDirect = (-1, 0);
        }
        else if (key == ConsoleKey.DownArrow)
        {
            newDirect = (1, 0);
        }
        else if (key == ConsoleKey.LeftArrow)
        {
            newDirect = (0, -1);
        }
        else if (key == ConsoleKey.RightArrow)
        {
            newDirect = (0, 1);
        } else
        {
            return;
        }

        if ((newDirect + direction) != (0, 0))
            direction = newDirect;
    }

    public bool MoveSnake()
    {
        if (!FeedRiched())
        {
            snake.RemoveAt(0);
        }

        int it1 = snake.Last().X + direction.X;
        int it2 = snake.Last().Y + direction.Y;
        Point temp = ((it1 + height) % height, (it2 + width) % width);

        if (snake.Contains(temp))
        {
            return false;
        }

        snake.Add(temp);

        return true;
    }

    bool FeedRiched()
    {
        if (prevFeedPos.Count != 0 && prevFeedPos.Peek() == snake[0])
        {
            prevFeedPos.Dequeue();
            return true;
        }

        if (feedPos == snake.Last())
        {
            while (snake.Contains(feedPos))
            {
                feedPos = (rand.Next(0, width), rand.Next(0, height));
            }

            prevFeedPos.Enqueue(feedPos);
        }

        return false;
    }
}


class InPoint
{
    static public void Main()
    {
        int width = Console.WindowWidth;
        int height = Console.WindowHeight - 1;

        SnakeGame game = new SnakeGame(height, height);
        game.ViewField();



        while (true)
        {
            game.ChangeDirection(Console.ReadKey(true).Key);

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(100);
                if (game.MoveSnake())
                {
                    game.ViewField();
                }
                else
                {
                    Console.WriteLine("Game over!");
                    Console.WriteLine("Your Result: " + game.SnakeLength);
                    Console.WriteLine("Press Enter to end:");
                    Console.Read();
                    return;
                }
            }
        }
    }
}
