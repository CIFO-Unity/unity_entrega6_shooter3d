using UnityEngine;
using UnityEngine.AI;

public class ManKiller : MonoBehaviour
{

    private GameObject fpsController;
    private bool bloquearAtaque;
    private int ataqueAleatorio;
    private Vector3 posFPS;

    private bool bloquearEnemigoMuerto;
    //distancia al jugador
    private float distancia;
    [Header("Ajustes ManKiller")]
    //distancia para que el enemigo se active y persiga al jugador
    [SerializeField]
    private float distanciaAlertaManKiller = 10.0f;
    //velocidad al andar de ManKiller
    [SerializeField]
    private float velocidadManKillerAndando = 3.5f;

    [SerializeField]
    private EnemiesManager enemiesManager; // Referencia al manager de enemigos

    private void DesbloquearAtaque()
    {
        bloquearAtaque = false;
        bloquearEnemigoMuerto = false;
    }

    void Start()
    {
        //instancia del jugador
        fpsController = GameObject.FindWithTag("Player");
        bloquearAtaque = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bloquearEnemigoMuerto)
        {
            //calculamos distancia entre el enemigo y el jugador
            distancia = Vector3.Distance(this.gameObject.transform.position, fpsController.transform.position);

            //posicion del jugador pero con la y del enemigo para que no se incline al mirar
            posFPS = new Vector3(fpsController.transform.position.x, this.gameObject.transform.position.y, fpsController.transform.position.z);
            //Miramos siempre al jugador
            this.gameObject.transform.LookAt(posFPS);
            if (bloquearAtaque == false)
            {
                if (distancia < 4.0f)
                {
                    ataqueAleatorio = Random.Range(0, 2);
                    bloquearAtaque = true;
                    //print("distancia: " + distancia);
                    //ajustamos velocidad del enemigo
                    this.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f;

                    if (ataqueAleatorio == 0)
                    {
                        //cambiar animacion para que entre el AttackDouble
                        this.gameObject.GetComponent<Animator>().SetTrigger("AttackLeftHandManKiller");
                        Invoke("DesbloquearAtaque", 2.8f);
                    }
                    else
                    {
                        //cambiar animacion para que entre el Attack_ManKiller
                        this.gameObject.GetComponent<Animator>().SetTrigger("Attack_ManKiller");
                        Invoke("DesbloquearAtaque", 2.5f);
                    }


                }
                else if (distancia < distanciaAlertaManKiller)
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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bala")
        {
            bloquearEnemigoMuerto = true;
            //cambiar animacion para que entre el morir
            this.gameObject.GetComponent<Animator>().SetTrigger("DieManKiller");
            //desactivamos collider para no empujar cadaver
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            //Destruimos la bala tras el impacto
            //Destroy(other.gameObject, 0.0f);

            // Notificar a EnemiesManager que se ha destruido un enemigo
            if (enemiesManager != null)
                enemiesManager.ActualizarNumeroEnemigosMuertos();
        }
    }
}
