using UnityEngine;
using UnityEngine.AI;

public class ManKiller : MonoBehaviour
{

    private GameObject fpsController;
    //distancia al jugador
    private float distancia;
    [Header("Ajustes ManKiller")]
    //distancia para que el enemigo se active y persiga al jugador
    [SerializeField]
    private float distanciaAlerta = 10.0f;
    //velocidad al andar de ManKiller
    [SerializeField]
    private float velocidadManKillerAndando = 3.5f;

    void Start()
    {
        //instancia del jugador
        fpsController = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //calculamos distancia entre el enemigo y el jugador
        distancia = Vector3.Distance(this.gameObject.transform.position, fpsController.transform.position);
        //print("Distancia: " + distancia);
        if (distancia < distanciaAlerta)
        {
            //cambiar animacion para que entre el caminar
            this.gameObject.GetComponent<Animator>().SetFloat("Walking_ManKiller", 1.0f);
            //el enemigo se mueve hacia el jugador según la distancia puesta
            this.gameObject.GetComponent<NavMeshAgent>().SetDestination(fpsController.transform.position);
            //ajustamos velocidad del enemigo
            this.gameObject.GetComponent<NavMeshAgent>().speed = velocidadManKillerAndando;
        }
        else
        {
            //cambiar animacion para que entre el idle
            this.gameObject.GetComponent<Animator>().SetFloat("Walking_ManKiller", 0.0f);
            //ponemos a cero la velocidad del enemigo
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f;
        }
   }
}
