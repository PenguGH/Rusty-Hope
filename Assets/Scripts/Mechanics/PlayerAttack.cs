using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
     [SerializeField] private GameObject[] plasma;
    private Animator anim;
    private PlayerController playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
            Attack();
        
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        //pool plasma balls

        plasma[FindPlasma()].transform.position = firePoint.position;
        plasma[FindPlasma()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindPlasma()
    {
        for(int i = 0; i < plasma.Length; i++)
        {
            if(!plasma[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}

}
