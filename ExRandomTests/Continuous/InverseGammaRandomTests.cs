using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;

namespace ExRandom.Continuous.Tests {
    [TestClass()]
    public class InverseGammaRandomTests {
        [TestMethod()]
        public void InverseGammaRandomTest() {
            const int N = 1000000, X_MIN = 0, X_MAX = 3, X_SCALE = 40;

            MT19937 mt = new();
            Random rd = new InverseGammaRandom(mt, kappa: 3, theta: 1);

            (double[] cnt, double ave) = Util.Histogram(N, X_MIN, X_MAX, X_SCALE, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, X_MIN, X_MAX, 0.5m);
            pg.DrawYScale(Color.Black, 0, 2.5m, 0.5m);

            pg.DrawLineGraph(Color.Black, X_MIN, X_MAX, cnt, 2);

            pg.DrawLine(Color.Gray, ave, 0, ave, 1);

            pg.Save(Workspace.OutDir + "plot_con_inverse_gamma.png");
        }
    }
}