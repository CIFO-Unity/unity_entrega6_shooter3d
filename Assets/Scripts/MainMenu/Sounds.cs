using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoBruja;
    [SerializeField] private AudioClip sonidoViento;
    
    [Header("Animators externos")]
    [SerializeField] private Animator animadorViento; // Asigna aquí el Animator que tiene el parámetro "Viento"

    public void SonidoBruja() {
        AudioSource audio = GetComponent<AudioSource>();
        if (sonidoBruja != null) {
            audio.PlayOneShot(sonidoBruja);
        } else {
            Debug.LogWarning("SonidoBruja: AudioClip no asignado en el Inspector");
        }
    }

    public void SonidoViento() {
        AudioSource audio = GetComponent<AudioSource>();
        if (sonidoViento != null) {
            audio.PlayOneShot(sonidoViento);
        } else {
            Debug.LogWarning("SonidoViento: AudioClip no asignado en el Inspector");
        }
    }

    // Activamos parámetro "Viento" de otro Animator para iniciar animacion sombrero
    public void ActivarViento() {
        if (animadorViento != null) {
            animadorViento.SetBool("Viento", true);
        } else {
            Debug.LogWarning("Animador Viento no asignado en el Inspector");
        }
    }
/*
    // Método para desactivar el viento si no destruimos el sombrero(hay que implementarlo)
    public void DesactivarViento() {
        if (animadorViento != null) {
            animadorViento.SetBool("Viento", false);
        } else {
            Debug.LogWarning("Animador Viento no asignado en el Inspector");
        }
    }
    */
}
