using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab09_EN
{
    public class CircularBuffer<T> : IBuffer<T>, IEnumerable where T : IComparable<T>
    {
        // TODO: Implement here

        private List<T> buffer;     
        private uint max;        

        public CircularBuffer(uint max)
        {            
            this.max = max;
            buffer = new List<T>();
        }

        public void Put(T value)
        {
            if (buffer.Count >= max) throw new IndexOutOfRangeException("Full buffer");
            buffer.Add(value);            
        }

        public T Get()
        {
            if (buffer.Count == 0) throw new IndexOutOfRangeException("Empty buffer");
            
            T temp = buffer[0];
            buffer.RemoveAt(0);
            
            return temp;
        }

        public uint Size { get => max; }

        public uint Count { get => (uint)buffer.Count; }

        public bool Empty { get => buffer.Count == 0; }

        public bool Full { get => buffer.Count == max; }

        public void Reset()
        {
            buffer.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < buffer.Count; i++)
            {
                yield return buffer[i];
            }
        }

        public IEnumerable FilterLowerThan(T filterValue)
        {
            List<T> newList = new List<T>();

            foreach (T elem in buffer)
            {
                if (elem.CompareTo(filterValue) < 0)   // if elem < filterValue
                    newList.Add(elem);
            }

            return newList;
        }

    }
}
