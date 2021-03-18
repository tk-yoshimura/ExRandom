using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class KumaraswamyRandom : Random{
        readonly MT19937 mt;
        readonly double inv_a, inv_b;

        public KumaraswamyRandom(MT19937 mt, double a = 2, double b = 2) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(a > 0) || !(b > 0)) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.inv_a = 1 / a;
            this.inv_b = 1 / b;
        }

        public override double Next() {
            double u = mt.NextDouble();

            return Math.Pow(1 - Math.Pow(u, inv_b), inv_a);
        }
    }
}
