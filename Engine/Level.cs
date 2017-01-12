using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Engine
{
    public class Level : EventArgs, IDisposable
    {
        public static int StatId { get; private set; }

        private static object _locker = new object();

        public int Id { get; private set; }

        internal EventHandler<int> LevelChange;

        private bool _run;
        private Thread _moveThread;

        private Dictionary<Keys.Key, bool> _keyHandler;

        private double _width;
        private double _height;
        private double _spacing;

        private List<Opening> _openings;

        private List<Line> _grid;
        private Creatures.Hero _hero;
        private List<Creatures.Blob> _blobs;

        private int _numEnemies;

        private Game _publisher;

        public Level(double width, double height, double spacing, int enemies)
        {
            Id = StatId++;

            _run = true;

            _width = width;
            _height = height;
            _spacing = spacing;

            _numEnemies = enemies;

            _openings = new List<Opening>();

            _keyHandler = new Dictionary<Keys.Key, bool>();
        }

        public void Setup()
        {
            double x = 0, y = 0;
            bool down = false;
            _grid = Enumerable.Repeat(0, (int)(Math.Ceiling(_width / _spacing) + Math.Ceiling(_height / _spacing))).Select(n =>
            {
                Line l = new Line();

                l.Stroke = Brushes.DarkGray;
                l.StrokeThickness = 2;

                l.Margin = new Thickness(x, y, 0, 0);

                l.X1 = 0;
                l.Y1 = 0;

                if (x >= _width)
                {
                    down = true;
                    x = 0;
                }
                else if (down)
                {
                    l.X2 = _width;
                    y += _spacing;
                }
                else
                {
                    l.Y2 = _height;
                    x += _spacing;
                }

                return l;
            }).ToList();
        }

        public void SetOpening(int to, double x, double y)
        {
            _openings.Add(new Opening(to, x, y));
        }

        public void Dispose()
        {
            _publisher.KeyChange -= KeyHandler;

            _run = false;
            GC.Collect();
        }

        internal void Setup(ref Creatures.Hero hero, Game publisher, TimeSpan speed, Canvas c)
        {
            _publisher = publisher;

            _run = true;

            _hero = hero;

            _blobs = Enumerable.Repeat(0, _numEnemies).Select(n => new Creatures.Blob(_width, _height, _spacing / 2, _spacing / 2, 0, 0, c)).ToList();

            publisher.KeyChange += KeyHandler;
            publisher.LevelChange += (s, id) =>
            {
                _run = id == Id;
            };

            _moveThread = new Thread(() =>
            {
                while (_run)
                {
                    foreach (Creatures.Blob b in _blobs)
                    {
                        b.Move();
                        edges(b, false);
                    }

                    lock (_locker)
                    {
                        foreach (KeyValuePair<Keys.Key, bool> pair in _keyHandler)
                        {
                            Creatures.Position pos = _hero.Position;

                            if (pair.Key.IsDown)
                            {
                                switch (pair.Key.Direction)
                                {
                                    case Move.Direction.East:
                                        _hero.Position.Set(pos.X + _spacing / 2, pos.Y);
                                        break;

                                    case Move.Direction.West:
                                        _hero.Position.Set(pos.X - _spacing / 2, pos.Y);
                                        break;

                                    case Move.Direction.South:
                                        _hero.Position.Set(pos.X, pos.Y + _spacing / 2);
                                        break;

                                    case Move.Direction.North:
                                        _hero.Position.Set(pos.X, pos.Y - _spacing / 2);
                                        break;
                                }
                            }
                        }

                        edges(_hero, true);
                    }

                    Thread.Sleep(speed);
                }
            });

            _moveThread.Start();
        }

        internal void KeyHandler(object sender, Keys.Key key)
        {
            _keyHandler[key] = key.IsDown;
        }

        internal void Draw(Canvas c)
        {
            _grid.ForEach(elm => c.Children.Add(elm));
            _openings.ForEach(o =>
            {
                Rectangle rect = new Rectangle();

                rect.Fill = Brushes.Aqua;

                rect.Width = _spacing;
                rect.Height = _spacing;

                rect.Margin = new Thickness(o.X, o.Y, 0, 0);

                c.Children.Add(rect);
            });
            _blobs.ForEach(b => c.Children.Add(b.Body));

            c.Children.Add(_hero.Body);
        }

        private void edges(Creatures.Creature thing, bool changable)
        {
            Creatures.Position pos = thing.Position;

            if (pos.X <= 0 || pos.X >= _width || pos.Y <= 0 || pos.Y >= _height)
            {
                int id = -1;
                if (changable && _openings.Any(o =>
                {
                    id = o.Id;
                    return (o.X >= pos.X - (thing.Size.Width + _spacing) && o.X <= pos.X + (thing.Size.Width + _spacing)) &&
                    (o.Y >= pos.Y - (thing.Size.Height + _spacing) && o.Y <= pos.Y + (thing.Size.Height + _spacing));
                }))
                {
                    thing.Position.Set(0, 0);
                    LevelChange(this, id);
                }
                else
                {
                    if (pos.X <= 0)
                        thing.Position.Set(0, pos.Y);
                    else if (pos.X >= _width)
                        thing.Position.Set(_width, pos.Y);

                    if (pos.Y <= 0)
                        thing.Position.Set(pos.X, 0);
                    else if (pos.Y >= _height)
                        thing.Position.Set(pos.X, _height);
                }
            }
        }
    }
}
