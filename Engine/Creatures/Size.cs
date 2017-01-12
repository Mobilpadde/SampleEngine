
namespace Engine.Creatures
{
    internal sealed class Size
    {
        /// <summary>
        /// Width of the <see cref="Creature"/>
        /// </summary>
        internal double Width { get; private set; }

        /// <summary>
        /// Height of the <see cref="Creature"/>
        /// </summary>
        internal double Height { get; private set; }

        /// <summary>
        /// Size of a <see cref="Creature"/>
        /// </summary>
        /// <param name="width">Width of the <see cref="Creature"/></param>
        /// <param name="height">Height of the <see cref="Creature"/></param>
        internal Size(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
