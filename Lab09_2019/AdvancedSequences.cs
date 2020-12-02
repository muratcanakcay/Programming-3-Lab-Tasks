using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EN_Lab_09
{
    public interface ISequenceCombine
    {
        public string Description();
        public IEnumerable Combine(IEnumerable e1, IEnumerable e2);
    }

    public class CantorPair :  ISequenceCombine
    {
        // Stage 3 - Combine() for infinite sequences:

        //public IEnumerable Combine(IEnumerable e1, IEnumerable e2)
        //{
        //    List<int> l1 = new List<int>();
        //    List<int> l2 = new List<int>();

        //    IEnumerator v1 = e1.GetEnumerator();
        //    IEnumerator v2 = e2.GetEnumerator();

        //    int count = 0;

        //    while(true)
        //    {
        //        v1.MoveNext();
        //        v2.MoveNext();

        //        l1.Add((int)v1.Current);
        //        l2.Add((int)v2.Current);

        //        for (int i = count; i >= 0; i--)
        //        {
        //            yield return (l1[i], l2[count - i]);
        //        }               

        //        count++;
        //    }
        //}

        // Stage 4 - Combine() improved to work for finite sequences:


        public IEnumerable Combine(IEnumerable e1, IEnumerable e2)
        {
            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();

            IEnumerator v1 = e1.GetEnumerator();
            IEnumerator v2 = e2.GetEnumerator();

            int count = 0;            
            bool endflag = false;

            while (true)
            {
                if (v1.MoveNext()) l1.Add((int)v1.Current);
                if (v2.MoveNext()) l2.Add((int)v2.Current);
                
            for (int i = count; i >= 0; i--)
                {
                    if (i < l1.Count && count-i < l2.Count) yield return (l1[i], l2[count - i]);                    
                    if (i == l1.Count && count - i == l2.Count) endflag = true;
                }

                if (endflag) yield break;
                count++;
            }
        }
       
        public string Description()
        {
            return "CantorPair";
        }
    }
}