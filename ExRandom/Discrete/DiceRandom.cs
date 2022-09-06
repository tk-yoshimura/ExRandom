using System;

namespace ExRandom.Discrete {
    public class DiceRandom : Random {
        readonly uint cut;

        public MT19937 Mt { get; }
        public uint Sides { get; }

        public DiceRandom(MT19937 mt, int sides = 6) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (sides < 1) {
                throw new ArgumentOutOfRangeException(nameof(sides));
            }

            this.Mt = mt;
            this.Sides = (uint)sides;
            this.cut = (uint.MaxValue % this.Sides + 1) % this.Sides;
        }

        public override int Next() {
            uint r;

            do {
                r = Mt.Next();
            } while (r < cut);

            return (int)(r % Sides);
        }
    }
}
