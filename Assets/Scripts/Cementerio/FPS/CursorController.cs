using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public RectTransform cursorUI; // Asigna aquí el objeto Image del puntero

    void Start()
    {
        // Ocultamos el cursor del sistema
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // Convertimos la posición del ratón a coordenadas del canvas
        Vector2 mousePos = Input.mousePosition;

        // Asignamos la posición directamente (Canvas en Screen Space - Overlay usa píxeles de pantalla)
        cursorUI.position = mousePos;
    }
}
