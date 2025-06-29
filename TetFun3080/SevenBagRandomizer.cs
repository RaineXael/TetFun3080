using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080
{
    internal class SevenBagRandomizer : IRandomizer
    {
        private List<Pieces> bag = new List<Pieces>();

        public SevenBagRandomizer()
        {
            RandomizeBag();
        }

        private void RandomizeBag()
        {
            // Iterate over all values of the Pieces enum
            foreach (Pieces piece in Enum.GetValues(typeof(Pieces)))
            {
                bag.Add(piece);
            }

            // Shuffle the bag
            Random rng = new Random();
            bag = bag.OrderBy(x => rng.Next()).ToList();
        }

        public Pieces GetNextPiece()
        {
            if (bag.Count == 0){
                RandomizeBag();
            }
            Pieces selected = bag[0];
            bag.RemoveAt(0);
            return selected;
        }
    }
}
