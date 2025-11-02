using UnityEngine;

public class Boy_Ghost : MonoBehaviour
{
    private GameObject fpsController;
    private bool bloquearAtaque;
    private int ataqueAleatorio;
    private Vector3 posFPS;

    private bool bloquearEnemigoMuerto;
    //distancia al jugador
    private float distancia;
    [Header("Ajustes Boy_Ghost")]
    //Vida Boy_Ghost
    [Range(0, 10)]
    [SerializeField]
    private int vidaBoy_Ghost = 10;
    //distancia para que el enemigo se active y persiga al jugador
    [SerializeField]
    private float distanciaAlertaBoy_Ghost = 10.0f;
    //velocidad al andar de Boy_Ghost
    [SerializeField]
    private float velocidadBoy_GhostCorriendo = 3.5f;

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
                if (distancia < distanciaAlertaBoy_Ghost)
                {
                    //ajustamos velocidad del enemigo
                    this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = velocidadBoy_GhostCorriendo;
                    //cambiar animacion para que entre el correr
                    this.gameObject.GetComponent<Animator>().SetTrigger("RunBoyGhost");
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bala")
        {

            vidaBoy_Ghost -= 1;
            if (vidaBoy_Ghost <= 0)
            {
                bloquearEnemigoMuerto = true;
                //cambiar animacion para que entre el morir
                this.gameObject.GetComponent<Animator>().SetTrigger("DieBoyGhost");
                //desactivamos collider para no empujar cadaver
                this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                // Notificar a EnemiesManager que se ha destruido un enemigo
                if (enemiesManager != null)
                    enemiesManager.ActualizarNumeroEnemigosMuertos();
            }


            // Llamar a DestruirBala() si la bala tiene el script correspondiente
            Bala bala = other.gameObject.GetComponent<Bala>();
            if (bala != null)
                bala.DestruirBala();

            // Reproducir sonido
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("ManKillerMuerte");


        }
    }
}
