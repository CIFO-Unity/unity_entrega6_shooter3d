using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuDeslizante : MonoBehaviour
{
    [SerializeField]
    public RectTransform panel;       // El panel que se mueve

    [SerializeField]
    public float desplazamientoX = 1025.0f; // Distancia a desplazar a la derecha

    [SerializeField]
    public float velocidad = 500.0f;       // Velocidad de desplazamiento (unidades/segundo)

    private bool abierto = false;     // Estado del menú
    private Vector2 posicionOriginal;
    private Vector2 posicionDestino;

    void Start()
    {
        posicionOriginal = panel.anchoredPosition;
        posicionDestino = posicionOriginal + new Vector2(desplazamientoX, 0);
    }
    
    public void MostrarPanel()
    {
        StopAllCoroutines(); // por si se pulsa rápido
        Vector2 destino = abierto ? posicionOriginal : posicionDestino;

        // Calcular la distancia actual hasta el destino
        float distancia = Vector2.Distance(panel.anchoredPosition, destino);
        float tiempo = distancia / velocidad;

        StartCoroutine(MoverPanel(panel, destino, tiempo));

        abierto = !abierto;
    }

    private IEnumerator MoverPanel(RectTransform objetivo, Vector2 destino, float tiempo)
    {
        Vector2 inicio = objetivo.anchoredPosition;
        float transcurrido = 0f;

        while (transcurrido < tiempo)
        {
            transcurrido += Time.deltaTime;
            objetivo.anchoredPosition = Vector2.Lerp(inicio, destino, transcurrido / tiempo);
            yield return null;
        }

        objetivo.anchoredPosition = destino;
    }
}
