using System;

////debug
//Next : output distribution check - OK

namespace ExRandom.Discrete {
    public class BinaryRandom : Random{
        readonly MT19937 mt;
        
        public BinaryRandom(MT19937 mt) {
            if(mt == null) {
                throw new ArgumentNullException();
            }

            this.mt = mt;
        }

        public override int Next() {
            return (int)(mt.Next() & 1);
        }

        public bool NextBool() {
            return (mt.Next() & 1) != 0;
        }
    }
}
