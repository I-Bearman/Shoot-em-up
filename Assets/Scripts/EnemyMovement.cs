using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public bool canWalk = true;
    public AudioClip[] zombieSounds = new AudioClip[3]; 
    private Collider targetCollider;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;

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

    private void OnTriggerEnter(Collider other)
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
        audioSource.PlayOneShot(zombieSounds[1]);
        animator.SetTrigger("Attack");
    }

    public void GiveDamage()
    {
        
        Physics.SphereCast(transform.position + Vector3.forward*0.5f,1,);
    }
}
