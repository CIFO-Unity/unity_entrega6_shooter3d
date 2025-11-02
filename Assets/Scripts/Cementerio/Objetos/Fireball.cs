using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Trail")]
    [Tooltip("Prefab del ParticleSystem que hará la estela/trail del proyectil. Asignar en el prefab Fireball si quieres que la bola lleve partículas.")]
    [SerializeField]
    private GameObject trailPrefab;

    [SerializeField]
    private float maxLifetime = 6f;

    [Header("Impact")]
    [SerializeField]
    private GameObject impactPrefab;

    [SerializeField]
    private string impactSound = "FireballImpact";

    [SerializeField]
    private float impactLifetime = 2f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Si hay un prefab de trail asignado, instanciarlo como child para que siga al proyectil
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
        Destroy(gameObject, maxLifetime);
    }

    private void FixedUpdate()
    {
        // Girar el proyectil para alinear su forward con la velocidad si existe Rigidbody
        if (rb != null && rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            transform.forward = rb.linearVelocity.normalized;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Obtener punto de contacto
        Vector3 hitPos = transform.position;
        Quaternion hitRot = Quaternion.identity;
        if (collision.contactCount > 0)
        {
            ContactPoint contact = collision.GetContact(0);
            hitPos = contact.point;
            hitRot = Quaternion.LookRotation(contact.normal);
        }

        // Instanciar efecto de impacto si existe
        if (impactPrefab != null)
        {
            GameObject imp = Instantiate(impactPrefab, hitPos, hitRot);
            var ps = imp.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                main.simulationSpace = ParticleSystemSimulationSpace.World;
                ps.Play();
            }
            Destroy(imp, impactLifetime);
        }

        // Reproducir sonido de impacto si existe SoundManager
        if (SoundManager.Instance != null && !string.IsNullOrEmpty(impactSound))
        {
            SoundManager.Instance.PlaySound(impactSound);
        }

        // Destruir el proyectil
        Destroy(gameObject);
    }
}
