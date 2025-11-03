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
    //Vida ManKiller
    [Range(0, 10)]
    [SerializeField]
    private int vidaManKiller = 10;
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
        //bloquearEnemigoMuerto = false;
    }

    void Start()
    {
        //instancia del jugador
        fpsController = GameObject.FindWithTag("Player");
        bloquearAtaque = false;




            Collider colJugador = fpsController.GetComponent<Collider>();
    Collider colEnemigo = GetComponent<Collider>();

    if (colJugador != null && colEnemigo != null)
        Physics.IgnoreCollision(colEnemigo, colJugador);

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

                        // Reproducir sonido
                        if (SoundManager.Instance != null)
                            SoundManager.Instance.PlaySound("ManKillerAtaque");
                    }
                    else
                    {
                        //cambiar animacion para que entre el Attack_ManKiller
                        this.gameObject.GetComponent<Animator>().SetTrigger("Attack_ManKiller");
                        Invoke("DesbloquearAtaque", 2.5f);

                        // Reproducir sonido
                        if (SoundManager.Instance != null)
                            SoundManager.Instance.PlaySound("ManKillerAtaque");
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

            vidaManKiller -= 1;
            if (vidaManKiller <= 0)
            {
                bloquearEnemigoMuerto = true;
                //cambiar animacion para que entre el morir
                this.gameObject.GetComponent<Animator>().SetTrigger("DieManKiller");
                //desactivamos collider para no empujar cadaver
                this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                // Notificar a EnemiesManager que se ha destruido un enemigo
                if (enemiesManager != null)
                    enemiesManager.ActualizarNumeroEnemigosMuertos();

                // Reproducir sonido
                if (SoundManager.Instance != null)
                    SoundManager.Instance.PlaySound("ManKillerMuerte");
            }

            // Reproducir sonido
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("RecibirGolpeEnemigo");

            // Llamar a DestruirBala() si la bala tiene el script correspondiente
            Bala bala = other.gameObject.GetComponent<Bala>();
            if (bala != null)
                bala.DestruirBala();            
        }
    }
}
