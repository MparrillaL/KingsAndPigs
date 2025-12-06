using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls controls;

    [SerializeField]
    private float valueX;

    // Propiedad pública de solo lectura para que otros scripts puedan usar el input
    public float ValueX => valueX;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        if (controls == null)
            controls = new Controls();

        controls.Player.Move.performed += StartMove;
        controls.Player.Move.canceled += EndMove;

        controls.Player.Enable();
        // O controls.Enable(); si quieres habilitar todos los mapas
    }

    private void OnDisable()
    {
        if (controls == null) return;

        controls.Player.Move.performed -= StartMove;
        controls.Player.Move.canceled -= EndMove;

        controls.Player.Disable();
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        // Acción configurada como 1D Axis → se lee como float
        float movementInput = context.ReadValue<float>();
        valueX = movementInput;
    }

    private void EndMove(InputAction.CallbackContext context)
    {
        valueX = 0f;
    }
}
