using System;

namespace ExRandom.Continuous {
    public class ExponentialRandom : Random {
        readonly MT19937 mt;
        readonly double lambda;

        public ExponentialRandom(MT19937 mt, double lambda = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(lambda > 0)) {
                throw new ArgumentOutOfRangeException(nameof(lambda));
            }

            this.mt = mt;
            this.lambda = lambda;
        }

        public override double Next() {
            double u = mt.NextDouble_OpenInterval0();

            return -Math.Log(u) / lambda;
        }
    }
}
