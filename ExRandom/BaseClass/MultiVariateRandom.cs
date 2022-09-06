using System;
using System.Linq;

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
            if (Dim > 0) {
                return string.Join(',', vals.Select((v) => v.ToString()));
            }
            else {
                return "null";
            }
        }

        public string ToString(string format, IFormatProvider provider) {
            if (Dim > 0) {
                return string.Join(',', vals.Select((v) => v.ToString(format, provider)));
            }
            else {
                return "null";
            }
        }

        public static bool operator ==(Vector<T> v1, Vector<T> v2) {
            return Equals(v1.vals, v2.vals);
        }

        public static bool operator !=(Vector<T> v1, Vector<T> v2) {
            return !Equals(v1.vals, v2.vals);
        }

        public override bool Equals(object obj) {
            return obj is Vector<T> vec && Equals(vals, vec.vals);
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
