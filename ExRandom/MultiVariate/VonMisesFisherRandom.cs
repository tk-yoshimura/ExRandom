using System;

namespace ExRandom.MultiVariate {
    public class VonMisesFisherRandom : Random<double> {
        readonly double inv_kappa, exp_m2kappa;
        readonly double qri, qrj, qij, qxx, qyy, qzz;

        public MT19937 Mt { get; }
        public Vector<double> Mu { get; }
        public double Kappa { get; }

        public VonMisesFisherRandom(MT19937 mt, Vector<double> mu, double kappa = 1) {
            if (mt is null) {
                throw new ArgumentNullException(nameof(mt));
            }
            if (mu.Dim != 3) {
                throw new ArgumentException(nameof(mu));
            }
            if (!(kappa >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(kappa));
            }

            double inv_norm = 1 / Math.Sqrt(mu.X * mu.X + mu.Y * mu.Y + mu.Z * mu.Z);

            if (double.IsNaN(inv_norm) || double.IsInfinity(inv_norm)) {
                throw new ArgumentException(nameof(mu));
            }

            this.Mt = mt;
            this.Mu = mu;
            this.Kappa = kappa;
            this.inv_kappa = 1 / kappa;
            this.exp_m2kappa = Math.Exp(-2 * kappa);

            double mx, my, mz, qr, qi, qj, angle, c, s, norm;

            mx = mu.X * inv_norm;
            my = mu.Y * inv_norm;
            mz = mu.Z * inv_norm;

            angle = Math.Acos(mz);
            c = Math.Cos(angle * 0.5);
            s = Math.Sin(angle * 0.5);
            norm = Math.Sqrt(mx * mx + my * my);

            if (norm > 0) {
                qr = c;
                qi = s * -my / norm;
                qj = s * mx / norm;
            }
            else {
                qr = 1;
                qi = 0;
                qj = 0;
            }

            this.qri = qr * qi;
            this.qij = qi * qj;
            this.qrj = qr * qj;
            this.qxx = qr * qr + qi * qi - qj * qj;
            this.qyy = qr * qr - qi * qi + qj * qj;
            this.qzz = qr * qr - qi * qi - qj * qj;
        }

        public override Vector<double> Next() {
            double r, w, s, theta, x, y, z;

            r = Mt.NextDouble();
            w = (Kappa > 0) ? (1 + Math.Log(r + (1 - r) * exp_m2kappa) * inv_kappa) : (2 * r - 1);
            s = Math.Sqrt(1 - w * w);
            theta = 2 * Math.PI * Mt.NextDouble_OpenInterval1();

            x = s * Math.Cos(theta);
            y = s * Math.Sin(theta);
            z = w;

            return new Vector<double>(x * qxx + 2 * (y * qij + z * qrj), y * qyy - 2 * (z * qri - x * qij), z * qzz - 2 * (x * qrj - y * qri));
        }
    }
}
