using UnityEngine;

/// <summary>
/// Rota lentamente un objeto para simular niebla en movimiento
/// </summary>
public class FogRotator : MonoBehaviour
{
    [Header("Configuración de Rotación")]
    [Tooltip("Velocidad de rotación en el eje Y (horizontal)")]
    public float rotationSpeed = 5f;

    private Vector3 rotationAxis;

    void Start()
    {
        //gira en eje Y
        rotationAxis = Vector3.up;

    }

    void Update()
    {
        // Rota el objeto lentamente
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.World);
    }
}
