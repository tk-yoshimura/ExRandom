using System;

namespace ExRandom.Continuous {
    public class KumaraswamyRandom : Random {
        readonly MT19937 mt;
        readonly double inv_a, inv_b;

        public KumaraswamyRandom(MT19937 mt, double a = 2, double b = 2) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(a > 0)) {
                throw new ArgumentOutOfRangeException(nameof(a));
            }
            if (!(b > 0)) {
                throw new ArgumentOutOfRangeException(nameof(b));
            }

            this.mt = mt;
            this.inv_a = 1 / a;
            this.inv_b = 1 / b;
        }

        public override double Next() {
            double u = mt.NextDouble();

            return Math.Pow(1 - Math.Pow(u, inv_b), inv_a);
        }
    }
}
