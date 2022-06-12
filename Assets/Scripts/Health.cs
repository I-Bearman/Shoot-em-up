using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public bool isAlive = true;
    private ActorView actorView;

    private void Awake()
    {
        actorView = GetComponent<ActorView>();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            actorView.PlayDeathAnimation();
        }
    }
}
