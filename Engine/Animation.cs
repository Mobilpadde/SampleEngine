using System;
using System.Threading;

namespace Engine
{
    public class Animation : IDisposable
    {
        private bool _run;
        private Thread _animationThread;

        /// <summary>
        /// Sets up a new <see cref="Animation"/> with an <paramref name="animation"/>
        /// </summary>
        /// <param name="animation">An animation to run, see 
        /// <list type="bullet">
        /// <item>
        /// <description><see cref="Animations.Dots"/></description>
        /// </item>,
        /// <item>
        /// <description><see cref="Animations.Linear"/></description>
        /// </item>
        /// </list>
        /// for choices
        /// </param>
        public Animation(Animations.IAnimation animation)
        {
            _run = true;

            _animationThread = new Thread(() =>
            {
                while (_run)
                    animation.Run();
            });
        }

        /// <summary>
        /// Starts the animation
        /// </summary>
        public void Start()
        {
            _animationThread.Start();
        }

        /// <summary>
        /// Stops the animation
        /// </summary>
        public void Stop()
        {
            _run = false;
        }

        /// <summary>
        /// Disposes of the animation
        /// </summary>
        public void Dispose()
        {
            this.Stop();
            _animationThread.Abort();
        }
    }
}
