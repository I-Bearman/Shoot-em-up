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
    private PlayerMovement playerMovement;
    private Animator animator;
    private bool canShoot = true;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
                //shootSound.Play();
                Ray ray = new Ray(gunpoint.position, playerMovement.lookDirection.normalized * maxDistance);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, maxDistance) && hit.transform.gameObject.layer == 7)
                {
                    hit.transform.gameObject.GetComponent<Health>().TakeDamage(damageForce);
                }
                canShoot = false;
                StartCoroutine(WaitShoot());
            }
            else
            {
                //misfireSound.Play();
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
}
