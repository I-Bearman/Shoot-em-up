using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 lookDirection;
    private Rigidbody rb;
    private Animator animator;
    private int screenWidth;
    private int screenHeight;

    private void Awake()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(float x, float y)
    {
        Vector3 movement = new Vector3(x, 0, y);
        rb.velocity = movement.normalized * speed;
    }

    public void Turn(float xMouse, float yMouse)
    {
        float dx = xMouse - screenWidth * 0.5f;
        float dy = yMouse - screenHeight * 0.5f;
        lookDirection = new Vector3(dx, 0, dy);
        transform.LookAt(lookDirection);
    }

    public void RunAnimationController()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            float angle = Vector3.SignedAngle(lookDirection, rb.velocity, Vector3.up);

            if (Mathf.Abs(angle) <= 45)
            {
                animator.SetFloat("Speed for_back", rb.velocity.magnitude);
            }
            else if (Mathf.Abs(angle) >= 135)
            {
                animator.SetFloat("Speed for_back", -rb.velocity.magnitude);
            }
            else if (angle > 45)
            {
                animator.SetFloat("Speed right_left", rb.velocity.magnitude);
            }
            else
            {
                animator.SetFloat("Speed right_left", -rb.velocity.magnitude);
            }
        }
        else
        {
            animator.SetFloat("Speed for_back", 0);
            animator.SetFloat("Speed right_left", 0);
        }
    }

    public void Fire()
    {
        animator.SetTrigger("Fire");
    }
}
