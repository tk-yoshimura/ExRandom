using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Continuous {
    public class NakagamiRandom : Random{
        readonly GammaRandom gr;

        public NakagamiRandom(MT19937 mt, double m = 1, double omega = 1) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(m >= 0.5) || !(omega > 0)) {
                throw new ArgumentException();
            }

            this.gr = new GammaRandom(mt, kappa : m, theta : omega / m);
        }

        public override double Next() {
            return Math.Sqrt(gr.Next());
        }
    }
}
