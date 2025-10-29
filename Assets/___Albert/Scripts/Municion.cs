using UnityEngine;

public class Municion : MonoBehaviour
{
    [SerializeField]
    private int municionAnadida = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Obtiene el componente Player del objeto que colisiona
            Player jugador = other.GetComponent<Player>();
            
            if (jugador != null)
            {
                // Llama a AñadirMunicion con la cantidad definida
                jugador.AnadirMunicion(municionAnadida);
            }

            // Destruye el objeto de munición
            Destroy(gameObject);
        }
    }
}
