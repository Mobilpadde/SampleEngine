
namespace Engine
{
    internal class Move
    {
        internal enum Direction : sbyte
        {
            East = 0,
            West = 1,
            South = 2,
            North = 3
        }

        internal Direction Dir { get; private set; }

        internal Move(Direction dir)
        {
            Dir = dir;
        }
    }
}
