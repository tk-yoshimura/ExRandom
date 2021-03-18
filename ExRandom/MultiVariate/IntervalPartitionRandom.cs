using System;

////debug
//Next : output distribution check - OK
//Next : output cross cov check - yet

namespace ExRandom.MultiVariate {
    public class IntervalPartitionDiscreteRandom : Random<int> {
        readonly MT19937 mt;
        readonly int dim, interval;

        public IntervalPartitionDiscreteRandom(MT19937 mt, int dim = 3, int interval = 10) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(interval <= 0 || interval >= int.MaxValue / 2 || dim <= 0 || dim >= int.MaxValue / 2) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.dim = dim;
            this.interval = interval;
        }

        public override Vector<int> Next() {
            int n = interval;
            int[] v = new int[dim];
            double p, r;

            for(int d = dim - 1, x; d > 0; d--) {
                r = mt.NextDouble_OpenInterval1();
                p = (double)(d) / (double)(n + d);
                x = 0;

                do {
                    r -= p;
                    if(r <= 0) {
                        break;
                    }

                    p *= (double)(n - x) / (double)(n + d - x - 1);
                    x++;
                } while(x < n);

                v[d] = x;
                n -= x;
            }

            v[0] = n;

            RandomUtilitys.Shuffle(mt, v);

            return new Vector<int>(v);
        }
    }
    
    public class IntervalPartitionContinuousRandom : Random<double> {
        readonly MT19937 mt;
        readonly int dim;
        readonly double interval;
        readonly double[] array;
        
        public IntervalPartitionContinuousRandom(MT19937 mt, int dim, double interval) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(interval > 0) || dim <= 0 || dim >= int.MaxValue / 2) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.dim = dim;
            this.interval = interval;
            this.array = new double[dim + 1];
            this.array[0] = 0;
            this.array[dim] = 1;
        }

        public override Vector<double> Next() {
            int i;

            for(i = 1; i < dim; i++) {
                array[i] = mt.NextDouble();
            }

            Array.Sort(array, 1, dim - 1);

            double[] v = new double[dim];
            
            for(i = 0; i < dim; i++) {
                v[i] = (array[i + 1] - array[i]) * interval;
            }

            RandomUtilitys.Shuffle(mt, v);

            return new Vector<double>(v);
        }
    }
}
