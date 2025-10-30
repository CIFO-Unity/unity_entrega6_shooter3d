using UnityEngine;
using TMPro;

public class MostrarRecord : MonoBehaviour
{
    [SerializeField] private RecordTiempo recordTiempo;
    [SerializeField] private TMP_Text textoRecord;

    void Start()
    {
        if (recordTiempo != null && textoRecord != null)
        {
            // Muestra el tiempo en formato M:SS
            textoRecord.text = string.Format("{0}:{1:00}", recordTiempo.Minutos, recordTiempo.Segundos);
        }
    }
}
