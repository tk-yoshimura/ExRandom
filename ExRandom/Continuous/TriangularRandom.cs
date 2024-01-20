using System;

namespace ExRandom.Continuous {
    public class TriangularRandom : Random {
        private readonly double thr, s0, s1;

        public MT19937 Mt { get; }
        public double Min { get; }
        public double Mode { get; }
        public double Max { get; }

        public TriangularRandom(MT19937 mt, double min = -1, double mode = 0, double max = 1) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(min < mode) || !(mode < max)) {
                throw new ArgumentOutOfRangeException($"{nameof(min)}<{nameof(mode)}<{nameof(max)}");
            }

            this.Mt = mt;
            this.Min = min;
            this.Mode = mode;
            this.Max = max;
            this.thr = (mode - min) / (max - min);
            this.s0 = (max - min) * (mode - min);
            this.s1 = (max - min) * (max - mode);
        }

        public override double Next() {
            double r = Mt.NextDouble();

            if (r < thr) {
                return Min + Math.Sqrt(r * s0);
            }
            else {
                return Max - Math.Sqrt((1.0 - r) * s1);
            }
        }
    }

    public class UnitTriangularRandom : Random {
        private readonly MT19937 mt;

        public UnitTriangularRandom(MT19937 mt) {
            ArgumentNullException.ThrowIfNull(mt);

            this.mt = mt;
        }

        public override double Next() {
            return mt.NextDouble() + mt.NextDouble() - 1;
        }
    }
}
