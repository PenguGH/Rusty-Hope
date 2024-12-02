using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 50;

        private Animator anim;
        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        public int currentHP;

        public enum EntityType
        {
            Player,
            Enemy
        }

         [SerializeField]public EntityType Type;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);

            Debug.Log($"Decrement called on: {gameObject.name} with type: {Type} and currentHP: {currentHP}");

            if (anim != null)
            {
                anim.SetTrigger("hurt"); // Trigger the hurt animation
            }

            if (currentHP == 0)
            {

                        // Schedule HealthIsZero event regardless of entity type
       //var ev = Schedule<HealthIsZero>();
        //ev.health = this;

       //Debug.Log($"{gameObject.name} has died. Triggering HealthIsZero event.");

            // Handle death logic based on entity type
            if (Type.Equals(EntityType.Player))
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
                
                Debug.Log("Player has died. Triggering game over logic.");
                
            }
            else if (Type == EntityType.Enemy)
            {
                Debug.Log("Enemy has died. Removing enemy from the game.");
                gameObject.SetActive(false); // Deactivate or destroy the enemy
            }
                
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();

        }

        void Awake()
        {
            currentHP = maxHP;
            anim = GetComponent<Animator>();

                // Assign Type based on the specific entity
    if (gameObject.CompareTag("Player"))
    {
        Type = EntityType.Player;
    }
    else if (gameObject.CompareTag("Enemy"))
    {
        Type = EntityType.Enemy;
    }
        }

    }
}
