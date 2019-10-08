using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    public class Board
    {
        public int boardSize { get; set; }
        public int laddersNumber { get; set; }
        public int snakesNumber { get; set; }
        public int uppperLimit { get; set; }
        public int lowerLimit { get; set; }
        public Board(int BoardSize, int LaddersNumber, int SnakesNumber, int UpperLimit, int LowerLimit)
        {
            boardSize = BoardSize;
            laddersNumber = LaddersNumber;
            snakesNumber = SnakesNumber;
            uppperLimit = UpperLimit;
            lowerLimit = LowerLimit;
        }
        public Box[] CreateBoard()
        {
            Box[] board = new Box[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                Box box = new Box();
                box.BoxNumber = i + 1;
                board[i] = box;
            }
            int snakesGenerated = 0;
            do
            {
                Random random = new Random();
                int snakeCellNumber = random.Next(uppperLimit, boardSize);
                int penaltyCellNumber = random.Next(lowerLimit, snakeCellNumber);
                Console.WriteLine("Serpiente, inicio: {0} final {1}", snakeCellNumber, penaltyCellNumber);
                Snake newSnake = new Snake();
                newSnake.BoxNumber = snakeCellNumber;
                newSnake.PenaltyCell = penaltyCellNumber;
                board[snakeCellNumber - 1] = newSnake;
                snakesGenerated++;
                System.Threading.Thread.Sleep(500);
            } while (snakesGenerated <= snakesNumber);

            int laddersGenerated = 0;
            do
            {
                Random random = new Random();
                int ladderCellNumber = random.Next(lowerLimit, boardSize-uppperLimit);
                int advantageCellNumber = random.Next(ladderCellNumber, boardSize);
                Console.WriteLine("Escalera, inicio: {0} final {1}", ladderCellNumber, advantageCellNumber);
                Ladder newLadder = new Ladder();
                newLadder.BoxNumber = ladderCellNumber;
                newLadder.AdvantageCell = advantageCellNumber;
                board[ladderCellNumber - 1] = newLadder;
                laddersGenerated++;
                System.Threading.Thread.Sleep(500);
            } while (laddersGenerated <= laddersNumber);
            return board;
        }
    }
}
