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
    /// Represents the current vital statistics of some game entity.
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
        public void Decrement(int damage = 1)
        {
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);

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
                if (Type == EntityType.Player)
                {
                    Debug.Log("Player has died. Triggering game over logic.");
                    var ev = Schedule<HealthIsZero>();
                    ev.health = this;
                }
                else if (Type == EntityType.Enemy)
                {
                    Debug.Log("Enemy has died. Removing enemy from the game.");
                    gameObject.SetActive(false); // Deactivate or destroy the enemy
                }
            }
        }

        private void ScheduleHealthIsZeroEvent()
        {
            Debug.Log("Scheduling HealthIsZero event for: " + gameObject.name);
            var ev = Schedule<HealthIsZero>();
            ev.health = this;
        }

        /// <summary>
        /// Permanently increases the maximum HP by a specified amount.
        /// Optionally, increase the current HP to match the new max HP.
        /// </summary>
        public void IncreaseMaxHP(int amount, bool increaseCurrentHP = false)
        {
            maxHP += amount;

            if (increaseCurrentHP)
            {
                currentHP = maxHP; // Set current HP to match the new max HP
            }
            else
            {
                currentHP = Mathf.Clamp(currentHP, 0, maxHP); // Keep current HP within valid range
            }
            Debug.Log($"Max HP increased by {amount}. New max HP: {maxHP}. Current HP: {currentHP}");
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
