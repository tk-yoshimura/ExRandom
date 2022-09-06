using System;
using System.Collections.Generic;

namespace ExRandom.Discrete {
    public class RouletteRandom : Random {
        readonly int[] indexs;
        readonly double[] probs;

        public MT19937 Mt { get; }
        public IReadOnlyList<double> Probs => probs;

        public RouletteRandom(MT19937 mt, params double[] probs) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            double sum_prob = 0;
            for (int i = 0; i < probs.Length; i++) {
                if (!(probs[i] >= 0)) {
                    throw new ArgumentOutOfRangeException(nameof(probs));
                }

                sum_prob += probs[i];
            }

            if (!(sum_prob > 0)) {
                throw new ArgumentOutOfRangeException(nameof(probs));
            }

            this.Mt = mt;

            this.indexs = new int[probs.Length];
            this.probs = new double[probs.Length];

            for (int i = 0; i < probs.Length; i++) {
                this.indexs[i] = i;
                this.probs[i] = probs[i] / sum_prob;
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
