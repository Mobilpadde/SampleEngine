
namespace Engine.Keys
{
    internal class Up : Key
    {
        /// <summary>
        /// Sets the <see cref="Move.Direction"/> to <see cref="Move.Direction.North"/>
        /// </summary>
        public Up()
        {
            this.Direction = Move.Direction.North;
        }
    }
}
