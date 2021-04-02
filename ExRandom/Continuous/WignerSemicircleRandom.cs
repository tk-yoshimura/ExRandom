using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class WignerSemicircleRandom : Random {
        readonly MT19937 mt;
        readonly UniformRandom ud;
        readonly double s;

        public WignerSemicircleRandom(MT19937 mt, double s = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(s > 0)) {
                throw new ArgumentOutOfRangeException(nameof(s));
            }

            this.mt = mt;
            this.ud = new UniformRandom(mt, -1, 1);
            this.s = s;
        }

        public override double Next() {
            double r, thr;

            do {
                r = ud.Next();
                thr = Math.Sqrt(1 - r * r);
            } while (!mt.NextBool(thr));

            return r * s;
        }
    }
}
