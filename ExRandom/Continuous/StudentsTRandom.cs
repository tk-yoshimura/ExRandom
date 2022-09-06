using System;

namespace ExRandom.Continuous {
    public class StudentsTRandom : Random {
        readonly NormalRandom nd;
        readonly ChiSquaredRandom cd;
        readonly double inv_nu;

        public MT19937 Mt { get; }
        public uint Nu { get; }

        public StudentsTRandom(MT19937 mt, uint nu = 2) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (nu < 1) {
                throw new ArgumentException(nameof(nu));
            }

            this.nd = new NormalRandom(mt);
            this.cd = new ChiSquaredRandom(mt, k: nu);
            this.inv_nu = 1.0 / nu;
            this.Mt = mt;
            this.Nu = nu;
        }

        public override double Next() {
            double c = cd.Next(), z = nd.Next();

            return z / Math.Max(Math.Sqrt(c * inv_nu), double.Epsilon);
        }
    }
}
