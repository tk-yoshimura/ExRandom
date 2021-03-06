using System;

namespace ExRandom.Continuous {
    public class WeibullRandom : Random {
        readonly MT19937 mt;
        readonly double inv_alpha, beta, lambda;

        public WeibullRandom(MT19937 mt, double alpha = 1, double beta = 1, double lambda = 0) {
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
            this.inv_alpha = 1 / alpha;
            this.beta = beta;
            this.lambda = lambda;
        }

        public override double Next() {
            double u = mt.NextDouble_OpenInterval0();

            return lambda + beta * Math.Pow(-Math.Log(u), inv_alpha);
        }
    }
}
