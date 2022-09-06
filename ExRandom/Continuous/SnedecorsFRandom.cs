using System;

namespace ExRandom.Continuous {
    public class SnedecorsFRandom : Random {
        private readonly ChiSquaredRandom cd1, cd2;

        public MT19937 Mt { get; }
        public double D1 { get; }
        public double D2 { get; }

        public SnedecorsFRandom(MT19937 mt, uint d1 = 2, uint d2 = 2) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.cd1 = new ChiSquaredRandom(mt, k: d1);
            this.cd2 = new ChiSquaredRandom(mt, k: d2);
            this.Mt = mt;
            this.D1 = d1;
            this.D2 = d2;
        }

        public override double Next() {
            double c1 = cd1.Next(), c2 = cd2.Next();

            return (c1 * D2) / Math.Max(c2 * D1, double.Epsilon);
        }
    }
}
