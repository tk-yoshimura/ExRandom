using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Discrete {
    public class HyperGeometricRandom : Random {
        readonly MT19937 mt;
        readonly int[] indexs;
        readonly double[] probs;

        public HyperGeometricRandom(MT19937 mt, int n = 30, int k = 20, int m = 10) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (n <= 0 || k <= 0 || m <= 0 || n < k || n < m) {
                throw new ArgumentOutOfRangeException($"{n}>={k}>={m}>0");
            }

            if (k < m) {
                int swap;
                swap = k; k = m; m = swap;
            }

            int max = m, min = Math.Max(0, k + m - n);
            double p;
            double[] next_probs;

            this.mt = mt;

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
            double r = mt.NextDouble_OpenInterval1();

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
