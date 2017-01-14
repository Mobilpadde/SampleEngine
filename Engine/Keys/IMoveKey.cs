using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Keys
{
    public interface IMoveKey : Actions.IMove
    {
        /// <summary>
        /// A list of accepted keys
        /// </summary>
        List<System.Windows.Input.Key> Accepted { get; set; }

        /// <summary>
        /// If the <see cref="Key"/> is pressed
        /// </summary>
        bool IsDown { get; set; }

        /// <summary>
        /// The <see cref="Move.Direction"/> to move in
        /// </summary>
        Actions.Movement.Direction Direction { get; set; }
    }
}
