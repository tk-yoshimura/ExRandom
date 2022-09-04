using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;

namespace ExRandom.Discrete.Tests {
    [TestClass()]
    public class LogarithmicRandomTests {
        [TestMethod()]
        public void LogarithmicRandomTest() {
            const int N = 1000000, INDEXES = 10;

            MT19937 mt = new();
            Random rd = new LogarithmicRandom(mt, p: 0.4, max: 9);

            double[] cnt = Util.Histogram(N, INDEXES, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, -0.5m, INDEXES - 0.5m, 1);
            pg.DrawYScale(Color.Black, 0, 1m, 0.2m);

            pg.DrawHistogram(Color.Gray, 0, INDEXES - 1, cnt);

            pg.Save(Workspace.OutDir + "plot_dis_logarithmic.png");
        }
    }
}