using UnityEngine;

public class Llave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Obtiene el componente Player del objeto que colisiona
            Player jugador = other.GetComponent<Player>();

            if (jugador != null)
            {
                // Llama a la función ObtenerLlave del Player
                jugador.ObtenerLlave();
            }

            // Destruye la llave
            Destroy(gameObject);
        }
    }
}
