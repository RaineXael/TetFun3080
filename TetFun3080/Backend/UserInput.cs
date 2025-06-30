using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{

    public class UserInput
    {
        public KeyboardState KeyboardState { set; get; }

        public Keys UpInput { get; }
        public bool IsUpDown { get { return KeyboardState.IsKeyDown(UpInput); } }

        public Keys LeftInput { get; }
        public bool IsLeftDown { get { return KeyboardState.IsKeyDown(LeftInput); } }
        public Keys RightInput { get; }
        public bool IsRightDown { get { return KeyboardState.IsKeyDown(RightInput); } }
        public Keys DropInput { get; }
        public bool IsDropDown { get { return KeyboardState.IsKeyDown(DropInput); } }
        public Keys RotateClockwiseInput { get; }
        public bool IsRotateClockwiseDown { get { return KeyboardState.IsKeyDown(RotateClockwiseInput); } }
        public Keys RotateCounterClockwiseInput { get; }
        public bool IsRotateCounterClockwiseDown { get { return KeyboardState.IsKeyDown(RotateCounterClockwiseInput); } }
        public Keys HoldInput { get; }
        public bool IsHoldDown { get { return KeyboardState.IsKeyDown(HoldInput); } }

        public UserInput()
        {
            LeftInput = Keys.A;
            RightInput = Keys.D;
            DropInput = Keys.S;
            RotateClockwiseInput = Keys.J;
            RotateCounterClockwiseInput = Keys.K;
            UpInput = Keys.W;
            HoldInput = Keys.Space;
        }

        public UserInput(Keys leftInput, Keys rightInput, Keys dropInput, Keys upInput, Keys clockwiseInput, Keys counterclockwiseInput, Keys holdInput)
        {
            LeftInput = leftInput;
            RightInput = rightInput;
            DropInput = dropInput;
            RotateClockwiseInput = clockwiseInput;
            RotateCounterClockwiseInput = counterclockwiseInput;
            UpInput = upInput;
            HoldInput = holdInput;
        }

    }
}
