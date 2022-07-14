using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Image HPBar;
    public int currentHealth;
    public bool isAlive = true;
    private Animator animator;
    private new Collider collider;
    private AudioSource audioSource;
    private EnemyMovement enemyMovement;
    private int maxHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        TryGetComponent(out enemyMovement);
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        maxHealth = currentHealth;
        if (enemyMovement)
        {
            StartCoroutine(Screaming(enemyMovement));
        }
    }

    public void TakeDamage(int dmg)
    {
        if (enemyMovement)
        {
            audioSource.PlayOneShot(enemyMovement.zombieSounds[2]);
        }
        animator.SetTrigger("TakeDamage");
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            Death();
        }
        if (!enemyMovement)
        {
            RefillHPBar();
        }
    }

    public void RefillHPBar()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        HPBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        if (enemyMovement)
        {
            gameObject.layer = 0;
            enemyMovement.enabled = false;
            int newScore = ++GameData.Instance.currentScore;
            GameData.Instance.scoreText.text = $"Score: {newScore}";
        }
        else if (TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.enabled = false;
        }
        collider.enabled = false;
    }

    IEnumerator Screaming(EnemyMovement enemyMovement)
    {
        Again:
        yield return new WaitForSeconds(Random.Range(5f,30f));
        if(isAlive)
        {
            audioSource.PlayOneShot(enemyMovement.zombieSounds[0]);
            goto Again;
        }
    }
}
