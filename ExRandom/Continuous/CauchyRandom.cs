using System;

////debug
//Next : output distribution check - OK?...invalid mu

namespace ExRandom.Continuous {
    public class CauchyRandom : Random{
        readonly MT19937 mt;
        readonly double gamma, mu;

        public CauchyRandom(MT19937 mt, double gamma = 1, double mu = 0) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            this.mt = mt;
            this.gamma = gamma;
            this.mu = mu;
        }

        public override double Next() {
            double u = mt.NextDouble_OpenInterval01() - 0.5;

            return mu + gamma * Math.Tan(Math.PI * u);
        }
    }
}
