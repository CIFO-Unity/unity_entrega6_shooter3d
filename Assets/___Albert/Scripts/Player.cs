using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Slider sliderVida;

    [SerializeField]
    private TextMeshProUGUI textMunicion;

    [Header("Vida")]
    [SerializeField]
    private int vida = 10;

    [SerializeField]
    private int vidaMaxima = 10;

    [Header("Munición")]
    [SerializeField]
    private int municion = 50;

    [SerializeField]
    private int municionMaxima = 100;


    #region Getters & Setters

    // Getter y Setter para Vida
    public int Vida
    {
        get { return vida; }
        set
        {
            vida = Mathf.Clamp(value, 0, vidaMaxima);
            ActualizarSliderVida();
        }
    }

    // Getter y Setter para Municion
    public int Municion
    {
        get { return municion; }
        set
        {
            municion = Mathf.Clamp(value, 0, municionMaxima);
            ActualizarTextoMunicion();
        }
    }

    #endregion

    #region Start & Update
    void Start()
    {
        if (sliderVida != null)
        {
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vida;
        }

        if (textMunicion != null)
        {
            ActualizarTextoMunicion(); // Inicializa el texto al comenzar
        }
    }

    void Update()
    {
        // Ejemplo de prueba:
        // if (Input.GetKeyDown(KeyCode.Space)) Vida -= 1;
        // if (Input.GetKeyDown(KeyCode.M)) Municion += 10;
    }

    #endregion

    #region Vida

    // 🔹 Nueva función para añadir vida
    public void AnadirVida(int cantidad)
    {
        if (cantidad > 0)
        {
            Vida += cantidad; // Usa el setter, así se aplica Clamp automáticamente
        }
    }

    public void RestarVida(int cantidad)
    {
        if (cantidad > 0)
        {
            Vida -= cantidad; // Usa el setter, así se aplica Clamp automáticamente
        }
    }

    // Devuelve true si la vida está al máximo
    public bool VidaAlMaximo()
    {
        return vida >= vidaMaxima;
    }

    #endregion

    #region Munición

    public void AnadirMunicion(int cantidad)
    {
        if (cantidad > 0)
        {
            Municion += cantidad; // Usa el setter, así se aplica Clamp automáticamente
        }
    }

    public void RestarMunicion(int cantidad)
    {
        if (cantidad > 0)
        {
            Municion -= cantidad; // Usa el setter, así se aplica Clamp automáticamente
        }
    }

    // Devuelve true si la munición está al máximo
    public bool MunicionAlMaximo()
    {
        return municion >= municionMaxima;
    }

    #endregion

    #region UI

    private void ActualizarSliderVida()
    {
        if (sliderVida != null)
        {
            sliderVida.value = vida;
        }
    }

    private void ActualizarTextoMunicion()
    {
        if (textMunicion != null)
        {
            textMunicion.text = $"{municion}/{municionMaxima}";
        }
    }

    #endregion
}
