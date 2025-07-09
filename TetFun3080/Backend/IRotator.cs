

using Microsoft.Xna.Framework;
using TetFun3080.Gameplay;

namespace TetFun3080.Backend
{
    //Implements different tetris rotation systems and wallkicks
    //(eg nes tetris has no kicks, tgm and guideline have different rotations)
    public interface IRotator
    {
       

        public int RotationState { get; set; }

        //Returns piece offset arrays for rotation (if can't rotate, either checks kick or doesn't rotate depending on type)
        public Vector2[] RotateClockwise(ref Vector2 pilotPiece, Vector2[] pieceOffset, Board board, Pieces type);
        public Vector2[] RotateCounterClockwise(ref Vector2 pilotPiece, Vector2[] pieceOffset, Board board, Pieces type);

       
    }

   
}
