using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class LogisticRandom : Random {
        readonly MT19937 mt;
        readonly double sigma, mu;

        public LogisticRandom(MT19937 mt, double sigma = 1, double mu = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.mt = mt;
            this.sigma = sigma;
            this.mu = mu;
        }

        public override double Next() {
            double r = mt.NextDouble_OpenInterval01();

            return mu + sigma * Math.Log(r / (1.0 - r));
        }
    }
}
