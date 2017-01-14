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
        /// <summary>
        /// Static id for <see cref="Level"/>s
        /// </summary>
        public static int StatId { get; private set; }

        private static object _locker = new object();

        /// <summary>
        /// The id of the <see cref="Level"/>
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Fires when <see cref="Level"/>s shoudl change
        /// </summary>
        internal EventHandler<int> LevelChange;

        private bool _run;
        private Thread _moveThread;

        private Helpers.SingleMoveKeyList _keys;

        private double _width;
        private double _height;
        private double _spacing;

        private List<Opening> _openings;

        private List<Line> _grid;
        private Creatures.Hero _hero;
        private List<Creatures.Blob> _blobs;

        private int _numEnemies;

        private Game _publisher;
        private Canvas _c;

        /// <summary>
        /// Contruct a new <see cref="Level"/>
        /// </summary>
        /// <param name="width">Sets the width of the <see cref="Level"/></param>
        /// <param name="height">Sets the height of the <see cref="Level"/></param>
        /// <param name="spacing">Spacing between the grid</param>
        /// <param name="enemies">How many <see cref="Creatures.Blob"/>s the <see cref="Level"/> should contain</param>
        public Level(double width, double height, double spacing, int enemies, Canvas c)
        {
            Id = StatId++;

            _run = true;

            _width = width;
            _height = height;
            _spacing = spacing;

            _numEnemies = enemies;

            _openings = new List<Opening>();

            _keys = new Helpers.SingleMoveKeyList();

            _c = c;

            _blobs = Enumerable.Repeat(0, _numEnemies).Select(n =>
            {
                Creatures.Blob b = new Creatures.Blob(_spacing / 2, _spacing / 2, 0, 0, c);
                b.Setup(_width, _height);

                return b;
            }).ToList();
        }

        /// <summary>
        /// Run to setup the level
        /// </summary>
        public void Setup()
        {
            double x = 0, y = 0;
            bool down = false;
            _grid = Enumerable.Repeat(0, (int)(System.Math.Ceiling(_width / _spacing) + System.Math.Ceiling(_height / _spacing))).Select(n =>
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

        /// <summary>
        /// Sets portals to another <see cref="Level"/>
        /// </summary>
        /// <param name="to">The id of the next <see cref="Level"/></param>
        /// <param name="x">X-coordinate of the portal</param>
        /// <param name="y">Y-coordinate of the portal</param>
        public void SetOpening(int to, double x, double y)
        {
            _openings.Add(new Opening(to, x, y));
        }

        /// <summary>
        /// Run this after each <see cref="Level"/> change
        /// </summary>
        public void Dispose()
        {
            _publisher.KeyChange -= KeyHandler;

            _run = false;
            GC.Collect();
        }

        /// <summary>
        /// Sets up the level
        /// </summary>
        /// <param name="hero"><see cref="Creatures.Hero"/> to move around</param>
        /// <param name="publisher">The <see cref="Game"/>-class</param>
        /// <param name="speed">Sets the update speed of the <see cref="Level"/></param>
        /// <param name="c"></param>
        internal void Setup(ref Creatures.Hero hero, Game publisher, TimeSpan speed)
        {
            _publisher = publisher;

            _run = true;

            _hero = hero;

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
                        b.LookAbout(_hero);
                        b.Move();
                        edges(b, false);
                    }

                    lock (_locker)
                    {
                        foreach (Keys.MoveKey key in _keys)
                        {
                            Creatures.Position pos = _hero.Position;

                            if (key.IsDown)
                            {
                                double[] dir = key.Fire(_spacing / 2);
                                _hero.Position.Set(pos.X + dir[0], pos.Y + dir[1]);
                            }
                        }

                        edges(_hero, true);
                    }

                    Thread.Sleep(speed);
                }
            });

            _moveThread.Start();
        }

        /// <summary>
        /// Fires when a key has been touched
        /// </summary>
        /// <param name="sender">The sender; not important</param>
        /// <param name="key">Sets the status of the current <see cref="Keys.Key"/></param>
        internal void KeyHandler(object sender, Keys.MoveKey key)
        {
            if (_keys.Overrride(key))
                _keys.Add(key);
        }

        /// <summary>
        /// Handles the drawing of the level
        /// </summary>
        /// <param name="c">The canvas to draw on</param>
        internal void Draw()
        {
            _grid.ForEach(elm => _c.Children.Add(elm));
            _openings.ForEach(o =>
            {
                Rectangle rect = new Rectangle();

                rect.Fill = Brushes.Aqua;

                rect.Width = _spacing;
                rect.Height = _spacing;

                rect.Margin = new Thickness(o.X, o.Y, 0, 0);

                _c.Children.Add(rect);
            });
            _blobs.ForEach(b => _c.Children.Add(b.Body));

            try
            {
                _c.Children.Add(_hero.Body);
            }
            catch (NullReferenceException) { }
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
