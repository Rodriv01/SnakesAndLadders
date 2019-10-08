using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    class Program
    {
        static void Main(string[] args)
        {
            Board newBoard = new Board(50,5,5,20,5);

            Game g = new Game(newBoard, 2);
            g.Play();

            Console.ReadKey();
        }
    }
}
