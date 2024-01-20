using System;

namespace ExRandom.Continuous {
    public class ChiSquaredRandom : Random {
        private readonly GammaRandom gr;

        public MT19937 Mt { get; }
        public uint K { get; }

        public ChiSquaredRandom(MT19937 mt, uint k = 2) {
            ArgumentNullException.ThrowIfNull(mt);
            if (k < 1) {
                throw new ArgumentOutOfRangeException(nameof(k));
            }

            this.gr = new GammaRandom(mt, kappa: k * 0.5, theta: 2);
            this.Mt = mt;
            this.K = k;
        }

        public override double Next() {
            return gr.Next();
        }
    }
}
