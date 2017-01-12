using System;

namespace Engine.Keys
{
    internal abstract class Key : EventArgs
    {
        internal bool IsDown { get; set; }
        internal Move.Direction Direction { get; set; }
    }
}
