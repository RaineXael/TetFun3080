using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TetFun3080.Backend;


//The object responsible for drawing and input handling the board.

namespace TetFun3080
{
    internal class BoardPlayer : IEntity
    {
        int currentGravity = 30;
        int gravityTimer;
        private Board board;
        private IRandomizer randomizer;
       
        private Vector2 boardPosition;
        private SpriteSheet _block_sprite;

        UserInput input;

        public Pieces currentShape = Pieces.I; // Default shape for the pilot piece

        public Vector2 pilot_position = new Vector2(0, 0); // Position of the piece on the board

        private Vector2[] piecePositions; // Array to hold the positions of the piece's blocks
        private Vector2 ghost_position; 

        private int boardDrawOffset;

        bool holdAvailable = true;

        private Pieces? heldPiece = null;

        private IRotator rotator = new RotatorQuick();

        private int appearanceDelay = 0; // Delay before the piece appears on the board
        private int lineclearDelay = 0; // Delay before lines are cleared

        private int lineClearTimer = 0; // Timer for line clearing

        int dasTimer = 0;
        int dasTime = 10;

        bool ghostVisible = true;
        SpriteFont font;

        int level = 0;

        private bool InstalockOnHardDrop = true;

        SoundEffectInstance lineclearSoundInstance;
        SoundEffectInstance pieceDropSoundInstance;

        private float lockInDelay = 3;
        private float lockInTimer = 0;

        private string soundSkin = "joel";

        private Sprite testspr;

        public BoardPlayer(Board board, IRandomizer randomizer, UserInput input)
        {
            AssetManager.LoadTexture("newSprite.png");

            this.board = board;
            this.randomizer = randomizer;
            _block_sprite = new SpriteSheet(AssetManager.GetTexture("Sprites/blocks"));
            testspr = new SpriteSheet(AssetManager.GetTexture("Sprites/blocks"));
            font = AssetManager.GetFont("Fonts/Font1");
            pieceDropSoundInstance = AssetManager.GetAudio($"Audio/GameSounds/{soundSkin}/place").CreateInstance();
            lineclearSoundInstance = AssetManager.GetAudio($"Audio/GameSounds/{soundSkin}/line").CreateInstance();
            _block_sprite.baseSize = 16;
            GenerateNewPiece(randomizer.GetNextPiece());
            this.input = input;
            boardDrawOffset = board.bufferHeight * 16;
            gravityTimer = currentGravity;
            lockInTimer = lockInDelay;

            testspr = new Sprite(AssetManager.GetTexture("newSprite.png"));
        }



        

        public void Draw(SpriteBatch spriteBatch)
        {
            _block_sprite.Alpha = 1f;
            for (int x = 0; x < board.boardState.GetLength(0); x++)
            {
                for (int y = 0; y < board.boardState.GetLength(1); y++)
                {
                    _block_sprite.Position = new Vector2(x * _block_sprite.baseSize, y * _block_sprite.baseSize - boardDrawOffset);
                    _block_sprite.DrawSheet(spriteBatch, board.boardState[x, y]);
                    //_block_sprite.DrawSheet(spriteBatch, 0);
                }
            }

            if (!lockedIn)
            {
                foreach (Vector2 piece in piecePositions)
                {
                    // Draw the main (solid) piece
                    int x = (int)pilot_position.X + (int)piece.X;
                    int y = (int)pilot_position.Y + (int)piece.Y;

                    // Draw the ghost piece if visible
                    if (ghostVisible)
                    {
                        int gx = (int)ghost_position.X + (int)piece.X;
                        int gy = (int)ghost_position.Y + (int)piece.Y;



                        _block_sprite.Alpha = 0.5f; // Set transparency for ghost
                        _block_sprite.Position = new Vector2(gx * _block_sprite.baseSize, gy * _block_sprite.baseSize - boardDrawOffset);
                        _block_sprite.DrawSheet(spriteBatch, (int)currentShape);

                        _block_sprite.Alpha = 1f; // Restore alpha for next draw
                    }
                    _block_sprite.Position = new Vector2(x * _block_sprite.baseSize, y * _block_sprite.baseSize - boardDrawOffset);
                    _block_sprite.DrawSheet(spriteBatch, (int)currentShape);


                }
            }
            
            spriteBatch.DrawString(font, heldPiece.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, level.ToString(), new Vector2(10, 128), Color.White);
            testspr.Position = new Vector2(180, -48 + board.height*16);
            testspr.Draw(spriteBatch);

        }

