using System;

namespace ExRandom.Continuous {
    public class ErlangRandom : Random {
        readonly GammaRandom gd;

        public ErlangRandom(MT19937 mt, uint k = 2, double theta = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.gd = new GammaRandom(mt, kappa: k, theta: theta);
        }

        public override double Next() {
            return gd.Next();
        }
    }
}
