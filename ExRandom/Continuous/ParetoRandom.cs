using System;

namespace ExRandom.Continuous {
    public class ParetoRandom : Random {
        readonly double inv_alpha;

        public MT19937 Mt { get; }
        public double Alpha { get; }
        public double Beta { get; }

        public ParetoRandom(MT19937 mt, double alpha = 1, double beta = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(alpha > 0)) {
                throw new ArgumentOutOfRangeException(nameof(alpha));
            }
            if (!(beta > 0)) {
                throw new ArgumentOutOfRangeException(nameof(beta));
            }

            this.Mt = mt;
            this.Alpha = alpha;
            this.inv_alpha = 1.0 / alpha;
            this.Beta = beta;
        }

        public override double Next() {
            double r = Mt.NextDouble_OpenInterval0();

            return Beta / Math.Pow(r, inv_alpha);
        }
    }
}
