using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Continuous {
    public class ChiSquaredRandom : Random{
        readonly GammaRandom gr;

        public ChiSquaredRandom(MT19937 mt, uint k = 2) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(k < 1) {
                throw new ArgumentException();
            }
            
            this.gr = new GammaRandom(mt, kappa : k * 0.5, theta : 2);
        }

        public override double Next() {
            return gr.Next();
        }
    }
}
