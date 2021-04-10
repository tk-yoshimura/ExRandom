using System;

namespace ExRandom.MultiVariate {
    public class InsideCircularRandom : Random<double> {
        readonly MT19937 mt;

        public InsideCircularRandom(MT19937 mt) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.mt = mt;
        }

        public override Vector<double> Next() {
            double theta, r;

            theta = 2 * Math.PI * mt.NextDouble_OpenInterval1();
            r = Math.Sqrt(mt.NextDouble());

            return new Vector<double>(r * Math.Cos(theta), r * Math.Sin(theta));
        }
    }
}
