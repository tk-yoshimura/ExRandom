using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Discrete {
    public class BinomialRandom : Random {
        const int skips = 1024;

        readonly RouletteRandom rd, rd_skip;
        readonly int n;

        public BinomialRandom(MT19937 mt, double prob = 0.5, int n = 10) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(n <= 0) {
                throw new ArgumentException();
            }
            
            this.n = n;

            if(n <= skips) {
                double[] p = Binomial.Coef(n, prob);
                this.rd = new RouletteRandom(mt, p);
            }
            else { 
                double[] p = Binomial.Coef(n % skips, prob);
                this.rd = new RouletteRandom(mt, p);

                double[] p_skip = Binomial.Coef(skips, prob);
                this.rd_skip = new RouletteRandom(mt, p_skip);
            }
        }

        public BinomialRandom(MT19937 mt, decimal prob, int n = 10) : this(mt, (double)prob, n){
        }

        public override int Next() {
            int i = n, cnt = 0;

            while(i > skips) {
                cnt += rd_skip.Next();
                i -= skips;
            }

            cnt += rd.Next();

            return cnt;
        }
    }
}
