using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private AudioClip[] zombieSounds = new AudioClip[3];
    [SerializeField] private int damageForce;
    private Collider targetCollider;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;
    private bool canPunch = false;

    public AudioClip[] ZombieSounds => zombieSounds;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetCollider = target.gameObject.GetComponent<Collider>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") || animator.GetCurrentAnimatorStateInfo(0).IsName("TakeDamage") || animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.SetDestination(transform.position);
        }
        else
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);
        }

        if(!navMeshAgent.isStopped)
        {
            animator.SetBool("Walk", true);
        }
    }

    public void Prepare(Transform hero, List<AudioClip> enemySounds)
    {
        target = hero;
        zombieSounds[0] = enemySounds[Random.Range(0, 4)];
        zombieSounds[1] = enemySounds[Random.Range(4, 9)];
        zombieSounds[2] = enemySounds[Random.Range(9, 13)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == targetCollider)
        {
            animator.SetBool("Walk", false);
            Attack();
            canPunch = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == targetCollider)
        {
            canPunch = false;
        }
    }
    private void Attack()
    {
        audioSource.PlayOneShot(zombieSounds[1]);
        animator.SetTrigger("Attack");
    }
    public void AttackAgain() //if the player still is in trigger
    {
        if (canPunch)
            Attack();
    }

    public void GiveDamage()
    {
        RaycastHit[] raycastHit = Physics.SphereCastAll(transform.position + Vector3.up, 0.8f, transform.forward + Vector3.up, 1);
        for (int i = 0; i < raycastHit.Length; i++)
        {
            if (raycastHit[i].transform.gameObject.layer == 6)
            {
                raycastHit[i].transform.gameObject.GetComponent<Health>().TakeDamage(damageForce);
                break;
            }
        }
    }
}
