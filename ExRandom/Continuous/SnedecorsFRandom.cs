using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Continuous {
    public class SnedecorsFRandom : Random {
        readonly ChiSquaredRandom cd1, cd2;
        readonly double d1, d2;

        public SnedecorsFRandom(MT19937 mt, uint d1 = 2, uint d2 = 2) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.cd1 = new ChiSquaredRandom(mt, k: d1);
            this.cd2 = new ChiSquaredRandom(mt, k: d2);
            this.d1 = d1;
            this.d2 = d2;
        }

        public override double Next() {
            double c1 = cd1.Next(), c2 = cd2.Next();

            return (c1 * d2) / Math.Max(c2 * d1, double.Epsilon);
        }
    }
}
