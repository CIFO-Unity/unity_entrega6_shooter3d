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
        // Inicia el temporizador
        //timerIsRunning = true;
    }

    public void IniciarCronometro()
    {
        timerIsRunning = true;
    }

    // Función para detener el temporizador manualmente
    public void PararGuardarCrono()
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
        }
    }
}
