using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class ParetoRandom : Random{
        readonly MT19937 mt;
        readonly double inv_alpha, beta;

        public ParetoRandom(MT19937 mt, double alpha = 1, double beta = 1) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(alpha > 0) || !(beta > 0)) {
                throw new ArgumentException();
            }
            
            this.mt = mt;
            this.inv_alpha = 1.0 / alpha;
            this.beta = beta;
        }

        public override double Next() {
            double r = mt.NextDouble_OpenInterval0();

            return beta / Math.Pow(r, inv_alpha);
        }
    }
}
