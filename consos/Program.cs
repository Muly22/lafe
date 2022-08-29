using lafelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace consos
{
    internal class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static void Main(string[] args)
        {
            Console.ReadLine();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(GetConsoleWindow(), 3);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            var gameEngin = new GameEngin
            (
                rows: 168,
                cols: 630,
                size: 2
            );
            while (true)
            {
                Console.Title = gameEngin.current.ToString();
                var field = gameEngin.GetCurr();
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    var str = new char[field.GetLength(0)];

                    for (int x = 0; x < field.GetLength(0); x++)
                    {
                        if (field[x, y])
                            str[x] = '#';
                        else
                            str[x] = ' ';
                    }
                    Console.WriteLine(str);
                }
                Console.SetCursorPosition(0, 0);
                gameEngin.Print();
            }
        }
    }
}
