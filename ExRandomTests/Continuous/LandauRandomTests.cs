﻿using ExRandomTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNGGraphPlot;
using System;
using System.Drawing;

namespace ExRandom.Continuous.Tests {
    [TestClass()]
    public class LandauRandomTests {
        [TestMethod()]
        public void LandauRandomTest() {
            const int N = 5000000, X_MIN = -10, X_MAX = 40, X_SCALE = 10;

            MT19937 mt = new();
            Random rd = new LandauRandom(mt, s: Math.PI / 2, mu: 0);

            (double[] cnt, double ave) = Util.Histogram(N, X_MIN, X_MAX, X_SCALE, rd);

            PNGGraphPloter pg = new(800, 400, 10, "Times New Roman", 10, 2);

            pg.DrawXLabel(Color.Black, "x");
            pg.DrawYLabel(Color.Black, "PSD");
            pg.DrawXScale(Color.Black, X_MIN, X_MAX, 2m);
            pg.DrawYScale(Color.Black, 0, 0.2m, 0.05m);

            pg.DrawLineGraph(Color.Black, X_MIN, X_MAX, cnt, 2);

            pg.DrawLine(Color.Gray, ave, 0, ave, 1);

            pg.Save(Workspace.OutDir + "plot_con_landau.png");
        }
    }
}