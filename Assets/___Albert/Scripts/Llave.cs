using UnityEngine;

public class Llave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Destruye la llave
            Destroy(gameObject);
        }
    }
}
