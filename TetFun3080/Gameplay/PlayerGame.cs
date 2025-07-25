﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TetFun3080.Backend;


//The object responsible for drawing and input handling the board.
//The object representing a game playing player. For menuing, see Player.cs or PlayerMenu.cs

namespace TetFun3080.Gameplay
{
    internal class PlayerGame : IEntity
    {
        private Player parent;

        int gravityTimer;
        private Board board;

        UserInput input;

        bool blockBottomHit = false;
        int score = 0;

        public Pieces currentShape = Pieces.I; // Default shape for the pilot piece

        public Vector2 pilot_position = new Vector2(0, 0); // Position of the piece on the board

        private Vector2[] piecePositions; // Array to hold the positions of the piece's blocks
        private Vector2 ghost_position;

        private int boardDrawOffset; //For drawing blocks (the buffer, offsets y to account for it)

        bool holdAvailable = true;

        private Pieces? heldPiece = null;

        private int lineClearTimer = 0;
        private int dasTimer = 0;
        private float lockInTimer = 0;

        private bool ghostVisible = true;

        int level = 0;

        private bool isDire;

        List<Pieces> PieceQueue = new List<Pieces>();

        private SpriteSheet _block_sprite;
        private SoundEffectInstance lineclearSoundInstance;
        private SoundEffectInstance pieceDropSoundInstance;
        private SpriteFont font;

        private string soundSkin = "tgm";

        private int LevelThreshold { get { return (int)MathF.Ceiling((float)level / 100) * 100; } } // Example threshold for level increase, can be adjusted

        public Vector2 Position { get; set; }

        private GameMode gameMode;
        private Ruleset ruleset;

        //Events
        public EventHandler OnLineClear;
        public EventHandler OnDeath;
        public EventHandler OnDire;
        public EventHandler OnUnDire;


        public PlayerGame(Board board, UserInput input, Vector2 Position, GameMode mode, Player parent)
        {
            level = 00;
            this.input = input;
            
            this.parent = parent;
            this.Position = Position;
            this.board = board;

            gameMode = mode;
            ruleset = gameMode.GetRulesetFromLevel(level);

            _block_sprite = new SpriteSheet(AssetManager.GetTexture("Blocks/default/blocks"), 16);

            
            pieceDropSoundInstance = AssetManager.GetAudio($"Audio/GameSounds/{soundSkin}/place").CreateInstance();
            lineclearSoundInstance = AssetManager.GetAudio($"Audio/GameSounds/{soundSkin}/line").CreateInstance();
            _block_sprite.baseSize = 16;

            for (int i = 0; i < 5; i++)
            {
                PieceQueue.Add(ruleset.randomizer.GetNextPiece());
            }
            GenerateNewPieceFromQueue();
            
            boardDrawOffset = board.bufferHeight * 16;
            gravityTimer = ruleset.gravity;
            lockInTimer = ruleset.lockInDelay;

            MusicManager.PlayMusic($"Audio/Mus/white");

            //ruleset = AssetManager.GetRuleset("Rulesets/TestRuleset");
            LoadSoundTheme(soundSkin);


        }

        List<SoundEffect> NextSpawnSounds = new List<SoundEffect>();

