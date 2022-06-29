using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public bool isAlive = true;
    private Animator animator;
    private new Collider collider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            Death();
        }
    }
    private void Death()
    {
        if (TryGetComponent(out EnemyMovement enemyMovement))
        {
            enemyMovement.enabled = false;
        }
        else if (TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.enabled = false;
        }
        collider.enabled = false;
        animator.SetTrigger("Death");
    }
}
