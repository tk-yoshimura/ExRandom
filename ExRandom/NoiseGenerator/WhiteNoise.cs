namespace ExRandom.NoiseGenerator {
    public class WhiteNoise : Noise {
        private readonly Continuous.NormalRandom nd;

        public WhiteNoise(MT19937 mt) : base(0) {
            this.nd = new Continuous.NormalRandom(mt);
        }

        public override double Generate() {
            return nd.Next();
        }
    }
}
