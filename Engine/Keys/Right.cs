
using System.Collections.Generic;

namespace Engine.Keys
{
    internal class Right : MoveKey
    {
        /// <summary>
        /// Sets the <see cref="Actions.Movement.Direction"/> to <see cref="Actions.Movement.Direction.East"/> & <see cref="Keys.Key.Accepted"/> right-keys
        /// </summary>
        public Right()
        {
            this.Direction = Actions.Movement.Direction.East;

            this.Accepted = new List<System.Windows.Input.Key>() {
                System.Windows.Input.Key.D,
                System.Windows.Input.Key.L,
                System.Windows.Input.Key.Right
            };
        }

        /// <summary>
        /// Returns a direction to move in
        /// </summary>
        /// <param name="speed">The speed to move with</param>
        /// <returns>A direction to move in</returns>
        public override double[] Fire(double speed)
        {
            return new double[] { speed, 0 };
        }
    }
}