        public void LoadSoundTheme(string skinName)
        {
            AssetManager.LoadAudio($"Audio/GameSounds/{skinName}/z");
            AssetManager.LoadAudio($"Audio/GameSounds/{skinName}/s");
            AssetManager.LoadAudio($"Audio/GameSounds/{skinName}/i");
            AssetManager.LoadAudio($"Audio/GameSounds/{skinName}/o");
            AssetManager.LoadAudio($"Audio/GameSounds/{skinName}/t");
            AssetManager.LoadAudio($"Audio/GameSounds/{skinName}/j");
            AssetManager.LoadAudio($"Audio/GameSounds/{skinName}/l");

            NextSpawnSounds.Add(AssetManager.GetAudio($"Audio/GameSounds/{skinName}/z"));
            NextSpawnSounds.Add(AssetManager.GetAudio($"Audio/GameSounds/{skinName}/s"));
            NextSpawnSounds.Add(AssetManager.GetAudio($"Audio/GameSounds/{skinName}/i"));
            NextSpawnSounds.Add(AssetManager.GetAudio($"Audio/GameSounds/{skinName}/o"));
            NextSpawnSounds.Add(AssetManager.GetAudio($"Audio/GameSounds/{skinName}/t"));
            NextSpawnSounds.Add(AssetManager.GetAudio($"Audio/GameSounds/{skinName}/j"));
            NextSpawnSounds.Add(AssetManager.GetAudio($"Audio/GameSounds/{skinName}/l"));
        }



        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {


            _block_sprite.Alpha = 1f;
            //Board
            for (int x = 0; x < board.boardState.GetLength(0); x++)
            {
                for (int y = 0; y < board.boardState.GetLength(1); y++)
                {
                    if (y > board.bufferHeight && board.boardState[x, y] != 0)
                    {
                        _block_sprite.Position = new Vector2(x * _block_sprite.baseSize + Position.X, y * _block_sprite.baseSize - boardDrawOffset + Position.Y) * ScreenManager.screenScale;
                        _block_sprite.spriteIndex = board.boardState[x, y];
                        _block_sprite.Draw(spriteBatch);
                        //_block_sprite.DrawSheet(spriteBatch, 0);
                    }

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
                    if (ghostVisible && ruleset.ghostEnabled)
                    {
                        int gx = (int)ghost_position.X + (int)piece.X;
                        int gy = (int)ghost_position.Y + (int)piece.Y;



                        _block_sprite.Alpha = 0.5f; // Set transparency for ghost
                        _block_sprite.Position = new Vector2(Position.X + gx * _block_sprite.baseSize, Position.Y + gy * _block_sprite.baseSize - boardDrawOffset) * ScreenManager.screenScale;
                        _block_sprite.spriteIndex = (int)currentShape;
                        _block_sprite.Draw(spriteBatch);

                        _block_sprite.Alpha = 1f; // Restore alpha for next draw
                    }



                    _block_sprite.Position = new Vector2(Position.X + x * _block_sprite.baseSize, Position.Y + y * _block_sprite.baseSize - boardDrawOffset) * ScreenManager.screenScale;
                    _block_sprite.spriteIndex = (int)currentShape;
                    _block_sprite.Draw(spriteBatch);


                }


            }


            int nextX = board.width + 3;
            int nextY = 1;
            int nextSep = 3;
            for (int i = 0; i < ruleset.visibleNextCount; i++)
            {
                foreach (Vector2 piece in GetPiecePositionsFromShape(PieceQueue[i]))
                {
                    // Draw the main (solid) piece
                    int x = nextX + (int)piece.X;
                    int y = nextY + (int)piece.Y + nextSep * i;

                    _block_sprite.Position = new Vector2(Position.X + x * _block_sprite.baseSize, -8 + Position.Y + y * _block_sprite.baseSize) * ScreenManager.screenScale;
                    _block_sprite.spriteIndex = (int)PieceQueue[i];
                    _block_sprite.Draw(spriteBatch);


                }
            }
            if (heldPiece != null)
            {
                // Draw the held piece

                foreach (Vector2 piece in GetPiecePositionsFromShape(heldPiece.Value))
                {
                    int x = (-1 + (int)piece.X) * 16;
                    int y = (1 + (int)piece.Y) * 16;
                    _block_sprite.Position = new Vector2(Position.X - 4 * 16 + x, y + Position.Y) * ScreenManager.screenScale;
                    _block_sprite.spriteIndex = (int)heldPiece.Value;
                    _block_sprite.Draw(spriteBatch);
                }
            }


            levelText.Text = level.ToString();
            levelText.Position = new Vector2(208 + Position.X, 274 + Position.Y);
            levelText.Draw(spriteBatch, gameTime);

            levelThresholdText.Text = LevelThreshold.ToString();
            levelThresholdText.Position = new Vector2(208 + Position.X, 294 + Position.Y);
            levelThresholdText.Draw(spriteBatch, gameTime);

            scoreText.Text = score.ToString();
            scoreText.Position = new Vector2(Position.X, Position.Y - 32);
            scoreText.Draw(spriteBatch, gameTime);




        }

        private TextObject levelText = new TextObject("");
        private TextObject levelThresholdText = new TextObject("");
        private TextObject scoreText = new TextObject("");

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
                if (gravityTimer < 0)
                {
                    gravityTimer = ruleset.gravity;
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
                    GenerateNewPieceFromQueue();
                    lockedIn = false;
                }
            }

