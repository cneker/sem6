using System;
using System.Linq;
using System.Text;

namespace spp4
{
    public class Sum
    {
        public static long Accum(params int[] values) => values.Select(Convert.ToInt64).Sum();
    }

    internal class Program
    {
        static void Main(String[] arts)
        { }
    }

    public class StringUtils
    {
        public static string Loose(string str, string remove) => (str, remove) switch
        {
            (null, null) => throw new NullReferenceException(),
            (null, not null) => null,
            ("", not null) => "",
            (not null, null) => str,
            (not null, "") => str,
            _ => string.Join("", str.Except(remove))
        };
    }

    public class Stack<T>
    {
        private class Node
        {
            public T _item;
            public Node _next;
        }

        private Node _first;
        private int _N;
        public Stack()
        {
            _first = null;
            _N = 0;
        }

        public bool IsEmpty() => _N < 1;

        public int Size() => _N;

        public void Push(T item)
        {
            Node oldfirst = _first;
            _first = new Node();
            _first._item = item;
            _first._next = oldfirst;
            _N++;
        }

        public T Pop()
        {
            if (Size() == 0)
                throw new ArgumentNullException();
            T item = _first._item;
            _first = _first._next;
            _N--;
            return item;
        }

        public T Peek()
        {
            if (Size() == 0)
                throw new ArgumentNullException();
            else
                return _first._item;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (Node current = _first; current != current._next; current = current._next)
            {
                T item = current._item;
                s.Append(item);
                if (current._next == null)
                    return s.ToString();
                s.Append(" - ");
            }

            return s.ToString();
        }
    }
}
