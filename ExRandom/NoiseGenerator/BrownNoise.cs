namespace ExRandom.NoiseGenerator {
    public class BrownNoise : Noise {
        const double decay = 0.999;
        readonly Continuous.NormalRandom nd;

        double state = 0;

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
