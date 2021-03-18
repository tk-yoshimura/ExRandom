using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.Continuous {
    public class StudentsTRandom : Random{
        readonly NormalRandom nd;
        readonly ChiSquaredRandom cd;
        readonly double inv_nu;

        public StudentsTRandom(MT19937 mt, uint nu = 2) { 
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(nu < 1) {
                throw new ArgumentException();
            }

            this.nd = new NormalRandom(mt);
            this.cd = new ChiSquaredRandom(mt, k : nu);
            this.inv_nu = 1.0 / (double)nu;
        }

        public override double Next() {
            double c = cd.Next(), z = nd.Next();

            return z / Math.Max(Math.Sqrt(c * inv_nu), Double.Epsilon);
        }
    }
}
