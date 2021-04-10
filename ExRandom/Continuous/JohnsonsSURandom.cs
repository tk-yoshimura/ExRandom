using System;

namespace ExRandom.Continuous {
    public class JohnsonsSURandom : Random {
        readonly MT19937 mt;
        readonly double delta, lambda, gamma, eta;

        public JohnsonsSURandom(MT19937 mt, double delta = 1, double lambda = 1, double gamma = 0, double eta = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(delta > 0)) {
                throw new ArgumentOutOfRangeException(nameof(delta));
            }
            if (!(lambda > 0)) {
                throw new ArgumentOutOfRangeException(nameof(lambda));
            }
            if (double.IsNaN(gamma)) {
                throw new ArgumentOutOfRangeException(nameof(gamma));
            }
            if (double.IsNaN(eta)) {
                throw new ArgumentOutOfRangeException(nameof(eta));
            }

            this.mt = mt;
            this.delta = delta;
            this.lambda = lambda;
            this.gamma = gamma;
            this.eta = eta;
        }

        public override double Next() {
            double u = mt.NextDouble_OpenInterval01();

            return lambda * Math.Sinh((ErrorFunction.Probit(u) - gamma) / delta) + eta;
        }
    }
}
