using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;

namespace ExRandom.Discrete.Tests {
    [TestClass()]
    public class HyperGeometricRandomTests {
        [TestMethod()]
        public void HyperGeometricRandomTest() {
            const int N = 1000000, INDEXES = 32;

            MT19937 mt = new();
            Random rd = new HyperGeometricRandom(mt, n: 30, k: 20, m: 10);

            double[] cnt = Util.Histogram(N, INDEXES, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, -0.5m, INDEXES - 0.5m, 1);
            pg.DrawYScale(Color.Black, 0, 0.4m, 0.05m);

            pg.DrawHistogram(Color.Gray, 0, INDEXES - 1, cnt);

            pg.Save(Workspace.OutDir + "plot_dis_hyper_geometric.png");
        }
    }
}