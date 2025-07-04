﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;

namespace TetFun3080.Gameplay
{
    internal class TrueRandomizer : IRandomizer
    {
        public Pieces GetNextPiece()
        {
            Random random = new Random();
            Array values = Enum.GetValues(typeof(Pieces));
            Pieces randomPiece = (Pieces)values.GetValue(random.Next(values.Length));
            return randomPiece;
        }
    }
}
