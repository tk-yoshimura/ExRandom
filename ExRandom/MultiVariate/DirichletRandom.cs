using System;

////debug
//Next : output distribution check - yet

namespace ExRandom.MultiVariate {
    public class DirichletRandom : Random<double> {
        readonly int dim;
        readonly Continuous.GammaRandom[] gr_list;

        public DirichletRandom(MT19937 mt, params double[] alpha_list) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(alpha_list == null || alpha_list.Length <= 1) {
                throw new ArgumentException();
            }

            this.dim = alpha_list.Length;

            this.gr_list = new Continuous.GammaRandom[dim];

            for(int i = 0; i < dim; i++) {
                this.gr_list[i] = new Continuous.GammaRandom(mt, kappa:alpha_list[i], theta:1);
            }
        }

        public override Vector<double> Next() {
            double r_sum = 0;
            double[] r_list = new double[dim];
            Vector<double> v = new Vector<double>(new double[dim - 1]);

            for(int i = 0; i < dim; i++) {
                r_list[i] = gr_list[i].Next();
                r_sum += r_list[i];
            }

            r_sum = Math.Max(r_sum, double.Epsilon);

            for(int i = 0; i < dim - 1; i++) {
                v[i] = r_list[i] / r_sum;
            }

            return v;
        }
    }
}
