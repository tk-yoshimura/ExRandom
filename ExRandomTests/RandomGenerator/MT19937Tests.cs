using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExRandom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExRandom.Tests {
    [TestClass()]
    public class MT19937Tests {
        [TestMethod()]
        public void MT19937Test() {
            int[] seeds = { 0x123, 0x234, 0x345, 0x456 };

            MT19937 mt = new MT19937(seeds);
            
            var output = mt.NextArray(1000);

            Assert.AreEqual(output[0], 1067595299u);
            Assert.AreEqual(output[1], 955945823u);
            Assert.AreEqual(output[624], 3768408841u);
            Assert.AreEqual(output[999], 3460025646u);
        }

        [TestMethod()]
        public void RangeTest() {
            const int double_mantissa_bits = 52, uint32_bits = 32, uint64_bits = uint32_bits * 2;
            
            UInt64 f = ((UInt64)(~0u >> (uint64_bits - double_mantissa_bits)) << uint32_bits) | ~0u;
            UInt64 z = 0;

            double one = 1 / Math.ScaleB(f, -double_mantissa_bits);

            double fv = Math.ScaleB(f, -double_mantissa_bits);
            double zv = Math.ScaleB(z, -double_mantissa_bits);
            double fp = Math.ScaleB(f + 1, -double_mantissa_bits);
            double zp = Math.ScaleB(z + 1, -double_mantissa_bits);
            double log_zp = Math.Log(zp);
            double pow_zp = Math.Pow(zp, 20);

            Assert.IsTrue(fv < 1.0);
            Assert.AreEqual(0.0, zv);
            Assert.AreEqual(1.0, fp);
            Assert.IsTrue(zp > 0.0);

            Assert.IsTrue(double.IsFinite(log_zp));
            Assert.IsTrue(pow_zp > 0.0);

            double o01f = Math.ScaleB(f | 1ul, -double_mantissa_bits);
            double o01z = Math.ScaleB(z | 1ul, -double_mantissa_bits);

            Assert.IsTrue(o01f < 1.0);
            Assert.IsTrue(o01z > 0.0);
            Assert.IsTrue(o01f == 1.0 - o01z);

            double c01f = Math.ScaleB(f, -double_mantissa_bits) * one;
            double c01z = Math.ScaleB(z, -double_mantissa_bits) * one;

            Assert.AreEqual(1.0, c01f);
            Assert.AreEqual(0.0, c01z);
        }

        [TestMethod()]
        public void NextDoubleTest() {
            MT19937 mt = new MT19937();

            Console.WriteLine(mt.Next52());
            Console.WriteLine(mt.NextDouble());
            Console.WriteLine(mt.NextDouble_OpenInterval0());
            Console.WriteLine(mt.NextDouble_OpenInterval1());
            Console.WriteLine(mt.NextDouble_OpenInterval01());
        }
    }
}