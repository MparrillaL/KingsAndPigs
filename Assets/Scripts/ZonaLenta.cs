using UnityEngine;

public class ZonaLenta : MonoBehaviour
{
    [Header("Multiplicadores")]
    [Range(0f, 1f)]
    public float speedMultiplier = 0.5f;   // 0.5 = mitad de velocidad

    [Range(0f, 1f)]
    public float jumpMultiplier = 0.6f;    // 0.6 = 60% de la fuerza de salto
}
