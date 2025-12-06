using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] 
    private float speed;       // ahora sí la podrás cambiar en el Inspector

    private Rigidbody2D m_rigidbody2D;
    private GatherInput m_gatherinput;
    private Transform m_transform;

    private void Awake()
    {
        // Coger referencias a los componentes del mismo GameObject
        m_gatherinput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (m_rigidbody2D == null || m_gatherinput == null) return;

        // Mantener la velocidad vertical actual
        Vector2 vel = m_rigidbody2D.linearVelocity; // o .velocity según tu versión
        vel.x = speed * m_gatherinput.ValueX;
        m_rigidbody2D.linearVelocity = vel; // o .velocity = vel;
    }
}
