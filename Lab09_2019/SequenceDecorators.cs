using System;
using System.Collections;
using System.Linq;

namespace EN_Lab_09
{
    public interface ISequenceDecorator
    {
        public string Description();
        public IEnumerable Decorate(IEnumerable enumerable);
    }

    public class SequenceSum : ISequenceDecorator
    {

        public IEnumerable Decorate(IEnumerable enumerable)
        {
            int i = 0;
            foreach (int elem in enumerable)
            {
                i += elem;
                yield return i;
            }
        }

        public string Description()
        {
            return $"SequenceSum()";
        }
    }

    public class LessThanFilter : ISequenceDecorator
    {
        private int limit;
        private bool memory;
        
        public LessThanFilter(int Limit, bool Memory = false)
        {
            limit = Limit;
            memory = Memory;
        }

        public IEnumerable Decorate(IEnumerable enumerable)
        {
            bool last_flag = false;
            int last = 0;
            
            foreach (int elem in enumerable)
            {
                if (!memory)
                {
                    if (elem < limit) yield return elem;
                }
                else
                {
                    if (elem < limit)
                    {
                        last = elem;
                        last_flag = true;
                        yield return elem;

                    }
                    else if (last_flag)
                    {
                        yield return last;
                    }
                }
            }
        }

        public string Description()
        {
            return $"LessThanFilter({limit}, {memory})";
        }
    }

    public class MedianFilter : ISequenceDecorator
    {
        private int filtersize;
        

        public MedianFilter(int FilterSize)
        {
            filtersize = FilterSize;            
        }

        public IEnumerable Decorate(IEnumerable enumerable)
        {
            IEnumerator pos = enumerable.GetEnumerator();
            int[] tab = new int[filtersize];
            int count = 0;

            while(pos.MoveNext())
            {
                count++;
                tab[count - 1] = (int)pos.Current;   //  copy elements to array
                 
                if(count == filtersize)  // when filtersize many elements are in array 
                {
                    Array.Sort(tab);                        // sort the array
                    yield return tab[filtersize / 2];       // return the median
                    count = 0;                              // reset counter
                }                
            }
            
            if (count != 0) // if sequence ended and there are elements in array
            {
                int[] tab2 = tab[0..count];                 // only use the last copied elements ("count"-many)
                Array.Sort(tab2);                           // sort the array
                yield return tab2[count / 2];               // return median of "count"-many elements
            }
        }

        public string Description()
        {
            return $"MedianFilter({filtersize})";
        }
    }
}