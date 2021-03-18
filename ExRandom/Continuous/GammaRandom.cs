using System;

////debug
//Next : output distribution check - yet
//Next : output average check - OK

namespace ExRandom.Continuous {
    public class GammaRandom : Random{
        readonly MT19937 mt;
        readonly NormalRandom nd;
        readonly double kappa, theta, c1, c2, c3;

        public GammaRandom(MT19937 mt, double kappa = 1, double theta = 1) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(!(theta > 0) || !(kappa > 0)) {
                throw new ArgumentException();
            }

            this.mt = mt;
            this.nd = new NormalRandom(mt);
            this.theta = theta;
            this.kappa = kappa;
            
            if(kappa < 1) {
                double t, dt, exp_t;
                
                t = 0.07 + 0.75 * Math.Sqrt(1 - kappa); 
                
                for(int i = 0; i < 24; i++) { 
                    exp_t = Math.Exp(t);
                    dt = (1 - t * (exp_t - 1) - kappa) / (1 - (t + 1) * exp_t);
                    t -= dt;

                    if(Math.Abs(dt) < 1.0e-12) {
                        break;
                    }
                }

                c1 = t;
                c2 = 1 + kappa * Math.Exp(-c1) / c1;
                c3 = 1 / kappa;
            }
            else{
                c1 = kappa - 1.0 / 3.0;
                c2 = 1 / (3 * Math.Sqrt(c1));
                c3 = double.NaN;
            }
        }

        public override double Next() {
            double x;

            if(kappa < 1) {
                double u1, u2, v, y;

                while(true){
                    u1 = mt.NextDouble_OpenInterval01();
                    u2 = mt.NextDouble_OpenInterval01();

                    v = c2 * u1;

                    if(v <= 1) {
                        x = c1 * Math.Pow(v, c3);

                        if((u2 <= (2 - x) / (2 + x)) || (u2 <= Math.Exp(-x))) {
                            break;
                        }
                    }
                    else {
                        x = -Math.Log(c1 * c3 * (c2 - v));
                        y = x / c1;

                        if((u2 * (kappa + y - kappa * y) <= 1) || (u2 <= Math.Pow(y, kappa - 1))) {
                            break;
                        }
                    }
                }
            }
            else{
                double z, squa_z, u, v;

                while(true){
                    z = nd.Next();

                    if(c2 * z < -1) {
                        continue;
                    }

                    squa_z = z * z;
                    v = 1 + c2 * z;
                    v = v * v * v;

                    u = mt.NextDouble_OpenInterval01();

                    if((u < 1 - 0.0331 * (squa_z * squa_z)) || (Math.Log(u) < (0.5 * squa_z) + c1 * (1 - v + Math.Log(v)))) {
                        x = c1 * v;
                        break;
                    }
                }
            }

            return x * theta;
        }
    }
}
