using System;

namespace ExRandom.Discrete {
    public class GeometricRandom : Random {
        private const int dist_size = 16;
        private readonly double[] dist;

        public MT19937 Mt { get; }
        public double Prob { get; }
        public int Max { get; }

        public GeometricRandom(MT19937 mt, double prob = 0.5, int max = int.MaxValue) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(prob > 0) || !(prob <= 1)) {
                throw new ArgumentOutOfRangeException(nameof(prob));
            }
            if (max < 1) {
                throw new ArgumentOutOfRangeException(nameof(max));
            }

            this.Mt = mt;
            this.Prob = prob;
            this.Max = max;

            double sum_prob = 0;

            this.dist = new double[dist_size];
            for (int i = 0; i < dist_size; i++) {
                this.dist[i] = (1 - sum_prob) * prob;
                sum_prob += this.dist[i];
            }
        }

        public override int Next() {
            int i = 1;

            for (; ; ) {
                double r = Mt.NextDouble_OpenInterval1();

                foreach (var d in dist) {
                    r -= d;
                    if (r <= 0 || i >= Max) {
                        return i;
                    }
                    i++;
                }
            }
        }
    }
}
