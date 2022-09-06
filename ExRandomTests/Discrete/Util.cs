using System;

namespace ExRandom.Discrete.Tests {
    public static class Util {
        public static double[] Histogram(int N, int INDEXES, Random rd) {
            double[] cnt = new double[INDEXES];

            for (int i = 0; i < N; i++) {
                int r = rd.Next();

                cnt[r]++;
            }

            for (int i = 0; i < INDEXES; i++) {
                cnt[i] /= N;
            }

            return cnt;
        }
    }
}