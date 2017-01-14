using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Helpers
{
    // http://stackoverflow.com/a/3769168/754471
    public class SingleList<T> : List<T>, IEnumerable<T>
    {
        /// <summary>
        /// The list to contain T in
        /// </summary>
        internal List<T> List;

        /// <summary>
        /// Retrieve elmenet I
        /// </summary>
        /// <param name="i">The index to retrieve</param>
        /// <returns>The element at index i</returns>
        public new T this[int i]
        {
            get { return List[i]; }
        }

        /// <summary>
        /// Sets up a new SingleList
        /// </summary>
        public SingleList()
        {
            List = new List<T>();
        }

        /// <summary>
        /// Adds to the <see cref="List"/>
        /// </summary>
        /// <param name="t">Item to add</param>
        /// <returns>If the add was a success</returns>
        public new bool Add(T t)
        {
            if (!List.Contains(t))
            {
                List.Add(t);
                return true;
            }

            return false;
        }

        /// <summary>
        /// To use enumerables
        /// </summary>
        /// <returns>The <see cref="List"/>s enumerator</returns>
        public new IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