        public void RecieveDamage()
        {

        }

        private bool dropInputPressed = false;
        private bool rotateClockPressed = false;
        private bool rotateCounterPressed = false;
        private bool leftPressed = false;
        private bool rightPressed = false;
        public void Update(GameTime gameTime)
        {
            
            gravityTimer--;
            if (gravityTimer < 0)
            {
                gravityTimer = currentGravity;
                MoveDown();
            }

            if (lockedIn)
            {
                if (lineClearTimer > 0)
                {
                    lineClearTimer--;
                }
                else
                {
                    GenerateNewPiece(randomizer.GetNextPiece());
                    lockedIn = false;
                }
            }
            HandleInput();

            if (ghostVisible)
            {
                List<int> possibleXValues = new List<int>();
                foreach (Vector2 blockOffset in piecePositions)
                {
                    
                    ghost_position = GetHardDropPos();
                }
            }

        }


        public void HandleInput()
        {
            input.KeyboardState = Keyboard.GetState();
           

            if (input.IsLeftDown)
            {
                PressLeft();
                leftPressed = true;
            }
            else
            {
                leftPressed = false;
            }
            if (input.IsRightDown)
            {
                PressRight();
                rightPressed = true;
            }
            else
            {
                rightPressed = false;
            }


            if (input.IsUpDown)
            {

                if (!dropInputPressed)
                {
                    dropInputPressed = true;
                    HardDrop();
                }


            }
            else
            {
                dropInputPressed = false;
            }
            if (input.IsDropDown)
            {
                MoveDown();
            }
            if (input.IsRotateClockwiseDown)
            {
                if (!rotateClockPressed && !lockedIn && !(currentShape == Pieces.O))
                {
                    piecePositions = rotator.RotateClockwise(pilot_position,piecePositions, board);
                    rotateClockPressed = true;
                }

            }
            else
            {
                rotateClockPressed = false;
            }
            if (input.IsRotateCounterClockwiseDown)
            {
                if (!rotateCounterPressed && !lockedIn && !(currentShape == Pieces.O))
                {
                    rotateCounterPressed = true;
                    piecePositions = rotator.RotateCounterClockwise(pilot_position,piecePositions, board);
                }

            }
            else
            {
                rotateCounterPressed = false;
            }
            if (input.IsHoldDown)
            {
                HoldPiece();
            }
        }

        public void PressLeft()
        {
            if (!lockedIn)
            {
                if (!leftPressed)
                {
                    //do one tick, first frame of input
                    MoveLeft();
                    dasTimer = dasTime;
                }
                else
                {
                    //wait for dastimer to run out, then move
                    if (dasTimer > 0)
                    {
                        dasTimer--;
                    }
                    else
                    {
                        MoveLeft();
                    }
                }
            }
        }

        private Vector2 GetHardDropPos()
        {
            Vector2 originalPos = pilot_position;
            bool blockFound = false;
            int i = 0;
            while (!blockFound)
            {
                //check below for any blocks. If any, return current pos. Else, dec y until block/boundary hit
                foreach (Vector2 blockOffset in piecePositions)
                {
                    Vector2 boardLookupPosition = new Vector2(pilot_position.X + blockOffset.X, originalPos.Y + blockOffset.Y + i);
                    //if(boardLookupPosition.Y >= board.height + board.bufferHeight || board.boardState[(int)(boardLookupPosition.X), (int)(boardLookupPosition.Y)] != 0)
                    if (!board.CheckIsEmptyCoord(boardLookupPosition))
                    {
                        blockFound = true;
                        return pilot_position + new Vector2(0,i-1);
                    }
                }
                i++;
            }
            return pilot_position;



        }


