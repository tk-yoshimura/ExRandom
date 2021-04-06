using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;
using ExRandomTests;

namespace ExRandom.Discrete.Tests {
    [TestClass()]
    public class RouletteRandomTests {
        [TestMethod()]
        public void RouletteRandomTest() {
            const int N = 1000000, INDEXES = 6;

            MT19937 mt = new();
            Random rd = new RouletteRandom(mt, new double[] { 1, 2, 3, 2.5, 4, 1.5 });

            double[] cnt = Util.Histogram(N, INDEXES, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, -0.5m, INDEXES - 0.5m, 1);
            pg.DrawYScale(Color.Black, 0, 0.5m, 0.1m);

            pg.DrawHistogram(Color.Gray, 0, INDEXES - 1, cnt);

            pg.Save(Workspace.OutDir + "plot_dis_roulette.png");
        }
    }
}