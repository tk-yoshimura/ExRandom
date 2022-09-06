using System;

namespace ExRandom.Continuous {
    public class CauchyRandom : Random {
        public MT19937 Mt { get; }
        public double Gamma { get; }
        public double Mu { get; }

        public CauchyRandom(MT19937 mt, double gamma = 1, double mu = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.Mt = mt;
            this.Gamma = gamma;
            this.Mu = mu;
        }

        public override double Next() {
            double u = Mt.NextDouble_OpenInterval01() - 0.5;

            return Mu + Gamma * Math.Tan(Math.PI * u);
        }
    }
}
