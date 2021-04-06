using System;

namespace ExRandom.Continuous.Tests {
    public static class Util {
        public static (double[] cnt, double ave) Histogram(int N, int X_MIN, int X_MAX, int X_SCALE, Random rd) {
            double[] cnt = new double[(X_MAX - X_MIN) * X_SCALE + 1];
            double ave = 0;
            double BOXR = (double)X_SCALE / (double)N;

            int in_box;
            double r;
            for (int i = 0; i < N; i++) {
                r = rd.Next();
                ave += r;

                in_box = (int)Math.Floor((r - X_MIN) * X_SCALE);

                if (in_box >= 0 && in_box < cnt.Length) {
                    cnt[in_box] += BOXR;
                }
            }

            ave /= N;

            return (cnt, ave);
        }
    }
}