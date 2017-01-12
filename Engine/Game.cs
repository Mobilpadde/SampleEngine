using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Threading;

namespace Engine
{
    public class Game : IDisposable
    {
        /// <summary>
        /// Fires when an accepted <see cref="Keys.Key"/> has been touched
        /// </summary>
        internal event EventHandler<Keys.Key> KeyChange;

        /// <summary>
        /// Fires when <see cref="Level"/> should change
        /// </summary>
        internal event EventHandler<int> LevelChange;

        private static Creatures.Hero _hero;

        private List<Level> _levels;
        private Level _currentLevel;

        private Dictionary<Move.Direction, Keys.Key> _keys;

        private Canvas _c;

        private TimeSpan _speed;

        /// <summary>
        /// Setup the main game
        /// </summary>
        /// <param name="width">Width of the hero</param>
        /// <param name="height">Height of the hero</param>
        /// <param name="c">The canvas to draw on</param>
        public Game(double width, double height, Canvas c)
        {
            _c = c;

            _hero = new Creatures.Hero(
                width, height, 
                Math.ThreadSafeRandom.NextDouble(-width / 2, width / 2),
                Math.ThreadSafeRandom.NextDouble(-height / 2, height / 2),
                _c);

            _keys = new Dictionary<Move.Direction, Keys.Key>()
            {
                { Move.Direction.East, new Keys.Right() },
                { Move.Direction.West, new Keys.Left() },
                { Move.Direction.South, new Keys.Down() },
                { Move.Direction.North, new Keys.Up() }
            };
        }

        public void Dispose()
        {
            _currentLevel.Dispose();
            GC.Collect();
        }

        /// <summary>
        /// Setup levels and speed of the game
        /// </summary>
        /// <param name="levels"><see cref="Level"/>s to load</param>
        /// <param name="current"><see cref="Level"/> to begin in</param>
        /// <param name="speed">Run speed of the <see cref="Game"/></param>
        public void Setup(List<Level> levels, Level current, TimeSpan speed)
        {
            _levels = levels;
            _currentLevel = current;

            _currentLevel.Setup(ref _hero, this, speed, _c);

            _levels.ForEach(l => l.LevelChange += levelChange);

            _speed = speed;
        }

        /// <summary>
        /// What to load
        /// </summary>
        /// <param name="a">An action to fire</param>
        public void Load(Action a)
        {
            fire(a);
        }

        /// <summary>
        /// What to do when everything has loaded
        /// </summary>
        /// <param name="a">An action to fire</param>
        public void Loaded(Action a)
        {
            fire(a);
        }

        /// <summary>
        /// Run everything
        /// </summary>
        public void Run()
        {
            _c.Dispatcher.InvokeAsync(new Action(() => 
            {
                _c.Children.Clear();

                _currentLevel.Draw(_c);
            }), System.Windows.Threading.DispatcherPriority.Render);
        }

        /// <summary>
        /// Handle keystrokes to walk around
        /// </summary>
        /// <param name="e">The keystroke</param>
        /// <param name="down">If the key is pressed or not</param>
        public void KeyHandler(KeyEventArgs e, bool down)
        {
            List<Key> accepted = new List<Key>(){
                Key.Right,  Key.Left,   Key.Down,   Key.Up,
                Key.D,      Key.A,      Key.S,      Key.W,
                Key.L,      Key.J,      Key.K,      Key.I
            };

            int idx = accepted.IndexOf(e.Key) % 4;

            if (idx >= 0)
            {
                _keys[(Move.Direction)idx].IsDown = down;

                KeyChange?.Invoke(this, _keys[(Move.Direction)idx]);
            }
        }

        private void levelChange(object sender, int levelId)
        {
            LevelChange?.Invoke(this, levelId);

            _currentLevel.Dispose();
            _currentLevel = _levels.First(l => l.Id == levelId);

            _c.Dispatcher.InvokeAsync(new Action(() => _currentLevel.Setup(ref _hero, this, _speed, _c)), System.Windows.Threading.DispatcherPriority.Render);
            

            Run();
        }

        private void fire(Action a)
        {
            new Thread(() => a()).Start();
        }
    }
}
