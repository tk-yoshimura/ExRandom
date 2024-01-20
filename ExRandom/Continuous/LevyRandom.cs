using System;

namespace ExRandom.Continuous {
    public class LevyRandom : Random {
        public MT19937 Mt { get; }
        public double C { get; }
        public double Mu { get; }

        public LevyRandom(MT19937 mt, double c = 1, double mu = 0) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(c > 0)) {
                throw new ArgumentOutOfRangeException(nameof(c));
            }

            this.Mt = mt;
            this.C = c;
            this.Mu = mu;
        }

        public override double Next() {
            double r = Mt.NextDouble_OpenInterval01();
            double p = ErrorFunction.Probit(1 - r * 0.5);

            return Mu + C / (p * p);
        }
    }
}
