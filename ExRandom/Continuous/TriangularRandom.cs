using System;

namespace ExRandom.Continuous {
    public class TriangularRandom : Random {
        readonly MT19937 mt;
        readonly double min, mode, max, thr, s0, s1;

        public TriangularRandom(MT19937 mt, double min = -1, double mode = 0, double max = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(min < mode) || !(mode < max)) {
                throw new ArgumentOutOfRangeException($"{nameof(min)}<{nameof(mode)}<{nameof(max)}");
            }

            this.mt = mt;
            this.min = min;
            this.mode = mode;
            this.max = max;
            this.thr = (mode - min) / (max - min);
            this.s0 = (max - min) * (mode - min);
            this.s1 = (max - min) * (max - mode);
        }

        public override double Next() {
            double r = mt.NextDouble();

            if (r < thr) {
                return min + Math.Sqrt(r * s0);
            }
            else {
                return max - Math.Sqrt((1.0 - r) * s1);
            }
        }
    }

    public class UnitTriangularRandom : Random {
        readonly MT19937 mt;

        public UnitTriangularRandom(MT19937 mt) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.mt = mt;
        }

        public override double Next() {
            return mt.NextDouble() + mt.NextDouble() - 1;
        }
    }
}
