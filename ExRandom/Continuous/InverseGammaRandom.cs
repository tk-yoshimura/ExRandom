using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Continuous {
    public class InverseGammaRandom : Random {
        readonly GammaRandom gr;

        public InverseGammaRandom(MT19937 mt, double kappa = 1, double theta = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.gr = new GammaRandom(mt, kappa: kappa, theta: theta);
        }

        public override double Next() {
            return 1.0 / gr.Next();
        }
    }
}
