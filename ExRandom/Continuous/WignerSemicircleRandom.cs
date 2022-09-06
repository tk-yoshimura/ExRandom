using System;

namespace ExRandom.Continuous {
    public class WignerSemicircleRandom : Random {
        readonly UniformRandom ud;

        public MT19937 Mt { get; }
        public double S { get; }

        public WignerSemicircleRandom(MT19937 mt, double s = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(s > 0)) {
                throw new ArgumentOutOfRangeException(nameof(s));
            }

            this.Mt = mt;
            this.ud = new UniformRandom(mt, -1, 1);
            this.S = s;
        }

        public override double Next() {
            double r, thr;

            do {
                r = ud.Next();
                thr = Math.Sqrt(1 - r * r);
            } while (!Mt.NextBool(thr));

            return r * S;
        }
    }
}
