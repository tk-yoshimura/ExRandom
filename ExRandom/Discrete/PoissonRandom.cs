using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Discrete {
    public class PoissonRandom : Random{
        readonly MT19937 mt;
        readonly double thr;
        readonly int max;

        public PoissonRandom(MT19937 mt, double lambda = 1, int max = int.MaxValue) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(lambda > 0) || (max < 1)) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.thr = Math.Exp(-lambda);
            this.max = max;
        }

        public PoissonRandom(MT19937 mt, decimal lambda, int max = int.MaxValue) : this(mt, (double)lambda, max){
        }

        public override int Next() {
            int cnt = -1;
            double m = 1;
            do {
                cnt++;
                m *= mt.NextDouble();

                if(cnt >= max)
                    break;
            } while(m >= thr);

            return cnt;
        }
    }
}
