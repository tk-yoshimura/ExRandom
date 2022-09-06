namespace ExRandom.NoiseGenerator {
    public class VioletNoise : Noise {
        private readonly Continuous.NormalRandom nd;
        private double state = 0;

        public MT19937 Mt => nd.Mt;

        public VioletNoise(MT19937 mt) : base(-2) {
            this.nd = new Continuous.NormalRandom(mt);

            Generate(128);
        }

        public override double Generate() {
            double r = nd.Next();
            double d = state - r;

            state = r;

            return d;
        }
    }
}
