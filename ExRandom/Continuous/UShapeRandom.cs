using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class UShapeRandom : Random {
        readonly MT19937 mt;
        readonly double inv_p;

        public UShapeRandom(MT19937 mt, double p = 2) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(p >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(p));
            }

            this.mt = mt;
            this.inv_p = 1 / (p + 1);
        }

        public override double Next() {
            double u = 2 * mt.NextDouble() - 1;
            double x = (u >= 0) ? Math.Pow(u, inv_p) : (-Math.Pow(-u, inv_p));

            return (x + 1) * 0.5;
        }
    }
}
