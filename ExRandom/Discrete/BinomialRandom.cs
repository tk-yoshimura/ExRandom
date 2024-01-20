using System;

namespace ExRandom.Discrete {
    public class BinomialRandom : Random {
        private const int skips = 1024;
        private readonly RouletteRandom rd, rd_skip;

        public MT19937 Mt { get; }
        public double Prob { get; }
        public int N { get; }

        public BinomialRandom(MT19937 mt, double prob = 0.5, int n = 10) {
            ArgumentNullException.ThrowIfNull(mt);
            if (n <= 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            this.Mt = mt;
            this.Prob = prob;
            this.N = n;

            if (n <= skips) {
                double[] p = Binomial.Coef(n, prob);
                this.rd = new RouletteRandom(mt, p);
            }
            else {
                double[] p = Binomial.Coef(n % skips, prob);
                this.rd = new RouletteRandom(mt, p);

                double[] p_skip = Binomial.Coef(skips, prob);
                this.rd_skip = new RouletteRandom(mt, p_skip);
            }
        }

        public override int Next() {
            int i = N, cnt = 0;

            while (i > skips) {
                cnt += rd_skip.Next();
                i -= skips;
            }

            cnt += rd.Next();

            return cnt;
        }
    }
}
