using System;

namespace ExRandom.Continuous {
    public class BetaRandom : Random {
        private readonly GammaRandom g1, g2;

        public MT19937 Mt { get; }
        public double Alpha { get; }
        public double Beta { get; }

        public BetaRandom(MT19937 mt, double alpha = 1, double beta = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.g1 = new GammaRandom(mt, kappa: alpha, theta: 1);
            this.g2 = new GammaRandom(mt, kappa: beta, theta: 1);
            this.Mt = mt;
            this.Alpha = alpha;
            this.Beta = beta;
        }

        public override double Next() {
            double r1 = g1.Next(), r2 = g2.Next();

            return r1 / Math.Max(r1 + r2, double.Epsilon);
        }
    }
}
