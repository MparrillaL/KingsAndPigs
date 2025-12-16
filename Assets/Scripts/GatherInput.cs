using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls controls;

    [SerializeField]
    private float valueX;
    public float ValueX => valueX;

    // Se usa en PlayerControler para saber si se ha pedido un salto
    public bool IsJumping { get; set; }

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        if (controls == null)
            controls = new Controls();

        // Movimiento
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled  += OnMoveCanceled;

        // Salto
        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Jump.canceled  += OnJumpCanceled;

        controls.Player.Enable();
    }

    private void OnDisable()
    {
        if (controls == null) return;

        controls.Player.Move.performed -= OnMovePerformed;
        controls.Player.Move.canceled  -= OnMoveCanceled;

        controls.Player.Jump.performed -= OnJumpPerformed;
        controls.Player.Jump.canceled  -= OnJumpCanceled;

        controls.Player.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Acción configurada como 1D Axis → float
        valueX = context.ReadValue<float>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        valueX = 0f;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        IsJumping = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        // Cuando suelta el botón, dejamos de "pedir salto"
        IsJumping = false;
    }
}
