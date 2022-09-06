using System;

namespace ExRandom.Continuous {
    public class HyperbolicSecantRandom : Random {
        public MT19937 Mt { get; }

        public HyperbolicSecantRandom(MT19937 mt) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.Mt = mt;
        }

        public override double Next() {
            const double PI_DIV_2 = Math.PI * 0.5, INV_PI_DIV_2 = 2.0 / Math.PI;

            double r = Mt.NextDouble_OpenInterval01();

            return INV_PI_DIV_2 * Math.Log(Math.Tan(PI_DIV_2 * r));
        }
    }
}
