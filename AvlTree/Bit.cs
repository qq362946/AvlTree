using System;

namespace AvlTree
{
    public class Bit : IComparable<Bit>
    {
        public int Value;

        public Bit(int value) => Value = value;

        public int CompareTo(Bit other) => Value - other.Value;
    }
}