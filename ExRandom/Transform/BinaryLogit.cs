using System;
using System.Runtime.Intrinsics.X86;

namespace ExRandom.Transform {
    public static class BinaryLogit {

        public static double Convert(double v) {
            return v < 0.5 ? (Math.Log2(v) + 1) : (-Math.Log2(1 - v) - 1);
        }

        public static double Convert(ulong v) {
            if (v == 0x8000000000000000ul) {
                return 0;
            }

            double sign = 1;

            if (v < 0x8000000000000000ul) {
                sign = -1;
            }
            else {
                v = ~v + 1;
            }

            int lzc = LeadingZeroCount(v);

            if (lzc >= 64) {
                return sign * 64;
            }

            ulong bits = v << lzc;

            int n = lzc - 1;
            double f = Math.Log2(Math.ScaleB(bits, -64));

            double y = sign * (n - f);

            return y;
        }

        public static double Convert(ulong hi, ulong lo) {
            if (hi == 0x8000000000000000ul && lo == 0) {
                return 0;
            }

            double sign = 1;

            if (hi < 0x8000000000000000ul) {
                sign = -1;
            }
            else {
                (hi, lo) = Invert(hi, lo);
            }

            int lzc = LeadingZeroCount(hi, lo);

            if (lzc >= 128) {
                return sign * 128;
            }

            ulong bits = LeftShift(hi, lo, lzc);

            int n = lzc - 1;
            double f = Math.Log2(Math.ScaleB(bits, -64));

            double y = sign * (n - f);

            return y;
        }

        private static (ulong hi, ulong lo) Invert(ulong hi, ulong lo) {
            if (lo > 0) {
                hi = ~hi;
                lo = ~lo + 1;
            }
            else {
                hi = ~hi + 1;
                lo = 0;
            }

            return (hi, lo);
        }

        private static ulong LeftShift(ulong hi, ulong lo, int shifts) {
            if (shifts >= 64) {
                return lo << (shifts - 64);
            }
            else {
                return hi << shifts | (lo >> (64 - shifts));
            }
        }

        private static int LeadingZeroCount(ulong v) {
            if (v >= 0x100000000ul) {
                return (int)Lzcnt.LeadingZeroCount((uint)(v >> 32));
            }
            else if (v >= 0x1ul) {
                return 32 + (int)Lzcnt.LeadingZeroCount((uint)v);
            }
            else {
                return 64;
            }
        }

        private static int LeadingZeroCount(ulong hi, ulong lo) {
            if (hi >= 0x100000000ul) {
                return (int)Lzcnt.LeadingZeroCount((uint)(hi >> 32));
            }
            else if (hi >= 0x1ul) {
                return 32 + (int)Lzcnt.LeadingZeroCount((uint)hi);
            }
            else if (lo >= 0x100000000ul) {
                return 64 + (int)Lzcnt.LeadingZeroCount((uint)(lo >> 32));
            }
            else if (lo >= 0x1ul) {
                return 96 + (int)Lzcnt.LeadingZeroCount((uint)lo);
            }
            else {
                return 128;
            }
        }
    }
}
