using System;

namespace ExRandom.Continuous {
    public class InverseChiSquaredRandom : Random {
        private readonly ChiSquaredRandom cr;

        public MT19937 Mt { get; }
        public uint K { get; }

        public InverseChiSquaredRandom(MT19937 mt, uint k = 2) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.cr = new ChiSquaredRandom(mt, k: k);
            this.Mt = mt;
            this.K = k;
        }

        public override double Next() {
            return 1.0 / cr.Next();
        }
    }
}
