using System;

namespace ExRandom.MultiVariate {
    public class Vector<T> : IFormattable where T : struct, IComparable, IFormattable {
        private readonly T[] vals;
        public T X => vals[0];
        public T Y => vals[1];
        public T Z => vals[2];
        public T W => vals[3];
        public int Dim => vals.Length;
        public T this[int index] => vals[index];

        public Vector(params T[] vals) {
            this.vals = (T[])vals.Clone();
        }

        public static implicit operator T[](Vector<T> vector) {
            return (T[])vector.vals.Clone();
        }

        public static implicit operator Vector<T>(T[] vals) {
            return new Vector<T>(vals);
        }

        public override string ToString() {
            string str = string.Empty;

            if (Dim > 0) {
                str += vals[0].ToString();

                for (int i = 1; i < Dim; i++) {
                    str += string.Format(",{0}", vals[i].ToString());
                }
            }
            else {
                return "null";
            }

            return str;
        }

        public string ToString(string format, IFormatProvider provider) {
            string str = string.Empty;

            if (Dim > 0) {
                str += vals[0].ToString(format, provider);

                for (int i = 1; i < Dim; i++) {
                    str += string.Format(",{0}", vals[i].ToString(format, provider));
                }
            }
            else {
                return "null";
            }

            return str;
        }

        public static bool operator ==(Vector<T> v1, Vector<T> v2) {
            return Array.Equals(v1.vals, v2.vals);
        }

        public static bool operator !=(Vector<T> v1, Vector<T> v2) {
            return !Array.Equals(v1.vals, v2.vals);
        }

        public override bool Equals(object obj) {
            var other = obj as Vector<T>;

            return other != null && Array.Equals(vals, other.vals);
        }

        public override int GetHashCode() {
            int hash = 0;
            foreach (var v in vals) {
                hash ^= v.GetHashCode();
            }
            return hash;
        }
    }

    public abstract class Random<T> where T : struct, IComparable, IFormattable {
        public abstract Vector<T> Next();

        public Vector<T>[] Next(int num) {
            Vector<T>[] array = new Vector<T>[num];

            for (int i = 0; i < array.Length; i++) {
                array[i] = Next();
            }

            return array;
        }
    }
}
