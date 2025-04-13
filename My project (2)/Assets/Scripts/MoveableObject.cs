using UnityEngine;
using UnityEngine.InputSystem;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference jumpHoldAction;
    private Vector3 dir;
    [SerializeField] private float speed = 2f;

    [SerializeField] private float maxJumpForce;
    [SerializeField] private const int maxJumps = 2;

    private float jumpForce;
    private bool higherJump = false;
    private bool isFalling = false;
    private int currentJump = 0;

    private float normalJumpHeight;
    private float highJumpHeight;
    private float maxJumpHeight;

    [SerializeField] Rigidbody rb;
    private bool isJumpRequested;

    private void OnEnable()
    {
        jumpForce = maxJumpForce / 2;

        //normalJumpHeight = (Vector3.up.y) * (jumpForce / 2);
        //highJumpHeight = (Vector3.up.y) * jumpForce;
        //maxJumpHeight = normalJumpHeight;

        dir = new Vector3(0f, 0f, 0f);

        SetInputFunctions();
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(dir.x, 0, dir.y) * Time.fixedDeltaTime * speed, ForceMode.Impulse);

        if (isJumpRequested)
        {
            CheckMaxJumps();

            CheckGrounded();
            
            if (!isFalling)
            {
                Jump();
            }

            isJumpRequested = false;
        }

    }

    private void SetInputFunctions()
    {
        //cada vez que cambia el valor
        moveAction.action.performed += HandleMoveInput;
        //cuando se queda quieto.
        moveAction.action.canceled += HandleMoveInput;

        jumpAction.action.started += HandleJumpInput;

        jumpHoldAction.action.performed += HandleJumpHoldInputPerformed;

        jumpHoldAction.action.canceled += HandleJumpHoldInputCanceled;
    }

    private void CheckMaxJumps()
    {
        if (currentJump == maxJumps)
        {
            isFalling = true;
            currentJump = 0;
        }
    }

    private void CheckGrounded()
    {
        if (this.transform.position.y <= 1f)
        {
            isFalling = false;
            currentJump = 0;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        currentJump++;
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        dir = ctx.ReadValue<Vector2>();
    }

    private void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        isJumpRequested = true;
    }

    private void HandleJumpHoldInputPerformed(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Performed");

        //jumpForce = maxJumpForce;

        //maxJumpHeight = highJumpHeight;

        //higherJump = true;
    }

    private void HandleJumpHoldInputCanceled(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Canceled");
        //jumpForce = maxJumpForce / 2;

        //maxJumpHeight = normalJumpHeight;

        //if (higherJump)
        //    higherJump = false;
    }
}
