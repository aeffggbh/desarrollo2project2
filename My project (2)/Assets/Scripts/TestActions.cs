using UnityEngine;
using UnityEngine.InputSystem;

public class TestActions : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;

    private void OnEnable()
    {
        //en el momento que entra en la accion
        moveAction.action.started += HandleMoveInput;
        //cada vez que cambia el valor
        moveAction.action.performed += HandleMoveInput;
        //cuando se queda quieto.
        moveAction.action.canceled += HandleMoveInput;
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        string state = "";

        if (ctx.performed)
            state = "permormed";
        if (ctx.started)
            state = "started";
        if (ctx.canceled)
            state = "canceles";


        Debug.Log(state);
    }
}
