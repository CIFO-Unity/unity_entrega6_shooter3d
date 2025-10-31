using UnityEngine;

public class EnemigoProvisional : MonoBehaviour
{
    [SerializeField] private EnemiesManager enemiesManager; // Referencia al manager de enemigos

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

            // Notificar a EnemiesManager que se ha destruido un enemigo
            if (enemiesManager != null)
                enemiesManager.ActualizarNumeroEnemigosMuertos();

            // Destruir este enemigo
            Destroy(gameObject);
        }
    }
}

