using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls controls;

    // Input horizontal (−1 a 1)
    [SerializeField]
    private float valueX;
    public float ValueX => valueX;

    // Input de salto
    [SerializeField]
    private bool _isJumping;
    public bool IsJumping
    {
        get => _isJumping;
        set => _isJumping = value;
    }

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        if (controls == null)
            controls = new Controls();

        // Movimiento
        controls.Player.Move.performed += StartMove;
        controls.Player.Move.canceled += EndMove;

        // Salto
        controls.Player.Jump.performed += StartJump;
        controls.Player.Jump.canceled += StopJump;

        controls.Player.Enable();
        // O controls.Enable(); si quieres habilitar todos los mapas
    }

    private void OnDisable()
    {
        if (controls == null) return;

        // Quitar suscripciones
        controls.Player.Move.performed -= StartMove;
        controls.Player.Move.canceled -= EndMove;

        controls.Player.Jump.performed -= StartJump;
        controls.Player.Jump.canceled -= StopJump;

        controls.Player.Disable();
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        // Acción configurada como 1D Axis → se lee como float
        valueX = context.ReadValue<float>();
    }

    private void EndMove(InputAction.CallbackContext context)
    {
        valueX = 0f;
    }

    private void StartJump(InputAction.CallbackContext context)
    {
        // Marcamos que se ha pedido salto
        IsJumping = true;
    }

    private void StopJump(InputAction.CallbackContext context)
    {
        // Soltó el botón de salto
        IsJumping = false;
    }
}
