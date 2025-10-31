using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private int numeroEnemigosMuertos = 0;

    [SerializeField]
    private Llave llave1;

    public void ActualizarNumeroEnemigosMuertos()
    {
        numeroEnemigosMuertos++;

        if(numeroEnemigosMuertos == 10 && llave1 != null)
            llave1.MostrarLlave();
    }

    public int NumeroEnemigosMuertos()
    {
        return numeroEnemigosMuertos;
    }
}
