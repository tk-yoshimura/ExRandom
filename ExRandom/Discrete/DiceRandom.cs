using System;

namespace ExRandom.Discrete {
    public class DiceRandom : Random {
        readonly MT19937 mt;
        readonly uint sides, cut;

        public DiceRandom(MT19937 mt, int sides = 6) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (sides < 1) {
                throw new ArgumentOutOfRangeException(nameof(sides));
            }

            this.mt = mt;
            this.sides = (uint)sides;
            this.cut = (uint.MaxValue % this.sides + 1) % this.sides;
        }

        public override int Next() {
            uint r;

            do {
                r = mt.Next();
            } while (r < cut);

            return (int)(r % sides);
        }
    }
}
