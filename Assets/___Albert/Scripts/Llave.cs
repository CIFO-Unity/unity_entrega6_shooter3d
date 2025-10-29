using UnityEngine;

public class Llave : MonoBehaviour
{
    [SerializeField]
    private int numLlave = 1;

    void Start()
    {
        if (numLlave == 2)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                MaterialPropertyBlock mpb = new MaterialPropertyBlock();
                renderer.GetPropertyBlock(mpb);

                // Para URP/HDRP, la propiedad suele ser "_BaseColor"
                mpb.SetColor("_BaseColor", Color.red);

                renderer.SetPropertyBlock(mpb);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Obtiene el componente Player del objeto que colisiona
            Player jugador = other.GetComponent<Player>();

            if (jugador != null)
            {
                // Llama a la función ObtenerLlave del Player
                jugador.ObtenerLlave(numLlave);
            }

            // Destruye la llave
            Destroy(gameObject);
        }
    }
}
