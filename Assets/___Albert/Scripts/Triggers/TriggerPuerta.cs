using UnityEngine;

public class TriggerPuerta : MonoBehaviour
{
    [Header("Número de puerta que abre este trigger")]
    [SerializeField]
    private int numLlave = 1;

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
