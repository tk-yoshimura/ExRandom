using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Discrete {
    public class DiceRandom : Random{
        readonly MT19937 mt;
        readonly UInt32 sides, cut;

        public DiceRandom(MT19937 mt, int sides = 6) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(sides < 1) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.sides = (UInt32)sides;
            this.cut = (UInt32.MaxValue % this.sides + 1) % this.sides;
        }

        public override int Next() {
            UInt32 r;

            do {
                r = mt.Next();
            } while(r < cut);

            return (int)(r % sides);
        }
    }
}
