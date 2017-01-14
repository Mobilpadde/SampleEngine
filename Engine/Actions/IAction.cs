using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    /// <summary>
    /// A simple action to fire
    /// </summary>
    /// <typeparam name="O">The return-type of the <see cref="Actions.IAction{O, I}.Fire(I)"/></typeparam>
    /// <typeparam name="I">The argument of <see cref="Actions.IAction{O, I}.Fire(I)"/></typeparam>
    public interface IAction<O, I>
    {
        O Fire(I input);
    }
}
