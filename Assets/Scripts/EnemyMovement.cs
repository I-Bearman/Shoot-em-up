using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public bool canWalk = true;
    private Collider targetCollider;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetCollider = target.gameObject.GetComponent<Collider>();
        animator = GetComponent<Animator>();
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

    private void OnTriggerStay(Collider other)
    {
        if (other == targetCollider)
        {
            animator.SetBool("Walk", false);
            Attack();
        }
    }
    private void Attack()
    {
        //navMeshAgent.isStopped = true;
        animator.SetTrigger("Attack");
    }
}
