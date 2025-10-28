using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Maneja los eventos onClick de los botones del Canvas principal
/// </summary>
public class BotonesClick : MonoBehaviour
{
    private string escenaJugar = "CementerioPrimeraPersona";

    private string escenaCreditos = "Creditos";

private void Start()
    {
        // Cursor del sistema (por si nos llaman desde FPS y q tenemos oculto allí.)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void CargarEscenaCreditos()
    {
        SceneManager.LoadScene(escenaCreditos);
    }

    public void CargarEscenaJugar()
    {
        SceneManager.LoadScene(escenaJugar);
    }

    public void SalirDelJuego()
    {
        
        #if UNITY_EDITOR
            // En el editor, detiene el modo Play
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // En build, cierra la aplicación
            Application.Quit();
        #endif
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
