namespace ExRandom.Discrete {
    public abstract class Random {
        public abstract int Next();

        public int[] Next(int num) {
            int[] array = new int[num];

            for (int i = 0; i < array.Length; i++) {
                array[i] = Next();
            }

            return array;
        }
    }
}
