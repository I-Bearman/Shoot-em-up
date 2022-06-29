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
        navMeshAgent.SetDestination(target.position);
        if(!navMeshAgent.isStopped)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == targetCollider)
        {
            Attack();
        }
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
