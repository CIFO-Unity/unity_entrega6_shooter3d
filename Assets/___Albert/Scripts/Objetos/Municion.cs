using UnityEngine;

public class Municion : MonoBehaviour
{
    [SerializeField]
    private int municionAnadida = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player jugador = other.GetComponent<Player>();

            if (jugador != null)
            {
                // Solo añade munición si no está al máximo
                if (!jugador.MunicionAlMaximo())
                {
                    jugador.AnadirMunicion(municionAnadida);
                    Destroy(gameObject); // Solo se destruye si se ha usado
                }
            }
        }
    }
}
