using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;

namespace ExRandom.Continuous.Tests {
    [TestClass()]
    public class LogisticRandomTests {
        [TestMethod()]
        public void LogisticRandomTest() {
            const int N = 1000000, X_MIN = -5, X_MAX = 20, X_SCALE = 10;

            MT19937 mt = new();
            Random rd = new LogisticRandom(mt, sigma: 3, mu: 9);

            (double[] cnt, double ave) = Util.Histogram(N, X_MIN, X_MAX, X_SCALE, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, X_MIN, X_MAX, 1m);
            pg.DrawYScale(Color.Black, 0, 0.25m, 0.05m);

            pg.DrawLineGraph(Color.Black, X_MIN, X_MAX, cnt, 2);

            pg.DrawLine(Color.Gray, ave, 0, ave, 1);

            pg.Save(Workspace.OutDir + "plot_con_logistic.png");
        }
    }
}