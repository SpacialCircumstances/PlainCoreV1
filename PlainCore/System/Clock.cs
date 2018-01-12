using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PlainCore.System
{
    public class Clock
    {
        public Clock()
        {
            clock = new Stopwatch();
            clock.Start();
        }

        protected Stopwatch clock;

        public TimeSpan GetElapsedTime()
        {
            return clock.Elapsed;
        }

        public TimeSpan Restart()
        {
            TimeSpan span = clock.Elapsed;
            clock.Restart();
            return span;
        }

        public TimeSpan Stop()
        {
            clock.Stop();
            return clock.Elapsed;
        }
    }
}
