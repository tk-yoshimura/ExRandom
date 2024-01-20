using System;

namespace ExRandom.Continuous {
    public class ExponentialRandom : Random {
        public MT19937 Mt { get; }
        public double Lambda { get; }

        public ExponentialRandom(MT19937 mt, double lambda = 1) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(lambda > 0)) {
                throw new ArgumentOutOfRangeException(nameof(lambda));
            }

            this.Mt = mt;
            this.Lambda = lambda;
        }

        public override double Next() {
            double u = Mt.NextDouble_OpenInterval0();

            return -Math.Log(u) / Lambda;
        }
    }
}
