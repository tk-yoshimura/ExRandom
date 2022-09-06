using System;

namespace ExRandom.Continuous {
    public class UShapeRandom : Random {
        private readonly double inv_p;

        public MT19937 Mt { get; }
        public double P { get; }

        public UShapeRandom(MT19937 mt, double p = 2) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(p >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(p));
            }

            this.Mt = mt;
            this.P = p;
            this.inv_p = 1 / (p + 1);
        }

        public override double Next() {
            double u = 2 * Mt.NextDouble() - 1;
            double x = (u >= 0) ? Math.Pow(u, inv_p) : (-Math.Pow(-u, inv_p));

            return (x + 1) * 0.5;
        }
    }
}
