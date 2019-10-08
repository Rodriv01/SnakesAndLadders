using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders
{
    public static class Dice
    {
        public static int Roll()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 6);

            return randomNumber;
        }
    }
}
