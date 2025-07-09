
using Microsoft.Xna.Framework;
using System;
using TetFun3080.Backend;

namespace TetFun3080.Gameplay
{
    internal class RotatorQuick : IRotator
    {
        public int RotationState { get; set; }

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
            ChangeRotatorState(-1);
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
            ChangeRotatorState(1);
            return result;
        }

        public void ChangeRotatorState(int value)
        {
            //0=spawn
            //1=r
            //2=2
            //3=left
            RotationState += value;
            if (RotationState >= 4)
            {
                RotationState = 0;
            }
            if(RotationState < 0)
            {
                RotationState = 3;
            }

            DebugConsole.Log("Rotator State is " +  RotationState.ToString());
        }
    }
}
