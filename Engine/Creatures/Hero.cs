﻿using System.Windows.Controls;
using System.Windows.Media;

namespace Engine.Creatures
{
    public class Hero : Creature
    {
        public Hero(double width, double height, double x, double y, Canvas c) : base(width, height, x, y, c)
        {
            Body.Fill = Brushes.Indigo;

            Hp = 100;
        }
    }
}
