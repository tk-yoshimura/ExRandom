using System;
using System.Collections.Generic;
using System.Linq;

namespace ExRandom {
    public static class RandomUtilitys {
        public static void Shuffle<Type>(MT19937 mt, Type[] array) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (array is null) {
                throw new ArgumentNullException(nameof(array));
            }

            Discrete.DiceRandom rd;

            for (int i = array.Length - 1, j; i >= 1; i--) {
                rd = new Discrete.DiceRandom(mt, i + 1);

                j = rd.Next();

                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        public static IEnumerable<Type> Select<Type>(MT19937 mt, Type[] array, int select_num) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (array is null) {
                throw new ArgumentNullException(nameof(array));
            }

            bool[] v = Fill(mt, array.Length, select_num);

            for (int i = 0; i < v.Length; i++) {
                if (v[i]) {
                    yield return array[i];
                }
            }

            yield break;
        }

        public static double RejectionNext(MT19937 mt, Continuous.Random rd, Func<double, double> adopt_prob_func) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (rd is null) {
                throw new ArgumentNullException(nameof(rd));
            }
            if (adopt_prob_func is null) {
                throw new ArgumentNullException(nameof(adopt_prob_func));
            }

            double r;

            do {
                r = rd.Next();
            } while (!mt.NextBool(adopt_prob_func(r)));

            return r;
        }

        public static int Round(MT19937 mt, double v) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            int v_int = (int)Math.Floor(v);
            double v_frac = v - v_int;

            return v_int + (mt.NextBool(v_frac) ? 1 : 0);
        }

        public static bool[] Fill(MT19937 mt, int length, int true_num) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (length <= 0 || length < true_num) {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            int index, cnt;
            bool[] array;

            Discrete.DiceRandom dr = new(mt, length);

            if (true_num < length / 2) {
                array = new bool[length];
                cnt = 0;

                while (cnt < true_num) {
                    index = dr.Next();

                    if (array[index] == false) {
                        array[index] = true;
                        cnt++;
                    }
                }
            }
            else {
                array = (new bool[length]).Select((b) => { return true; }).ToArray();
                cnt = length;

                while (cnt > true_num) {
                    index = dr.Next();

                    if (array[index]) {
                        array[index] = false;
                        cnt--;
                    }
                }
            }


            return array;
        }

        public static bool[,] Fill(MT19937 mt, int length0, int length1, int true_num) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            int array_size;

            checked {
                array_size = length0 * length1;
            }

            if (length0 <= 0 || length1 <= 0) {
                throw new ArgumentOutOfRangeException($"{nameof(length0)},{nameof(length1)}");
            }
            if (array_size < true_num) {
                throw new ArgumentOutOfRangeException(nameof(true_num));
            }

            bool[] v = Fill(mt, array_size, true_num);
            bool[,] array = new bool[length0, length1];

            for (int i, j = 0, index = 0; j < length1; j++) {
                for (i = 0; i < length0; i++, index++) {
                    array[i, j] = v[index];
                }
            }

            return array;
        }

        public static bool[,,] Fill(MT19937 mt, int length0, int length1, int length2, int true_num) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            int array_size;

            checked {
                array_size = length0 * length1 * length2;
            }

            if (length0 <= 0 || length1 <= 0 || length2 <= 0) {
                throw new ArgumentOutOfRangeException($"{nameof(length0)},{nameof(length1)},{nameof(length2)}");
            }
            if (array_size < true_num) {
                throw new ArgumentOutOfRangeException(nameof(true_num));
            }

            bool[] v = Fill(mt, array_size, true_num);
            bool[,,] array = new bool[length0, length1, length2];

            for (int i, j, k = 0, index = 0; k < length2; k++) {
                for (j = 0; j < length1; j++) {
                    for (i = 0; i < length0; i++, index++) {
                        array[i, j, k] = v[index];
                    }
                }
            }

            return array;
        }

        public static bool[,,,] Fill(MT19937 mt, int length0, int length1, int length2, int length3, int true_num) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }

            int array_size;

            checked {
                array_size = length0 * length1 * length2 * length3;
            }

            if (length0 <= 0 || length1 <= 0 || length2 <= 0 || length3 <= 0) {
                throw new ArgumentOutOfRangeException($"{nameof(length0)},{nameof(length1)},{nameof(length2)},{nameof(length3)}");
            }
            if (array_size < true_num) {
                throw new ArgumentOutOfRangeException(nameof(true_num));
            }

            bool[] v = Fill(mt, array_size, true_num);
            bool[,,,] array = new bool[length0, length1, length2, length3];

            for (int i, j, k, l = 0, index = 0; l < length3; l++) {
                for (k = 0; k < length2; k++) {
                    for (j = 0; j < length1; j++) {
                        for (i = 0; i < length0; i++, index++) {
                            array[i, j, k, l] = v[index];
                        }
                    }
                }
            }

            return array;
        }
    }
}
