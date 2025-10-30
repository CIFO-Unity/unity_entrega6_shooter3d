using UnityEngine;

public class ObjetoDestructible : MonoBehaviour
{
    [SerializeField] private GameObject vfxPrefab;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            // Reproducir sonido
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("DestruirObjeto");

            // Instanciar el VFX en la posición de la calabaza
            if (vfxPrefab != null)
            {
                GameObject vfx = Instantiate(vfxPrefab, transform.position, Quaternion.identity);
                Destroy(vfx, 2.0f); // Destruye el VFX después de 2 segundos
            }

            // Destruir la calabaza
            Destroy(gameObject);

            // Destruir la bala
            Destroy(other.gameObject);
        }
    }
}
