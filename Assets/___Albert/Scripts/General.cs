using UnityEngine;

public class General
{
    private static int numeroEnemigosMuertos = 0;

    public static void ActualizarNumeroEnemigosMuertos()
    {
        numeroEnemigosMuertos++;
    }

    public static int NumeroEnemigosMuertos()
    {
        return numeroEnemigosMuertos;
    }
}
