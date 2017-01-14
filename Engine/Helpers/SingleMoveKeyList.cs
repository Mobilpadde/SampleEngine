using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Helpers
{
    public sealed class SingleMoveKeyList : SingleList<Keys.MoveKey>
    {
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
