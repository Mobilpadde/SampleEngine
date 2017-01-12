using System.Windows.Controls;
using System.Windows.Media;

namespace Engine.Creatures
{
    public class Blob : Creature
    {
        public Blob(double width, double height, double x, double y, Canvas c) : base(width, height, x, y, c)
        {
            Body.Fill = Brushes.ForestGreen;

            Hp = 10;
        }
    }
}
