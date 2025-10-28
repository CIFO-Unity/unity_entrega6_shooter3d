using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [Header("Padre a Activar")]
    [SerializeField] private GameObject camarasPadre;

    private void Start()
    {
        cambiarCamaras(0);
    }

    private void Update()
    {

    }

    public void cambiarCamaras(int posicionBotonCamara)
    {
        for (int i = 0; i < camarasPadre.transform.childCount; i++)
        {
            camarasPadre.transform.GetChild(i).gameObject.SetActive(false);
        }

        camarasPadre.transform.GetChild(posicionBotonCamara).gameObject.SetActive(true);
    }



}