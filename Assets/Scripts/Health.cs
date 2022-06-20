using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public bool isAlive = true;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log("dmg");

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            playerMovement.PlayDeathAnimation();
        }
    }
}
