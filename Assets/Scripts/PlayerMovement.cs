using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private int screenWidth;
    private int screenHeight;

    private void Awake()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    public void Move(float x, float y)
    {
        Vector3 newPosition = new Vector3(transform.position.x + x * speed, transform.position.y, transform.position.z + y * speed);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
    }

    public void Turn(float xMouse, float yMouse)
    {
        float dx = xMouse - screenWidth * 0.5f;
        float dy = yMouse - screenHeight * 0.5f;
        transform.LookAt(new Vector3(dx, 0, dy));
    }

    public void Fire()
    {
        
    }
}
