using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioSource runSound;
    private Vector3 lookDirection;
    private Rigidbody rb;
    private Animator animator;

    public Vector3 LookDirection => lookDirection;

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
        float dx = xMouse - Screen.width * 0.5f;
        float dy = yMouse - Screen.height * 0.5f;
        lookDirection = new Vector3(dx, 0, dy);
        transform.LookAt(lookDirection);
    }

    public void RunAnimationController()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            animator.SetBool("Run", true);
            if (!runSound.isPlaying)
            {
                runSound.Play();
            }
            float angle = Vector3.SignedAngle(lookDirection, rb.velocity, Vector3.up);
            angle *= Mathf.Deg2Rad;
            animator.SetFloat("Speed for_back", rb.velocity.magnitude * Mathf.Cos(angle));
            animator.SetFloat("Speed right_left", rb.velocity.magnitude * Mathf.Sin(angle));
        }
        else
        {
            runSound.Pause();
            animator.SetBool("Run", false);
            animator.SetFloat("Speed for_back", 0);
            animator.SetFloat("Speed right_left", 0);
        }
    }
}
