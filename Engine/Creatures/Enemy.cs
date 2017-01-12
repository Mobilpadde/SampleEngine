using System.Windows.Controls;

namespace Engine.Creatures
{
    public abstract class Enemy : Creature
    {
        /// <summary>
        /// A <see cref="Position"/> which the <see cref="Creatures.Blob"/> wishes to go
        /// </summary>
        internal Position Wish { get; private set; }

        /// <summary>
        /// How long the <see cref="Creatures.Enemy"/> can see
        /// </summary>
        internal double Radius { get; private set; }

        /// <summary>
        /// The running/walking speed of the <see cref="Creatures.Enemy"/>
        /// </summary>
        internal double Speed { get; private set; }

        private double _walk;
        private double _run;
        private double _width;
        private double _height;

        /// <summary>
        /// Constructs an enemy
        /// </summary>
        /// <param name="width">Width of the <see cref="Creatures.Enemy"/></param>
        /// <param name="height">Height of the <see cref="Creatures.Enemy"/></param>
        /// <param name="x">X-coordinate of the <see cref="Creatures.Enemy"/></param>
        /// <param name="y">X-coordinate of the <see cref="Creatures.Enemy"/></param>
        /// <param name="c">The Canvas to draw the <see cref="Creatures.Enemy"/> on</param>
        public Enemy(double width, double height, double x, double y, Canvas c) : base(width, height, x, y, c)
        {
            Radius = Math.ThreadSafeRandom.NextDouble(10, 35);

            _walk = Math.ThreadSafeRandom.NextDouble(0.1, 1);
            _run = Math.ThreadSafeRandom.NextDouble(_walk, _walk + 0.5);

            Speed = _walk;

            Wish = new Position(0, 0);
            checkWish(null, null);

            Position.PostionChanged += checkWish;
        }

        /// <summary>
        /// Uses the Width and Height of the <see cref="Level"/>
        /// </summary>
        /// <param name="width">Width of the <see cref="Level"/></param>
        /// <param name="height">Height of the <see cref="Level"/></param>
        internal void Setup(double width, double height)
        {
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Checks if the <see cref="Creatures.Hero"/> is within <see cref="Creatures.Enemy"/>'s vision
        /// if so, it starts running, otherwise, it'll walk
        /// </summary>
        /// <param name="hero">The <see cref="Creatures.Hero"/> of the <see cref="Game"/></param>
        internal void LookAbout(Hero hero)
        {
            if (Math.Helper.Distance(hero.Position, this.Position) < Radius)
            {
                Wish = hero.Position;
                Speed = _run;
            }
            else
                Speed = _walk;
        }

        /// <summary>
        /// Moves the <see cref="Creatures.Blob"/> around
        /// </summary>
        internal void Move()
        {
            if (Position.X < Wish.X)
                Position.Set(Position.X + Speed, Position.Y);

            if (Position.X > Wish.X)
                Position.Set(Position.X - Speed, Position.Y);

            if (Position.Y < Wish.Y)
                Position.Set(Position.X, Position.Y + Speed);

            if (Position.Y > Wish.Y)
                Position.Set(Position.X, Position.Y - Speed);
        }

        /// <summary>
        /// Checks if the wish has come to life
        /// </summary>
        /// <param name="s">The sender; not important</param>
        /// <param name="pos">The new <see cref="Position"/> of the <see cref="Creatures.Blob"/></param>
        private void checkWish(object s, Position pos)
        {
            if (Position.X == Wish.X && Position.Y == Wish.Y)
                Wish = new Position(
                Math.ThreadSafeRandom.NextDouble(0, _width),
                Math.ThreadSafeRandom.NextDouble(0, _height));
        }
    }
}
