﻿using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.MultiVariate {
    public class MultiVariateNormalRandom : Random<double> {
        readonly Continuous.NormalRandom nd;
        readonly int dim;
        readonly double[] mu_vector;
        readonly double[][] lower_tri_matrix;

        public MultiVariateNormalRandom(MT19937 mt, double[,] cov_matrix, double[] mu_vector = null){
            if(mt == null) {
                throw new ArgumentNullException();
            }

            if(cov_matrix == null) {
                throw new ArgumentException();
            }

            if(mu_vector != null && mu_vector.Length != cov_matrix.GetLength(0)){
                throw new ArgumentException();
            }

            double[][] l;

            CholeskyDecomp(cov_matrix, out l);

            for(int i = 0, j; i < dim; i++) {
                for(j = 0; j <= i; j++) {
                    if(Double.IsNaN(l[i][j])) { 
                        throw new ArgumentException();
                    }
                }
            }

            this.nd = new Continuous.NormalRandom(mt);
            this.dim = cov_matrix.GetLength(0);
            this.lower_tri_matrix = l;

            this.mu_vector = (mu_vector != null) ? mu_vector : new double[dim];
        }

        public override Vector<double> Next() {
            double[] z = nd.Next(dim), x = new double[dim];

            for(int i = 0, j; i < dim; i++) { 
                x[i] = mu_vector[i];
                
                for(j = 0; j <= i; j++) {
                    x[i] += lower_tri_matrix[i][j] * z[j];
                }
            }

            return new Vector<double>(x);
        }

        private static void CholeskyDecomp(double[,] m, out double[][] l) {
            if(m.GetLength(0) <= 0 || m.GetLength(0) != m.GetLength(1)) {
                throw new ArgumentException();
            }

            int i, j, k, n = m.GetLength(0);

            for(i = 1; i < n; i++) {
                for(j = 0; j < i; j++) {
                    if(m[i, j] != m[j, i]) { 
                        throw new ArgumentException();
                    }
                }
            }

            double ld, lld;
            double[] d = new double[n];
            
            l = new double[n][];

            for(i = 0; i < n; i++) { 
                l[i] = new double[i + 1];
            }

            l[0][0] = 1;
            d[0] = m[0, 0];

            for(i = 0; i < n; i++) {
                for(j = 0; j < i; j++) {
                    lld = m[i, j];
                    for(k = 0; k < j; k++) {
                        lld -= l[i][k] * l[j][k] * d[k];
                    }
                    l[i][j] = lld / d[j];
                }

                ld = m[i, i];
                for(k = 0; k < i; k++) { 
                    ld -= l[i][k] * l[j][k] * d[k];
                }
                l[i][j] = 1;
                d[i] = ld;
            }

            for(j = 0; j < n; j++) { 
                d[j] = Math.Sqrt(d[j]);
            }

            for(i = 0; i < n; i++) {
                for(j = 0; j <= i; j++) {
                    l[i][j] *= d[j];
                }
            }
        }
    }
}
