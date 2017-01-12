using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
