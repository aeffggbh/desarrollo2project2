using UnityEngine;
using UnityEngine.InputSystem;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    private Vector3 dir;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] Rigidbody rb;
    private bool isJumpRequested;

    private void OnEnable()
    {
        dir = new Vector3(0f, 0f, 0f);

        //en el momento que entra en la accion
        moveAction.action.started += HandleMoveInput;
        //cada vez que cambia el valor
        moveAction.action.performed += HandleMoveInput;
        //cuando se queda quieto.
        moveAction.action.canceled += HandleMoveInput;

        jumpAction.action.started += HandleJumpInput;
    }

    //private void Update()
    //{
    //    this.transform.Translate(dir * speed * Time.deltaTime);
    //    // rider4 es mej or

    //}

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(dir.x, 0, dir.y) * Time.fixedDeltaTime * speed, ForceMode.Impulse);

        if (isJumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumpRequested = false;
        }
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        dir = ctx.ReadValue<Vector2>();
    }

    private void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        isJumpRequested = true;
    }
}
