using System;

namespace ExRandom.Continuous {
    public class LogisticRandom : Random {
        public MT19937 Mt { get; }
        public double Sigma { get; }
        public double Mu { get; }

        public LogisticRandom(MT19937 mt, double sigma = 1, double mu = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.Mt = mt;
            this.Sigma = sigma;
            this.Mu = mu;
        }

        public override double Next() {
            double r = Mt.NextDouble_OpenInterval01();

            return Mu + Sigma * Math.Log(r / (1.0 - r));
        }
    }
}
