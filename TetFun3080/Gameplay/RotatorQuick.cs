
using Microsoft.Xna.Framework;
using System;
using TetFun3080.Backend;

namespace TetFun3080.Gameplay
{
    internal class RotatorQuick : IRotator
    {
        public int RotationState { get; set; }

        public Vector2[] RotateClockwise(ref Vector2 pilotPiece, Vector2[] pieceOffset, Board board, Pieces type)
        {
            Vector2[] result = new Vector2[pieceOffset.Length];
            for (int i=0; i < pieceOffset.Length; i++)
            {
                result[i] = pieceOffset[i];
            }

            //(x, y) → (y, -x)

            int initialRot = RotationState;
            ChangeRotatorState(-1);
            int endRot = RotationState;

            for (int i = 0; i < 4; i++)
            {
                //Set the rotation here
                result[i] = new Vector2(pieceOffset[i].Y, -pieceOffset[i].X);

                
            }

                for (int iter = 0; iter < 5; iter++)
                {
                    Vector2? res = PerformValidCheckFromKickTable(ref pilotPiece, result, board, type, initialRot, endRot, iter);
                    if (res != null)
                    {
                        //rotation valid! Apply the tf to the pilot piece and return.
                        pilotPiece += (Vector2)res;
                        return result;
                    }
                }
                //If no non-null found in all the iterations, return the original offset
                return pieceOffset;

        }

