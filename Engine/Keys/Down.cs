
namespace Engine.Keys
{
    internal class Down : Key
    {
        /// <summary>
        /// Sets the <see cref="Move.Direction"/> to <see cref="Move.Direction.South"/>
        /// </summary>
        public Down()
        {
            this.Direction = Move.Direction.South;
        }
    }
}
