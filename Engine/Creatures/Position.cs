using System;

namespace Engine.Creatures
{
    internal sealed class Position : EventArgs
    {
        /// <summary>
        /// Fires when the <see cref="Creature"/>'s position has changed
        /// </summary>
        internal EventHandler<Position> PostionChanged;

        /// <summary>
        /// X-coordinate of <see cref="Creature"/>
        /// </summary>
        internal double X { get; private set; }

        /// <summary>
        /// Y-coordinate of <see cref="Creature"/>
        /// </summary>
        internal double Y { get; private set; }

        /// <summary>
        /// Sets the initial position of the <see cref="Creature"/>
        /// </summary>
        /// <param name="x">X-coordinate of the <see cref="Creature"/></param>
        /// <param name="y">Y-coordinate of the <see cref="Creature"/></param>
        internal Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Sets a new positon for the <see cref="Creature"/>
        /// </summary>
        /// <param name="x">New x-coordinate for the <see cref="Creature"/></param>
        /// <param name="y">new Y-coordinate for the <see cref="Creature"/></param>
        internal void Set(double x, double y)
        {
            X = x;
            Y = y;

            PostionChanged(this, this);
        }
    }
}
