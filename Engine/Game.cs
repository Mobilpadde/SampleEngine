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
        /// Fires when an accepted <see cref="Keys.MoveKey"/> has been touched
        /// </summary>
        internal event EventHandler<Keys.MoveKey> KeyChange;

        /// <summary>
        /// Fires when <see cref="Level"/> should change
        /// </summary>
        internal event EventHandler<int> LevelChange;

        private static Creatures.Hero _hero;

        private List<Level> _levels;
        private Level _currentLevel;

        private Helpers.SingleMoveKeyList _moveKeys;
        private List<Key> _acceptedKeys;

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

            _moveKeys = new Helpers.SingleMoveKeyList();
            _acceptedKeys = new List<Key>();
        }

        /// <summary>
        /// Dispose of everything and clean up using GC
        /// </summary>
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

            _currentLevel.Setup(ref _hero, this, speed);

            _levels.ForEach(l => l.LevelChange += levelChange);

            _speed = speed;
        }

        /// <summary>
        /// A shortcut to add movement keys to the game
        /// </summary>
        public void AddMoveKeys()
        {
            foreach (Keys.MoveKey key in new List<Keys.MoveKey>() { new Keys.Up(), new Keys.Down(), new Keys.Left(), new Keys.Right() })
            {
                if (_moveKeys.Add(key))
                    _acceptedKeys.AddRange(key.Accepted);
            }
        }

        /// <summary>
        /// Add keys to the game to listen for
        /// </summary>
        /// <param name="key"></param>
        public void AddKey(Keys.MoveKey key)
        {
            if(_moveKeys.Add(key))
                _acceptedKeys.AddRange(key.Accepted);
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

                _currentLevel.Draw();
            }), System.Windows.Threading.DispatcherPriority.Render);
        }

        /// <summary>
        /// Handle keystrokes
        /// </summary>
        /// <param name="e">The keystroke</param>
        /// <param name="down">If the key is pressed or not</param>
        public void KeyHandler(KeyEventArgs e, bool down)
        {
            int idx = _acceptedKeys.IndexOf(e.Key);

            if (idx > -1)
            {
                Keys.MoveKey key = _moveKeys.List.Where(k => k.Accepted.IndexOf(e.Key) > -1).ToArray()[0];

                if (key != null)
                {
                    key.IsDown = down;
                    KeyChange?.Invoke(this, key);
                }
            }
        }

        private void levelChange(object sender, int levelId)
        {
            LevelChange?.Invoke(this, levelId);

            _currentLevel.Dispose();
            _currentLevel = _levels.First(l => l.Id == levelId);

            _c.Dispatcher.InvokeAsync(new Action(() => _currentLevel.Setup(ref _hero, this, _speed)), System.Windows.Threading.DispatcherPriority.Render);
            
            Run();
        }

        private void fire(Action a)
        {
            new Thread(() => a()).Start();
        }
    }
}
