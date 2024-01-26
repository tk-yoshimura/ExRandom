using System;

namespace ExRandom.Continuous {
    public class LandauRandom : Random {
        private const double pi_half = Math.PI / 2;
        private readonly double bias;

        public MT19937 Mt { get; }
        public double S { get; }
        public double Mu { get; }


        public LandauRandom(MT19937 mt, double s = 1, double mu = 0) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(s > 0)) {
                throw new ArgumentOutOfRangeException(nameof(s));
            }
            if (double.IsNaN(mu)) {
                throw new ArgumentOutOfRangeException(nameof(mu));
            }

            this.Mt = mt;
            this.S = s;
            this.Mu = mu;

            this.bias = s * Math.Log(s) / pi_half + mu;
        }

        public override double Next() {
            double u = (Mt.NextDouble_OpenInterval01() - 0.5) * Math.PI;
            double w = -Math.Log(Mt.NextDouble_OpenInterval0());

            double x = ((pi_half + u) * Math.Tan(u) - Math.Log(pi_half * w * Math.Cos(u) / (pi_half + u))) / pi_half;

            return x * S + bias;
        }
    }
}
