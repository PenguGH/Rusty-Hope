using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using Platformer.Gameplay; // Importing the namespace for EnemyController

namespace Platformer.Mechanics
{
    public class Projectile : MonoBehaviour
{
    private float direction;
    [SerializeField] private float speed;
    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("CineMachine")) return; 

         Debug.Log("Projectile collided with: " + collision.name);

         // Check if the collision is with an enemy
         //var enemy = collision.GetComponent<EnemyController>();
         //if (enemy != null)
           // {
              //  Debug.Log("Enemy hit by projectile.");
              //  Schedule<EnemyDeath>().enemy = enemy; // Trigger EnemyDeath event
              //  collision.gameObject.SetActive(false); // Optionally deactivate the enemy
           // }
           var enemy = collision.GetComponent<EnemyController>();
            var enemyHealth = collision.GetComponent<Health>(); // Access Health component for the enemy

        if (enemy != null && collision.CompareTag("Enemy") && enemyHealth != null)
    {
        Debug.Log("Enemy hit by projectile.");

        enemyHealth.Decrement(); // Decrement enemy health

        Debug.Log(enemyHealth);
    
    if (!enemyHealth.IsAlive) // Check if health is 0
    {
        Debug.Log("Enemy has died.");
        Schedule<EnemyDeath>().enemy = enemy; // Trigger EnemyDeath event
        collision.gameObject.SetActive(false); // Optionally deactivate the enemy
    }
}
            
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
}