            if (blockBottomHit && !lockedIn)
            {
                LockInPiece();
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

        private void GenerateNewPieceFromQueue()
        {
            //take 0 queue item, remove it, and generate a new piece from it.

            SpawnPiece(PieceQueue[0], false);
            PieceQueue.RemoveAt(0);
            PieceQueue.Add(ruleset.randomizer.GetNextPiece());
            PlayNextSpawnSound();
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
                if (blockBottomHit)
                {
                    if (lockInTimer > 0)
                    {
                        lockInTimer = 0;
                    }
                }
                else
                {
                    MoveDown(true);
                }

            }
            if (input.IsRotateClockwiseDown)
            {
                if (!rotateClockPressed)
                {
                    rotateClockPressed = true;
                    RotateClockwise();
                }
                

            }
            else
            {
                rotateClockPressed = false;
            }
            if (input.IsRotateCounterClockwiseDown)
            {
                if (!rotateCounterPressed)
                {
                    rotateCounterPressed = true;
                    RotateCounter();
                }
              

            }
            else
            {
                rotateCounterPressed = false;
            }
            if (input.IsHoldDown)
            {
                if(isHoldHeld == false)
                {
                    HoldPiece();
                    isHoldHeld = true;
                }
                
            }
            else
            {
                isHoldHeld = false;
            }
        }
        bool isHoldHeld = false;
        private void RotateClockwise()
        {
            if (!lockedIn && !(currentShape == Pieces.O))
            {
                
                piecePositions = ruleset.rotator.RotateClockwise(ref pilot_position, piecePositions, board, currentShape);
                
            }
        }

