using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using Platformer.Gameplay;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;

    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;

    private Health playerHealth;

    private EnemyPatrol enemyPatrol;
    private EnemyFollow enemyFollow;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        enemyFollow = GetComponentInParent<EnemyFollow>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player is in sight?
        if(PlayerInSight())
        {
            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }

        if(enemyPatrol != null) 
        {
            enemyPatrol.enabled = !PlayerInSight();
        }    

         if(enemyFollow != null) 
        {
            enemyFollow.enabled = !PlayerInSight();
        }    
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        if (hit.collider != null && hit.transform.CompareTag("Player")) // Ensure it's the player
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null && hit.transform.CompareTag("Player");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight() && playerHealth != null)
        {

        playerHealth.Decrement(); // Decrement the player's health

        if (!playerHealth.IsAlive) // Check if the player's health has reached 0
        {
            Debug.Log("Player has died.");
            playerHealth.Die();
        }
        else
        {
            Debug.Log($"Player took damage. Remaining health: {playerHealth.currentHP}");
        }
        }
    }
}
