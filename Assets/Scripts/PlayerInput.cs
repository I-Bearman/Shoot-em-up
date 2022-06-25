using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTex;
    private PlayerMovement playerMovement;
    private Shooting shooting;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        shooting = GetComponent<Shooting>();
        Cursor.SetCursor(cursorTex, new Vector2(25,25), CursorMode.Auto);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuickMenu.Instance.OnPause();
        }

        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            float xMouse = Input.mousePosition.x;
            float yMouse = Input.mousePosition.y;
            shooting.Fire(xMouse,yMouse);
        }

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
    }
}
