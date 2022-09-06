using System;

namespace ExRandom.Continuous {
    public class NormalRandom : Random {
        double r;
        bool is_pear_generate = false;

        public MT19937 Mt { get; }
        public double Sigma { get; }
        public double Mu { get; }

        public NormalRandom(MT19937 mt, double sigma = 1, double mu = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.Mt = mt;
            this.Sigma = sigma;
            this.Mu = mu;
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
                sq_log_z1 = Math.Sqrt(-2.0 * Math.Log(z1));
                pi_z2 = 2.0 * Math.PI * z2;

                r = sq_log_z1 * Math.Sin(pi_z2);
                return sq_log_z1 * Math.Cos(pi_z2);
            }
        }

        public override double Next() {
            return Generate() * Sigma + Mu;
        }
    }
}
