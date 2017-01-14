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
        internal List<T> List;

        public new T this[int i]
        {
            get { return List[i]; }
        }

        public SingleList()
        {
            List = new List<T>();
        }

        public new bool Add(T t)
        {
            if (!List.Contains(t))
            {
                List.Add(t);
                return true;
            }

            return false;
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public new int Count()
        {
            return List.Count();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
