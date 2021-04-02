using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class RiceRandom : Random {
        readonly MT19937 mt;
        readonly NormalRandom nd;
        readonly double nu, sigma;

        public RiceRandom(MT19937 mt, double nu = 0.5, double sigma = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(nu >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(nu));
            }
            if (!(sigma >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(sigma));
            }

            this.mt = mt;
            this.nd = new NormalRandom(mt, sigma);
            this.nu = nu;
            this.sigma = sigma;
        }

        public override double Next() {
            double theta = 2 * Math.PI * mt.NextDouble_OpenInterval1();
            double x = nu * Math.Cos(theta) + nd.Next();
            double y = nu * Math.Sin(theta) + nd.Next();

            return Math.Sqrt(x * x + y * y);
        }
    }
}
