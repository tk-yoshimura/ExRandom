﻿using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Continuous {
    public class LevyRandom : Random{
        readonly MT19937 mt;
        readonly double c, mu;

        public LevyRandom(MT19937 mt, double c = 1, double mu = 0) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(mu > 0)) {
                throw new ArgumentException();
            }
            
            this.mt = mt;
            this.c = c;
            this.mu = mu;
        }

        public override double Next() {
            double r = mt.NextDouble_OpenInterval01();
            double p = ErrorFuntion.Probit(1 - r * 0.5);

            return mu + c / (p * p);
        }
    }
}