namespace ExRandom.Continuous {
    public abstract class Random {
        public abstract double Next();

        public double[] Next(int num) {
            double[] array = new double[num];

            for (int i = 0; i < array.Length; i++) {
                array[i] = Next();
            }

            return array;
        }
    }
}
