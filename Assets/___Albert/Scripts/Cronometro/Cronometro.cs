using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Cronometro : MonoBehaviour
{
    [Header("Configuración de tiempo")]
    public float timeRemaining = 180f; // 180 segundos = 3 minutos
    public bool timerIsRunning = false;

    private int minutes;
    private int seconds;

    [Header("UI")]
    public TMP_Text timeText;

    void Start()
    {
        timerIsRunning = false;

        this.gameObject.SetActive(false);
    }

    public void IniciarCronometro()
    {
        timerIsRunning = true;

        this.gameObject.SetActive(true);
    }

    // Función para detener el temporizador manualmente
    public void DetenerCronometro()
    {
        timerIsRunning = false;
    }

    // Obtener minutos restantes
    public int GetMinutos()
    {
        return (int)(timeRemaining / 60);
    }

    // Obtener segundos restantes
    public int GetSeconds()
    {
        return (int)(timeRemaining % 60);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Resta tiempo
                timeRemaining -= Time.deltaTime;

                // Asegura que timeRemaining esté entre 0 y su valor máximo
                timeRemaining = Mathf.Clamp(timeRemaining, 0f, Mathf.Infinity);

                // Actualiza UI
                if (timeText != null)
                {
                    minutes = Mathf.FloorToInt(timeRemaining / 60);
                    seconds = Mathf.FloorToInt(timeRemaining % 60);
                    timeText.text = string.Format("{0}:{1:00}", minutes, seconds);
                }
            }
            else
            {
                // Tiempo agotado
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene("Derrota"); // Carga la escena de derrota
            }

            // Pulsar P para guardar el tiempo manualmente
            // --> Sólo para hacer pruebas
            /*if (Input.GetKeyDown(KeyCode.P))
            {
                GuardarTiempoActual();
                Debug.Log($"Tiempo guardado: {minutes}:{seconds:00}");
            }*/
        }
    }

    public void GuardarTiempoActual()
    {
        ControladorBaseDeDatos cbd = Object.FindFirstObjectByType<ControladorBaseDeDatos>();

        if (cbd != null)
            cbd.GuardarTiempo(minutes, seconds);
    }
}
