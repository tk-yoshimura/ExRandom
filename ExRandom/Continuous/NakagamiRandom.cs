using System;

namespace ExRandom.Continuous {
    public class NakagamiRandom : Random {
        readonly GammaRandom gr;

        public NakagamiRandom(MT19937 mt, double m = 1, double omega = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(m >= 0.5)) {
                throw new ArgumentOutOfRangeException(nameof(m));
            }
            if (!(omega > 0)) {
                throw new ArgumentOutOfRangeException(nameof(omega));
            }

            this.gr = new GammaRandom(mt, kappa: m, theta: omega / m);
        }

        public override double Next() {
            return Math.Sqrt(gr.Next());
        }
    }
}
