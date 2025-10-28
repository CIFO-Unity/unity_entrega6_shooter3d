using UnityEngine;
using UnityEngine.EventSystems;


/// Script genérico para objetos animables con clic del ratón.
/// Añadimos este script para poder hacer clic en objetos y activar animaciones (puertas, ataúdes, jaulas, etc.)

public class ObjetoAnimableClick : MonoBehaviour
{
    [Header("Configuración de Animación")]
    [Tooltip("El Animator del objeto (puede ser este mismo objeto o un hijo)")]
    [SerializeField] private Animator animator;
    
    [Tooltip("Parámetro bool que activa animacion (ej: 'estadoAtaud', 'estadoPuertas')")]
    [SerializeField] private string nombreParametro = "";

    private void Start()
    {
        // Si no se asignó animator, intentar obtenerlo del mismo objeto
        if (animator == null)
        {
            print("Falta asignar animator en " + gameObject.name);
        }
    }

    private void OnMouseDown()//clicamos y activamos animacion
    {
        if (animator != null)
        {
            // Obtener el estado actual del objeto y alternarlo (toggle) --> solo tenemos bools
            bool estadoActual = animator.GetBool(nombreParametro);
            animator.SetBool(nombreParametro, !estadoActual);
        }
    }

    private void OnMouseEnter()
    {
        if (animator != null)
        {
            //queremos que se pongan de color rojo las dos puertas si señalamos una, hemmos hecho un padre con el script.
            if (nombreParametro == "estadoPuertas")
            {
                MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
                foreach (var mesh in meshes)
                {
                    mesh.material.color = Color.red;
                }
            }
            else
            {
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }

    private void OnMouseExit()
    {
        if (animator != null)
        {
            if (nombreParametro == "estadoPuertas")
            {
                MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
                foreach (var mesh in meshes)
                {
                    mesh.material.color = Color.white;
                }
            }
            else
            {
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }

}