        public Vector2[] RotateCounterClockwise(ref Vector2 pilotPiece, Vector2[] pieceOffset, Board board, Pieces type)
        {
  
                
            Vector2[] result = new Vector2[pieceOffset.Length];
            for (int i = 0; i < pieceOffset.Length; i++)
            {
                result[i] = pieceOffset[i];
            }

            //(x, y) → (y, -x)

            int initialRot = RotationState;
            ChangeRotatorState(-1);
            int endRot = RotationState;

            for (int i = 0; i < 4; i++)
            {
                //Set the rotation here
                result[i] = new Vector2(-pieceOffset[i].Y, pieceOffset[i].X);

            }

            for (int iter = 0; iter < 5; iter++)
            {
                Vector2? res = PerformValidCheckFromKickTable(ref pilotPiece, result, board, type, initialRot, endRot, iter);
                if (res != null)
                {
                    //rotation valid! Apply the tf to the pilot piece and return.
                    pilotPiece += (Vector2)res;
                    return result;
                }
            }
            //If no non-null found in all the iterations, return the original offset
            return pieceOffset;

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
    
    
        private Vector2? PerformValidCheckFromKickTable(ref Vector2 pilotPiece, Vector2[] pieceOffset, Board board, Pieces type, int initialRotation, int nextRotation, int iteration)
        {
            //Gets an input of a rotated piece offset.
            //Checks the validity of the rotation based on the "kick table"

            Vector2 kickedPilotOffset = Vector2.Zero;
            int oppositeModifier = 1;

            if ((initialRotation == 1 && nextRotation == 0) || (initialRotation == 2 && nextRotation == 1) || (initialRotation == 3 && nextRotation == 2) || (initialRotation == 0 && nextRotation == 3))
            {
                oppositeModifier = -1;
            }


            if (type == Pieces.I)
            {
                //O to R
                if (initialRotation == 0 && nextRotation == 1 || initialRotation == 1 && nextRotation == 0)
                {
                    switch (iteration)
                    {
                        default:
                            break;
                        case 1:
                            kickedPilotOffset = new Vector2(-1, 0) * oppositeModifier;
                            break;
                        case 2:
                            kickedPilotOffset = new Vector2(-1, 1) * oppositeModifier;
                            break;
                        case 3:
                            kickedPilotOffset = new Vector2(0, -2) * oppositeModifier;
                            break;
                        case 4:
                            kickedPilotOffset = new Vector2(-1, -2) * oppositeModifier;
                            break;

                    }
                }
                //R to 2
                if (initialRotation == 1 && nextRotation == 2 || initialRotation == 2 && nextRotation == 1)
                {
                    switch (iteration)
                    {
                        default:
                            break;
                        case 1:
                            kickedPilotOffset = new Vector2(1, 0);
                            break;
                        case 2:
                            kickedPilotOffset = new Vector2(1, -1);
                            break;
                        case 3:
                            kickedPilotOffset = new Vector2(0, 2);
                            break;
                        case 4:
                            kickedPilotOffset = new Vector2(1, 2);
                            break;

                    }
                }
                //2 to L
                if (initialRotation == 2 && nextRotation == 3 || initialRotation == 3 && nextRotation == 2)
                {
                    switch (iteration)
                    {
                        default:
                            break;
                        case 1:
                            kickedPilotOffset = new Vector2(1, 0);
                            break;
                        case 2:
                            kickedPilotOffset = new Vector2(1, 1);
                            break;
                        case 3:
                            kickedPilotOffset = new Vector2(0, -2);
                            break;
                        case 4:
                            kickedPilotOffset = new Vector2(1, -2);
                            break;

                    }
                }
                //L to 0
                if (initialRotation == 3 && nextRotation == 0 || initialRotation == 0 && nextRotation == 3)
                {
                    switch (iteration)
                    {
                        default:
                            break;
                        case 1:
                            kickedPilotOffset = new Vector2(-1, 0);
                            break;
                        case 2:
                            kickedPilotOffset = new Vector2(-1, -1);
                            break;
                        case 3:
                            kickedPilotOffset = new Vector2(0, 2);
                            break;
                        case 4:
                            kickedPilotOffset = new Vector2(-1, 2);
                            break;

                    }
                }

            }
            else
            {
                //O to R
                if (initialRotation == 0 && nextRotation == 1 || initialRotation == 1 && nextRotation == 0)
                {
                    switch (iteration)
                    {
                        default:
                            break;
                        case 1:
                            kickedPilotOffset = new Vector2(-1, 0) * oppositeModifier;
                            break;
                        case 2:
                            kickedPilotOffset = new Vector2(-1, 1) * oppositeModifier;
                            break;
                        case 3:
                            kickedPilotOffset = new Vector2(0, -2) * oppositeModifier;
                            break;
                        case 4:
                            kickedPilotOffset = new Vector2(-1, -2) * oppositeModifier;
                            break;

                    }
                }
                //R to 2
                if (initialRotation == 1 && nextRotation == 2 || initialRotation == 2 && nextRotation == 1)
                {
                    switch (iteration)
                    {
                        default:
                            break;
                        case 1:
                            kickedPilotOffset = new Vector2(1, 0);
                            break;
                        case 2:
                            kickedPilotOffset = new Vector2(1, -1);
                            break;
                        case 3:
                            kickedPilotOffset = new Vector2(0, 2);
                            break;
                        case 4:
                            kickedPilotOffset = new Vector2(1, 2);
                            break;

                    }
                }
                //2 to L
                if (initialRotation == 2 && nextRotation == 3 || initialRotation == 3 && nextRotation == 2)
                {
                    switch (iteration)
                    {
                        default:
                            break;
                        case 1:
                            kickedPilotOffset = new Vector2(1, 0);
                            break;
                        case 2:
                            kickedPilotOffset = new Vector2(1, 1);
                            break;
                        case 3:
                            kickedPilotOffset = new Vector2(0, -2);
                            break;
                        case 4:
                            kickedPilotOffset = new Vector2(1, -2);
                            break;

                    }
                }
                //L to 0
                if (initialRotation == 3 && nextRotation == 0 || initialRotation == 0 && nextRotation == 3)
                {
                    switch (iteration)
                    {
                        default:
                            break;
                        case 1:
                            kickedPilotOffset = new Vector2(-1, 0);
                            break;
                        case 2:
                            kickedPilotOffset = new Vector2(-1, -1);
                            break;
                        case 3:
                            kickedPilotOffset = new Vector2(0, 2);
                            break;
                        case 4:
                            kickedPilotOffset = new Vector2(-1, 2);
                            break;

                    }
                }

            }


            //check the piece offset's emptiness with the kickedOffset

            foreach (Vector2 piece in pieceOffset) 
            {
                if (!board.CheckIsEmptyCoord(pilotPiece + piece + kickedPilotOffset))
                {
                    return null; //Invalid rot, send null and make the rotator function do another iteration
                }
            }
            //if all are empty
            return kickedPilotOffset; 
        }
    
    
    }
}
