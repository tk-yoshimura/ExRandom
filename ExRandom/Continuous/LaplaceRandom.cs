using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Continuous {
    public class LaplaceRandom : Random {
        readonly MT19937 mt;
        readonly double b, mu;

        public LaplaceRandom(MT19937 mt, double b = 1, double mu = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            
            this.mt = mt;
            this.b = b;
            this.mu = mu;
        }

        public override double Next() {
            double r = mt.NextDouble_OpenInterval01() - 0.5;

            return mu - b * Math.Sign(r) * Math.Log(1.0 - 2.0 * Math.Abs(r));
        }
    }
}
