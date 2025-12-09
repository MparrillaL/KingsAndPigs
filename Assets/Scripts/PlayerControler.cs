using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private int direction = 1;

    private Rigidbody2D m_rigidbody2D;
    private GatherInput m_gatherinput;
    private Transform m_transform;

    private void Awake()
    {
        m_gatherinput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        if (m_rigidbody2D == null || m_gatherinput == null) return;
        Flip();
        Vector2 vel = m_rigidbody2D.linearVelocity; 
        vel.x = speed * m_gatherinput.ValueX;
        m_rigidbody2D.linearVelocity = vel; 
    }

    private void FixedUpdate()
    {        Move();
    }

    private void Flip()
    {
        if(m_gatherinput.ValueX * direction < 0)
        {
            m_transform.localScale = new Vector3(-m_transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }
}
