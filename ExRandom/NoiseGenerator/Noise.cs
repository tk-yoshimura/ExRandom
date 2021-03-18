using System.Linq;

namespace ExRandom.NoiseGenerator {
    public abstract class Noise {
        protected Noise(double alpha) {
            this.Alpha = alpha;
        }

        public double Alpha { get; }

        public abstract double Generate();

        public double[] Generate(int length) {
            return (new double[length]).Select((_) => Generate()).ToArray();
        }
    }
}
