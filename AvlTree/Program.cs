using System;

namespace AvlTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new AvlTree<Bit>();
            
            // Insert
            
            tree.Insert(new Bit(8));
            tree.Insert(new Bit(7));
            tree.Insert(new Bit(6));
            tree.Insert(new Bit(5));
            tree.Insert(new Bit(4));
            tree.Insert(new Bit(3));
            tree.Insert(new Bit(2));
            tree.Insert(new Bit(1));
            
            // Remove
            
            tree.Remove(new Bit(4));
            
            // Find

            var result = tree.Find(new Bit(2));
        }
    }
}