using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpForce = 10f;   // ← ESTO saldrá en el Inspector

    private int direction = 1;
    private int idSpeed;

    private Rigidbody2D m_rigidbody2D;
    private GatherInput m_gatherinput;
    private Transform m_transform;
    private Animator m_animator;

    private void Awake()
    {
        m_gatherinput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        if (m_animator != null)
        {
            idSpeed = Animator.StringToHash("Speed");
        }
    }

    private void Update()
    {
        SetAnimatorValues();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (m_rigidbody2D == null || m_gatherinput == null) return;

        Flip();

        Vector2 vel = m_rigidbody2D.linearVelocity;    // <- velocity, no linearVelocity
        vel.x = speed * m_gatherinput.ValueX;
        m_rigidbody2D.linearVelocity = vel;
    }

    private void SetAnimatorValues()
    {
        if (m_animator == null || m_rigidbody2D == null) return;

        // velocity.x, no linearVelocityX
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigidbody2D.linearVelocity.x));
    }

    private void Flip()
    {
        if (m_gatherinput == null) return;

        if (m_gatherinput.ValueX * direction < 0)
        {
            m_transform.localScale = new Vector3(-m_transform.localScale.x, m_transform.localScale.y, m_transform.localScale.z);
            direction *= -1;
        }
    }

    private void Jump()
    {
        // OJO: esto asume que en tu GatherInput tienes una bandera IsJumping
        if (m_gatherinput == null || m_rigidbody2D == null) return;

        if (m_gatherinput.IsJumping)
        {
            Vector2 vel = m_rigidbody2D.linearVelocity;
            vel.y = jumpForce;          // ← aquí usamos jumpForce
            m_rigidbody2D.linearVelocity = vel;

            m_gatherinput.IsJumping = false;   // consumimos el salto
        }
    }
}
