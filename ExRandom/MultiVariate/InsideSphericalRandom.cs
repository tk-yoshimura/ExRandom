using System;

namespace ExRandom.MultiVariate {
    public class InsideSphericalRandom : Random<double> {
        readonly MT19937 mt;

        public InsideSphericalRandom(MT19937 mt) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.mt = mt;
        }

        public override Vector<double> Next() {
            double theta, phi, r, s;

            theta = mt.NextDouble() * 2 - 1;
            s = Math.Sqrt(1 - theta * theta);

            phi = 2 * Math.PI * mt.NextDouble_OpenInterval1();
            r = Math.Pow(mt.NextDouble(), 1.0 / 3.0);

            return new Vector<double>(r * s * Math.Cos(phi), r * s * Math.Sin(phi), r * theta);
        }
    }
}
