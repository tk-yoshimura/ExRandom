using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;

namespace ExRandom.MultiVariate.Tests {
    [TestClass()]
    public class IntervalPartitionDiscreteRandomTests {
        [TestMethod()]
        public void IntervalPartitionDiscreteRandomTest1() {
            const int times = 1000;
            const decimal minX = -0.5m, maxX = 100.5m, minY = -0.5m, maxY = 100.5m;

            MT19937 mt = new();
            IntervalPartitionDiscreteRandom rd = new IntervalPartitionDiscreteRandom(mt, dim: 2, interval: 100);

            PNGGraphPloter graph = new PNGGraphPloter(500, 500, 20, "Times New Roman", 20, 2);

            graph.DrawXLabel(Color.Black, "X");
            graph.DrawYLabel(Color.Black, "Y");
            graph.DrawXScale(Color.Black, minX, maxX, 10m);
            graph.DrawYScale(Color.Black, minY, maxY, 10m);

            for (int i = 0; i <= times; i++) {
                var v = rd.Next();

                graph.DrawPoint(Color.Black, v.X, v.Y, 1.2);
            }

            graph.Save(Workspace.OutDir + "plot_multi_interval_partition_discrete_dim_2.png");
        }

        [TestMethod()]
        public void IntervalPartitionDiscreteRandomTest2() {
            const int times = 1000;
            const decimal minX = -0.5m, maxX = 100.5m, minY = -0.5m, maxY = 100.5m;

            MT19937 mt = new();
            IntervalPartitionDiscreteRandom rd = new IntervalPartitionDiscreteRandom(mt, dim: 3, interval: 100);

            PNGGraphPloter graph = new PNGGraphPloter(500, 500, 20, "Times New Roman", 20, 2);

            graph.DrawXLabel(Color.Black, "X");
            graph.DrawYLabel(Color.Black, "Y");
            graph.DrawXScale(Color.Black, minX, maxX, 10m);
            graph.DrawYScale(Color.Black, minY, maxY, 10m);

            for (int i = 0; i <= times; i++) {
                var v = rd.Next();

                graph.DrawPoint(Color.Black, v.X, v.Y, 1.2);
            }

            graph.Save(Workspace.OutDir + "plot_multi_interval_partition_discrete_dim_3.png");
        }

        [TestMethod()]
        public void IntervalPartitionDiscreteRandomTest3() {
            const int times = 1000;
            const decimal minX = -0.5m, maxX = 100.5m, minY = -0.5m, maxY = 100.5m;

            MT19937 mt = new();
            IntervalPartitionDiscreteRandom rd = new IntervalPartitionDiscreteRandom(mt, dim: 4, interval: 100);

            PNGGraphPloter graph = new PNGGraphPloter(500, 500, 20, "Times New Roman", 20, 2);

            graph.DrawXLabel(Color.Black, "X");
            graph.DrawYLabel(Color.Black, "Y");
            graph.DrawXScale(Color.Black, minX, maxX, 10m);
            graph.DrawYScale(Color.Black, minY, maxY, 10m);

            for (int i = 0; i <= times; i++) {
                var v = rd.Next();

                graph.DrawPoint(Color.Black, v.X, v.Y, 1.2);
            }

            graph.Save(Workspace.OutDir + "plot_multi_interval_partition_discrete_dim_4.png");
        }
    }
}