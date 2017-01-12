using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Engine.Animations
{
    public sealed class Linear : IAnimation
    {
        private Line _line;
        private bool _right;
        private Canvas _c;

        /// <summary>
        /// Constructs a new <see cref="Animation"/> of type <see cref="Animations.Linear"/>
        /// </summary>
        /// <param name="c"></param>
        public Linear(Canvas c)
        {
            _c = c;

            _line = new Line();

            _line.Fill = Brushes.Black;

            _line.StrokeThickness = 2;

            _line.X1 = -30;
            _line.X2 = -30;
        }

        /// <summary>
        /// Runs the <see cref="Animation"/>
        /// </summary>
        public void Run()
        {
            _c.Dispatcher.InvokeAsync(new Action(() =>
            {
                if (_right)
                {
                    _line.X1++;
                    _line.X2++;

                    if (_line.X1 == 30)
                        _line.X1 = 30;

                    if (_line.X2 == 30)
                        _right = false;
                }
                else
                {
                    _line.X1--;
                    _line.X2--;

                    if (_line.X1 == -30)
                        _line.X1 = -30;

                    if (_line.X2 == -30)
                        _right = true;
                }

                Thread.Sleep(250);
            }), System.Windows.Threading.DispatcherPriority.Background);
            
        }
    }
}
