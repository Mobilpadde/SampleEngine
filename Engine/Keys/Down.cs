using Engine.Actions;
using System.Collections.Generic;
using System;

namespace Engine.Keys
{
    internal sealed class Down : MoveKey
    {
        /// <summary>
        /// Sets the <see cref="Movement.Direction"/> to <see cref="Movement.Direction.South"/> & <see cref="Keys.Key.Accepted"/> down-keys
        /// </summary>
        public Down()
        {
            this.Direction = Actions.Movement.Direction.South;

            this.Accepted = new List<System.Windows.Input.Key>() {
                System.Windows.Input.Key.S,
                System.Windows.Input.Key.K,
                System.Windows.Input.Key.Down
            };
        }
        
        /// <summary>
        /// Returns a direction to move in
        /// </summary>
        /// <param name="speed">The speed to move with</param>
        /// <returns>A direction to move in</returns>
        public override double[] Fire(double speed)
        {
            return new double[] { 0, speed };
        }
    }
}
