using System;
using System.Linq;

namespace ExRandom.MultiVariate {
    public class HypercubeRandom : Random<double> {
        public MT19937 Mt { get; }
        public int Dim { get; }

        public HypercubeRandom(MT19937 mt, int dim) {
            ArgumentNullException.ThrowIfNull(mt);
            if (dim < 1) {
                throw new ArgumentOutOfRangeException(nameof(dim));
            }

            this.Mt = mt;
            this.Dim = dim;
        }

        public override Vector<double> Next() {
            return new Vector<double>(new double[Dim].Select((d) => Mt.NextDouble()).ToArray());
        }
    }
}
