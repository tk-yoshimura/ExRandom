using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.MultiVariate {
    public class SurfaceSphericalRandom : Random<double> {
        readonly MT19937 mt;

        public SurfaceSphericalRandom(MT19937 mt) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.mt = mt;
        }

        public override Vector<double> Next() {
            double theta, phi, s;

            theta = mt.NextDouble() * 2 - 1;
            s = Math.Sqrt(1 - theta * theta);

            phi = 2 * Math.PI * mt.NextDouble_OpenInterval1();

            return new Vector<double>(s * Math.Cos(phi), s * Math.Sin(phi), theta);
        }
    }
}