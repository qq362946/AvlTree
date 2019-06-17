using System;

namespace AvlTree
{
    public class AvlTreeNote<TKey> where TKey : IComparable<TKey>
    {
        public TKey Key;
        public int Height;
        public AvlTreeNote<TKey> LChild;
        public AvlTreeNote<TKey> RChild;

        public AvlTreeNote(TKey key, AvlTreeNote<TKey> lChild, AvlTreeNote<TKey> rChild)
        {
            Key = key;
            LChild = lChild;
            RChild = rChild;
        }
    }
}