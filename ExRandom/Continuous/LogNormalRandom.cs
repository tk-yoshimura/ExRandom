using System;

namespace ExRandom.Continuous {
    public class LogNormalRandom : Random {
        private readonly NormalRandom nd;

        public MT19937 Mt { get; }
        public double S { get; }
        public double M { get; }

        public LogNormalRandom(MT19937 mt, double s = 1, double m = 0) {
            ArgumentNullException.ThrowIfNull(mt);

            this.nd = new NormalRandom(mt, sigma: s, mu: m);
            this.Mt = mt;
            this.S = s;
            this.M = m;
        }

        public override double Next() {
            return Math.Exp(nd.Next());
        }
    }
}
