using System;

namespace Engine
{
    public class ThreadSafeRandom
    {
        private static Random _rng = new Random();

        private static object _locker = new object();

        /// <summary>
        /// Generates a thread-safe int between <paramref name="min"/> and <paramref name="max"/>
        /// </summary>
        /// <param name="min">Minimum number to generate</param>
        /// <param name="max">Maximum (not included) to generate</param>
        /// <returns>int</returns>
        public static int Next(int min, int max)
        {
            lock(_locker)
                return _rng.Next(min, max);
        }

        /// <summary>
        /// Generates a thread-safe double between <paramref name="min"/> and <paramref name="max"/>
        /// </summary>
        /// <param name="min">Minimum number to generate</param>
        /// <param name="max">Maximum (not included) to generate</param>
        /// <returns>int</returns>
        public static double NextDouble(double min, double max)
        {
            lock (_locker)
                return _rng.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Selects an int in <paramref name="choices"/> and returns it
        /// </summary>
        /// <param name="choices">An array of ints to choose from</param>
        /// <returns>A random choice from the <paramref name="choices"/>-array</returns>
        public static int Selector(int[] choices)
        {
            lock (_locker)
                return choices[ThreadSafeRandom.Next(0, choices.Length)];
        }

        /// <summary>
        /// Selects a double in <paramref name="choices"/> and returns it
        /// </summary>
        /// <param name="choices">An array of ints to choose from</param>
        /// <returns>A random choice from the <paramref name="choices"/>-array</returns>
        public static double Selector(double[] choices)
        {
            lock (_locker)
                return choices[ThreadSafeRandom.Next(0, choices.Length)];
        }
    }
}
