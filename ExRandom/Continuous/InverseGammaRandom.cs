﻿using System;

namespace ExRandom.Continuous {
    public class InverseGammaRandom : Random {
        private readonly GammaRandom gr;

        public MT19937 Mt { get; }
        public double Kappa { get; }
        public double Theta { get; }

        public InverseGammaRandom(MT19937 mt, double kappa = 1, double theta = 1) {
            ArgumentNullException.ThrowIfNull(mt);

            this.gr = new GammaRandom(mt, kappa: kappa, theta: theta);
            this.Mt = mt;
            this.Kappa = kappa;
            this.Theta = theta;
        }

        public override double Next() {
            return 1.0 / gr.Next();
        }
    }
}
