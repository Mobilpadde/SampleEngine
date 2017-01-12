using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SampleUsage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Engine.Game _game;
        private Canvas _c;

        public MainWindow()
        {
            InitializeComponent();

            _c = new Canvas();

            _game = new Engine.Game(7, 7, _c);

            _c.Width = this.Width;
            _c.Height = this.Height;

            this.AddChild(_c);

            Engine.Animation loader = new Engine.Animation(new Engine.Animations.Dots(_c));

            _game.Load(new Action(() =>
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    loader.Start();
                    this.load();
                }));
            }));

            _c.Loaded += (s, e) => _game.Loaded(new Action(() =>
            {
                loader.Stop();
                _game.Run();

                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.Width += 100;
                    this.Height += 100;
                }));
            }));

            this.KeyDown += (s, e) => _game.KeyHandler(e, true);
            this.KeyUp += (s, e) => _game.KeyHandler(e, false);
        }

        private void load()
        {
            List<Engine.Level> levels = new List<Engine.Level>();
            for (int i = 0; i < 4; i++)
            {
                Engine.Level l = createLevel((i + 1) % 3);
                l.Setup();

                levels.Add(l);
            }

            _game.Setup(levels, levels[0], new TimeSpan(0, 0, 0, 0, 1000 / 30)); // We're aiming for 30 fps
        }

        private Engine.Level createLevel(int next)
        {
            double spacing = Engine.ThreadSafeRandom.NextDouble(10, 25);

            Engine.Level l = new Engine.Level(this.Width, this.Height, spacing, Engine.ThreadSafeRandom.Next(1, 4));

            double x = Engine.ThreadSafeRandom.Selector(new double[] { spacing, this.Width - spacing });
            double y = Engine.ThreadSafeRandom.Selector(new double[] { spacing, this.Height - spacing });

            l.SetOpening(next, x, y);

            return l;
        }
    }
}
