using System;

namespace ExRandom.Continuous {
    public class ReciprocalRandom : Random {
        readonly MT19937 mt;
        readonly double a, b;

        public ReciprocalRandom(MT19937 mt, double a = 1, double b = 2) {
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
            this.a = a;
            this.b = b;
        }

        public override double Next() {
            double u = mt.NextDouble();

            return Math.Pow(a, u) * Math.Pow(b, 1 - u);
        }
    }
}
