using System;

namespace ExRandom.Discrete {
    public class BernoulliRandom : Random {
        readonly MT19937 mt;
        readonly double thr;

        public BernoulliRandom(MT19937 mt, double prob = 0.5) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(prob >= 0) || prob > 1) {
                throw new ArgumentOutOfRangeException(nameof(prob));
            }

            this.mt = mt;
            this.thr = prob;
        }

        public override int Next() {
            return NextBool() ? 1 : 0;
        }

        public bool NextBool() {
            return mt.NextDouble_OpenInterval1() < thr;
        }
    }
}
