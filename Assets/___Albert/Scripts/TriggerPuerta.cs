using TMPro;
using UnityEngine;

public class TriggerPuerta : MonoBehaviour
{
    [SerializeField]
    private GameObject puerta;

    [SerializeField]
    private TextMeshProUGUI textLlave;

    [SerializeField]
    private int numLlave = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Player jugador = other.GetComponent<Player>();

            if (jugador != null)
            {
                // Obtiene el componente MensajeLlave del mismo objeto que el TextMeshProUGUI
                MensajeLlave mensaje = textLlave.GetComponent<MensajeLlave>();

                if (mensaje != null)
                {
                    // Llama a MostrarMensaje según si tiene la llave
                    mensaje.MostrarMensaje(jugador.TieneLlave(numLlave));
                }
                    
                // Comprueba si el jugador tiene la llave correspondiente
                if (jugador.TieneLlave(numLlave))
                {
                    // Abrir puerta asociada a esta llave
                    if (puerta != null)
                    {
                        Animator animatorPuerta = puerta.GetComponent<Animator>();
                        animatorPuerta.SetBool("estadoPuertas", true);

                        // Destrucción del trigger
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
