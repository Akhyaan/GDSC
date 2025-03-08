using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint, parent;
    [SerializeField] GameObject explosion,fireBall;


    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = 1000;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();

        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        GameObject temp = Instantiate(fireBall, firePoint.position, firePoint.rotation);
        StartCoroutine(temp.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x), explosion));
    }

    // //to apply pooling to fireballs
    // private int FindFireball()
    // {
    //     for (int i = 0; i < fireballs.Length; i++)
    //     {
    //         if (!fireballs[i].activeInHierarchy)
    //         { return i; }
    //     }
    //     return 0;
    // }


}
