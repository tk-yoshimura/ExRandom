using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Continuous {
    public class InverseGaussRandom : Random {
        readonly MT19937 mt;
        readonly NormalRandom nd;
        readonly double mu, lambda;

        public InverseGaussRandom(MT19937 mt, double lambda = 1, double mu = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(mu > 0)) {
                throw new ArgumentOutOfRangeException(nameof(mu));
            }
            if (!(lambda > 0)) {
                throw new ArgumentOutOfRangeException(nameof(lambda));
            }

            this.mt = mt;
            this.nd = new NormalRandom(mt);
            this.mu = mu;
            this.lambda = lambda;
        }

        public override double Next() {
            double x, y, z, w;

            x = nd.Next();
            y = x * x * mu;
            z = mt.NextDouble();
            w = mu - (0.5 * mu / lambda) * (Math.Sqrt(y * (y + 4.0 * lambda)) - y);

            return (z < (mu / (mu + w))) ? w : (mu * mu / w);
        }
    }
}
