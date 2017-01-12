
namespace Engine.Math
{
    internal static class Helper
    {
        /// <summary>
        /// Calculates the distance between <paramref name="p1"/> and <paramref name="p2"/>
        /// </summary>
        /// <param name="p1">First <see cref="Creatures.Position"/></param>
        /// <param name="p2">Second <see cref="Creatures.Position"/></param>
        /// <returns>Distance between <paramref name="p1"/> and <paramref name="p2"/></returns>
        internal static double Distance(Creatures.Position p1, Creatures.Position p2)
        {
            return System.Math.Sqrt(System.Math.Pow(p2.X - p1.X, 2) + System.Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
