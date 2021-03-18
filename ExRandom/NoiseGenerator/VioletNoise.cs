namespace ExRandom.NoiseGenerator {
    public class VioletNoise : Noise {
        readonly Continuous.NormalRandom nd;

        double state = 0;

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
