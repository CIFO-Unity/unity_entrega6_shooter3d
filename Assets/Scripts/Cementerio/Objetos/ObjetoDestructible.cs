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

            // Instanciar el VFX en la posición del objeto
            if (vfxPrefab != null)
            {
                GameObject vfx = Instantiate(vfxPrefab, transform.position, Quaternion.identity);
                Destroy(vfx, 2.0f); // Destruye el VFX después de 2 segundos
            }

            // Llamar a DestruirBala si el objeto tiene el script Bala
            Bala bala = other.gameObject.GetComponent<Bala>();
            if (bala != null)
                bala.DestruirBala();

            // Destruir el objeto destructible
            Destroy(gameObject);
        }
    }
}
