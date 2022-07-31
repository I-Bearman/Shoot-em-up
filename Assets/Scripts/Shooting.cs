using UnityEngine;
using TMPro;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField] private int ammo;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Transform gunpoint;
    [SerializeField] private float maxDistance;
    [SerializeField] private int damageForce;
    [SerializeField] private float rateOfFire;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip misfireSound;
    [SerializeField] private AudioClip[] boxesSounds = new AudioClip[2];
    private PlayerMovement playerMovement;
    private AudioSource audioSource;
    private Animator animator;
    private bool canShoot = true;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ammoText.text = $"Ammo:{ammo}";
    }
    public void Fire()
    {
        if (canShoot)
        {
            animator.SetTrigger("Fire");
            if (ammo > 0)
            {
                ammo--;
                ammoText.text = $"Ammo:{ammo}";
                audioSource.PlayOneShot(shootSound);
                Ray ray = new Ray(gunpoint.position, playerMovement.LookDirection.normalized * maxDistance);
                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance) && hit.transform.gameObject.layer == 7)
                {
                    hit.transform.gameObject.GetComponent<Health>().TakeDamage(damageForce);
                }
                canShoot = false;
                StartCoroutine(WaitShoot());
            }
            else
            {
                audioSource.PlayOneShot(misfireSound);
                canShoot = false;
                StartCoroutine(WaitShoot());
            }
        }
    }
    public IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            ammo += 50;
            ammoText.text = $"Ammo:{ammo}";
            audioSource.PlayOneShot(boxesSounds[0]);
        }
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            Health health = GetComponent<Health>();
            health.Healing();
            health.RefillHPBar();
            audioSource.PlayOneShot(boxesSounds[1]);
        }
    }
}
