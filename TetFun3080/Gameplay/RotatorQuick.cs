
using Microsoft.Xna.Framework;
using System;
using TetFun3080.Backend;

namespace TetFun3080.Gameplay
{
    internal class RotatorQuick : IRotator
    {
        public bool KicksEnabled { get; set; } = true;

        public Vector2 KickCheckLeft(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board)
        {
            throw new NotImplementedException();
        }

        public Vector2 KickCheckRight(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board)
        {
            throw new NotImplementedException();
        }

        

        public Vector2[] RotateClockwise(Vector2 pilotPiece, Vector2[] pieceOffset, Board board)
        {
            Vector2[] result = new Vector2[pieceOffset.Length];
            for (int i=0; i < pieceOffset.Length; i++)
            {
                result[i] = pieceOffset[i];
            }

            //(x, y) → (y, -x)
            for(int i = 0; i < 4; i++) {

                result[i] = new Vector2(pieceOffset[i].Y, -pieceOffset[i].X);

                //check if piece valid. If not, return original pieceOffset
                if (!board.CheckIsEmptyCoord(pilotPiece+result[i]))
                {
                    return pieceOffset; //return original pieceOffset if rotation is invalid
                }

            }

            return result;
        }

        public Vector2[] RotateCounterClockwise(Vector2 pilotPiece, Vector2[] pieceOffset, Board board)
        {
            //(x, y) → (-y, x)
            Vector2[] result = new Vector2[pieceOffset.Length];
            for (int i = 0; i < pieceOffset.Length; i++)
            {
                result[i] = pieceOffset[i];
            }
            for (int i = 0; i < 4; i++)
            {

                result[i] = new Vector2(-pieceOffset[i].Y, pieceOffset[i].X);
                if (!board.CheckIsEmptyCoord(pilotPiece+result[i]))
                {
                    return pieceOffset; //return original pieceOffset if rotation is invalid
                }

            }
            return result;
        }
    }
}
