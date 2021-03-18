using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Discrete {
    class NegativeBinomialRandom : Random{
        readonly BernoulliRandom bd;
        readonly int r, max;

        public NegativeBinomialRandom(MT19937 mt, double prob = 0.5, int r = 4, int max = int.MaxValue) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(max < 1 || r < 1) {
                throw new ArgumentException();
            }
            
            this.bd = new BernoulliRandom(mt, prob);
            this.r = r;
            this.max = max;
        }

        public NegativeBinomialRandom(MT19937 mt, decimal prob, int r = 4, int max = int.MaxValue) : this(mt, (double)prob, r, max){
        }

        public override int Next() {
            int cnt = 0;

            for(int i = 1; i < max; i++) {
                if(bd.NextBool()) {
                    cnt++;
                    if(cnt >= r) {
                        return i - cnt;
                    }
                }
            }

            return int.MaxValue;
        }
    }
}
