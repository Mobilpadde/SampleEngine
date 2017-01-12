using System;

namespace Engine.Keys
{
    internal abstract class Key : EventArgs
    {
        /// <summary>
        /// If the <see cref="Key"/> is pressed
        /// </summary>
        internal bool IsDown { get; set; }

        /// <summary>
        /// The <see cref="Move.Direction"/> to move in
        /// </summary>
        internal Move.Direction Direction { get; set; }
    }
}
