using System;

namespace ExRandom.Discrete {
    public class HyperGeometricRandom : Random {
        private readonly int[] indexs;
        private readonly double[] probs;

        public MT19937 Mt { get; }
        public int N { get; }
        public int K { get; }
        public int M { get; }

        public HyperGeometricRandom(MT19937 mt, int n = 30, int k = 20, int m = 10) {
            ArgumentNullException.ThrowIfNull(mt);
            if (n <= 0 || k <= 0 || m <= 0 || n < k || n < m) {
                throw new ArgumentOutOfRangeException($"{nameof(n)}>={nameof(k)}>={nameof(m)}>0");
            }

            if (k < m) {
                int swap;
                swap = k; k = m; m = swap;
            }

            int max = m, min = Math.Max(0, k + m - n);
            double p;
            double[] next_probs;

            this.Mt = mt;
            this.N = n;
            this.K = k;
            this.M = m;
            this.indexs = new int[max - min + 1];
            this.probs = new double[max - min + 1];

            for (int i = 0; i < indexs.Length; i++) {
                indexs[i] = i + min;
            }

            probs[max - min] = 1;

            for (int i = n, j; i > k; i--) {
                next_probs = new double[max - min + 1];

                for (j = Math.Max(max - n + i, min); j <= max; j++) {
                    if (j > 0) {
                        p = (i - j) / (double)(i);

                        next_probs[j - min] = probs[j - min] * p;
                        next_probs[j - min - 1] += probs[j - min] * (1 - p);
                    }
                    else {
                        next_probs[j - min] = probs[j - min];
                    }
                }

                probs = next_probs;
            }

            Array.Sort(this.probs, this.indexs);
        }

        public override int Next() {
            double r = Mt.NextDouble_OpenInterval1();

            for (int i = probs.Length - 1; i >= 0; i--) {
                r -= probs[i];
                if (r <= 0) {
                    return indexs[i];
                }
            }

            return indexs[probs.Length - 1];
        }
    }
}
