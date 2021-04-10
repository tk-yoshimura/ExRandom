using System;

namespace ExRandom.Continuous {
    public class VonMisesRandom : Random {
        readonly MT19937 mt;
        readonly double kappa, mu, s;

        public VonMisesRandom(MT19937 mt, double kappa, double mu) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(kappa >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(kappa));
            }

            this.mt = mt;
            this.kappa = kappa;
            this.mu = RoundMu(mu);
            this.s = (kappa > 1.3) ? (1 / Math.Sqrt(kappa)) : (Math.PI * Math.Exp(-kappa));
        }

        public override double Next() {
            double t;

            while (true) {
                double r1 = mt.NextDouble_OpenInterval01(), r2 = mt.NextDouble_OpenInterval01();
                t = s * (2 * r2 - 1) / r1;

                if (Math.Abs(t) > Math.PI) {
                    continue;
                }

                if ((kappa * t * t < 4 - 4 * r1) || (kappa * Math.Cos(t) >= 2 * Math.Log(r1) + kappa)) {
                    break;
                }
            }

            return RoundTheta(t + mu);
        }

        private static double RoundTheta(double theta) {
            if (theta > Math.PI) {
                theta -= 2 * Math.PI;
            }
            else if (theta < -Math.PI) {
                theta += 2 * Math.PI;
            }

            return theta;
        }

        private static double RoundMu(double mu) {
            mu += Math.PI;

            if (mu >= 0) {
                mu %= 2 * Math.PI;
            }
            else {
                mu = 2 * Math.PI - ((-mu) % (2 * Math.PI));
            }

            mu -= Math.PI;

            return mu;
        }
    }
}
