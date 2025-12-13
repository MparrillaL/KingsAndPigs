using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Ground Check")]
    [SerializeField] private Transform lFoot;
    [SerializeField] private Transform rFoot;
    [SerializeField] private float rayLength = 0.15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;

    private int direction = 1;
    private int idSpeed;

    private Rigidbody2D rb;
    private GatherInput input;
    private Animator animator;
    private Transform tr;

    private void Awake()
    {
        input = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tr = transform;

        if (animator != null)
            idSpeed = Animator.StringToHash("Speed");
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        Jump();
    }

    private void Update()
    {
        UpdateAnimator();
    }

    // ================= MOVEMENT =================
    private void Move()
    {
        if (rb == null || input == null) return;

        Flip();

        Vector2 vel = rb.linearVelocity;
        vel.x = input.ValueX * speed;
        rb.linearVelocity = vel;
    }

    private void Flip()
    {
        if (input.ValueX * direction < 0)
        {
            tr.localScale = new Vector3(
                -tr.localScale.x,
                tr.localScale.y,
                tr.localScale.z
            );
            direction *= -1;
        }
    }

    // ================= JUMP =================
    private void Jump()
    {
        if (input.JumpPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // ================= GROUND CHECK =================
    private void CheckGround()
    {
        if (lFoot == null || rFoot == null) return;

        RaycastHit2D leftRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLength, groundLayer);

        isGrounded = leftRay.collider != null || rightRay.collider != null;

        // Dibujar rayos para depuración
        Debug.DrawRay(lFoot.position, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(rFoot.position, Vector2.down * rayLength, Color.blue);
    }

    // ================= ANIMATOR =================
    private void UpdateAnimator()
    {
        if (animator == null || rb == null) return;

        animator.SetFloat(idSpeed, Mathf.Abs(rb.linearVelocity.x));
    }
}
