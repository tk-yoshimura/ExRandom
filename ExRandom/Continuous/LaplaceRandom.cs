using System;

namespace ExRandom.Continuous {
    public class LaplaceRandom : Random {
        public MT19937 Mt { get; }
        public double B { get; }
        public double Mu { get; }

        public LaplaceRandom(MT19937 mt, double b = 1, double mu = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.Mt = mt;
            this.B = b;
            this.Mu = mu;
        }

        public override double Next() {
            double r = Mt.NextDouble_OpenInterval01() - 0.5;

            return Mu - B * Math.Sign(r) * Math.Log(1.0 - 2.0 * Math.Abs(r));
        }
    }
}
