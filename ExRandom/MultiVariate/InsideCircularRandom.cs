using System;

namespace ExRandom.MultiVariate {
    public class InsideCircularRandom : Random<double> {
        public MT19937 Mt { get; }

        public InsideCircularRandom(MT19937 mt) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.Mt = mt;
        }

        public override Vector<double> Next() {
            double theta, r;

            theta = 2 * Math.PI * Mt.NextDouble_OpenInterval1();
            r = Math.Sqrt(Mt.NextDouble());

            return new Vector<double>(r * Math.Cos(theta), r * Math.Sin(theta));
        }
    }
}
