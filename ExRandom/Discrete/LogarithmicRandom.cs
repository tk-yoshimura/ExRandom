using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Discrete {
    public class LogarithmicRandom : Random {
        readonly MT19937 mt;
        readonly int max;
        readonly double p, f1;

        public LogarithmicRandom(MT19937 mt, double p = 0.5, int max = int.MaxValue) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(p > 0) || p >= 1) {
                throw new ArgumentOutOfRangeException(nameof(p));
            }
            if (max < 1) {
                throw new ArgumentOutOfRangeException(nameof(max));
            }

            this.mt = mt;
            this.max = max;
            this.p = p;
            this.f1 = -p / Math.Log(1 - p);
        }

        public override int Next() {
            int k = 1;
            double f = f1, r = mt.NextDouble_OpenInterval1();

            for (; ; ) {
                r -= f;
                if (r <= 0 || k >= max || f < double.Epsilon) {
                    return k;
                }

                f *= p * k / (k + 1);
                k++;
            }
        }
    }
}
