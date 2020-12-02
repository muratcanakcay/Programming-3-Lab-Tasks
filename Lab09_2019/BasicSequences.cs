using System;
using System.Collections;
using System.Linq;

namespace EN_Lab_09
{
    public interface ISequence : IEnumerable
    {
        public string Description();        
    }

    public class Repeat : ISequence
    {
        public int R { get; set; }
        public int V { get; set; }

        public Repeat(int value, int repetition)
        {
            R = repetition;
            V = value;
        }

        public string Description()
        {
            return $"Repeat({V}, {R})";
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < R; i++)
			{
                yield return V;
			}
        }
    }

    public class ArithmeticProgression : ISequence
    {
        public int V { get; set; }
        public int S { get; set; }

        public ArithmeticProgression(int value, int step = 1)
        {
            V = value;
            S = step;
        }

        public string Description()
        {
            return $"ArithmeticProgression({V}, {S})";
        }

        public IEnumerator GetEnumerator()
        {
            int ret = V;
            while (true)
            {
                yield return ret;
                ret += S;
            }
        }
    }

    public class GeometricProgression : ISequence
    {
        public int V { get; set; }
        public int S { get; set; }

        public GeometricProgression(int value, int step = 1)
        {
            V = value;
            S = step;
        }

        public virtual string Description()
        {
            return $"GeometricProgression ({V}, {S})";
        }

        public IEnumerator GetEnumerator()
        {
            int ret = V;
            while (true)
            {
                yield return ret;
                ret *= S;
            }
        }
    }

    public class PowersOf : GeometricProgression
    {
        public PowersOf(int value) : base(1, value) {}

        public override string Description()
        {
            return $"PowerOf ({V}, {S})";
        }        
    }
}