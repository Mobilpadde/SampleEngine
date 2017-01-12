
namespace Engine
{
    internal class Move
    {
        /// <summary>
        /// The <see cref="Move.Direction"/>s one can move
        /// </summary>
        internal enum Direction : sbyte
        {
            East = 0,
            West = 1,
            South = 2,
            North = 3
        }

        /// <summary>
        /// The <see cref="Move.Direction"/> to move 
        /// </summary>
        internal Direction Dir { get; private set; }

        /// <summary>
        /// Sets the direction of the <see cref="Move"/>
        /// </summary>
        /// <param name="dir">The <see cref="Move.Direction"/> to move</param>
        internal Move(Direction dir)
        {
            Dir = dir;
        }
    }
}