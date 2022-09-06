using System;

namespace ExRandom.Continuous {
    public class RayleighRandom : Random {
        public MT19937 Mt { get; }
        public double Sigma { get; }

        public RayleighRandom(MT19937 mt, double sigma = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.Mt = mt;
            this.Sigma = sigma;
        }

        public override double Next() {
            double r = Mt.NextDouble_OpenInterval0();

            return Sigma * Math.Sqrt(-2.0 * Math.Log(r));
        }
    }
}
