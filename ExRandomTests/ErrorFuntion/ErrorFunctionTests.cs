using Microsoft.VisualStudio.TestTools.UnitTesting;

using static ExRandom.ErrorFunction;

namespace ExRandom.Tests {
    [TestClass()]
    public class ErrorFunctionTests {
        [TestMethod()]
        public void ErfTest() {
            Assert.AreEqual(-1, Erf(double.NegativeInfinity));
            Assert.AreEqual(+1, Erf(double.PositiveInfinity));

            Assert.AreEqual(-0.99532226501895273416206925636725293, Erf(-2), 1e-15);
            Assert.AreEqual(-0.84270079294971486934122063508260926, Erf(-1), 1e-15);
            Assert.AreEqual(-0.52049987781304653768274665389196453, Erf(-0.5), 1e-15);
            Assert.AreEqual(0, Erf(0));
            Assert.AreEqual(+0.52049987781304653768274665389196453, Erf(+0.5), 1e-15);
            Assert.AreEqual(+0.84270079294971486934122063508260926, Erf(+1), 1e-15);
            Assert.AreEqual(+0.99532226501895273416206925636725293, Erf(+2), 1e-15);
        }

        [TestMethod()]
        public void ErfcTest() {
            Assert.AreEqual(2, Erfc(double.NegativeInfinity));
            Assert.AreEqual(0, Erfc(double.PositiveInfinity));

            Assert.AreEqual(1.9953222650189527341620692563672529, Erfc(-2), 1e-15);
            Assert.AreEqual(1.8427007929497148693412206350826093, Erfc(-1), 1e-15);
            Assert.AreEqual(1.5204998778130465376827466538919645, Erfc(-0.5), 1e-15);
            Assert.AreEqual(1, Erfc(0));
            Assert.AreEqual(0.47950012218695346231725334610803547, Erfc(+0.5), 1e-15);
            Assert.AreEqual(0.15729920705028513065877936491739074, Erfc(+1), 1e-16);
            Assert.AreEqual(0.0046777349810472658379307436327470714, Erfc(+2), 1e-17);

            Assert.AreEqual(2, Erfc(-2) + Erfc(2));
            Assert.AreEqual(2, Erfc(-1) + Erfc(1));
            Assert.AreEqual(2, Erfc(-0.5) + Erfc(0.5));
        }

        [TestMethod()]
        public void InverseErfTest() {
            Assert.IsTrue(double.IsInfinity(InverseErf(-1)) && InverseErf(-1) < 0);
            Assert.IsTrue(double.IsInfinity(InverseErf(+1)) && InverseErf(+1) > 1);
            Assert.IsTrue(double.IsNaN(InverseErf(-2)));
            Assert.IsTrue(double.IsNaN(InverseErf(+2)));

            Assert.AreEqual(-2, InverseErf(Erf(-2)), 1e-14);
            Assert.AreEqual(-1, InverseErf(Erf(-1)), 1e-14);
            Assert.AreEqual(-0.5, InverseErf(Erf(-0.5)), 1e-14);
            Assert.AreEqual(0, InverseErf(Erf(0)));
            Assert.AreEqual(0.5, InverseErf(Erf(0.5)), 1e-14);
            Assert.AreEqual(1, InverseErf(Erf(1)), 1e-14);
            Assert.AreEqual(2, InverseErf(Erf(2)), 1e-14);
        }

        [TestMethod()]
        public void InverseErfcTest() {
            Assert.IsTrue(double.IsInfinity(InverseErfc(2)) && InverseErfc(2) < 0);
            Assert.IsTrue(double.IsInfinity(InverseErfc(0)) && InverseErfc(0) > 1);
            Assert.IsTrue(double.IsNaN(InverseErfc(-3)));
            Assert.IsTrue(double.IsNaN(InverseErfc(-1)));

            Assert.AreEqual(-2, InverseErfc(Erfc(-2)), 1e-14);
            Assert.AreEqual(-1, InverseErfc(Erfc(-1)), 1e-14);
            Assert.AreEqual(-0.5, InverseErfc(Erfc(-0.5)), 1e-14);
            Assert.AreEqual(0, InverseErfc(Erfc(0)));
            Assert.AreEqual(0.5, InverseErfc(Erfc(0.5)), 1e-14);
            Assert.AreEqual(1, InverseErfc(Erfc(1)), 1e-14);
            Assert.AreEqual(2, InverseErfc(Erfc(2)), 1e-14);
        }

        [TestMethod()]
        public void NormalCDFTest() {
            Assert.AreEqual(0, NormalCDF(double.NegativeInfinity));
            Assert.AreEqual(+1, NormalCDF(double.PositiveInfinity));

            Assert.AreEqual(3.167124183311992125377075672215129844e-5, NormalCDF(-4), 1e-19);
            Assert.AreEqual(1.349898031630094526651814767594977378e-3, NormalCDF(-3), 1e-17);
            Assert.AreEqual(2.275013194817920720028263716653343747e-2, NormalCDF(-2), 1e-16);
            Assert.AreEqual(1.586552539314570514147674543679620775e-1, NormalCDF(-1), 1e-15);
            Assert.AreEqual(3.085375387259868963622953893916622601e-1, NormalCDF(-0.5), 1e-15);
            Assert.AreEqual(0.5, NormalCDF(0));
            Assert.AreEqual(6.914624612740131036377046106083377399e-1, NormalCDF(+0.5), 1e-15);
            Assert.AreEqual(8.413447460685429485852325456320379225e-1, NormalCDF(+1), 1e-15);
            Assert.AreEqual(9.772498680518207927997173628334665625e-1, NormalCDF(+2), 1e-15);
            Assert.AreEqual(9.986501019683699054733481852324050226e-1, NormalCDF(+3), 1e-15);
            Assert.AreEqual(9.999683287581668800787462292432778487e-1, NormalCDF(+4), 1e-15);
        }

        [TestMethod()]
        public void ProbitTest() {
            Assert.IsTrue(double.IsInfinity(Probit(0)) && Probit(0) < 0);
            Assert.IsTrue(double.IsInfinity(Probit(1)) && Probit(1) > 0);
            Assert.IsTrue(double.IsNaN(Probit(-1)));
            Assert.IsTrue(double.IsNaN(Probit(2)));

            Assert.AreEqual(-4, Probit(NormalCDF(-4)), 1e-14);
            Assert.AreEqual(-3, Probit(NormalCDF(-3)), 1e-14);
            Assert.AreEqual(-2, Probit(NormalCDF(-2)), 1e-14);
            Assert.AreEqual(-1, Probit(NormalCDF(-1)), 1e-14);
            Assert.AreEqual(-0.5, Probit(NormalCDF(-0.5)), 1e-14);
            Assert.AreEqual(0, Probit(NormalCDF(0)));
            Assert.AreEqual(+0.5, Probit(NormalCDF(+0.5)), 1e-14);
            Assert.AreEqual(+1, Probit(NormalCDF(+1)), 1e-14);
            Assert.AreEqual(+2, Probit(NormalCDF(+2)), 1e-14);
            Assert.AreEqual(+3, Probit(NormalCDF(+3)), 1e-14);
            Assert.AreEqual(+4, Probit(NormalCDF(+4)), 1e-14);
        }
    }
}