using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("Player Detection")]
    private Transform player;
    [SerializeField] private float followRange;

    private bool isFollowingPlayer;

    private void Awake()
    {
        initScale = enemy.localScale;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (IsPlayerWithinFollowRange())
        {
            isFollowingPlayer = true;
            FollowPlayer();
        }
        else
        {
            isFollowingPlayer = false;
            Patrol();
        }
    }

    private void Patrol()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);

        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
            idleTimer = 0;
        }
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        // Make the enemy face the direction of movement
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
                                     enemy.position.y, enemy.position.z);
    }

    private bool IsPlayerWithinFollowRange()
    {
        float distanceToPlayer = Vector2.Distance(player.position, enemy.position);
        return distanceToPlayer <= followRange;
    }

    private void FollowPlayer()
    {
        anim.SetBool("moving", true);

        // Move towards the player
        enemy.position = Vector2.MoveTowards(enemy.position, player.position, speed * Time.deltaTime);

        // Make the enemy face the player
        int direction = player.position.x > enemy.position.x ? 1 : -1;
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);
    }
}

