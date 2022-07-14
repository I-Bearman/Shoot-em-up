using UnityEngine;

public class EnemyMeetHero : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            gameObject.GetComponentInParent<EnemyMovement>().enabled = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
