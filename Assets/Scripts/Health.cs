using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [SerializeField] Image HPBar;
    [SerializeField] int pointsForKill;
    public int currentHealth;
    public bool isAlive = true;
    [SerializeField] GameObject DeathPanel;
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
        HPBar.fillAmount = (float)currentHealth / maxHealth;
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        gameObject.layer = 0;
        if (enemyMovement)
        {
            enemyMovement.enabled = false;
            GetComponent<NavMeshAgent>().isStopped = true;
            GameData.Instance.currentScore += pointsForKill;
            GameData.Instance.scoreText.text = $"Score: {GameData.Instance.currentScore}";
        }
        else if (TryGetComponent(out PlayerInput playerInput))
        {
            playerInput.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(DeathPanelActivation());
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
    IEnumerator DeathPanelActivation()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
        DeathPanel.SetActive(true);
        QuickMenu.Instance.DeathScore();
        GameData.Instance.SaveData();
    }
}
