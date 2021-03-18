using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class UniformRandom : Random{
        readonly MT19937 mt;
        readonly double min, max, range;

        public UniformRandom(MT19937 mt, double min = 0, double max = 1){
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(min < max)) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.min = min;
            this.max = max;
            this.range = max - min;
        }

        public override double Next() {
            return mt.NextDouble() * range + min;
        }
    }
}
