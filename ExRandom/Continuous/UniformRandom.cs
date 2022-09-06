using System;

namespace ExRandom.Continuous {
    public class UniformRandom : Random {
        private readonly double range;

        public MT19937 Mt { get; }
        public double Min { get; }
        public double Max { get; }

        public UniformRandom(MT19937 mt, double min = 0, double max = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(min < max)) {
                throw new ArgumentOutOfRangeException($"{nameof(min)}<{nameof(max)}");
            }

            this.Mt = mt;
            this.Min = min;
            this.Max = max;
            this.range = max - min;
        }

        public override double Next() {
            return Mt.NextDouble() * range + Min;
        }
    }
}
