
namespace Engine
{
    internal class Opening
    {
        /// <summary>
        /// Id of the next <see cref="Level"/>
        /// </summary>
        internal protected int Id;

        /// <summary>
        /// X-coordinate of the portal
        /// </summary>
        internal protected double X;

        /// <summary>
        /// Y-coordinate of the portal
        /// </summary>
        internal protected double Y;

        /// <summary>
        /// Creates a portal to another <see cref="Level"/>
        /// </summary>
        /// <param name="id">Id to the next <see cref="Level"/></param>
        /// <param name="x">X-coordinate of the portal</param>
        /// <param name="y">Y-coordinate of the portal</param>
        internal Opening(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}
