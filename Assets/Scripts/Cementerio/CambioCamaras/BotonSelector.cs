using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectorBotones : MonoBehaviour
{
    [Header("Botones dentro del panel")]
    [SerializeField] private Button[] botones;

    [Header("Botón que debe comenzar seleccionado")]
    [SerializeField] private Button botonPorDefecto;

    [Header("Colores")]
    [SerializeField] private Color colorSeleccionado = Color.yellow;
    [SerializeField] private Color colorNormal = Color.white;

    private void Start()
    {
        // Establecer el botón por defecto en amarillo al iniciar
        if (botonPorDefecto != null)
        {
            SeleccionarBoton(botonPorDefecto);
        }
    }

    public void SeleccionarBoton(Button botonPulsado)
    {
        // Primero ponemos todos en color normal
        foreach (Button b in botones)
        {
            TMP_Text texto = b.GetComponentInChildren<TMP_Text>();
            if (texto != null)
                texto.color = colorNormal;
        }

        // Ahora ponemos el pulsado en amarillo
        TMP_Text textoPulsado = botonPulsado.GetComponentInChildren<TMP_Text>();
        if (textoPulsado != null)
            textoPulsado.color = colorSeleccionado;
    }
}
