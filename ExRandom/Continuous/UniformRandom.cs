using System;

namespace ExRandom.Continuous {
    public class UniformRandom : Random {
        readonly MT19937 mt;
        readonly double min, max, range;

        public UniformRandom(MT19937 mt, double min = 0, double max = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(min < max)) {
                throw new ArgumentOutOfRangeException($"{nameof(min)}<{nameof(max)}");
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
