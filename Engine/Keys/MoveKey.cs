using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Engine.Actions;

namespace Engine.Keys
{
    public abstract class MoveKey : EventArgs, IMoveKey
    {
        /// <summary>
        /// A list of accepted keys
        /// </summary>
        public List<System.Windows.Input.Key> Accepted { get; set; }

        /// <summary>
        /// If the <see cref="Key"/> is pressed
        /// </summary>
        public bool IsDown { get; set; }

        /// <summary>
        /// The <see cref="Move.Direction"/> to move in
        /// </summary>
        public Movement.Direction Direction { get; set; }

        /// <summary>
        /// What to do when <see cref="IsDown"/> is true
        /// </summary>
        /// <param name="input">Direction to move in</param>
        /// <returns>Fixed array with movements-coordinates</returns>
        public abstract double[] Fire(double input);
    }
}
