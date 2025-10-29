using UnityEngine;
using TMPro;

public class MensajeLlave : MonoBehaviour
{
    private TextMeshProUGUI texto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Obtiene el componente TextMeshProUGUI del mismo objeto
        texto = GetComponent<TextMeshProUGUI>();

        // Asegura que esté oculto al inicio
        if (texto != null)
            texto.gameObject.SetActive(false);

    }

    public void MostrarMensaje(bool tieneLlave)
    {
        if (texto != null)
        {
            texto.text = tieneLlave ? "Tienes acceso." : "Necesitas una llave para acceder.";
            texto.gameObject.SetActive(true);
            Invoke(nameof(OcultarMensaje), 3f);
        }
    }

    private void OcultarMensaje()
    {
        if (texto != null)
            texto.gameObject.SetActive(false);
    }
}