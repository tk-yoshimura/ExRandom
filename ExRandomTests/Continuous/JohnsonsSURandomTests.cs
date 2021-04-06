using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;

namespace ExRandom.Continuous.Tests {
    [TestClass()]
    public class JohnsonsSURandomTests {
        [TestMethod()]
        public void JohnsonsSURandomTest() {
            const int N = 1000000, X_MIN = -10, X_MAX = 5, X_SCALE = 20;

            MT19937 mt = new();
            Random rd = new JohnsonsSURandom(mt, delta: 2, lambda: 1.5, gamma: 3, eta: 1.1);

            (double[] cnt, double ave) = Util.Histogram(N, X_MIN, X_MAX, X_SCALE, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, X_MIN, X_MAX, 0.5m);
            pg.DrawYScale(Color.Black, 0, 0.5m, 0.1m);

            pg.DrawLineGraph(Color.Black, X_MIN, X_MAX, cnt, 2);

            pg.DrawLine(Color.Gray, ave, 0, ave, 1);

            pg.Save(Workspace.OutDir + "plot_con_johnsons_su.png");
        }
    }
}