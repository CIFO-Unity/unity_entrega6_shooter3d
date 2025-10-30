using UnityEngine;

public class TriggerBruja : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Cronometro panelCronometro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Inicia el cronómetro
            if (panelCronometro != null)
                panelCronometro.IniciarCronometro();

            // Destruir el trigger para que solo se active una vez
            Destroy(this.gameObject);
        }
    }
}
