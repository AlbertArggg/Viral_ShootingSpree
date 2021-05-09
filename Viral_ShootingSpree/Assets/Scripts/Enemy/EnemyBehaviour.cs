using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public float DamPoints = 5;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public Animator anim;
    public bool Attacking = false;
    public bool isDead;
    public bool Spidey;

    public float timeBetweenAttacks = 1;
    bool alreadyAttacked;

    public float attackRange = 0.5f;
    public bool playerInAttackRange;
    public bool justSpawned = true;

    public void Awake()
    {
        player = GameObject.Find("player").transform;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(spawnDelay());
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (playerInAttackRange == true && justSpawned == false && isDead == false && Attacking == false)
        {
            AttackPlayer();
        }

        else if (justSpawned == false && isDead == false && Attacking == false)
        {
            ChasePlayer();
        }

        if (isDead == false)
        {
            transform.LookAt(player);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        anim.SetBool("Run", true);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (alreadyAttacked == false)
        {
            anim.SetBool("Run", false);
            Attacking = true;
            int i = Random.Range(0, 4);
            if (i == 0)
            {
                anim.SetTrigger("Attack1");
                anim.SetBool("Run", true);
            }
            else if (i == 1)
            {
                anim.SetTrigger("Attack2");
                anim.SetBool("Run", true);
            }
            else if (i == 2)
            {
                anim.SetTrigger("Attack3");
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetTrigger("Attack4");
                anim.SetBool("Run", true);
            }
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public void inflictDam()
    {
        if (playerInAttackRange)
        {
            player.GetComponent<Health>().subHealth(DamPoints);
        }
        else
        {
            ChasePlayer();
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        Attacking = false;
    }

    IEnumerator spawnDelay()
    {
        if (Spidey == true)
        { 
            yield return new WaitForSeconds(3);
            justSpawned = false;
            anim.SetBool("Run", true);
        }
    }

    public void DeadBitch()
    {
        isDead = true;
        agent.SetDestination(this.gameObject.transform.position);
        anim.SetTrigger("Dead");
    }
}
