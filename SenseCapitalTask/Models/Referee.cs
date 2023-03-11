namespace SenseCapitalTask.Models
{
    public class Referee
    {
        private readonly int[,] winPositions = new int[,]
        { 
          { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
          { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
          { 0, 4, 8 }, { 2, 4, 6 } 
        };

        public bool IsItWin(string field)
        {
            for(int i = 0; i < winPositions.GetLength(0); i++) 
            {
                bool isWin = true;
                char c = field[winPositions[i, 0]];
                if (c != '*')
                {
                    for (int j = 1; j < winPositions.GetLength(1); j++)
                    {
                        if (c != field[winPositions[i, j]]) { isWin = false; }
                    }

                    if (isWin) { return true; }
                }
            }

            return false;
        }
    }
}
