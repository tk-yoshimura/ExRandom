using System;

namespace ExRandom.Continuous {
    public class QExponentialRandom : Random {
        readonly MT19937 mt;
        readonly double q, q_prime, lambda, c;
        readonly Func<double, double> q_logarithm;

        public QExponentialRandom(MT19937 mt, double q = 0.5, double lambda = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (!(q < 2)) {
                throw new ArgumentOutOfRangeException(nameof(q));
            }
            if (!(lambda > 0)) {
                throw new ArgumentOutOfRangeException(nameof(lambda));
            }

            this.mt = mt;
            this.q = q;
            this.q_prime = 1 / (2 - q);
            this.lambda = lambda;
            this.c = -q_prime / lambda;

            if (q_prime == 1) {
                this.q_logarithm = (x) => Math.Log(x);
            }
            else {
                this.q_logarithm = (x) => (Math.Pow(x, 1 - q_prime) - 1) / (1 - q_prime);
            }
        }

        public override double Next() {
            return c * q_logarithm(mt.NextDouble_OpenInterval0());
        }
    }
}
