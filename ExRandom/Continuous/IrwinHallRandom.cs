using System;

namespace ExRandom.Continuous {
    public class IrwinHallRandom : Random {
        readonly MT19937 mt;
        readonly int n;

        public IrwinHallRandom(MT19937 mt, int n = 3) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (n < 1) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            this.mt = mt;
            this.n = n;
        }

        public override double Next() {
            double w = 0;

            for (int i = 0; i < n; i++) {
                w += mt.NextDouble();
            }

            return w;
        }
    }
}
