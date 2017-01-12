using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Engine.Animations
{
    public sealed class Dots : IAnimation
    {
        private List<Ellipse> _dots;
        private int _idx;

        private Brush _main;
        private Brush _secondary;

        private Canvas _c;

        /// <summary>
        /// Constructs a new <see cref="Animation"/> of type <see cref="Animations.Dots"/>
        /// </summary>
        /// <param name="c"></param>
        public Dots(Canvas c)
        {
            _c = c;

            _main = Brushes.DarkGray;
            _secondary = Brushes.Black;

            double x = -30;

            _dots = Enumerable.Repeat(0, 3).Select(n =>
            {
                Ellipse dot = new Ellipse();

                dot.Fill = _main;

                dot.Width = 10;
                dot.Height = 10;

                dot.Margin = new Thickness(x, 0, 0, 0);

                x += 30;

                c.Children.Add(dot);

                return dot;
            }).ToList();

            _idx = 0;

            _dots[_idx].Fill = _secondary;
        }

        /// <summary>
        /// Runs the <see cref="Animation"/>
        /// </summary>
        public void Run()
        {
            _c.Dispatcher.InvokeAsync(new Action(() =>
            {
                _dots[_idx].Fill = _main;

                _idx++;
                _idx %= _dots.Count();

                _dots[_idx].Fill = _secondary;

                Thread.Sleep(250);
            }), System.Windows.Threading.DispatcherPriority.Background);
        }
    }
}
