
namespace Engine.Creatures
{
    internal sealed class Size
    {
        internal double Width { get; private set; }
        internal double Height { get; private set; }

        internal Size(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
