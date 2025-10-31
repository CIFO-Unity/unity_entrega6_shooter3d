using UnityEngine;

public class EnemigoProvisional : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            // Llamar a DestruirBala() si la bala tiene el script correspondiente
            Bala bala = other.gameObject.GetComponent<Bala>();
            if (bala != null)
            {
                bala.DestruirBala();
            }

            // Reproducir sonido (si tienes SoundManager)
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("DestruirEnemigo");

            // Notificar a la clase General que se ha destruido un enemigo
            General.ActualizarNumeroEnemigosMuertos();

            // Destruir este enemigo
            Destroy(gameObject);
        }
    }
}
