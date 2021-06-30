using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExRandom.Transform.Tests {
    [TestClass()]
    public class BinaryLogitTests {
        [TestMethod()]
        public void ConvertDoubleTest() {
            Assert.AreEqual(0,  BinaryLogit.Convert(0.5));
            Assert.AreEqual(-1, BinaryLogit.Convert(0.25), 1e-16);
            Assert.AreEqual(+1, BinaryLogit.Convert(0.75), 1e-16);
            Assert.AreEqual(-2, BinaryLogit.Convert(0.125), 1e-16);
            Assert.AreEqual(+2, BinaryLogit.Convert(0.875), 1e-16);
        }

        [TestMethod()]
        public void ConvertUInt64Test() {
            Assert.AreEqual(0,  BinaryLogit.Convert(0x8000000000000000ul));
            Assert.AreEqual(-1, BinaryLogit.Convert(0x4000000000000000ul), 1e-16);
            Assert.AreEqual(+1, BinaryLogit.Convert(0xC000000000000000ul), 1e-16);
            Assert.AreEqual(-2, BinaryLogit.Convert(0x2000000000000000ul), 1e-16);
            Assert.AreEqual(+2, BinaryLogit.Convert(0xE000000000000000ul), 1e-16);
            Assert.AreEqual(-3, BinaryLogit.Convert(0x1000000000000000ul), 1e-16);
            Assert.AreEqual(+3, BinaryLogit.Convert(0xF000000000000000ul), 1e-16);
            Assert.AreEqual(-4, BinaryLogit.Convert(0x0800000000000000ul), 1e-16);
            Assert.AreEqual(+4, BinaryLogit.Convert(0xF800000000000000ul), 1e-16);

            Assert.AreEqual(-0.1926450779423958, BinaryLogit.Convert(0x7000000000000000ul), 1e-16);
            Assert.AreEqual(+0.1926450779423958, BinaryLogit.Convert(0x9000000000000000ul), 1e-16);

            Assert.AreEqual(-6.813781191217037, BinaryLogit.Convert(0x0123456789ABCDEFul), 1e-16);
            Assert.AreEqual(+6.813781191217037, BinaryLogit.Convert(0xFEDCBA9876543210ul), 1e-16);

            Assert.AreEqual(-32, BinaryLogit.Convert(0x0000000080000000ul));
            Assert.AreEqual(-33, BinaryLogit.Convert(0x0000000040000000ul), 1e-16);
            Assert.AreEqual(+33, BinaryLogit.Convert(0xFFFFFFFFC0000000ul), 1e-16);
            Assert.AreEqual(-34, BinaryLogit.Convert(0x0000000020000000ul), 1e-16);
            Assert.AreEqual(+34, BinaryLogit.Convert(0xFFFFFFFFE0000000ul), 1e-16);
            Assert.AreEqual(-35, BinaryLogit.Convert(0x0000000010000000ul), 1e-16);
            Assert.AreEqual(+35, BinaryLogit.Convert(0xFFFFFFFFF0000000ul), 1e-16);
            Assert.AreEqual(-36, BinaryLogit.Convert(0x0000000008000000ul), 1e-16);
            Assert.AreEqual(+36, BinaryLogit.Convert(0xFFFFFFFFF8000000ul), 1e-16);

            Assert.AreEqual(-32.1926450779423958, BinaryLogit.Convert(0x0000000070000000ul), 1e-16);
            Assert.AreEqual(+32.1926450779423958, BinaryLogit.Convert(0xFFFFFFFF90000000ul), 1e-16);

            Assert.AreEqual(-38.81378123186138, BinaryLogit.Convert(0x0000000001234567ul), 1e-16);
            Assert.AreEqual(+38.81378123186138, BinaryLogit.Convert(0xFFFFFFFFFEDCBA99ul), 1e-16);

            Assert.AreEqual(-64, BinaryLogit.Convert(0x0000000000000000ul));
            Assert.AreEqual(-63, BinaryLogit.Convert(0x0000000000000001ul));
            Assert.AreEqual(+63, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul));
            Assert.AreEqual(-62, BinaryLogit.Convert(0x0000000000000002ul));
            Assert.AreEqual(+62, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFEul));
        }

        [TestMethod()]
        public void ConvertUInt128Test() {
            Assert.AreEqual(0,  BinaryLogit.Convert(0x8000000000000000ul, 0ul));
            Assert.AreEqual(-1, BinaryLogit.Convert(0x4000000000000000ul, 0ul), 1e-16);
            Assert.AreEqual(+1, BinaryLogit.Convert(0xC000000000000000ul, 0ul), 1e-16);
            Assert.AreEqual(-2, BinaryLogit.Convert(0x2000000000000000ul, 0ul), 1e-16);
            Assert.AreEqual(+2, BinaryLogit.Convert(0xE000000000000000ul, 0ul), 1e-16);
            Assert.AreEqual(-3, BinaryLogit.Convert(0x1000000000000000ul, 0ul), 1e-16);
            Assert.AreEqual(+3, BinaryLogit.Convert(0xF000000000000000ul, 0ul), 1e-16);
            Assert.AreEqual(-4, BinaryLogit.Convert(0x0800000000000000ul, 0ul), 1e-16);
            Assert.AreEqual(+4, BinaryLogit.Convert(0xF800000000000000ul, 0ul), 1e-16);

            Assert.AreEqual(-0.1926450779423958, BinaryLogit.Convert(0x7000000000000000ul, 0ul), 1e-16);
            Assert.AreEqual(+0.1926450779423958, BinaryLogit.Convert(0x9000000000000000ul, 0ul), 1e-16);

            Assert.AreEqual(-6.813781191217037, BinaryLogit.Convert(0x0123456789ABCDEFul, 0ul), 1e-16);
            Assert.AreEqual(+6.813781191217037, BinaryLogit.Convert(0xFEDCBA9876543210ul, 0ul), 1e-16);

            Assert.AreEqual(-32, BinaryLogit.Convert(0x0000000080000000ul, 0ul));
            Assert.AreEqual(-33, BinaryLogit.Convert(0x0000000040000000ul, 0ul), 1e-16);
            Assert.AreEqual(+33, BinaryLogit.Convert(0xFFFFFFFFC0000000ul, 0ul), 1e-16);
            Assert.AreEqual(-34, BinaryLogit.Convert(0x0000000020000000ul, 0ul), 1e-16);
            Assert.AreEqual(+34, BinaryLogit.Convert(0xFFFFFFFFE0000000ul, 0ul), 1e-16);
            Assert.AreEqual(-35, BinaryLogit.Convert(0x0000000010000000ul, 0ul), 1e-16);
            Assert.AreEqual(+35, BinaryLogit.Convert(0xFFFFFFFFF0000000ul, 0ul), 1e-16);
            Assert.AreEqual(-36, BinaryLogit.Convert(0x0000000008000000ul, 0ul), 1e-16);
            Assert.AreEqual(+36, BinaryLogit.Convert(0xFFFFFFFFF8000000ul, 0ul), 1e-16);

            Assert.AreEqual(-32.1926450779423958, BinaryLogit.Convert(0x0000000070000000ul, 0ul), 1e-16);
            Assert.AreEqual(+32.1926450779423958, BinaryLogit.Convert(0xFFFFFFFF90000000ul, 0ul), 1e-16);

            Assert.AreEqual(-38.813781191217037, BinaryLogit.Convert(0x0000000001234567ul, 0x89ABCDEF00000000ul), 1e-16);
            Assert.AreEqual(+38.813781191217037, BinaryLogit.Convert(0xFFFFFFFFFEDCBA98ul, 0x7654321000000000ul), 1e-16);

            Assert.AreEqual(-64, BinaryLogit.Convert(0ul, 0x8000000000000000ul));
            Assert.AreEqual(-65, BinaryLogit.Convert(0ul, 0x4000000000000000ul), 1e-16);
            Assert.AreEqual(+65, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xC000000000000000ul), 1e-16);
            Assert.AreEqual(-66, BinaryLogit.Convert(0ul, 0x2000000000000000ul), 1e-16);
            Assert.AreEqual(+66, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xE000000000000000ul), 1e-16);
            Assert.AreEqual(-67, BinaryLogit.Convert(0ul, 0x1000000000000000ul), 1e-16);
            Assert.AreEqual(+67, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xF000000000000000ul), 1e-16);
            Assert.AreEqual(-68, BinaryLogit.Convert(0ul, 0x0800000000000000ul), 1e-16);
            Assert.AreEqual(+68, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xF800000000000000ul), 1e-16);

            Assert.AreEqual(-64.1926450779423958, BinaryLogit.Convert(0ul, 0x7000000000000000ul), 1e-16);
            Assert.AreEqual(+64.1926450779423958, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0x9000000000000000ul), 1e-16);

            Assert.AreEqual(-70.813781191217037, BinaryLogit.Convert(0ul, 0x0123456789ABCDEFul), 1e-16);
            Assert.AreEqual(+70.813781191217037, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xFEDCBA9876543210ul), 1e-16);

            Assert.AreEqual(-96, BinaryLogit.Convert(0ul, 0x0000000080000000ul));
            Assert.AreEqual(-97, BinaryLogit.Convert(0ul, 0x0000000040000000ul), 1e-16);
            Assert.AreEqual(+97, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFC0000000ul), 1e-16);
            Assert.AreEqual(-98, BinaryLogit.Convert(0ul, 0x0000000020000000ul), 1e-16);
            Assert.AreEqual(+98, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFE0000000ul), 1e-16);
            Assert.AreEqual(-99, BinaryLogit.Convert(0ul, 0x0000000010000000ul), 1e-16);
            Assert.AreEqual(+99, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFF0000000ul), 1e-16);
            Assert.AreEqual(-100, BinaryLogit.Convert(0ul, 0x0000000008000000ul), 1e-16);
            Assert.AreEqual(+100, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFF8000000ul), 1e-16);

            Assert.AreEqual(-96.1926450779423958, BinaryLogit.Convert(0ul, 0x0000000070000000ul), 1e-16);
            Assert.AreEqual(+96.1926450779423958, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFF90000000ul), 1e-16);


            Assert.AreEqual(-102.81378123186137, BinaryLogit.Convert(0ul, 0x0000000001234567ul), 1e-16);
            Assert.AreEqual(+102.81378123186137, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFFEDCBA99ul), 1e-16);

            Assert.AreEqual(-128, BinaryLogit.Convert(0x0000000000000000ul, 0x0000000000000000ul));
            Assert.AreEqual(-127, BinaryLogit.Convert(0x0000000000000000ul, 0x0000000000000001ul));
            Assert.AreEqual(+127, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFFFFFFFFFul));
            Assert.AreEqual(-126, BinaryLogit.Convert(0x0000000000000000ul, 0x0000000000000002ul));
            Assert.AreEqual(+126, BinaryLogit.Convert(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFFFFFFFFEul));
        }
    }
}