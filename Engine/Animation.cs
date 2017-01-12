using System;
using System.Threading;

namespace Engine
{
    public class Animation : IDisposable
    {
        private bool _run;
        private Thread _animationThread;

        public Animation(Animations.IAnimation animation)
        {
            _run = true;

            _animationThread = new Thread(() =>
            {
                while (_run)
                    animation.Run();
            });
        }

        public void Start()
        {
            _animationThread.Start();
        }

        public void Stop()
        {
            _run = false;
        }

        public void Dispose()
        {
            this.Stop();
            _animationThread.Abort();
        }
    }
}
