using System;

namespace ExRandom.MultiVariate {
    public class IntervalPartitionDiscreteRandom : Random<int> {
        public MT19937 Mt { get; }
        public int Dim { get; }
        public int Interval { get; }

        public IntervalPartitionDiscreteRandom(MT19937 mt, int dim = 3, int interval = 10) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (interval <= 0 || interval >= int.MaxValue / 2) {
                throw new ArgumentOutOfRangeException(nameof(interval));
            }
            if (dim <= 0 || dim >= int.MaxValue / 2) {
                throw new ArgumentOutOfRangeException(nameof(dim));
            }

            this.Mt = mt;
            this.Dim = dim;
            this.Interval = interval;
        }

        public override Vector<int> Next() {
            int n = Interval;
            int[] v = new int[Dim];
            double p, r;

            for (int d = Dim - 1, x; d > 0; d--) {
                r = Mt.NextDouble_OpenInterval1();
                p = d / (double)(n + d);
                x = 0;

                do {
                    r -= p;
                    if (r <= 0) {
                        break;
                    }

                    p *= (n - x) / (double)(n + d - x - 1);
                    x++;
                } while (x < n);

                v[d] = x;
                n -= x;
            }

            v[0] = n;

            RandomUtilitys.Shuffle(Mt, v);

            return new Vector<int>(v);
        }
    }

    public class IntervalPartitionContinuousRandom : Random<double> {
        readonly double[] array;

        public MT19937 Mt { get; }
        public int Dim { get; }
        public double Interval { get; }

        public IntervalPartitionContinuousRandom(MT19937 mt, int dim, double interval) {
            if (!(interval > 0)) {
                throw new ArgumentOutOfRangeException(nameof(interval));
            }

            if (dim <= 0 || dim >= int.MaxValue / 2) {
                throw new ArgumentException(nameof(dim));
            }

            this.Mt = mt;
            this.Dim = dim;
            this.Interval = interval;
            this.array = new double[dim + 1];
            this.array[0] = 0;
            this.array[dim] = 1;
        }

        public override Vector<double> Next() {
            int i;

            for (i = 1; i < Dim; i++) {
                array[i] = Mt.NextDouble();
            }

            Array.Sort(array, 1, Dim - 1);

            double[] v = new double[Dim];

            for (i = 0; i < Dim; i++) {
                v[i] = (array[i + 1] - array[i]) * Interval;
            }

            RandomUtilitys.Shuffle(Mt, v);

            return new Vector<double>(v);
        }
    }
}
