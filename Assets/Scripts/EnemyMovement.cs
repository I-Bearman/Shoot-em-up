using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public bool canWalk = true;
    public AudioClip[] zombieSounds = new AudioClip[3];
    public int damageForce;
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
        audioSource.PlayOneShot(zombieSounds[1]);
        animator.SetTrigger("Attack");
    }

    public void GiveDamage()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.rotation.eulerAngles);
        if (Physics.SphereCast(ray, 1, out RaycastHit raycastHit, 0.5f) && raycastHit.transform.gameObject.layer == 6)
        {
            raycastHit.transform.gameObject.GetComponent<Health>().TakeDamage(damageForce);
            Debug.Log("135");
        }
    }
}
