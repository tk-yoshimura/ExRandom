using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Discrete {
    public class BernoulliRandom : Random{
        readonly MT19937 mt;
        readonly double thr;
        
        public BernoulliRandom(MT19937 mt, double thr = 0.5) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(thr >= 0) || thr > 1) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.thr = thr;
        }

        public BernoulliRandom(MT19937 mt, decimal thr) : this(mt, (double)thr){
        }

        public override int Next() {
            return NextBool() ? 1 : 0;
        }

        public bool NextBool() {
            return mt.NextDouble_OpenInterval1() < thr;
        }
    }
}
