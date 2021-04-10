using System;

namespace ExRandom.Continuous {
    public class ParetoRandom : Random {
        readonly MT19937 mt;
        readonly double inv_alpha, beta;

        public ParetoRandom(MT19937 mt, double alpha = 1, double beta = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(alpha > 0)) {
                throw new ArgumentOutOfRangeException(nameof(alpha));
            }
            if (!(beta > 0)) {
                throw new ArgumentOutOfRangeException(nameof(beta));
            }

            this.mt = mt;
            this.inv_alpha = 1.0 / alpha;
            this.beta = beta;
        }

        public override double Next() {
            double r = mt.NextDouble_OpenInterval0();

            return beta / Math.Pow(r, inv_alpha);
        }
    }
}
