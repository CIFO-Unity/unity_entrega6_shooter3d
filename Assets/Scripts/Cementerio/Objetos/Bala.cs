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
        if (other.gameObject.tag == "Escenario")
            impactoClon = (GameObject)Instantiate(impacto, this.gameObject.transform.position, Quaternion.identity);
        else if (other.gameObject.tag == "Man_Killer")
            impactoClon = (GameObject)Instantiate(impactoEnemigo, this.gameObject.transform.position, Quaternion.identity);

        DestruirBala(); // Mejor efecto visual; de la otra manera, las balas rebotan con físicas y parece raro :P
    }
}
