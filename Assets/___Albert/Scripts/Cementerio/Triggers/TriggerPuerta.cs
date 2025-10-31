using UnityEngine;
using UnityEngine.UI;

public class TriggerPuerta : MonoBehaviour
{
    [Header("Número de puerta que abre este trigger")]
    [SerializeField]
    private int numLlave = 1;

    [SerializeField]
    private Image imageLlave;

    [Header("Puerta que abre este trigger")]
    [SerializeField]
    private GameObject puerta;

    [Header("UI Mensaje Llave")]
    [SerializeField]
    private MensajeLlave mensajeLlave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player jugador = other.GetComponent<Player>();

            if (jugador != null)
            {
                // Llama a MostrarMensaje usando la referencia serializable
                if (mensajeLlave != null)
                    mensajeLlave.MostrarMensaje(jugador.TieneLlave(numLlave));

                // Abrir la puerta si tiene la llave
                if (jugador.TieneLlave(numLlave))
                {
                    if (puerta != null)
                    {
                        Animator animatorPuerta = puerta.GetComponent<Animator>();
                        if (animatorPuerta != null)
                            animatorPuerta.SetBool("estadoPuertas", true);

                        if (SoundManager.Instance != null)
                            SoundManager.Instance.PlaySound("AbrirPuerta");

                        // Cambia opacidad de la imagen de la llave
                        imageLlave.color = new Color(49f / 255f, 43f / 255f, 43f / 255f, 1f);

                        Destroy(this.gameObject); // Destruye el trigger
                    }
                }
                else
                {
                    if (SoundManager.Instance != null)
                        SoundManager.Instance.PlaySound("NecesitasLlave");
                }
            }
        }
    }
}
