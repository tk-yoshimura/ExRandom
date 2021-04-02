using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class NormalRandom : Random {
        readonly MT19937 mt;
        readonly double sigma, mu;

        double r;
        bool is_pear_generate = false;

        public NormalRandom(MT19937 mt, double sigma = 1, double mu = 0) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            this.mt = mt;
            this.sigma = sigma;
            this.mu = mu;
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
                sq_log_z1 = Math.Sqrt(-2.0 * Math.Log(z1));
                pi_z2 = 2.0 * Math.PI * z2;

                r = sq_log_z1 * Math.Sin(pi_z2);
                return sq_log_z1 * Math.Cos(pi_z2);
            }
        }

        public override double Next() {
            return Generate() * sigma + mu;
        }
    }
}
