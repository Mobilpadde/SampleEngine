
using System.Collections.Generic;

namespace Engine.Keys
{
    internal class Up : MoveKey
    {
        /// <summary>
        /// Sets the <see cref="Actions.Movement.Direction"/> to <see cref="Actions.Movement.Direction.North"/> & <see cref="Keys.Key.Accepted"/> up-keys
        /// </summary>
        public Up()
        {
            this.Direction = Actions.Movement.Direction.North;

            this.Accepted = new List<System.Windows.Input.Key>() {
                System.Windows.Input.Key.W,
                System.Windows.Input.Key.I,
                System.Windows.Input.Key.Up
            };
        }
        
        /// <summary>
         /// Returns a direction to move in
         /// </summary>
         /// <param name="speed">The speed to move with</param>
         /// <returns>A direction to move in</returns>
        public override double[] Fire(double speed)
        {
            return new double[] { 0, -speed };
        }
    }
}
