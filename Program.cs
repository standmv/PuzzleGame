using System;
using System.Threading;

namespace PuzzleGame
{
    public enum GameState
    {
        Menu,
        Playing,
        End
    }

    public class Game
    {
        public GameState CurrentState { get; set; }
        public int[,] Board { get; set; }
       
    }

    class Program
    {
        public static Game CurrentGame;
        static Thread _inputThread;
        const int FPS = 33;
        const int LEFTMARGIN = 1, COLSIZE = 3, ROWSIZE = 1;
        static int aux;

        public static void Main(string[] args)
        {
            Bootstrap();
            while (CurrentGame.CurrentState != GameState.End)
            {
                switch (CurrentGame.CurrentState)
                {
                    case GameState.Menu:
                        //ShowFrame();
                        //UpdateBoard();
                        ShowMenu();
                        break;
                    case GameState.Playing:
                        ShowFrame();
                        UpdateBoard();
                        break;
                }
                Thread.Sleep(FPS);
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Juguemos! Puzzle Game");
            Console.WriteLine("\nSeleccione una opción:");
            Console.WriteLine("\n\t1: Jugar");
            Console.WriteLine("\t2: Salir");
            Console.WriteLine("\n\tSeleccione: ");
        }

        static void GetInput()
        {
            string _currentInput;
            while (true)
            {
                switch (CurrentGame.CurrentState)
                {
                    case GameState.Menu:
                        _currentInput = Console.ReadKey().KeyChar.ToString();
                        CurrentGame.CurrentState = _currentInput == "1" ? GameState.Playing : GameState.End;
                        break;
                    case GameState.Playing:
                        _currentInput = Console.ReadKey().KeyChar.ToString();
                        aux = Convert.ToInt32(_currentInput);
                        if (aux < 1 || aux > 8)
                            continue;

                        break;

                }
            }
        }

        public static void Bootstrap()
        {
            CurrentGame = new Game();
            CurrentGame.Board = new int[,] { { 6, 4, 3 }, { 5, 2, 7 }, { 1, 0, 8 } };
            CurrentGame.CurrentState = GameState.Menu;
            _inputThread = new Thread(GetInput);
            _inputThread.Start();
        }

        public static void ShowFrame()
        {
            Console.Clear();
            Console.WriteLine("[ ][ ][ ]");
            Console.WriteLine("[ ][ ][ ]");
            Console.WriteLine("[ ][ ][ ]");
            Console.WriteLine("Indique el movimiento [1-9]: ");
        }

        public static void EndGame()
        {
            _inputThread.Abort();
            _inputThread.Join();
        }

        public static void UpdateBoard()
        {
            int _current = 0;
            foreach (int number in CurrentGame.Board)
            {
                Console.SetCursorPosition(_current % 3 * COLSIZE + LEFTMARGIN, _current / 3 * ROWSIZE);
                if (number != 0)
                    Console.Write(number);
                _current++;
            }
        }
    }
}
