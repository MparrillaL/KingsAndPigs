using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls controls;

    [SerializeField] private float valueX;
    public float ValueX => valueX;

    private bool jumpPressed;
    public bool JumpPressed
    {
        get
        {
            bool temp = jumpPressed;
            jumpPressed = false; // se consume una vez
            return temp;
        }
    }

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        if (controls == null)
            controls = new Controls();

        controls.Player.Move.performed += ctx => valueX = ctx.ReadValue<float>();
        controls.Player.Move.canceled += ctx => valueX = 0f;

        controls.Player.Jump.performed += ctx => jumpPressed = true;

        controls.Player.Enable();
    }

    private void OnDisable()
    {
        if (controls == null) return;

        controls.Player.Move.performed -= ctx => valueX = ctx.ReadValue<float>();
        controls.Player.Move.canceled -= ctx => valueX = 0f;

        controls.Player.Jump.performed -= ctx => jumpPressed = true;

        controls.Player.Disable();
    }
}
