using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;

namespace ExRandom.Discrete.Tests {
    [TestClass()]
    public class BernoulliRandomTests {
        [TestMethod()]
        public void BernoulliRandomTest() {
            const int N = 1000000, INDEXES = 2;

            MT19937 mt = new();
            Random rd = new BernoulliRandom(mt, prob: 0.4);

            double[] cnt = Util.Histogram(N, INDEXES, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, -0.5m, INDEXES - 0.5m, 1);
            pg.DrawYScale(Color.Black, 0, 1m, 0.2m);

            pg.DrawHistogram(Color.Gray, 0, INDEXES - 1, cnt);

            pg.Save(Workspace.OutDir + "plot_dis_bernoulli.png");
        }
    }
}