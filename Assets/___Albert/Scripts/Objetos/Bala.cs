using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField]
    private GameObject impacto;
    [SerializeField]
    private GameObject impactoEnemigo;
    private GameObject impactoClon;

    public void DestruirBala()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Terreno") // OJO FALTA PONERLE EL TAG
        {
            impactoClon = (GameObject)Instantiate(impacto, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 1.0f);
        }
        else if (other.gameObject.tag == "Enemigo") // OJO FALTAN LOS ENEMIGOS
        {
            impactoClon = (GameObject)Instantiate(impactoEnemigo, this.gameObject.transform.position, Quaternion.identity);
            DestruirBala();
        }
    }
}
