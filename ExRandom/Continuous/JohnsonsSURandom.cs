using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class JohnsonsSURandom : Random{
        readonly MT19937 mt;
        readonly double delta, lambda, gamma, eta;

        public JohnsonsSURandom(MT19937 mt, double delta = 1, double lambda = 1, double gamma = 0, double eta = 0) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(delta > 0) || !(lambda > 0) || Double.IsNaN(gamma) || Double.IsNaN(eta)) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.delta = delta;
            this.lambda = lambda;
            this.gamma = gamma;
            this.eta = eta;
        }

        public override double Next() {
            double u = mt.NextDouble_OpenInterval01();

            return lambda * Math.Sinh((ErrorFuntion.Probit(u) - gamma) / delta) + eta;
        }
    }
}
