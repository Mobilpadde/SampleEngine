using System;

namespace Engine.Actions
{
    public class Movement
    {
        /// <summary>
        /// The four directions one can move in
        /// </summary>
        public enum Direction : sbyte
        {
            East = 0,
            West = 1,
            South = 2,
            North = 3
        }

        /// <summary>
        /// See <see cref="Movement.Movement"/>
        /// </summary>
        internal Direction Dir { get; private set; }

        /// <summary>
        /// Sets the direction to move in
        /// </summary>
        /// <param name="dir"><see cref="Movement.Direction"/> to move in</param>
        internal Movement(Direction dir)
        {
            Dir = dir;
        }
    }
}
