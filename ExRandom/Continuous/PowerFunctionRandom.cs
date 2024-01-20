using System;

namespace ExRandom.Continuous {
    public class PowerFunctionRandom : Random {
        private readonly double inv_p, range;

        public MT19937 Mt { get; }
        public double P { get; }
        public double Min { get; }
        public double Max { get; }

        public PowerFunctionRandom(MT19937 mt, double p = 1, double min = 0, double max = 1) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(p > 0)) {
                throw new ArgumentOutOfRangeException(nameof(p));
            }
            if (!(min < max)) {
                throw new ArgumentOutOfRangeException($"{nameof(min)}<{nameof(max)}");
            }

            this.Mt = mt;
            this.P = p;
            this.inv_p = 1 / p;

            this.Min = min;
            this.Max = max;
            this.range = max - min;
        }

        public override double Next() {
            double u = Mt.NextDouble();

            return Min + range * Math.Pow(u, inv_p);
        }
    }
}
