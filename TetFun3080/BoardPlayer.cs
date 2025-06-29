using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


//The object responsible for drawing and input handling the board.

namespace TetFun3080
{
    internal class BoardPlayer : IEntity
    {
        private Board board;
        private IRandomizer randomizer;
       
        private Vector2 boardPosition;
        private SpriteSheet _block_sprite;



        public Pieces currentShape = Pieces.I; // Default shape for the pilot piece

        public Vector2 pilot_position = new Vector2(0, 0); // Position of the piece on the board

        private Vector2[] piecePositions; // Array to hold the positions of the piece's blocks



        public BoardPlayer(AssetManager assets, Board board, IRandomizer randomizer)
        {
            this.board = board;
            this.randomizer = randomizer;
            _block_sprite = new SpriteSheet(assets.GetTexture("Sprites/blocks"));
            _block_sprite.baseSize = 16;
            GenerateNewPiece();
        }



        int test = 0;

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < board.boardState.GetLength(0); x++)
            {
                for (int y = 0; y < board.boardState.GetLength(1); y++)
                {
                    if (y < board.boardState.GetLength(1) - board.bufferHeight)
                        _block_sprite.Position = new Vector2(x * _block_sprite.baseSize, y * _block_sprite.baseSize);
                    _block_sprite.DrawSheet(spriteBatch, board.boardState[x, y]);

                }
            }

            foreach (Vector2 piece in piecePositions)
            {
                int x = (int)pilot_position.X + (int)piece.X;
                int y = (int)pilot_position.Y + (int)piece.Y;

                _block_sprite.Position = new Vector2(x * _block_sprite.baseSize, y * _block_sprite.baseSize);
                _block_sprite.DrawSheet(spriteBatch, (int)currentShape);
            }
            
        }

        public void Update(GameTime gameTime)
        {
            test--;
            if (test < 0)
            {
                test = 60;
                PieceMoveTick();
            }
        }



        public void MoveLeft()
        {
            // Logic to move the piece left
            pilot_position.X--; // Move the piece down by one unit
        }
        public void MoveRight()
        {
            // Logic to move the piece right
            pilot_position.X++; // Move the piece down by one unit
        }
        public void RotateClockwise()
        {
            // Logic to rotate the piece clockwise
        }
        public void RotateCounterClockwise()
        {
            // Logic to rotate the piece counter-clockwise
        }
        public void MoveDown()
        {
            // Logic to move the piece down
        }
        public void Drop()
        {
            // Logic to drop the piece to the bottom
        }
        public void ResetPosition()
        {
            // Logic to reset the piece's position
        }

        public void Draw(SpriteBatch spriteBatch, SpriteSheet spriteSheet)
        {
            // Logic to draw the piece using the provided sprite sheet
            // This would typically involve determining the piece's position and drawing the corresponding sprite
        }

        private void PieceMoveTick()
        {
            Console.WriteLine("rahhhhh");
            pilot_position.Y++; // Move the piece down by one unit
        }


        public void GenerateNewPiece()
        {
            Pieces piece = randomizer.GetNextPiece();
            currentShape = piece;
            switch (piece)
            {
                case Pieces.I:
                    piecePositions = new Vector2[] { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) };
                    break;
                case Pieces.T:
                    piecePositions = new Vector2[] { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1) };
                    break;
                case Pieces.L:
                    piecePositions = new Vector2[] { new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(0, 0), new Vector2(1, 0) };
                    break;
                case Pieces.J:
                    piecePositions = new Vector2[] { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
                    break;
                case Pieces.S:
                    piecePositions = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 1) };
                    break;
                case Pieces.Z:
                    piecePositions = new Vector2[] { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
                    break;
                case Pieces.O:
                    piecePositions = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };
                    break;
            }

            pilot_position = board.spawnPosition; // Reset the piece's position to the spawn position on the board
        }

    }
}
