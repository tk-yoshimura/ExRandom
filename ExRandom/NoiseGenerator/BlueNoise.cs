namespace ExRandom.NoiseGenerator {
    public class BlueNoise : ColoredNoise {
        public BlueNoise(MT19937 mt, int precision = 6) : base(mt, -1, precision) { }
    }
}
