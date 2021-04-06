using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Discrete {
    public class NegativeBinomialRandom : Random {
        readonly BernoulliRandom bd;
        readonly int r, max;

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
            this.r = r;
            this.max = max;
        }

        public override int Next() {
            int cnt = 0;

            for (int i = 1; i < max; i++) {
                if (bd.NextBool()) {
                    cnt++;
                    if (cnt >= r) {
                        return i - cnt;
                    }
                }
            }

            return max - r;
        }
    }
}
