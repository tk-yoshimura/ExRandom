using System;

namespace ExRandom.Continuous {
    public class QGaussianRandom : Random {
        private readonly double q_prime, c;
        private readonly Func<double, double> q_logarithm;
        private double r;
        private bool is_pear_generate = false;

        public MT19937 Mt { get; }
        public double Q { get; }
        public double Beta { get; }
        public double Mu { get; }

        public QGaussianRandom(MT19937 mt, double q = 2, double beta = 1, double mu = 0) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(q < 3)) {
                throw new ArgumentOutOfRangeException(nameof(q));
            }
            if (!(beta > 0)) {
                throw new ArgumentOutOfRangeException(nameof(beta));
            }

            this.Mt = mt;
            this.Q = q;
            this.q_prime = (1 + q) / (3 - q);
            this.Beta = beta;
            this.Mu = mu;
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

                z1 = Mt.NextDouble_OpenInterval0();
                z2 = Mt.NextDouble_OpenInterval0();
                sq_log_z1 = Math.Sqrt(-2.0 * q_logarithm(z1));
                pi_z2 = 2.0 * Math.PI * z2;

                r = sq_log_z1 * Math.Sin(pi_z2);
                return sq_log_z1 * Math.Cos(pi_z2);
            }
        }

        public override double Next() {
            return Generate() * c + Mu;
        }
    }
}
