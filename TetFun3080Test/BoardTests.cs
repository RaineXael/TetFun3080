using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Gameplay;

namespace TetFun3080Test
{
    [TestClass]
    public sealed class BoardTests
    {
        [TestMethod] 
        public void BoardLineCheckTest()
        {
            int[,] initialBoard = new int[10, 40];
            for (int x = 0; x < initialBoard.GetLength(0); x++)
            {
                for (int y = 0; y < initialBoard.GetLength(1); y++) // Fix loop to iterate over correct dimension
                {
                    initialBoard[x, y] = 1; // Initialize board values (example logic)
                }
            }

            Board board = new Board(initialBoard); // Fix declaration of 'board' to remove 'public' keyword

            int linesCleared = board.ScanForLines(); // Call ScanForLines to check for cleared lines


            Assert.IsTrue(linesCleared == 40, "Board should have cleared lines based on initial state.");
        }
    }
}

