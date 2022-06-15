using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis(GameData.HORIZONTAL_AXIS);
        float y = Input.GetAxis(GameData.VERTICAL_AXIS);
        playerMovement.Move(x, y);

        float xMouse = Input.mousePosition.x;
        float yMouse = Input.mousePosition.y;
        playerMovement.Turn(xMouse, yMouse);

        playerMovement.RunAnimationController();

        if(Input.GetMouseButtonDown(0))
        {
            playerMovement.Fire();
        }
    }
}
