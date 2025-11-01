using UnityEngine;

public class BossProvisional : MonoBehaviour
{
    [SerializeField] private Player player; // Referencia al jugador

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            // Llamar a DestruirBala() si la bala tiene el script correspondiente
            Bala bala = other.gameObject.GetComponent<Bala>();
            if (bala != null)
                bala.DestruirBala();

            // Reproducir sonido (si tienes SoundManager)
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("DestruirObjeto");

            // Notificar al Player que ha ganado
            if (player != null)
                player.Ganar();

            // Destruir este objeto (boss)
            Destroy(gameObject);
        }
    }
}
