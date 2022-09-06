using System;

namespace ExRandom.Continuous {
    public class InverseGaussRandom : Random {
        readonly NormalRandom nd;

        public MT19937 Mt { get; }
        public double Mu { get; }
        public double Lambda { get; }

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

            this.Mt = mt;
            this.nd = new NormalRandom(mt);
            this.Mu = mu;
            this.Lambda = lambda;
        }

        public override double Next() {
            double x, y, z, w;

            x = nd.Next();
            y = x * x * Mu;
            z = Mt.NextDouble();
            w = Mu - (0.5 * Mu / Lambda) * (Math.Sqrt(y * (y + 4.0 * Lambda)) - y);

            return (z < (Mu / (Mu + w))) ? w : (Mu * Mu / w);
        }
    }
}
