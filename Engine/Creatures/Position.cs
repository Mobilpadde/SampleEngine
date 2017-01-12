using System;

namespace Engine.Creatures
{
    internal sealed class Position : EventArgs
    {
        internal EventHandler<Position> PostionChanged;

        internal double X { get; private set; }
        internal double Y { get; private set; }

        internal Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        internal void Set(double x, double y)
        {
            X = x;
            Y = y;

            PostionChanged(this, this);
        }
    }
}
