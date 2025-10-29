using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Slider sliderVida;

    [SerializeField]
    private TextMeshProUGUI textMunicion;

    [SerializeField]
    private Image imageLlave1;

    [SerializeField]
    private Image imageLlave2;

    [Header("Sonidos")]
    [SerializeField]
    private GameObject sonidos;

    [Header("Balas")]
    [SerializeField]
    private GameObject bala;
    private GameObject balaClon;

    [SerializeField]
    private GameObject pivotBala;

    [SerializeField]
    private GameObject particulasArma;
    private GameObject particulasArmaClon;

    [SerializeField]
    private float fuerzaBala = 50;

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

    [Header("Llaves")]
    [SerializeField]
    private bool tieneLlave1 = false;

    [SerializeField]
    private bool tieneLlave2 = false;


    #region Getters & Setters

    // Getter y Setter para Vida
    public int Vida
    {
        get { return vida; }
        set
        {
            vida = Mathf.Clamp(value, 0, vidaMaxima);
            ActualizarSliderVida();

            // Si la vida llega a 0 o menos, cargar escena Derrota
            if (vida <= 0)
            {
                CargarEscenaDerrota();
            }
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

        if (!tieneLlave1 && imageLlave1 != null)
        {
            // Aplica tint con el color #312B2B
            imageLlave1.color = new Color(49f / 255f, 43f / 255f, 43f / 255f, 1f); // A=1 para opacidad total
        }

        if (!tieneLlave2 && imageLlave2 != null)
        {
            // Aplica tint con el color #312B2B
            imageLlave2.color = new Color(49f / 255f, 43f / 255f, 43f / 255f, 1f); // A=1 para opacidad total
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Comprueba si el jugador tiene munición antes de disparar
            if (Municion > 0)
            {
                // Instancia la bala
                balaClon = (GameObject)Instantiate(bala, pivotBala.transform.position, Quaternion.identity);
                balaClon.GetComponent<Rigidbody>().linearVelocity = transform.GetChild(0).forward * fuerzaBala;

                // Resta una unidad de munición
                RestarMunicion(1);

                // Destruye la bala después de 5 segundos
                Destroy(balaClon, 5.0f);

                // Instancia las partículas
                particulasArmaClon = (GameObject)Instantiate(particulasArma, pivotBala.transform.position, Quaternion.identity);

                // Destruye las partículas después de 0.2 segundos
                Destroy(particulasArmaClon, 0.2f);
            }
        }
    }

    #endregion

    #region Vida

    // 🔹 Nueva función para añadir vida
    public void AnadirVida(int cantidad)
    {
        if (cantidad > 0)
        {
            Vida += cantidad; // Usa el setter, así se aplica Clamp automáticamente

            sonidos.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play(); // Reproducir sonido
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

            sonidos.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play(); // Reproducir sonido
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

    #region Llaves

    public void ObtenerLlave(int numLlave)
    {
        if (numLlave == 1)
        {
            tieneLlave1 = true;

            if (imageLlave1 != null)
            {
                imageLlave1.color = Color.white;
            }
        }
        else if (numLlave == 2)
        {
            tieneLlave2 = true;

            if (imageLlave2 != null)
            {
                imageLlave2.color = Color.red;
            }
        }
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

    #region Escena Derrota

    private void CargarEscenaDerrota()
    {
        SceneManager.LoadScene("Derrota");
    }

    #endregion
}
