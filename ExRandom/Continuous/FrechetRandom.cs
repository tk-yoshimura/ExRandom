using System;

namespace ExRandom.Continuous {
    public class FrechetRandom : Random {
        private readonly double inv_alpha;

        public MT19937 Mt { get; }
        public double Alpha { get; }
        public double Beta { get; }
        public double Lambda { get; }

        public FrechetRandom(MT19937 mt, double alpha = 1, double beta = 1, double lambda = 0) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(alpha > 0)) {
                throw new ArgumentOutOfRangeException(nameof(alpha));
            }
            if (!(beta > 0)) {
                throw new ArgumentOutOfRangeException(nameof(beta));
            }

            this.Mt = mt;
            this.Alpha = alpha;
            this.inv_alpha = 1 / alpha;
            this.Beta = beta;
            this.Lambda = lambda;
        }


        public override double Next() {
            double u = Mt.NextDouble_OpenInterval01();

            return Lambda + Beta / Math.Pow(-Math.Log(u), inv_alpha);
        }
    }
}
