using System.Windows.Controls;
using System.Windows.Media;

namespace Engine.Creatures
{
    public class Blob : Creature
    {
        internal Position Wish { get; private set; }

        private double _width;
        private double _height;

        public Blob(double lWidth, double lHeight, double width, double height, double x, double y, Canvas c) : base(width, height, x, y, c)
        {
            _width = lWidth;
            _height = lHeight;

            Body.Fill = Brushes.ForestGreen;

            Hp = 10;

            Wish = new Position(0, 0);
            checkWish(null, null);

            Position.PostionChanged += checkWish;
        }

        public void Move()
        {
            if (Position.X < Wish.X)
                Position.Set(Position.X + 1, Position.Y);

            if (Position.X > Wish.X)
                Position.Set(Position.X - 1, Position.Y);

            if (Position.Y < Wish.Y)
                Position.Set(Position.X, Position.Y + 1);

            if (Position.Y > Wish.Y)
                Position.Set(Position.X, Position.Y - 1);
        }

        private void checkWish(object s, Position pos)
        {
            if (Position.X == Wish.X && Position.Y == Wish.Y)
                Wish = new Position(
                ThreadSafeRandom.NextDouble(0, _width),
                ThreadSafeRandom.NextDouble(0, _height));
        }
    }
}
