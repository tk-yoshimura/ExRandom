using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Continuous {
    public class ErlangRandom : Random{
        readonly GammaRandom gd;

        public ErlangRandom(MT19937 mt, uint k = 2, double theta = 1) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            this.gd = new GammaRandom(mt, kappa : k, theta : theta);
        }

        public override double Next() {
            return gd.Next();
        }
    }
}
