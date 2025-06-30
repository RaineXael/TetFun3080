

using Microsoft.Xna.Framework;

namespace TetFun3080.Backend
{
    //Implements different tetris rotation systems and wallkicks
    //(eg nes tetris has no kicks, tgm and guideline have different rotations)
    internal interface IRotator
    {
        //Returns piece offset arrays for rotation (if can't rotate, either checks kick or doesn't rotate depending on type)
        public Vector2[] RotateClockwise(Vector2[] pieceOffset, Board board);
        public Vector2[] RotateCounterClockwise(Vector2[] pieceOffset, Board board);

        //Returns kicked pilotposition
        public Vector2 KickCheckLeft(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board);
        public Vector2 KickCheckRight(Vector2 pilotPosition, Vector2[] rotatedPieceOffset, Board board);
    }
}
