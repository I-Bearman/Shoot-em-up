using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    [SerializeField] private int ammo;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Transform gunpoint;
    [SerializeField] private float maxDistance;
    [SerializeField] private int damageForce;
    private PlayerMovement playerMovement;
    private Animator animator;

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
        animator.SetTrigger("Fire");
        if (ammo > 0)
        {
            ammo--;
            ammoText.text = $"Ammo:{ammo}";
            Ray ray = new Ray(gunpoint.position,playerMovement.lookDirection.normalized * maxDistance);
            Debug.DrawRay(gunpoint.position,playerMovement.lookDirection.normalized * maxDistance, Color.red, 50);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance) && hit.transform.gameObject.layer ==7)
            {
                Debug.Log("hit");
                hit.transform.gameObject.GetComponent<Health>().TakeDamage(damageForce);
            }
        }

    }

}
