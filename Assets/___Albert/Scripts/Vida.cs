using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField]
    private int vidaAnadida = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Obtiene el componente Player del objeto que colisiona
            Player jugador = other.GetComponent<Player>();
            
            if (jugador != null)
            {
                // Llama a AñadirVida con la cantidad definida
                jugador.AnadirVida(vidaAnadida);
            }

            // Destruye el objeto de vida
            Destroy(gameObject);
        }
    }
}
