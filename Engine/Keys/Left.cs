using System;
using System.Collections.Generic;

namespace Engine.Keys
{
    internal class Left : MoveKey
    {
        /// <summary>
        /// Sets the <see cref="Actions.Movement.Direction"/> to <see cref="Actions.Movement.Direction.West"/> &  & <see cref="Keys.Key.Accepted"/> left-keys
        /// </summary>
        public Left()
        {
            this.Direction = Actions.Movement.Direction.West;

            this.Accepted = new List<System.Windows.Input.Key>() {
                System.Windows.Input.Key.A,
                System.Windows.Input.Key.J,
                System.Windows.Input.Key.Left
            };
        }

        /// <summary>
        /// Returns a direction to move in
        /// </summary>
        /// <param name="speed">The speed to move with</param>
        /// <returns>A direction to move in</returns>
        public override double[] Fire(double speed)
        {
            return new double[] { -speed, 0 };
        }
    }
}
