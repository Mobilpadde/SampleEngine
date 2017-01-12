using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class ThreadSafeRandom
    {
        private static Random _rng = new Random();

        private static object _locker = new object();

        public static int Next(int min, int max)
        {
            lock(_locker)
                return _rng.Next(min, max);
        }

        public static double NextDouble(double min, double max)
        {
            lock (_locker)
                return _rng.NextDouble() * (max - min) + min;
        }

        public static int Selector(int[] choices)
        {
            lock (_locker)
                return choices[ThreadSafeRandom.Next(0, choices.Length)];
        }

        public static double Selector(double[] choices)
        {
            lock (_locker)
                return choices[ThreadSafeRandom.Next(0, choices.Length)];
        }
    }
}
