using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField]
    private GameObject impacto;
    [SerializeField]
    private GameObject impactoEnemigo;
    private GameObject impactoClon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
            Destroy(this.gameObject, 1.0f);
        }
    }
}
