using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class FrechetRandom : Random {
        readonly MT19937 mt;
        readonly double inv_alpha, beta, lambda;

        public FrechetRandom(MT19937 mt, double alpha = 1, double beta = 1, double lambda = 0) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(alpha > 0) || !(beta > 0)) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.inv_alpha = 1 / alpha;
            this.beta = beta;
            this.lambda = lambda;
        }

        public override double Next() {
            double u = mt.NextDouble_OpenInterval01();

            return lambda + beta / Math.Pow(-Math.Log(u), inv_alpha);
        }
    }
}
