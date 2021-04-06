using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Continuous {
    public class InverseChiSquaredRandom : Random {
        readonly ChiSquaredRandom cr;

        public InverseChiSquaredRandom(MT19937 mt, uint k = 2) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.cr = new ChiSquaredRandom(mt, k: k);
        }

        public override double Next() {
            return 1.0 / cr.Next();
        }
    }
}
