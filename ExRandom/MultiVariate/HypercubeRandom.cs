using System;
using System.Linq;

////debug
//Next : output distribution OK

namespace ExRandom.MultiVariate {
    public class HypercubeRandom : Random<double>{
        readonly MT19937 mt;
        readonly int dim;

        public HypercubeRandom(MT19937 mt, int dim) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(dim < 1) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.dim = dim;
        }

        public override Vector<double> Next() {
            return new Vector<double>(new double[dim].Select((d) => mt.NextDouble()).ToArray());
        }
    }
}
