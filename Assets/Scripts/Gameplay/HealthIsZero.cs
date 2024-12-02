using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;
using UnityEngine; 

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player health reaches 0. This usually would result in a 
    /// PlayerDeath event.
    /// </summary>
    /// <typeparam name="HealthIsZero"></typeparam>
    public class HealthIsZero : Simulation.Event<HealthIsZero>
    {
        public Health health;

        public override void Execute()
        {
            Debug.Log("HealthIsZero event executed for: " + health.gameObject.name);
            Schedule<PlayerDeath>();
        }    
    }
}