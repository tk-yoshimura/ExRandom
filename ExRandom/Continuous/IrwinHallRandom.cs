using System;

namespace ExRandom.Continuous {
    public class IrwinHallRandom : Random {
        public MT19937 Mt { get; }
        public int N { get; }

        public IrwinHallRandom(MT19937 mt, int n = 3) {
            ArgumentNullException.ThrowIfNull(mt);
            if (n < 1) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            this.Mt = mt;
            this.N = n;
        }

        public override double Next() {
            double w = 0;

            for (int i = 0; i < N; i++) {
                w += Mt.NextDouble();
            }

            return w;
        }
    }
}
