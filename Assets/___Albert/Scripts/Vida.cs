using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField]
    private int vidaAnadida = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player jugador = other.GetComponent<Player>();

            if (jugador != null)
            {
                // Solo añade vida si no está al máximo
                if (!jugador.VidaAlMaximo())
                {
                    jugador.AnadirVida(vidaAnadida);
                    Destroy(gameObject); // Solo se destruye si se ha usado
                }
            }
        }
    }
}
