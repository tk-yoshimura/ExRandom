using System;

namespace ExRandom.Continuous {
    public class QGaussianRandom : Random {
        readonly MT19937 mt;
        readonly double q, q_prime, beta, mu, c;
        readonly Func<double, double> q_logarithm;

        double r;
        bool is_pear_generate = false;

        public QGaussianRandom(MT19937 mt, double q = 2, double beta = 1, double mu = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(q < 3)) {
                throw new ArgumentOutOfRangeException(nameof(q));
            }
            if (!(beta > 0)) {
                throw new ArgumentOutOfRangeException(nameof(beta));
            }

            this.mt = mt;
            this.q = q;
            this.q_prime = (1 + q) / (3 - q);
            this.beta = beta;
            this.mu = mu;
            this.c = 1 / Math.Sqrt(beta * (3 - q));

            if (q_prime == 1) {
                this.q_logarithm = (x) => Math.Log(x);
            }
            else {
                this.q_logarithm = (x) => (Math.Pow(x, 1 - q_prime) - 1) / (1 - q_prime);
            }
        }

        private double Generate() {
            if (is_pear_generate) {
                is_pear_generate = false;

                return r;
            }
            else {
                double z1, z2, sq_log_z1, pi_z2;

                is_pear_generate = true;

                z1 = mt.NextDouble_OpenInterval0();
                z2 = mt.NextDouble_OpenInterval0();
                sq_log_z1 = Math.Sqrt(-2.0 * q_logarithm(z1));
                pi_z2 = 2.0 * Math.PI * z2;

                r = sq_log_z1 * Math.Sin(pi_z2);
                return sq_log_z1 * Math.Cos(pi_z2);
            }
        }

        public override double Next() {
            return Generate() * c + mu;
        }
    }
}
