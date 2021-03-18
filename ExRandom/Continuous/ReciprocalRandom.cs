using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class ReciprocalRandom : Random{
        readonly MT19937 mt;
        readonly double a, b;

        public ReciprocalRandom(MT19937 mt, double a = 1, double b = 2) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(a > 0) || !(b > 0)) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.a = a;
            this.b = b;
        }

        public override double Next() {
            double u = mt.NextDouble();

            return Math.Pow(a, u) * Math.Pow(b, 1 - u);
        }
    }
}
