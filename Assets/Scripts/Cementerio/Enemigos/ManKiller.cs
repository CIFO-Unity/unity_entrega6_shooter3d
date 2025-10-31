using UnityEngine;
using UnityEngine.AI;

public class ManKiller : MonoBehaviour
{

    private GameObject fpsController;
    private float distancia;
    //distancia para que el enemigo se active y persiga al jugador
    private float distanciaAlerta = 10.0f;

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
        if(distancia < distanciaAlerta)
        {
            //el enemigo se mueve hacia el jugador según la distancia puesta
            this.gameObject.GetComponent<NavMeshAgent>().SetDestination(fpsController.transform.position);
        }
   }
}
