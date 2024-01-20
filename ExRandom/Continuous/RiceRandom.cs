using System;

namespace ExRandom.Continuous {
    public class RiceRandom : Random {
        private readonly NormalRandom nd;

        public MT19937 Mt { get; }
        public double Nu { get; }
        public double Sigma { get; }

        public RiceRandom(MT19937 mt, double nu = 0.5, double sigma = 1) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(nu >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(nu));
            }
            if (!(sigma >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(sigma));
            }

            this.Mt = mt;
            this.nd = new NormalRandom(mt, sigma);
            this.Nu = nu;
            this.Sigma = sigma;
        }

        public override double Next() {
            double theta = 2 * Math.PI * Mt.NextDouble_OpenInterval1();
            double x = Nu * Math.Cos(theta) + nd.Next();
            double y = Nu * Math.Sin(theta) + nd.Next();

            return Math.Sqrt(x * x + y * y);
        }
    }
}
