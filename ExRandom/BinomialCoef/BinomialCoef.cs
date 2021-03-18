namespace ExRandom {
    internal static class Binomial {
        public static double[] Coef(int n, double p) {
            double[] b0 = {1}, b1;

            for(int i = 1, j; i <= n; i++) {
                b1 = new double[i + 1];

                for(j = 0; j < i; j++) {
                    b1[j] += (1 - p) * b0[j];
                    b1[j + 1] += p * b0[j]; 
                }

                b0 = b1;
            }

            return b0;
        }
    }
}
