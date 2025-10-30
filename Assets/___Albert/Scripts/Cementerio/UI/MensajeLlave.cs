using UnityEngine;
using UnityEngine.UI;

public class MensajeLlave : MonoBehaviour
{
    [Header("UI Image a controlar")]
    [SerializeField] private Image imageMensaje;

    [Header("Sprites según el estado")]
    [SerializeField] private Sprite spriteTienesLlave;
    [SerializeField] private Sprite spriteNecesitasLlave;

    void Start()
    {
        // Asegura que la imagen esté oculta al inicio
        if (imageMensaje != null)
            imageMensaje.gameObject.SetActive(false);
    }

    public void MostrarMensaje(bool tieneLlave)
    {
        if (imageMensaje != null)
        {
            // Cambia el sprite según el parámetro
            imageMensaje.sprite = tieneLlave ? spriteTienesLlave : spriteNecesitasLlave;

            // Activa la imagen
            imageMensaje.gameObject.SetActive(true);

            // Oculta la imagen después de 3 segundos
            Invoke(nameof(OcultarMensaje), 3f);
        }
    }

    private void OcultarMensaje()
    {
        if (imageMensaje != null)
            imageMensaje.gameObject.SetActive(false);
    }
}
