using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;

namespace ExRandom.MultiVariate.Tests {
    [TestClass()]
    public class MultiVariateNormalRandomTests {
        [TestMethod()]
        public void MultiVariateNormalRandomTest() {
            const int times = 10000;
            const decimal minX = -8.05m, maxX = 8.05m, minY = -8.05m, maxY = 8.05m;

            double[,] cov = { { 2, -1 }, { -1, 3 } };

            MT19937 mt = new();
            MultiVariateNormalRandom rd = new MultiVariateNormalRandom(mt, cov, new double[] { 1, 2 });

            PNGGraphPloter graph = new PNGGraphPloter(500, 500, 20, "Times New Roman", 20, 2);

            graph.DrawXLabel(Color.Black, "X");
            graph.DrawYLabel(Color.Black, "Y");
            graph.DrawXScale(Color.Black, minX, maxX, 2m);
            graph.DrawYScale(Color.Black, minY, maxY, 2m);

            for (int i = 0; i <= times; i++) {
                var v = rd.Next();

                graph.DrawPoint(Color.Black, v.X, v.Y, 1.2);
            }

            graph.Save(Workspace.OutDir + "plot_multi_normal_xy.png");
        }
    }
}