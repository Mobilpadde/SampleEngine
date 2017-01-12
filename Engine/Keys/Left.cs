
namespace Engine.Keys
{
    internal class Left : Key
    {
        /// <summary>
        /// Sets the <see cref="Move.Direction"/> to <see cref="Move.Direction.West"/>
        /// </summary>
        public Left()
        {
            this.Direction = Move.Direction.West;
        }
    }
}
