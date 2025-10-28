using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // Para TextMeshPro

/// <summary>
/// Maneja el efecto visual cuando el mouse pasa sobre un botón (Hover)
/// Cambia el tamaño de fuente y la intensidad/color del texto hijo
/// </summary>
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Referencia al Texto")]
    [Tooltip("El componente de texto hijo del botón. Si se deja vacío, se busca automáticamente")]
    public TextMeshProUGUI textoBoton;

    [Header("Configuración Normal (sin hover)")]
    [Tooltip("Tamaño de fuente normal")]
    public float tamañoFuenteNormal = 24f;
    
    [Tooltip("Color normal del texto")]
    public Color colorNormal = Color.white;
    
    [Tooltip("Intensidad/Alpha normal (0-1)")]
    [Range(0f, 1f)]
    public float intensidadNormal = 1f;

    [Header("Configuración Hover (al pasar el mouse)")]
    [Tooltip("Tamaño de fuente al hacer hover")]
    public float tamañoFuenteHover = 28f;
    
    [Tooltip("Color al hacer hover")]
    public Color colorHover = Color.yellow;
    
    [Tooltip("Intensidad/Alpha al hacer hover (0-1)")]
    [Range(0f, 1f)]
    public float intensidadHover = 1f;

    [Header("Animación")]
    [Tooltip("Velocidad de transición")]
    public float velocidadTransicion = 10f;
    
    [Tooltip("Usar transición suave (lerp)")]
    public bool transicionSuave = true;

    private float tamañoObjetivo;
    private Color colorObjetivo;

    void Start()
    {
        // Si no se asignó manualmente, buscar el texto hijo
        if (textoBoton == null)
        {
            textoBoton = GetComponentInChildren<TextMeshProUGUI>();
        }

        // Guardar valores iniciales
        if (textoBoton != null)
        {
            tamañoFuenteNormal = textoBoton.fontSize;
            colorNormal = textoBoton.color;
            intensidadNormal = colorNormal.a;
            
            tamañoObjetivo = tamañoFuenteNormal;
            colorObjetivo = colorNormal;
        }
    }

    void Update()
    {
        // Transición suave si está activada
        if (transicionSuave && textoBoton != null)
        {
            textoBoton.fontSize = Mathf.Lerp(textoBoton.fontSize, tamañoObjetivo, Time.deltaTime * velocidadTransicion);
            textoBoton.color = Color.Lerp(textoBoton.color, colorObjetivo, Time.deltaTime * velocidadTransicion);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textoBoton == null) return;

        // Aplicar efecto hover
        tamañoObjetivo = tamañoFuenteHover;
        colorObjetivo = new Color(colorHover.r, colorHover.g, colorHover.b, intensidadHover);

        if (!transicionSuave)
        {
            textoBoton.fontSize = tamañoObjetivo;
            textoBoton.color = colorObjetivo;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (textoBoton == null) return;

        // Volver al estado normal
        tamañoObjetivo = tamañoFuenteNormal;
        colorObjetivo = new Color(colorNormal.r, colorNormal.g, colorNormal.b, intensidadNormal);

        if (!transicionSuave)
        {
            textoBoton.fontSize = tamañoObjetivo;
            textoBoton.color = colorObjetivo;
        }
    }

    public void ResetearEstado()
    {
        if (textoBoton != null)
        {
            textoBoton.fontSize = tamañoFuenteNormal;
            textoBoton.color = new Color(colorNormal.r, colorNormal.g, colorNormal.b, intensidadNormal);
        }
    }
}
