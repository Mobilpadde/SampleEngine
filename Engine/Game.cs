using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Threading;

namespace Engine
{
    public class Game
    {
        internal event EventHandler<Keys.Key> KeyChange;
        internal event EventHandler<int> LevelChange;

        private static Creatures.Hero _hero;

        private List<Level> _levels;
        private Level _currentLevel;

        private Dictionary<Move.Direction, Keys.Key> _keys;

        private Canvas _c;

        private TimeSpan _speed;

        public Game(double width, double height, Canvas c)
        {
            _c = c;

            _hero = new Creatures.Hero(
                width, height, 
                ThreadSafeRandom.NextDouble(-width / 2, width / 2), 
                ThreadSafeRandom.NextDouble(-height / 2, height / 2),
                _c);

            _keys = new Dictionary<Move.Direction, Keys.Key>()
            {
                { Move.Direction.East, new Keys.Right() },
                { Move.Direction.West, new Keys.Left() },
                { Move.Direction.South, new Keys.Down() },
                { Move.Direction.North, new Keys.Up() }
            };
        }

        public void Setup(List<Level> levels, Level current, TimeSpan speed)
        {
            _levels = levels;
            _currentLevel = current;

            _currentLevel.Setup(ref _hero, this, speed, _c);

            _levels.ForEach(l => l.LevelChange += levelChange);

            _speed = speed;
        }

        public void Load(Action a)
        {
            fire(a);
        }

        public void Loaded(Action a)
        {
            fire(a);
        }

        public void Run()
        {
            _c.Dispatcher.InvokeAsync(new Action(() => 
            {
                _c.Children.Clear();

                _currentLevel.Draw(_c);
            }), System.Windows.Threading.DispatcherPriority.Render);
        }

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
