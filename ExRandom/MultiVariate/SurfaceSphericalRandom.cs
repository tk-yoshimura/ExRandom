using System;

namespace ExRandom.MultiVariate {
    public class SurfaceSphericalRandom : Random<double> {
        public MT19937 Mt { get; }

        public SurfaceSphericalRandom(MT19937 mt) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.Mt = mt;
        }

        public override Vector<double> Next() {
            double theta, phi, s;

            theta = Mt.NextDouble() * 2 - 1;
            s = Math.Sqrt(1 - theta * theta);

            phi = 2 * Math.PI * Mt.NextDouble_OpenInterval1();

            return new Vector<double>(s * Math.Cos(phi), s * Math.Sin(phi), theta);
        }
    }
}