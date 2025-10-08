using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;

//Board data class responsible for holding the current board state and clearing certain blocks (not gameplay)

namespace TetFun3080.Gameplay
{
    public class Board
    {
        public int width = 10;
        public int height = 20; 
        public int bufferHeight = 20;

        public int[,] boardState = null;

        public Vector2 spawnPosition;

        public Board(int width = 10, int height = 20, int bufferHeight = 20) { 
            this.width = width;
            this.height = height;
            this.bufferHeight = bufferHeight;
            spawnPosition = new Vector2(width/2-1, height-1);
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
            ScanAndRemoveLines();


        }
        /**
         * Returns the line clear count.
         */
        public List<int> ScanAndRemoveLines()
        {
            List<int> result = new List<int>();
            
            for (int y = 0; y < boardState.GetLength(1); y++)
            {
                bool lineHasHole = false;
                for (int x = 0; x < boardState.GetLength(0); x++)
                {
                   
                    if (boardState[x,y] == 0)
                    {
                        //An empty block. Break and go to next line.
                        lineHasHole = true;
                        break;
                    }
                }
                if (!lineHasHole)
                {
                    //this line is clear, remove it and downshift all lines above it
                    result.Add(y);
                    for (int x = 0; x < boardState.GetLength(0); x++)
                    {
                        //shift all lines above this line down by one
                        for (int y2 = y; y2 > 0; y2--)
                        {
                            boardState[x, y2] = boardState[x, y2 - 1];
                        }
                        //clear the top line
                        boardState[x, 0] = 0;
                    }
                }
            }
            return result;
        }

        public int GetStackHeight()
        {
            //Returns the size of the stack, the peak of the tower
            int count = 0;
            for (int y = height+bufferHeight-1; y >=0; y--)
            {
                bool emptyRow = true;
                for( int x = 0; x < boardState.GetLength(0); x++)
                {
                    if (boardState[x,y] != 0)
                    {
                        emptyRow = false;
                        //there's a block, add to count and break
                        count++;
                        break;
                    }
                    
                }
                //If not broken at this point, row is empty
                if (emptyRow)
                {
                    return count;
                }
                
            }
            return count;
        }
        public bool CheckIsEmptyCoord(Vector2 pos)
        {
           //Check if the position is within the board bounds
            if (pos.X >= 0 && pos.X < width && pos.Y >= 0 && pos.Y < height + bufferHeight && boardState[(int)pos.X, (int)pos.Y] == 0)
            {
                //return true if empty and valid
                return true;
            }
            return false;
        }

        internal void AddBlock(Pieces currentShape, Vector2 pilot_position, Vector2[] piecePositions)
        {
            foreach(Vector2 offset in piecePositions)
            {
                Vector2 truepos = pilot_position + offset;
                if (CheckIsEmptyCoord(truepos))
                {
                    boardState[(int)truepos.X, (int)truepos.Y] = (int)currentShape;
                }
                
            }
        }
    }
}
