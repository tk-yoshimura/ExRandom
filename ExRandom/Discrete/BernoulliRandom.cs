using System;

namespace ExRandom.Discrete {
    public class BernoulliRandom : Random {
        public MT19937 Mt { get; }
        public double Prob { get; }

        public BernoulliRandom(MT19937 mt, double prob = 0.5) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(prob >= 0) || prob > 1) {
                throw new ArgumentOutOfRangeException(nameof(prob));
            }

            this.Mt = mt;
            this.Prob = prob;
        }

        public override int Next() {
            return NextBool() ? 1 : 0;
        }

        public bool NextBool() {
            return Mt.NextDouble_OpenInterval1() < Prob;
        }
    }
}
