using System;

namespace ExRandom.MultiVariate {
    public class Vector<T> : IFormattable where T : struct, IComparable, IFormattable {
        private readonly T[] val_list;

        public Vector(params T[] val_list) {
            this.val_list = (T[])val_list.Clone();
        }

        public T this[int index] {
            set {
                val_list[index] = value;
            }

            get {
                return val_list[index];
            }
        }

        public int Dim {
            get {
                return val_list.Length;
            }
        }

        public override string ToString() {
            string str = string.Empty;

            if (Dim > 0) {
                str += val_list[0].ToString();

                for (int i = 1; i < Dim; i++) {
                    str += string.Format(",{0}", val_list[i].ToString());
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
                str += val_list[0].ToString(format, provider);

                for (int i = 1; i < Dim; i++) {
                    str += string.Format(",{0}", val_list[i].ToString(format, provider));
                }
            }
            else {
                return "null";
            }

            return str;
        }

        public static bool operator ==(Vector<T> v1, Vector<T> v2) {
            return Array.Equals(v1.val_list, v2.val_list);
        }

        public static bool operator !=(Vector<T> v1, Vector<T> v2) {
            return !Array.Equals(v1.val_list, v2.val_list);
        }

        public override bool Equals(object obj) {
            var other = obj as Vector<T>;

            return other != null && Array.Equals(val_list, other.val_list);
        }

        public override int GetHashCode() {
            int hash = 0;
            foreach (var v in val_list) {
                hash ^= v.GetHashCode();
            }
            return hash;
        }

        public T X {
            set {
                val_list[0] = value;
            }

            get {
                return val_list[0];
            }
        }

        public T Y {
            set {
                val_list[1] = value;
            }

            get {
                return val_list[1];
            }
        }

        public T Z {
            set {
                val_list[2] = value;
            }

            get {
                return val_list[2];
            }
        }

        public T W {
            set {
                val_list[3] = value;
            }

            get {
                return val_list[3];
            }
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
