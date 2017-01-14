using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Helpers
{
    public sealed class SingleMoveKeyList : SingleList<Keys.MoveKey>
    {
        /// <summary>
        /// Overwrites a <see cref="Keys.MoveKey"/> to <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key to overwrite</param>
        /// <returns>If the key has been overwritten</returns>
        public bool Overrride(Keys.MoveKey key)
        {
            if (List.Contains(key))
                return false;
            else
            {
                for (int i = 0; i < List.Count(); i++)
                {
                    if (List[i] == key)
                    {
                        List[i] = key;
                        break;
                    }
                }
            }

            return true;
        }
    }
}
