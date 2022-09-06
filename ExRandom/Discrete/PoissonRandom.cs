using System;

namespace ExRandom.Discrete {
    public class PoissonRandom : Random {
        private readonly double thr;

        public MT19937 Mt { get; }
        public int Max { get; }
        public double Lambda { get; }

        public PoissonRandom(MT19937 mt, double lambda = 1, int max = int.MaxValue) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(lambda > 0)) {
                throw new ArgumentOutOfRangeException(nameof(lambda));
            }
            if (max < 1) {
                throw new ArgumentOutOfRangeException(nameof(max));
            }

            this.Mt = mt;
            this.Lambda = lambda;
            this.thr = Math.Exp(-lambda);
            this.Max = max;
        }

        public override int Next() {
            int cnt = -1;
            double m = 1;
            do {
                cnt++;
                m *= Mt.NextDouble();

                if (cnt >= Max)
                    break;
            } while (m >= thr);

            return cnt;
        }
    }
}
