using System;

namespace ExRandom.Continuous {
    public class FishersZRandom : Random {
        readonly SnedecorsFRandom sd;

        public FishersZRandom(MT19937 mt, uint d1 = 2, uint d2 = 2) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.sd = new SnedecorsFRandom(mt, d1, d2);
        }

        public override double Next() {
            double s;

            do {
                s = sd.Next();
            } while (s <= 0);

            return Math.Log(s) * 0.5;
        }
    }
}
