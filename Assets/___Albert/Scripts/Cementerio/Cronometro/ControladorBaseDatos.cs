using UnityEngine;

public class ControladorBaseDeDatos : MonoBehaviour
{
    [SerializeField] private RecordTiempo rt;

    private int minutos;
    private int segundos;

    void Start()
    {
        minutos = rt.Minutos;
        segundos = rt.Segundos;

        Debug.Log($"Tiempo guardado: {minutos}:{segundos:00}");
    }

    // Guardar el tiempo si es mejor que el récord actual
    public void GuardarTiempo(int minutos, int segundos)
    {
        if (rt.Minutos < minutos)
        {
            rt.Minutos = minutos;
            rt.Segundos = segundos;
        }
        else if (rt.Minutos == minutos)
        {
            if (rt.Segundos < segundos)
            {
                rt.Minutos = minutos;
                rt.Segundos = segundos;
            }
        }
    }

    public int RetornarRecordMinutos()
    {
        return minutos;
    }

    public int RetornarRecordSegundos()
    {
        return segundos;
    }
}