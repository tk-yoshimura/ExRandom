using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class IrwinHallRandom : Random{
        readonly MT19937 mt;
        readonly int n;

        public IrwinHallRandom(MT19937 mt, int n = 3) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(n < 1) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.n = n;
        }

        public override double Next() {
            double w = 0;

            for(int i = 0; i < n; i++) {
                w += mt.NextDouble();
            }

            return w;
        }
    }
}
