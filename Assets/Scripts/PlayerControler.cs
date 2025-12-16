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

    [Header("Slow Zones")]
    [SerializeField] private float currentSpeedMultiplier = 1f; // 1 = normal
    [SerializeField] private float currentJumpMultiplier  = 1f; // 1 = salto normal

    private int direction = 1;
    private int idSpeed;

    private Rigidbody2D rb;
    private GatherInput input;
    private Animator animator;
    private Transform tr;

    private void Awake()
    {
        input    = GetComponent<GatherInput>();
        rb       = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tr       = transform;

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

    // ================= MOVIMIENTO =================
    private void Move()
    {
        if (rb == null || input == null) return;

        Flip();

        float finalSpeed = speed * currentSpeedMultiplier;

        Vector2 vel = rb.linearVelocity;
        vel.x = finalSpeed * input.ValueX;
        rb.linearVelocity = vel;
    }

    private void Flip()
    {
        if (input == null) return;

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

    // ================= SALTO =================
    private void Jump()
    {
        if (input == null || rb == null) return;

        if (input.IsJumping && isGrounded)
        {
            float finalJumpForce = jumpForce * currentJumpMultiplier;

            Vector2 vel = rb.linearVelocity;
            vel.y = finalJumpForce;
            rb.linearVelocity = vel;

            input.IsJumping = false;
        }
    }

    // ================= GROUND CHECK =================
    private void CheckGround()
    {
        if (lFoot == null || rFoot == null) return;

        RaycastHit2D leftRay  = Physics2D.Raycast(lFoot.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLength, groundLayer);

        isGrounded = leftRay.collider != null || rightRay.collider != null;

        Debug.DrawRay(lFoot.position,  Vector2.down * rayLength, Color.red);
        Debug.DrawRay(rFoot.position, Vector2.down * rayLength, Color.blue);
    }

    // ================= ANIMATOR =================
    private void UpdateAnimator()
    {
        if (animator == null || rb == null) return;

        animator.SetFloat(idSpeed, Mathf.Abs(rb.linearVelocity.x));
    }

    // ================= ZONAS RALENTIZADAS =================
    private void OnTriggerEnter2D(Collider2D other)
    {
        ZonaLenta zonaLenta = other.GetComponent<ZonaLenta>();
        if (zonaLenta != null)
        {
            Debug.Log("Entrando en ZonaLenta: " + other.name);

            currentSpeedMultiplier = zonaLenta.speedMultiplier;
            currentJumpMultiplier  = zonaLenta.jumpMultiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ZonaLenta zonaLenta = other.GetComponent<ZonaLenta>();
        if (zonaLenta != null)
        {
            Debug.Log("Saliendo de ZonaLenta: " + other.name);

            currentSpeedMultiplier = 1f;
            currentJumpMultiplier  = 1f;
        }
    }
}
