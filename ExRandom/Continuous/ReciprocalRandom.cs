using System;

namespace ExRandom.Continuous {
    public class ReciprocalRandom : Random {
        public MT19937 Mt { get; }
        public double A { get; }
        public double B { get; }

        public ReciprocalRandom(MT19937 mt, double a = 1, double b = 2) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(a > 0)) {
                throw new ArgumentOutOfRangeException(nameof(a));
            }
            if (!(b > 0)) {
                throw new ArgumentOutOfRangeException(nameof(b));
            }

            this.Mt = mt;
            this.A = a;
            this.B = b;
        }


        public override double Next() {
            double u = Mt.NextDouble();

            return Math.Pow(A, u) * Math.Pow(B, 1 - u);
        }
    }
}
