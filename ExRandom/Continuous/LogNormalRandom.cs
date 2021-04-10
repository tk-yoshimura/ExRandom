using System;

namespace ExRandom.Continuous {
    public class LogNormalRandom : Random {
        readonly NormalRandom nd;

        public LogNormalRandom(MT19937 mt, double s = 1, double m = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.nd = new NormalRandom(mt, sigma: s, mu: m);
        }

        public override double Next() {
            return Math.Exp(nd.Next());
        }
    }
}
