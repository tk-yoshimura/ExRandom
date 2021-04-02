using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class GumbelRandom : Random {
        readonly MT19937 mt;
        readonly double beta, lambda;

        public GumbelRandom(MT19937 mt, double beta = 1, double lambda = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(beta > 0)) {
                throw new ArgumentOutOfRangeException(nameof(beta));
            }

            this.mt = mt;
            this.beta = beta;
            this.lambda = lambda;
        }

        public override double Next() {
            double u = mt.NextDouble_OpenInterval01();

            return lambda - beta * Math.Log(-Math.Log(u));
        }
    }
}
