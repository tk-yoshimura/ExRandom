using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class LogNormalRandom : Random{
        readonly NormalRandom nd;

        public LogNormalRandom(MT19937 mt, double s = 1, double m = 0){
            if(mt == null) {
                throw new ArgumentNullException();
            }

            this.nd = new NormalRandom(mt, sigma : s, mu : m);
        }

        public override double Next() {
            return Math.Exp(nd.Next());
        }
    }
}
