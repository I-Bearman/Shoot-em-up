using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
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
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Attack");
    }
}
