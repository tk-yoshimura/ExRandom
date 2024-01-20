using System;

namespace ExRandom.NoiseGenerator {
    public class ColoredNoise : Noise {
        private readonly int size;
        private readonly double decay;
        private readonly double[] coef, state;
        private readonly Continuous.NormalRandom nd;
        private int pos = 0;

        public MT19937 Mt { get; }

        public ColoredNoise(MT19937 mt, double alpha, int precision = 6) : base(alpha) {
            ArgumentNullException.ThrowIfNull(mt);
            if (!(alpha >= -2) || alpha > 2) {
                throw new ArgumentOutOfRangeException(nameof(alpha));
            }
            if (precision < 4 || precision > 12) {
                throw new ArgumentOutOfRangeException(nameof(precision));
            }

            this.size = 1 << precision;
            this.coef = new double[this.size];
            this.state = new double[this.size];

            this.nd = new Continuous.NormalRandom(mt);

            double a = (alpha >= 0) ? alpha : (alpha + 2);
            this.decay = 1 - a * a * 2.5e-4;

            this.coef[0] = -a / 2;
            for (int i = 1; i < this.size; i++) {
                this.coef[i] = this.coef[i - 1] * (i - a / 2) / (i + 1);
                this.state[i] = nd.Next();
            }

            Generate(this.size * 8);
            this.Mt = mt;
        }

        public override double Generate() {
            double prev = state[pos], next = nd.Next();

            for (int i = 0; i < size; i++) {
                next -= coef[i] * state[(i + pos) & (size - 1)];
            }

            pos = pos > 0 ? (pos - 1) : (size - 1);

            next *= decay;
            state[pos] = next;

            return (Alpha >= 0) ? next : (next - prev);
        }
    }
}
