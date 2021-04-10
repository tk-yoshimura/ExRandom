using System;

namespace ExRandom.Continuous {
    public class LevyRandom : Random {
        readonly MT19937 mt;
        readonly double c, mu;

        public LevyRandom(MT19937 mt, double c = 1, double mu = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(c > 0)) {
                throw new ArgumentOutOfRangeException(nameof(c));
            }

            this.mt = mt;
            this.c = c;
            this.mu = mu;
        }

        public override double Next() {
            double r = mt.NextDouble_OpenInterval01();
            double p = ErrorFunction.Probit(1 - r * 0.5);

            return mu + c / (p * p);
        }
    }
}
