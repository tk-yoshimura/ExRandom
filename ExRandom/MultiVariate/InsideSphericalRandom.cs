using System;

namespace ExRandom.MultiVariate {
    public class InsideSphericalRandom : Random<double> {
        public MT19937 Mt { get; }

        public InsideSphericalRandom(MT19937 mt) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.Mt = mt;
        }

        public override Vector<double> Next() {
            double theta, phi, r, s;

            theta = Mt.NextDouble() * 2 - 1;
            s = Math.Sqrt(1 - theta * theta);

            phi = 2 * Math.PI * Mt.NextDouble_OpenInterval1();
            r = Math.Pow(Mt.NextDouble(), 1.0 / 3.0);

            return new Vector<double>(r * s * Math.Cos(phi), r * s * Math.Sin(phi), r * theta);
        }
    }
}
