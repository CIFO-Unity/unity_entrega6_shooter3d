using UnityEngine;

[CreateAssetMenu(fileName = "RecordTiempo", menuName = "Scriptable Objects/RecordTiempo")]
public class RecordTiempo : ScriptableObject
{
    [SerializeField] private int minutos;
    [SerializeField] private int segundos;

    public int Minutos
    {
        get => minutos;
        set => minutos = value;
    }

    public int Segundos
    {
        get => segundos;
        set => segundos = value;
    }
}
