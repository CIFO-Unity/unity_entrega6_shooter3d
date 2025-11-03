using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.InteropServices;

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

    [SerializeField]
    private Image panelVictoria;

    [SerializeField]
    private TextMeshProUGUI textTiempoActual;

    [SerializeField]
    private TextMeshProUGUI textMejorTiempo;

    [SerializeField]
    private RecordTiempo recordTiempo;

    [SerializeField]
    private Image panelMuerte;

    [SerializeField]
    private Cronometro cronometro;

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

    [SerializeField]
    private int danoManKillerHalloween = 1;

    [SerializeField]
    private int danoBoy_Ghost = 2;
    [SerializeField]
    private int danoFireBall = 3;
    private int ultimoGolpe = -1; // Último sonido de golpe recibido reproducido; para evitar repetir el mismo dos veces seguidas

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

    [Header("Enemigos")]
    [SerializeField]
    private GameObject particleImpactBoyGhostPrefab;
    [SerializeField]
    private EnemiesManager enemiesManager;

    [Header("Efectos Locales")]
    [SerializeField]
    private GameObject fxFire01;
    [SerializeField]
    private GameObject fxFire01_2;

    //[SerializeField]
    //private GameObject explosionEffect; //Boy_Ghost

    #region Getters & Setters

    // Getter y Setter para Vida
    public int Vida
    {
        get { return vida; }
        set
        {
            vida = Mathf.Clamp(value, 0, vidaMaxima);
            ActualizarSliderVida();

            // Si la vida llega a 0 o menos, morir
            if (vida <= 0)
                Morir();
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

        if (panelVictoria != null)
            panelVictoria.gameObject.SetActive(false);

        if (panelMuerte != null)
            panelMuerte.gameObject.SetActive(false);
    }

    void Update()
    {
        // Para debuggear que el usuario reciba daño
        if (Input.GetMouseButtonDown(1))
        {
            RestarVida(1);
        }

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

    public void AnadirVida(int cantidad)
    {
        if (cantidad > 0)
        {
            Vida += cantidad; // Usa el setter, así se aplica Clamp automáticamente

            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("CogerVida");
        }
    }

    public void RestarVida(int cantidad)
    {
        if (cantidad > 0)
        {
            Vida -= cantidad; // Usa el setter, así se aplica Clamp automáticamente

            if (Vida > 0)
            {
                if (SoundManager.Instance != null)
                {
                    int golpeAleatorio;

                    // Genera un número diferente al último
                    do
                    {
                        golpeAleatorio = Random.Range(1, 7); // 1 a 6 inclusive
                    } while (golpeAleatorio == ultimoGolpe);

                    // Guarda el sonido actual como último
                    ultimoGolpe = golpeAleatorio;

                    // Construye el nombre del sonido
                    string nombreSonido = $"RecibirGolpe{golpeAleatorio}";

                    // Reproduce el sonido
                    SoundManager.Instance.PlaySound(nombreSonido);
                }
            }
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

            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("CogerMunicion");
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

            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("CogerLlave");
        }
        else if (numLlave == 2)
        {
            tieneLlave2 = true;

            if (imageLlave2 != null)
            {
                imageLlave2.color = Color.white;
            }

            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("CogerLlave");
        }
    }

    public bool TieneLlave(int numLlave)
    {
        if (numLlave == 1)
        {
            if (tieneLlave1) return true;
            else return false;
        }
        else if (numLlave == 2)
        {
            if (tieneLlave2) return true;
            else return false;
        }

        else return false;
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
            textMunicion.text = municion.ToString();
        }
    }

    #endregion

    #region Escenas Ganar & Derrota

    public void Ganar()
    {
        // 1️⃣ Detener el cronómetro y guardar tiempo
        if (cronometro != null)
        {
            cronometro.DetenerCronometro();
            cronometro.GuardarTiempoActual();

            AnadirTiempoActual(cronometro.GetMinutos(), cronometro.GetSeconds());
        }

        // 2️⃣ Mostrar mejor tiempo en pantalla
        if (recordTiempo != null)
            AnadirMejorTiempo(recordTiempo.Minutos, recordTiempo.Segundos);

        // 3️⃣ Ralentizar el juego
        Time.timeScale = 0.3f; // 30% de velocidad

        // 4️⃣ Esperar 5 segundos reales y mostrar victoria
        StartCoroutine(SecuenciaVictoria());
    }

    public void Morir()
    {
        // Reproducir sonido de muerte
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySound("Morir");

        // Detiene el cronómetro
        if (cronometro != null)
            cronometro.DetenerCronometro();

        // Ralentizar el juego
        Time.timeScale = 0.3f; // 30% de velocidad

        // Mostrar el panel rojo
        if (panelMuerte != null)
        {
            panelMuerte.gameObject.SetActive(true);
            StartCoroutine(FadeRojo(panelMuerte, 0f, 0.2f, 4.0f));
        }

        // Llamar a la función de cargar la escena después de 4 segundos
        StartCoroutine(CargarDerrotaConDelay(4.0f));
    }

    private IEnumerator SecuenciaVictoria()
    {
        // Esperar 5 segundos reales
        yield return new WaitForSecondsRealtime(7.0f);

        // Reproducir sonido de victoria
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySound("Victoria");

        // Mostrar el panel de victoria con fade
        if (panelVictoria != null)
        {
            panelVictoria.gameObject.SetActive(true);
            StartCoroutine(FadeNegro(panelVictoria.GetComponent<UnityEngine.UI.Image>(), 0f, 1f, 2f));
        }

        // Esperar 7 segundos más para dar tiempo al fade y sonido
        yield return new WaitForSecondsRealtime(7.0f);

        // Restaurar velocidad normal
        Time.timeScale = 1f;

        // Cargar escena de victoria
        SceneManager.LoadScene("Ganar");
    }

    private IEnumerator CargarDerrotaConDelay(float segundosReales)
    {
        // Espera usando tiempo real, sin verse afectado por Time.timeScale
        yield return new WaitForSecondsRealtime(segundosReales);

        // Restaurar la velocidad normal antes de cambiar de escena
        Time.timeScale = 1f;

        // Cargar la escena de Derrota
        CargarEscenaDerrota();
    }

    private void CargarEscenaDerrota()
    {
        SceneManager.LoadScene("Derrota");
    }

    public IEnumerator FadeNegro(Image img, float startAlpha, float endAlpha, float duration)
    {
        float t = 0f;
        Color c = Color.black;   // Color base negro
        c.a = startAlpha;        // alpha inicial
        img.color = c;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime; // tiempo real, ignora Time.timeScale
            c.a = Mathf.Lerp(startAlpha, endAlpha, t / duration);
            img.color = c;
            yield return null;
        }

        c.a = endAlpha;
        img.color = c;
    }

    private IEnumerator FadeRojo(Image img, float startAlpha, float endAlpha, float duration)
    {
        float t = 0f;
        Color c = img.color;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime; // tiempo real
            c.a = Mathf.Lerp(startAlpha, endAlpha, t / duration);
            img.color = c;
            yield return null;
        }
        c.a = endAlpha;
        img.color = c;
    }

    #endregion

    #region Tiempos

    // Actualiza el texto del tiempo actual
    public void AnadirTiempoActual(int minutos, int segundos)
    {
        if (textTiempoActual != null)
        {
            textTiempoActual.text = string.Format("Tiempo actual: {0}:{1:00}", minutos, segundos);
        }
    }

    // Actualiza el texto del mejor tiempo
    public void AnadirMejorTiempo(int minutos, int segundos)
    {
        if (textMejorTiempo != null)
        {
            textMejorTiempo.text = string.Format("Mejor tiempo: {0}:{1:00}", minutos, segundos);
        }
    }

    #endregion

    #region Trigger Enemigos

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "RightHandManKiller")
        {
            // Resta vida al jugador al colisionar con Man_Killer
            RestarVida(danoManKillerHalloween);
        }

        if (other.gameObject.tag == "LeftHandManKiller")
        {
            // Resta vida al jugador al colisionar con Man_Killer
            RestarVida(danoManKillerHalloween);
        }

        if (other.gameObject.tag == "Boy_Ghost")
        {

            // Resta vida al jugador al colisionar con Boy_Ghost
            RestarVida(danoBoy_Ghost);

            // Reproducir sonido
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("BoyGhostAtaque");

            //activamos prefab de fuego
            if (fxFire01 != null)
            {
                fxFire01.SetActive(true);
                // Buscar el objeto FX_Fire_04 (llama boyghost) dentro de los hijos
                Transform hijo = transform.Find("boy-ghost-halloween/FX_Fire_04");
                if (hijo != null)
                {
                    hijo.gameObject.SetActive(false);
                    Debug.Log("🔥 FX_Fire_04 desactivado correctamente");
                }
                else
                {
                    Debug.LogWarning("⚠ No se encontró el hijo FX_Fire_04");
                }
                
                StartCoroutine(StopFireAfter(1f, fxFire01));
            }



            // Notificar a EnemiesManager que se ha destruido un enemigo
            if (enemiesManager != null)
                enemiesManager.ActualizarNumeroEnemigosMuertos();
        }

        if (other.gameObject.tag == "FireBall")
        {
            //activamos prefab de fuego
            if (fxFire01 != null)
            {
                fxFire01.SetActive(true);
                //lo mantenemos en play solo medio segundo
                StartCoroutine(StopFireAfter(2.0f, fxFire01));
            }

            RestarVida(danoFireBall);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "IceBall")//CONGELAR AL PLAYER Y NO QUITAR VIDA!!!
        {
            if (fxFire01_2 != null)
            {
                fxFire01_2.SetActive(true);
                //lo mantenemos en play solo medio segundo
                StartCoroutine(StopFireAfter(2.0f, fxFire01_2));
            }

            RestarVida(danoFireBall);
            Destroy(other.gameObject);
        }
    }



    private IEnumerator StopFireAfter(float time, GameObject fxFire)
    {
        // Espera el tiempo indicado
        yield return new WaitForSeconds(time);

        // Desactiva el GameObject
        fxFire.SetActive(false);
    }

    #endregion
}
