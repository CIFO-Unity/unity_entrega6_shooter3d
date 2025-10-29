using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int vida = 10;

    [SerializeField]
    private int vidaMaxima = 10;

    [SerializeField]
    private int municion = 50;

    [SerializeField]
    private int municionMaxima = 100;


    #region Getters & Setters

    // Getter y Setter para Vida
    public int Vida
    {
        get { return vida; }
        set { vida = Mathf.Clamp(value, 0, vidaMaxima); } // entre 0 y vidaMaxima
    }

    // Getter y Setter para Municion
    public int Municion
    {
        get { return municion; }
        set { municion = Mathf.Clamp(value, 0, municionMaxima); } // entre 0 y municionMaxima
    }

    #endregion

    #region Start & Update
    void Start()
    {
        //Debug.Log($"Vida inicial: {vida}/{vidaMaxima}, Munición inicial: {municion}/{municionMaxima}");
    }

    void Update()
    {
        // Ejemplo de prueba:
        // if (Input.GetKeyDown(KeyCode.Space)) Vida -= 1;
        // if (Input.GetKeyDown(KeyCode.M)) Municion += 10;
    }

    #endregion

    #region Vida

    // 🔹 Nueva función para añadir vida
    public void AnadirVida(int cantidad)
    {
        if (cantidad > 0)
        {
            Vida += cantidad; // Usa el setter, así se aplica Clamp automáticamente
            //Debug.Log($"Vida aumentada: {vida}/{vidaMaxima}");
        }
    }

    public void RestarVida(int cantidad)
    {
        if (cantidad > 0)
        {
            Vida -= cantidad; // Usa el setter, así se aplica Clamp automáticamente
            //Debug.Log($"Vida reducida: {vida}/{vidaMaxima}");
        }
    }

    #endregion

    #region Munición

    public void AnadirMunicion(int cantidad)
    {
        if (cantidad > 0)
        {
            Municion += cantidad; // Usa el setter, así se aplica Clamp automáticamente
            //Debug.Log($"Munición aumentada: {municion}/{municionMaxima}");
        }
    }

    public void RestarMunicion(int cantidad)
    {
        if (cantidad > 0)
        {
            Municion -= cantidad; // Usa el setter, así se aplica Clamp automáticamente
            //Debug.Log($"Munición reducida: {municion}/{municionMaxima}");
        }
    }

    #endregion
}
