using System;

namespace ExRandom.Continuous {
    public class JohnsonsSBRandom : Random {
        public MT19937 Mt { get; }
        public double Delta { get; }
        public double Lambda { get; }
        public double Gamma { get; }
        public double Eta { get; }

        public JohnsonsSBRandom(MT19937 mt, double delta = 1, double lambda = 1, double gamma = 0, double eta = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(delta > 0)) {
                throw new ArgumentOutOfRangeException(nameof(delta));
            }
            if (!(lambda > 0)) {
                throw new ArgumentOutOfRangeException(nameof(lambda));
            }
            if (double.IsNaN(gamma)) {
                throw new ArgumentOutOfRangeException(nameof(gamma));
            }
            if (double.IsNaN(eta)) {
                throw new ArgumentOutOfRangeException(nameof(eta));
            }

            this.Mt = mt;
            this.Delta = delta;
            this.Lambda = lambda;
            this.Gamma = gamma;
            this.Eta = eta;
        }

        public override double Next() {
            double u = Mt.NextDouble_OpenInterval01(), inv_erf_u = ErrorFunction.Probit(u);
            double x = Math.Exp((inv_erf_u - Gamma) / Delta);

            return Lambda * x / (1 + x) + Eta;
        }
    }
}
