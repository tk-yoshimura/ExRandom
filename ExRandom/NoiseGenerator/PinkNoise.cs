namespace ExRandom.NoiseGenerator {
    public class PinkNoise : ColoredNoise {
        public PinkNoise(MT19937 mt, int precision = 6) : base(mt, 1, precision) { }
    }
}
