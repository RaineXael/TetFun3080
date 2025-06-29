using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Board data class responsible for holding the current board state and clearing certain blocks (not gameplay)

namespace TetFun3080
{
    public class Board
    {
        private int width = 10;
        private int height = 20; 
        public int bufferHeight = 20;

        public int[,] boardState = null;

        public Vector2 spawnPosition;

        public Board(int width = 10, int height = 20, int bufferHeight = 20) { 
            this.width = width;
            this.height = height;
            this.bufferHeight = bufferHeight;
            spawnPosition = new Vector2((width/2)-1, height - bufferHeight);
            SetupBoard();
        }

        public Board(int[,] initialState)
        {

            SetupBoard(initialState);
        }

        private void SetupBoard(int[,] initialState = null)
        {
            if (initialState != null)
            {
                boardState = initialState;
            }
            else
            {
                //[x,y]
                boardState = new int[width, height + bufferHeight];

            }
            ScanForLines();


        }
        /**
         * Returns the line clear count.
         */
        public int ScanForLines()
        {
            int clearedLines = 0;
            for (int y = 0; y < boardState.GetLength(1); y++)
            {
                bool lineHasHole = false;
                for (int x = 0; x < boardState.GetLength(0); x++)
                {
                    Console.WriteLine(x);
                    if (boardState[x,y] == 0)
                    {
                        //An empty block. Break and go to next line.
                        lineHasHole = true;
                        break;
                    }
                }
                if (!lineHasHole)
                {
                    clearedLines++;
                }

            }
            
            return clearedLines;
        }
    }
}
