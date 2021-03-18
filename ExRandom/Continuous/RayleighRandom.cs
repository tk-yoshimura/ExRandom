﻿using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class RayleighRandom : Random{
        readonly MT19937 mt;
        readonly double sigma;

        public RayleighRandom(MT19937 mt, double sigma = 1) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            this.mt = mt;
            this.sigma = sigma;
        }

        public override double Next() {
            double r = mt.NextDouble_OpenInterval0();

            return sigma * Math.Sqrt(-2.0 * Math.Log(r));
        }
    }
}