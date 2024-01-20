using System;

namespace ExRandom.Discrete {
    public class LogarithmicRandom : Random {
        private readonly double f1;

        public MT19937 Mt { get; }
        public double P { get; }
        public int Max { get; }

        public LogarithmicRandom(MT19937 mt, double p = 0.5, int max = int.MaxValue) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(p > 0) || p >= 1) {
                throw new ArgumentOutOfRangeException(nameof(p));
            }
            if (max < 1) {
                throw new ArgumentOutOfRangeException(nameof(max));
            }

            this.Mt = mt;
            this.Max = max;
            this.P = p;
            this.f1 = -p / Math.Log(1 - p);
        }

        public override int Next() {
            int k = 1;
            double f = f1, r = Mt.NextDouble_OpenInterval1();

            for (; ; ) {
                r -= f;
                if (r <= 0 || k >= Max || f < double.Epsilon) {
                    return k;
                }

                f *= P * k / (k + 1);
                k++;
            }
        }
    }
}
