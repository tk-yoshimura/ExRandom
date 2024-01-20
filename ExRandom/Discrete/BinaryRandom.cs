using System;

namespace ExRandom.Discrete {
    public class BinaryRandom : Random {
        public MT19937 Mt { get; }

        public BinaryRandom(MT19937 mt) {
            ArgumentNullException.ThrowIfNull(mt);

            this.Mt = mt;
        }

        public override int Next() {
            return (int)(Mt.Next() & 1);
        }

        public bool NextBool() {
            return (Mt.Next() & 1) != 0;
        }
    }
}