        public void PressRight()
        {
            if (!lockedIn)
            {
                if (!rightPressed)
                {
                    //do one tick, first frame of input
                    MoveRight();
                    dasTimer = dasTime;
                }
                else
                {
                    //wait for dastimer to run out, then move
                    if (dasTimer > 0)
                    {
                        dasTimer--;
                    }
                    else
                    {
                        MoveRight();
                    }
                }
            }
        }

        private void MoveLeft()
        {
            // Logic to move the piece left
            foreach (Vector2 blockOffset in piecePositions)
            {
                Vector2 blockpos = blockOffset + pilot_position;
                blockpos.X--;
                if (blockpos.X < 0 || board.boardState[(int)blockpos.X, (int)blockpos.Y] != 0)
                {

                    return;
                }
            }
            pilot_position.X--; // Move the piece down by one unit
        }

        private void MoveRight()
        {
            // Logic to move the piece right
            foreach (Vector2 blockOffset in piecePositions)
            {
                Vector2 blockpos = blockOffset + pilot_position;
                blockpos.X++;
                if (blockpos.X >= board.width || board.boardState[(int)blockpos.X, (int)blockpos.Y] != 0)
                {

                    return;
                }
            }
            pilot_position.X++; // Move the piece down by one unit
        }
        bool blockBottomHit = false;
        public void MoveDown()
        {
            foreach(Vector2 blockOffset in piecePositions)
            {
                Vector2 blockpos = blockOffset + pilot_position;
                blockpos.Y += 1;
                if (blockpos.Y >= board.height+board.bufferHeight ||board.boardState[(int)blockpos.X, (int)blockpos.Y] != 0)
                {
                    //Lockinplace run every frame when the bottom is reached.
                    //Do a timer when done so that still allows player movement & rot.
                    //Timer resets on piece Y move. 
                    if (!lockedIn)
                    {
                        LockInPiece();
                    }
                    
                    
                    return;
                }

            }
            lockInTimer = lockInDelay;
            pilot_position.Y++; // Move the piece down by one unit
        }
        public void HardDrop()
        {
            // Logic to drop the piece to the bottom
            pilot_position = GetHardDropPos();
            if (InstalockOnHardDrop)
            {
                lockInTimer = 0;
                LockInPiece();
            }
        }
        public void ResetPosition()
        {
            // Logic to reset the piece's position
        }

        public void HoldPiece()
        {
            if (holdAvailable && !lockedIn)
            {
                if (heldPiece == null)
                {
                    heldPiece = currentShape;
                    GenerateNewPiece(randomizer.GetNextPiece());
                }
                else
                {
                    Pieces placeholder = (Pieces)heldPiece;
                    heldPiece = currentShape;
                    currentShape = placeholder;
                    GenerateNewPiece(currentShape);
                }
                
             
                holdAvailable = false;
            }
        }
        private bool lockedIn = false; 
        public void LockInPiece()
        {
            //Happens when the piece locks into place. 
            //Copies the piece into the board, check board for lines, then generates a new piece.

            if(lockInTimer > 0)
            {
                lockInTimer--;
            }
            else
            {
                board.AddBlock(currentShape, pilot_position, piecePositions);
                int linesCleared = board.ScanAndRemoveLines();
                holdAvailable = true;
                lockedIn = true;
                lineClearTimer = appearanceDelay;
                if (linesCleared > 0)
                {
                    lineclearSoundInstance.Stop();
                    lineclearSoundInstance.Play();
                    lineClearTimer += lineclearDelay;
                    level += linesCleared; 
                }
                else
                {
                    pieceDropSoundInstance.Stop();
                    pieceDropSoundInstance.Play();
                }
            }
        }

        public void GenerateNewPiece(Pieces piece)
        {
            
            int lastTwoDigits = level % 100;
            if (lastTwoDigits != 99) {
                level++;
            }
            
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
                    piecePositions = new Vector2[] { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1) };
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
