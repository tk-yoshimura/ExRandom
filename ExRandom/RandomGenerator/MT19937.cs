using System;

////debug
//Genenate : output matching test - OK
//NextDouble : range check - OK
//NextDouble_OpenInterval** : range check - OK

// Mersenne Twister
// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html
// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/MT2002/emt19937ar.html
// This is a port of mt19937ar.c
// Last Change:  2010-08-17
// Maintainer:   Yukihiro Nakadaira <yukihiro.nakadaira@gmail.com>
// Original Copyright:
//   A C-program for MT19937, with initialization improved 2002/1/26.
//   Coded by Takuji Nishimura and Makoto Matsumoto.
//
//   Before using, initialize the state by using init_genrand(seed)
//   or init_by_array(init_key, key_length).
//
//   Copyright (C) 1997 - 2002, Makoto Matsumoto and Takuji Nishimura,
//   All rights reserved.
//
//   Redistribution and use in source and binary forms, with or without
//   modification, are permitted provided that the following conditions
//   are met:
//
//     1. Redistributions of source code must retain the above copyright
//        notice, this list of conditions and the following disclaimer.
//
//     2. Redistributions in binary form must reproduce the above copyright
//        notice, this list of conditions and the following disclaimer in the
//        documentation and/or other materials provided with the distribution.
//
//     3. The names of its contributors may not be used to endorse or promote
//        products derived from this software without specific prior written
//        permission.
//
//   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//   "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//   LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//   A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//   CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//   EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//   PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//   PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//   LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//   NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//   SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

namespace ExRandom {
    public class MT19937 {
        /* Floating-point number processing of .NET Core 3.0+ conforms to IEEE 754-2008. */
        const int double_mantissa_bits = 52, uint32_bits = 32, uint64_bits = uint32_bits * 2;
        const UInt64 mantissa_full = ((UInt64)(~0u >> (uint64_bits - double_mantissa_bits)) << uint32_bits) | (UInt64)(~0u);
        static readonly double one = 1 / Math.ScaleB(mantissa_full, -double_mantissa_bits);

        UInt32 pos;
        readonly UInt32[] state = new UInt32[624];

        /// <summary>コンストラクタ 時間による乱数種設定</summary>
        public MT19937() {
            Initialize();
        }

        /// <summary>コンストラクタ 乱数種指定</summary>
        public MT19937(int seed) {
            Initialize(seed);
        }

        /// <summary>コンストラクタ 乱数種指定配列版</summary>
        public MT19937(int[] seeds) {
            Initialize(seeds);
        }

        /// <summary>32bit符号なし整数の乱数値の生成</summary>
        private UInt32 Generate() {
            const UInt32 N = 624, M = 397, MATRIX = 0x9908B0DF, UPPER_MASK = 0x80000000, LOWER_MASK = 0x7FFFFFFF;

            int kk;
            UInt32 y;

            unchecked {
                if(pos >= N) {
                    for(kk = 0; kk < N - M; kk++) {
                        y = (state[kk] & UPPER_MASK) | (state[kk + 1] & LOWER_MASK);
                        state[kk] = state[kk + M] ^ (y >> 1) ^ (((y & 1) != 0) ? MATRIX : 0);
                    }
                    for(; kk < N - 1; kk++) {
                        y = (state[kk] & UPPER_MASK) | (state[kk + 1] & LOWER_MASK);
                        state[kk] = state[kk - (N - M)] ^ (y >> 1) ^ (((y & 1) != 0) ? MATRIX : 0);
                    }
                    y = (state[kk] & UPPER_MASK) | (state[0] & LOWER_MASK);
                    state[kk] = state[M - 1] ^ (y >> 1) ^ (((y & 1) != 0) ? MATRIX : 0);

                    pos = 0;
                }

                y = state[pos];
                pos++;

                y ^= (y >> 11);
                y ^= (y << 7) & 0x9D2C5680;
                y ^= (y << 15) & 0xEFC60000;
                y ^= (y >> 18);
            }

            return y;
        }

        /// <summary>時間による乱数種設定</summary>
        public void Initialize() {
            Initialize(Environment.TickCount);
        }

        /// <summary>乱数種指定</summary>
        public void Initialize(int seed) {
            state[0] = (UInt32)seed;
            for(pos = 1; pos < state.Length; pos++) {
                state[pos] = 0x6C078965 * (state[pos - 1] ^ (state[pos - 1] >> 30)) + pos;
            }
        }

        /// <summary>乱数種指定配列版</summary>
        public void Initialize(int[] seeds) {
            if(seeds == null || seeds.Length < 1) {
                throw new ArgumentException(nameof(seeds));
            }

            const int N = 624;

            int i = 1, j = 0, k = seeds.Length > N ? seeds.Length : N;

            Initialize(19650218);

            for(; k > 0; k--) {
                state[i] = (state[i] ^ ((state[i - 1] ^ (state[i - 1] >> 30)) * 1664525u)) + (UInt32)seeds[j] + (UInt32)j;
                i++;
                j++;
                if(i >= N) {
                    state[0] = state[N - 1];
                    i = 1;
                }
                if(j >= seeds.Length) {
                    j = 0;
                }
            }
            for(k = N - 1; k > 0; k--) {
                state[i] = (state[i] ^ ((state[i - 1] ^ (state[i - 1] >> 30)) * 1566083941u)) - (UInt32)i;
                i++;
                if(i >= N) {
                    state[0] = state[N - 1];
                    i = 1;
                }
            }

            state[0] = 0x80000000;

        }

        /// <summary>32bit符号なし整数の乱数値の生成</summary>
        public UInt32 Next() {
            return Generate();
        }

        /// <summary>32bit符号なし整数の乱数値配列の生成</summary>
        public UInt32[] NextArray(int size) {
            var array = new UInt32[size];

            for(int i = 0; i < array.Length; i++) {
                array[i] = Generate();
            }

            return array;
        }

        /// <summary>64bit符号なし整数の乱数値の生成</summary>
        public UInt64 Next64() {
            return ((UInt64)Generate()) << uint32_bits | (UInt64)Generate();
        }

        /// <summary>52bit符号なし整数の乱数値の生成</summary>
        public UInt64 Next52() {
            UInt32 h = Generate() >> (uint64_bits - double_mantissa_bits), l = Generate();

            return ((UInt64)h << uint32_bits) | ((UInt64)l);
        }

        /// <summary>二値乱数の生成 確率thrで1を返す</summary>
        public bool NextBool(double thr = 0.5) {
            return NextDouble_OpenInterval1() < thr;
        }

        /// <summary>閉区間[0,1]の乱数の生成</summary>
        public double NextDouble() {
            return Math.ScaleB(Next52(), -double_mantissa_bits) * one;
        }

        /// <summary>半開区間(0,1]の乱数の生成</summary>
        public double NextDouble_OpenInterval0() {
            return Math.ScaleB(Next52() + 1, -double_mantissa_bits);
        }

        /// <summary>半開区間[0,1)の乱数の生成</summary>
        public double NextDouble_OpenInterval1() {
            return Math.ScaleB(Next52(), -double_mantissa_bits);
        }

        /// <summary>開区間(0,1)の乱数の生成</summary>
        public double NextDouble_OpenInterval01() {
            return Math.ScaleB(Next52() | 1ul, -double_mantissa_bits);
        }
    }
}
