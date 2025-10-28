using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LightsController : MonoBehaviour
{
    [Header("Canvas")]

    [SerializeField]
    private GameObject canvas;


    [Header("Luna")]

    [SerializeField]
    private GameObject luna;

    [SerializeField] 
    private Button[] botonesLuna;

    [SerializeField]
    private GameObject sliderIntensidadLuna;

    [SerializeField]
    private Button botonApagarLuzLuna;

    [SerializeField]
    private Sprite iconoLuzLunaOn;
    
    [SerializeField] 
    private Sprite iconoLuzLunaOff;


    [Header("Calabazas")]

    [SerializeField]
    private Button[] botonesCalabazas;

    [SerializeField]
    private GameObject sliderIntensidadCalabazas;

    [SerializeField]
    private Button botonApagarCalabazas;

    [SerializeField]
    private Sprite iconoLuzCalabazasOn;
    
    [SerializeField] 
    private Sprite iconoLuzCalabazasOff;
    
    private GameObject[] calabazas;


    [Header("Farolas")]

    [SerializeField] 
    private Button[] botonesFarolas;

    [SerializeField]
    private GameObject sliderIntensidadFarolas;

    [SerializeField]
    private Button botonApagarFarolas;
    
    [SerializeField]
    private Sprite iconoLuzFarolasOn;
    
    [SerializeField] 
    private Sprite iconoLuzFarolasOff;
    private GameObject[] farolas;


    #region Start & Update

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Luz de la luna

        Light luzLuna = luna.GetComponent<Light>();

        if (luzLuna != null && sliderIntensidadLuna != null)
        {
            // Establecer el valor del slider a la intensidad actual de la luz
            sliderIntensidadLuna.gameObject.GetComponent<Slider>().value = luzLuna.intensity;
        }

        // Outline de los botones de la luna
        for (int i = 0; i < botonesLuna.Length; i++)
        {
            Outline outline = botonesLuna[i].GetComponent<Outline>();
            if (outline != null)
                outline.enabled = (i == 0); // solo el primer botón activo
        }


        // Luces de las calabazas

        calabazas = GameObject.FindGameObjectsWithTag("Calabaza");

        if (calabazas.Length > 0 && sliderIntensidadCalabazas != null)
        {
            // Suponemos que la luz es el primer hijo de la calabaza
            Light luzPrimeraCalabaza = calabazas[0].transform.GetChild(0).GetComponent<Light>();

            if (luzPrimeraCalabaza != null)
            {
                // Asignar al slider la intensidad actual de la luz
                sliderIntensidadCalabazas.GetComponent<Slider>().value = luzPrimeraCalabaza.intensity;
            }
        }

        // Outline de los botones de las calabazas
        for (int i = 0; i < botonesCalabazas.Length; i++)
        {
            Outline outline = botonesCalabazas[i].GetComponent<Outline>();
            if (outline != null)
                outline.enabled = (i == 0); // solo el primer botón activo
        }


        // Luces de las farolas

        farolas = GameObject.FindGameObjectsWithTag("Farola");

        if (farolas.Length > 0 && sliderIntensidadFarolas != null)
        {
            // Suponemos que la luz es el primer hijo de la farola
            Light luzPrimeraFarola = farolas[0].transform.GetChild(0).GetComponent<Light>();

            if (luzPrimeraFarola != null)
            {
                // Asignar al slider la intensidad actual de la luz
                sliderIntensidadFarolas.GetComponent<Slider>().value = luzPrimeraFarola.intensity;
            }
        }

        // Outline de los botones de las farolas
        for (int i = 0; i < botonesFarolas.Length; i++)
        {
            Outline outline = botonesFarolas[i].GetComponent<Outline>();
            if (outline != null)
                outline.enabled = (i == 0); // solo el primer botón activo
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Mostrar/ocultar el panel de cambio de cámara y luces al pulsar Escape
        // --> Eliminado; ahora los paneles de cámara y luces se muestran al hacer click en un botón del mismo panel

        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvas.gameObject.activeSelf)
                canvas.gameObject.SetActive(false);
            else
                canvas.gameObject.SetActive(true);
        }*/
    }

    #endregion

    #region Luna

    public void CambiarColorLuzLuna(GameObject boton)
    {
        Light luzLuna = luna.GetComponent<Light>();

        if (luzLuna != null && boton != null)
        {
            // Obtener el color del Image del botón
            Image img = boton.GetComponent<Image>();
            if (img != null)
            {
                Color colorLuna = img.color;

                // Asignar a la luz
                luzLuna.color = colorLuna;
            }
        }

        // Activar el Outline del botón seleccionado y desactivar el de los demás
        foreach (Button b in botonesLuna)
        {
            Outline outline = b.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = (b.gameObject == boton); // solo habilitar en el botón pulsado
            }
        }
    }

    public void CambiarIntensidadLuzLuna()
    {
        Light luzLuna = luna.GetComponent<Light>();
        Slider slider = sliderIntensidadLuna.GetComponent<Slider>();

        if (luzLuna != null && slider != null)
        {
            // Solo cambiar la intensidad si la luz está encendida
            if (luzLuna.enabled)
            {
                luzLuna.intensity = slider.value;
            }
        }
    }

   public void ToggleLuzLuna()
    {
        Light luzLuna = luna.GetComponent<Light>();
        Slider slider = sliderIntensidadLuna.GetComponent<Slider>();

        if (luzLuna != null && slider != null)
        {
            if (luzLuna.enabled)
            {
                // Apagar luz: desactivar y poner intensidad a 0
                luzLuna.enabled = false;
                luzLuna.intensity = 0f;
            }
            else
            {
                // Encender luz: activar y usar la intensidad actual del slider
                luzLuna.enabled = true;
                luzLuna.intensity = slider.value;
            }

            // Actualizar el botón
            ActualizarSpriteBotonLuna();
        }
    }

    private void ActualizarSpriteBotonLuna()
    {
        if (botonApagarLuzLuna != null && luna != null)
        {
            Light luzLuna = luna.GetComponent<Light>();
            if (luzLuna != null)
            {
                Image imagenBoton = botonApagarLuzLuna.GetComponent<Image>();
                if (imagenBoton != null)
                {
                    imagenBoton.sprite = luzLuna.enabled ? iconoLuzLunaOn : iconoLuzLunaOff;
                }
            }
        }
    }

    #endregion

    #region Calabazas

    public void CambiarColorLuzCalabazas(GameObject boton)
    {
        if (calabazas != null && boton != null)
        {
            // Obtener el color del Image del botón
            Image img = boton.GetComponent<Image>();
            if (img != null)
            {
                Color colorCalabazas = img.color;

                // Asignar el color a todas las luces de las calabazas
                foreach (GameObject calabaza in calabazas)
                {
                    if (calabaza.transform.childCount > 0)
                    {
                        Transform hijo = calabaza.transform.GetChild(0);
                        Light luz = hijo.GetComponent<Light>();

                        if (luz != null)
                        {
                            luz.color = colorCalabazas;
                        }
                    }
                }
            }
        }

        // Activar el Outline del botón seleccionado y desactivar el de los demás
        foreach (Button b in botonesCalabazas)
        {
            Outline outline = b.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = (b.gameObject == boton); // solo habilitar en el botón pulsado
            }
        }
    }

    public void CambiarIntensidadLuzCalabazas()
    {
        if (calabazas != null)
        {
            foreach (GameObject calabaza in calabazas)
            {
                if (calabaza.transform.childCount > 0)
                {
                    // Suponemos que el hijo es la Point Light
                    Transform hijo = calabaza.transform.GetChild(0);
                    Light luz = hijo.GetComponent<Light>();

                    if (luz != null)
                    {
                        // Cambiar intensidad
                        luz.intensity = sliderIntensidadCalabazas.gameObject.GetComponent<Slider>().value;
                    }
                }
            }
        }
    }

    public void ToggleLuzCalabazas()
    {
        Slider slider = sliderIntensidadCalabazas.GetComponent<Slider>();

        if (calabazas != null && slider != null)
        {
            foreach (GameObject calabaza in calabazas)
            {
                if (calabaza.transform.childCount > 0)
                {
                    Transform hijo = calabaza.transform.GetChild(0);
                    Light luz = hijo.GetComponent<Light>();

                    if (luz != null)
                    {
                        if (luz.enabled)
                        {
                            luz.enabled = false;
                            luz.intensity = 0f;
                        }
                        else
                        {
                            luz.enabled = true;
                            luz.intensity = slider.value;
                        }
                    }
                }
            }

            ActualizarSpriteBotonCalabazas();
        }
    }

    private void ActualizarSpriteBotonCalabazas()
    {
        if (botonApagarCalabazas != null && calabazas != null && calabazas.Length > 0)
        {
            Light luzPrimera = calabazas[0].transform.GetChild(0).GetComponent<Light>();
            if (luzPrimera != null)
            {
                Image imagenBoton = botonApagarCalabazas.GetComponent<Image>();
                if (imagenBoton != null)
                {
                    imagenBoton.sprite = luzPrimera.enabled ? iconoLuzCalabazasOn : iconoLuzCalabazasOff;
                }
            }
        }
    }

    #endregion

    #region Farolas

    public void CambiarColorLuzFarolas(GameObject boton)
    {
        if (farolas != null && boton != null)
        {
            // Obtener el color del Image del botón
            Image img = boton.GetComponent<Image>();
            if (img != null)
            {
                Color colorFarolas = img.color;

                // Asignar el color a todas las luces de las farolas
                foreach (GameObject farola in farolas)
                {
                    if (farola.transform.childCount > 0)
                    {
                        Transform hijo = farola.transform.GetChild(0);
                        Light luz = hijo.GetComponent<Light>();

                        if (luz != null)
                        {
                            luz.color = colorFarolas;
                        }
                    }
                }
            }
        }

        // Activar el Outline del botón seleccionado y desactivar el de los demás
        foreach (Button b in botonesFarolas)
        {
            Outline outline = b.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = (b.gameObject == boton); // solo habilitar en el botón pulsado
            }
        }
    }

    public void CambiarIntensidadLuzFarolas()
    {
        if (farolas != null)
        {
            foreach (GameObject farola in farolas)
            {
                if (farola.transform.childCount > 0)
                {
                    // Suponemos que el hijo es la Point Light
                    Transform hijo = farola.transform.GetChild(0);
                    Light luz = hijo.GetComponent<Light>();

                    if (luz != null)
                    {
                        // Cambiar intensidad
                        luz.intensity = sliderIntensidadFarolas.gameObject.GetComponent<Slider>().value;
                    }
                }
            }
        }
    }

    public void ToggleLuzFarolas()
    {
        Slider slider = sliderIntensidadFarolas.GetComponent<Slider>();

        if (farolas != null && slider != null)
        {
            foreach (GameObject farola in farolas)
            {
                if (farola.transform.childCount > 0)
                {
                    Transform hijo = farola.transform.GetChild(0);
                    Light luz = hijo.GetComponent<Light>();

                    if (luz != null)
                    {
                        if (luz.enabled)
                        {
                            luz.enabled = false;
                            luz.intensity = 0f;
                        }
                        else
                        {
                            luz.enabled = true;
                            luz.intensity = slider.value;
                        }
                    }
                }
            }

            ActualizarSpriteBotonFarolas();
        }
    }

    private void ActualizarSpriteBotonFarolas()
    {
        if (botonApagarFarolas != null && farolas != null && farolas.Length > 0)
        {
            Light luzPrimera = farolas[0].transform.GetChild(0).GetComponent<Light>();
            if (luzPrimera != null)
            {
                Image imagenBoton = botonApagarFarolas.GetComponent<Image>();
                if (imagenBoton != null)
                {
                    imagenBoton.sprite = luzPrimera.enabled ? iconoLuzFarolasOn : iconoLuzFarolasOff;
                }
            }
        }
    }

    #endregion
}
