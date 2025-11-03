using UnityEngine;

public class TriggerBruja : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Cronometro panelCronometro;

    [Header("Bruja")]
    [SerializeField] private GameObject bruja;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Inicia el cronómetro
            if (panelCronometro != null)
                panelCronometro.IniciarCronometro();

            // Activar la bruja para que empiece a atacar
            if (bruja != null)
            {
                WomanWitch scriptBruja = bruja.GetComponent<WomanWitch>();
                if (scriptBruja != null)
                    scriptBruja.ActivarBruja();
            }

            // Destruir el trigger para que solo se active una vez
            Destroy(this.gameObject);
        }
    }
}
