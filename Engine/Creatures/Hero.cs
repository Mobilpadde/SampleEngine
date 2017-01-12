using System.Windows.Controls;
using System.Windows.Media;

namespace Engine.Creatures
{
    public class Hero : Creature
    {
        /// <summary>
        /// Creates a new <see cref="Creatures.Hero"/>
        /// </summary>
        /// <param name="width">Width of the <see cref="Creatures.Hero"/></param>
        /// <param name="height">Height of the <see cref="Creatures.Hero"/></param>
        /// <param name="x">X-coordinate of the <see cref="Creatures.Hero"/></param>
        /// <param name="y">Y-coordinate of the <see cref="Creatures.Hero"/></param>
        /// <param name="c">The canvas to draw the <see cref="Creatures.Hero"/> on</param>
        public Hero(double width, double height, double x, double y, Canvas c) : base(width, height, x, y, c)
        {
            Body.Fill = Brushes.Indigo;

            Hp = 100;
        }
    }
}
