﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System.Drawing;
using ExRandomTests;

namespace ExRandom.MultiVariate.Tests {
    [TestClass()]
    public class InsideSphericalRandomTests {
        [TestMethod()]
        public void InsideSphericalRandomTest() {
            const int times = 10000;
            const decimal minX = -1.05m, maxX = 1.05m, minY = -1.05m, maxY = 1.05m;

            MT19937 mt = new();
            InsideSphericalRandom rd = new InsideSphericalRandom(mt);

            PNGGraphPloter graph = new PNGGraphPloter(500, 500, 20, "Times New Roman", 20, 2);

            graph.DrawXLabel(Color.Black, "X");
            graph.DrawYLabel(Color.Black, "Y");
            graph.DrawXScale(Color.Black, minX, maxX, 0.5m);
            graph.DrawYScale(Color.Black, minY, maxY, 0.5m);

            for(int i = 0; i <= times; i++) {
                var v = rd.Next();

                graph.DrawPoint(Color.Black, v.X, v.Y, 1.2);
            }
            
            graph.Save(Workspace.OutDir + "plot_multi_inside_spherical_xy.png");
        }
    }
}