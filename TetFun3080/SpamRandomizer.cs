
using TetFun3080.Backend;

namespace TetFun3080
{
    internal class SpamRandomizer : IRandomizer
    {
        public Pieces GetNextPiece()
        {
            return Pieces.I;
        }
    }
}
