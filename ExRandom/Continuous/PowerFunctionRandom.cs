using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class PowerFunctionRandom : Random{
        readonly MT19937 mt;
        readonly double inv_p, min, range;

        public PowerFunctionRandom(MT19937 mt, double p = 1, double min = 0, double max = 1) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(p > 0) || !(min < max)) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.inv_p = 1 / p;

            this.min = min;
            this.range = max - min;
        }

        public override double Next() {
            double u = mt.NextDouble();

            return min + range * Math.Pow(u, inv_p);
        }
    }
}
