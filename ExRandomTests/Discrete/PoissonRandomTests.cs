using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;
using ExRandomTests;

namespace ExRandom.Discrete.Tests {
    [TestClass()]
    public class PoissonRandomTests {
        [TestMethod()]
        public void PoissonRandomTest() {
            const int N = 1000000, INDEXES = 100;

            MT19937 mt = new();
            Random rd = new PoissonRandom(mt, lambda: 5.5, max: 99);

            double[] cnt = Util.Histogram(N, INDEXES, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, -0.5m, INDEXES - 0.5m, 10);
            pg.DrawYScale(Color.Black, 0, 0.4m, 0.1m);

            pg.DrawHistogram(Color.Gray, 0, INDEXES - 1, cnt);

            pg.Save(Workspace.OutDir + "plot_dis_poisson.png");
        }
    }
}