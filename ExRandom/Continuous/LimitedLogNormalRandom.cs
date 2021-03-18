using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class LimitedLogNormalRandom : Random {
        readonly MT19937 mt;
        readonly LogNormalRandom lnd;
        readonly double inv_sq_limit;

        public LimitedLogNormalRandom(MT19937 mt, double s = 1, double m = 0, double limit = 10){
            if(mt == null) {
                throw new ArgumentNullException();
            }
            if(!(limit > 0)) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.lnd = new LogNormalRandom(mt, s, m);
            this.inv_sq_limit = 1.0 / (limit * limit);
        }

        public override double Next() {
            double r;

            do {
                r = lnd.Next();
            } while(mt.NextBool(r * r * inv_sq_limit));

            return r;
        }
    }
}