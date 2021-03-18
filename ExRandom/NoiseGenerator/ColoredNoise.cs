using System;

namespace ExRandom.NoiseGenerator {
    public class ColoredNoise : Noise {
        readonly int size;
        readonly double decay;
        readonly double[] coef, state;
        readonly Continuous.NormalRandom nd;

        int pos = 0;

        public ColoredNoise(MT19937 mt, double alpha, int precision = 6) : base(alpha){
            if (!(alpha >= -2) || alpha > 2) {
                throw new ArgumentException(nameof(alpha));
            }

            if (precision < 4 || precision > 12) {
                throw new ArgumentException(nameof(precision));
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
