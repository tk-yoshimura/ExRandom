using System;
using System.Collections.Generic;

namespace ExRandom.MultiVariate {
    public class DirichletRandom : Random<double> {
        readonly int dim;
        readonly Continuous.GammaRandom[] grs;

        public MT19937 Mt { get; }
        public IReadOnlyList<double> Alphas { get; }

        public DirichletRandom(MT19937 mt, params double[] alphas) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (alphas is null || alphas.Length <= 1) {
                throw new ArgumentException(nameof(alphas));
            }

            this.dim = alphas.Length;

            this.grs = new Continuous.GammaRandom[dim];

            for (int i = 0; i < dim; i++) {
                this.grs[i] = new Continuous.GammaRandom(mt, kappa: alphas[i], theta: 1);
            }

            this.Mt = mt;
            this.Alphas = alphas;
        }

        public override Vector<double> Next() {
            double r_sum = 0;
            double[] rs = new double[dim];
            double[] v = new double[dim - 1];

            for (int i = 0; i < dim; i++) {
                rs[i] = grs[i].Next();
                r_sum += rs[i];
            }

            r_sum = Math.Max(r_sum, double.Epsilon);

            for (int i = 0; i < dim - 1; i++) {
                v[i] = rs[i] / r_sum;
            }

            return new Vector<double>(v);
        }
    }
}
