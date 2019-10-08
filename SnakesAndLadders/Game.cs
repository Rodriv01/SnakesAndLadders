using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    public class Game
    {
        Player currentPlayer;
        Box[] generatedBoard;
        Board emptyBoard;
        Player[] playerQueue;
        int totalPlayers;

        public Game(Board newBoard, int NumberOfPlayers)
        {
            totalPlayers = NumberOfPlayers;
            generatedBoard = newBoard.CreateBoard();
            playerQueue = AssignPlayers(totalPlayers);
        }

        private Player[] AssignPlayers(int numberOfPlayers)
        {
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i] = new Player();
                players[i].CurrentCellPosition = 0;
                players[i].PlayerNumber = i + 1;
                players[i].PlayerName = string.Format("Jugador {0}", i+1);
            }
            return players;
        }

        private void NextChance()
        {
            if (currentPlayer.PlayerNumber < totalPlayers)
            {
                currentPlayer = playerQueue[(currentPlayer.PlayerNumber - 1) + 1];
            }
            else
            {
                currentPlayer = playerQueue[0];
            }
        }

        private void CalculatePlayerPosition(int diceNumber)
        {
            Console.WriteLine("{0} lanza el dado, sale: {1}",currentPlayer.PlayerName, diceNumber);
            int moveLocation = currentPlayer.CurrentCellPosition;
            if ((moveLocation + diceNumber) <= generatedBoard.Length)
            {
                moveLocation = moveLocation + diceNumber;
                Console.WriteLine("{0} avanza a la casilla: {1}",currentPlayer.PlayerName, moveLocation);
            }
            else
            {
                Console.WriteLine("{0} se queda en la casilla {1}",currentPlayer.PlayerName, moveLocation);
            }

            while (generatedBoard[moveLocation - 1].GetType() == typeof(Snake) || generatedBoard[moveLocation - 1].GetType() == typeof(Ladder))
            {
                if (generatedBoard[moveLocation - 1].GetType() == typeof(Snake))
                {
                    moveLocation = (generatedBoard[moveLocation - 1] as Snake).PenaltyCell;
                    Console.WriteLine("Serpiente! {0}, cae a la casilla {1}", currentPlayer.PlayerName, moveLocation);
                }
                if (generatedBoard[moveLocation - 1].GetType() == typeof(Ladder))
                {
                    moveLocation = (generatedBoard[moveLocation - 1] as Ladder).AdvantageCell;
                    Console.WriteLine("Escalera! {0}, sube a la casilla {1}",currentPlayer.PlayerName, moveLocation);
                }
            }
            currentPlayer.CurrentCellPosition = moveLocation;
            Console.ReadKey();
        }
        public void Play()
        {
            int player1FirstRoll, player2FirstRoll;
            do
            {
                player1FirstRoll = Dice.Roll();
                Console.WriteLine("Juagdor 1 lanza el dado: {0}", player1FirstRoll);
                Console.ReadKey();
                player2FirstRoll = Dice.Roll();
                Console.WriteLine("Juagdor 2 lanza el dado: {0}", player2FirstRoll);
                if (player1FirstRoll > player2FirstRoll)
                {
                    Console.WriteLine("Empieza el jugador 1");
                    currentPlayer = playerQueue[0];
                }
                else if (player2FirstRoll > player1FirstRoll)
                {
                    Console.WriteLine("Empieza el jugador 2");
                    currentPlayer = playerQueue[1];
                }
            } while (player1FirstRoll == player2FirstRoll);

            bool isFirstMove = true;
            while (currentPlayer.CurrentCellPosition != generatedBoard.Length)
            {
                if (!isFirstMove)
                {
                    NextChance();
                }
                isFirstMove = false;
                CalculatePlayerPosition(Dice.Roll());
            }
            Console.WriteLine("{0} Gana!", currentPlayer.PlayerName);
            foreach (Player p in playerQueue)
            {
                Console.WriteLine("{0} se encuentra en {1}", p.PlayerName, p.CurrentCellPosition);
            }
            Console.WriteLine("Game Over!");
        }
    }
}
