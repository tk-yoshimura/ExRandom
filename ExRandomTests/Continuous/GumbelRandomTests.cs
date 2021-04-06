using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;

namespace ExRandom.Continuous.Tests {
    [TestClass()]
    public class GumbelRandomTests {
        [TestMethod()]
        public void GumbelRandomTest() {
            const int N = 1000000, X_MIN = -5, X_MAX = 20, X_SCALE = 10;

            MT19937 mt = new();
            Random rd = new GumbelRandom(mt, beta: 4.0, lambda: 3);

            (double[] cnt, double ave) = Util.Histogram(N, X_MIN, X_MAX, X_SCALE, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, X_MIN, X_MAX, 1m);
            pg.DrawYScale(Color.Black, 0, 0.2m, 0.05m);

            pg.DrawLineGraph(Color.Black, X_MIN, X_MAX, cnt, 2);

            pg.DrawLine(Color.Gray, ave, 0, ave, 1);

            pg.Save(Workspace.OutDir + "plot_con_gumbel.png");
        }
    }
}