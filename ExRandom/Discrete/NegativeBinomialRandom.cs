using System;

namespace ExRandom.Discrete {
    public class NegativeBinomialRandom : Random {
        private readonly BernoulliRandom bd;

        public MT19937 Mt { get; }
        public double Prob { get; }
        public int R { get; }
        public int Max { get; }

        public NegativeBinomialRandom(MT19937 mt, double prob = 0.5, int r = 4, int max = int.MaxValue) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (max < 1) {
                throw new ArgumentOutOfRangeException(nameof(max));
            }
            if (r < 1 || r > max) {
                throw new ArgumentOutOfRangeException(nameof(r));
            }

            this.bd = new BernoulliRandom(mt, prob);
            this.Mt = mt;
            this.Prob = prob;
            this.R = r;
            this.Max = max;
        }

        public override int Next() {
            int cnt = 0;

            for (int i = 1; i < Max; i++) {
                if (bd.NextBool()) {
                    cnt++;
                    if (cnt >= R) {
                        return i - cnt;
                    }
                }
            }

            return Max - R;
        }
    }
}
