using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    /// <summary>
    /// Simple movements to do
    /// </summary>
    public interface IMove : IAction<double[], double>
    {
        /// <summary>
        /// Move by returned direction
        /// </summary>
        /// <param name="speed">The speed to move in</param>
        /// <returns>A direction to move</returns>
        new double[] Fire(double speed);
    }
}
