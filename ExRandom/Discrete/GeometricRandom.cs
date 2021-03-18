using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Discrete {
    public class GeometricRandom : Random{
        const int dist_size = 16;
        readonly MT19937 mt;
        readonly int max;
        readonly double[] dist;

        public GeometricRandom(MT19937 mt, double prob = 0.5, int max = int.MaxValue) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(prob > 0) || !(prob <= 1) || max < 1) { 
                throw new ArgumentException();
            }

            this.mt = mt;
            this.max = max;

            double sum_prob = 0;

            this.dist = new double[dist_size];
            for(int i = 0; i < dist_size; i++) {
                this.dist[i] = (1 - sum_prob) * prob;
                sum_prob += this.dist[i];
            }
        }

        public GeometricRandom(MT19937 mt, decimal prob, int max = int.MaxValue) : this(mt, (double)prob, max){
        }

        public override int Next() {
            int i = 1;
            
            for(;;) {
                double r = mt.NextDouble_OpenInterval1();

                foreach(var d in dist) {
                    r -= d;
                    if(r <= 0 || i >= max){
                        return i;
                    }
                    i++;
                }
            }
        }
    }
}
