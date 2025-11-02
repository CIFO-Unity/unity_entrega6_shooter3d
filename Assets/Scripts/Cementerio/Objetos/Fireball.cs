using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Trail")]
    [SerializeField]
    private GameObject trailPrefab;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Si hay un prefab de trail asignado, instanciarlo como child para que siga al proyectil (humo, fuego etc)
        if (trailPrefab != null)
        {
            GameObject trail = Instantiate(trailPrefab, transform);
            trail.transform.localPosition = Vector3.zero;
            trail.transform.localRotation = Quaternion.identity;
            var psTrail = trail.GetComponent<ParticleSystem>();
            if (psTrail != null)
            {
                psTrail.Play();
            }
        }
        else
        {
            // Si no hay prefab de trail, arrancar cualquier ParticleSystem hijo existente
            var ps = GetComponentInChildren<ParticleSystem>(true);
            if (ps != null)
            {
                ps.Play();
            }
        }

        // Seguridad: destruir tras tiempo
        Destroy(gameObject, 3.0f);
    }

    private void FixedUpdate()
    {
        // Girar el proyectil para alinear su forward con la velocidad si existe Rigidbody
        if (rb != null && rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            transform.forward = rb.linearVelocity.normalized;
        }
    }
}