        private void RotateCounter()
        {
            if (!lockedIn && !(currentShape == Pieces.O))
            {
                
                piecePositions = ruleset.rotator.RotateCounterClockwise(ref pilot_position, piecePositions, board, currentShape);
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
                    dasTimer = ruleset.dasTime;
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

        private Vector2 GetHardDropPos(bool addScore = false)
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

                        //if addscore, take the difference between the original position and the new position, and add it to the score.
                        if (addScore)
                        {
                            score += 2 * ((int)boardLookupPosition.Y - (int)originalPos.Y);
                        }

                        return pilot_position + new Vector2(0, i - 1);




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
                    dasTimer = ruleset.dasTime;
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

        public void MoveDown(bool grantScore = false)
        {
            foreach (Vector2 blockOffset in piecePositions)
            {
                Vector2 blockpos = blockOffset + pilot_position;
                blockpos.Y += 1;



                if (blockpos.Y >= board.height + board.bufferHeight || board.boardState[(int)blockpos.X, (int)blockpos.Y] != 0)
                {
                    blockBottomHit = true;
                    //Lockinplace run every frame when the bottom is reached.
                    //Do a timer when done so that still allows player movement & rot.
                    //Timer resets on piece Y move. 
                    //if (!lockedIn)
                    //{
                    //    LockInPiece();
                    //}


                    return;
                }
                else
                {
                    blockBottomHit = false;
                    if (grantScore && !lockedIn)
                    {

                        score++;
                    }
                }


            }
            lockInTimer = ruleset.lockInDelay;
            pilot_position.Y++; // Move the piece down by one unit
        }
        private bool hardDropPossible = true;
        public void HardDrop()
        {
            // Logic to drop the piece to the bottom
            if (hardDropPossible)
            {
                pilot_position = GetHardDropPos(true);
                if (ruleset.InstalockOnHardDrop)
                {
                    lockInTimer = 0;
                    LockInPiece();
                }

                hardDropPossible = false;
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
                holdAvailable = false;
                if (heldPiece == null)
                { 
                    heldPiece = currentShape;
                    GenerateNewPieceFromQueue();

                }
                else
                {
                    Pieces placeholder = (Pieces)heldPiece;
                    heldPiece = currentShape;
                    currentShape = placeholder;
                    SpawnPiece(currentShape, true);
                }

            }
        }
        private bool lockedIn = false;

        public void LockInPiece()
        {
            //Happens when the piece locks into place. 
            //Copies the piece into the board, check board for lines, then generates a new piece.

            if (lockInTimer > 0)
            {
                lockInTimer--;

            }
            else
            {
                board.AddBlock(currentShape, pilot_position, piecePositions);
                int linesCleared = board.ScanAndRemoveLines();
                holdAvailable = true;
                lockedIn = true;
                //DebugConsole.Log(lockincounter.ToString());
                lineClearTimer = ruleset.appearanceDelay;
                if (linesCleared > 0)
                {
                    lineclearSoundInstance.Stop();
                    lineclearSoundInstance.Play();
                    lineClearTimer += ruleset.lineclearDelay;
                    IncrementLevel(linesCleared, true); // Increment level based on lines cleared

                    int multLevel = level / 100;

                    switch (linesCleared)
                    {
                        case 1:
                            score += 100 * multLevel;
                            break;
                        case 2:
                            score += 300 * multLevel;
                            break;
                        case 3:
                            score += multLevel * 500;
                            break;
                        case 4:
                            score += multLevel * 800;
                            break;
                        default:
                            score += multLevel * linesCleared * 100 + 800; // Fallback for 4+lines somehow
                            break;

                            //insert more for tspins and such
                    }
                }
                else
                {
                    pieceDropSoundInstance.Stop();
                    pieceDropSoundInstance.Play();
                }
            }
        }

        private void IncrementLevel(int count = 1, bool lineclear = true)
        {
            if (!lineclear)
            {
                int lastTwoDigits = level % 100;
                if (lastTwoDigits == 99)
                {
                    return;
                }
            }
            level += count;

            ruleset = gameMode.GetRulesetFromLevel(level);

        }

        private void SpawnPiece(Pieces piece, bool fromHold)
        {
            ruleset.rotator.RotationState = 0;
            blockBottomHit = false;
            hardDropPossible = true;
            lockedIn = false;
            if (!fromHold)
            {
                IncrementLevel(1, false); // Increment level by 1 if not at a level threshold
            }

            currentShape = piece;
            piecePositions = GetPiecePositionsFromShape(piece);

            pilot_position = board.spawnPosition; // Reset the piece's position to the spawn position on the board

            if (ruleset.IRSEnabled)
            {
                if (input.IsRotateClockwiseDown)
                {
                    DebugConsole.Log("irstate clock");
                    RotateClockwise();
                }
                else if (input.IsRotateCounterClockwiseDown)
                {
                    DebugConsole.Log("irstaterighjt");
                    RotateCounter();
                }
                else if(input.IsHoldDown && !fromHold)
                {
                    DebugConsole.Log("irshold");
                    HoldPiece();
                }
            }

            //check dire status (for mus and companion)
            if (board.GetStackHeight() >= 16)
            {
                if (!isDire)
                {
                    isDire = true;
                    OnDire?.Invoke(this, EventArgs.Empty);
                    //sirensound.Play();
                }
            }
            else
            {
                if (isDire)
                {
                    isDire = false;
                    OnUnDire?.Invoke(this, EventArgs.Empty);
                    
                }
            }


            //check if any pieces collide w/ the piece, if so, end game.
            foreach (Vector2 blockOffset in piecePositions)
            {
                Vector2 blockpos = blockOffset + pilot_position;
                if (!board.CheckIsEmptyCoord(pilot_position + blockOffset))
                {
                    // Game over condition
                    OnTopOut();
                    return;
                }
            }
        }

        private void OnTopOut()
        {
            parent.BeginMenu();
        }

        private void PlayNextSpawnSound()
        {
            // Play a sound based on the next piece type
            if (NextSpawnSounds.Count > 0)
            {
                NextSpawnSounds[(int)PieceQueue[0] - 1].Play();
            }
        }

        private Vector2[] GetPiecePositionsFromShape(Pieces piece)
        {
            switch (piece)
            {
                default: //I
                    return new Vector2[] { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) };
                case Pieces.T:
                    return new Vector2[] { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1) };
                case Pieces.L:
                    return new Vector2[] { new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(0, 0), new Vector2(1, 0) };
                case Pieces.J:
                    return new Vector2[] { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1) };
                case Pieces.S:
                    return new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 1) };
                case Pieces.Z:
                    return new Vector2[] { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
                case Pieces.O:
                    return new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

            }
        }
    }
}
