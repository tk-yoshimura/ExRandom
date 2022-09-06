namespace ExRandom.NoiseGenerator {
    public class BrownNoise : Noise {
        private const double decay = 0.999;
        private readonly Continuous.NormalRandom nd;
        private double state = 0;

        public MT19937 Mt => nd.Mt;

        public BrownNoise(MT19937 mt) : base(2) {
            this.nd = new Continuous.NormalRandom(mt);

            Generate(128);
        }

        public override double Generate() {
            state += nd.Next();
            state *= decay;

            return state;
        }
    }
}
