using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class ExponentialRandom : Random{
        readonly MT19937 mt;
        readonly double lambda;

        public ExponentialRandom(MT19937 mt, double lambda = 1) { 
            if(mt == null) {
                throw new ArgumentNullException();
            }

            this.mt = mt;
            this.lambda = lambda;
        }

        public override double Next() {
            double u = mt.NextDouble_OpenInterval0();

            return -lambda * Math.Log(u);
        }
    }
}
