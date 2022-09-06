using System;

namespace ExRandom.Continuous {
    public class GumbelRandom : Random {
        public MT19937 Mt { get; }
        public double Beta { get; }
        public double Lambda { get; }

        public GumbelRandom(MT19937 mt, double beta = 1, double lambda = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(beta > 0)) {
                throw new ArgumentOutOfRangeException(nameof(beta));
            }

            this.Mt = mt;
            this.Beta = beta;
            this.Lambda = lambda;
        }

        public override double Next() {
            double u = Mt.NextDouble_OpenInterval01();

            return Lambda - Beta * Math.Log(-Math.Log(u));
        }
    }
}
