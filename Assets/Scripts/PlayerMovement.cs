using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public void Move(float x, float y)
    {
        Vector3 newPosition = new Vector3(transform.position.x + x * speed, 0, transform.position.y + y * speed);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
    }
}
