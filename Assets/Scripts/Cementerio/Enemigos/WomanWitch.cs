using System.Runtime.InteropServices;
using UnityEngine;

public class WomanWitch : MonoBehaviour
{
    private GameObject fpsController;
    private bool bloquearAtaque;
    private int ataqueAleatorio;
    private Vector3 posFPS;

    private bool bloquearEnemigoMuerto;
    //distancia al jugador
    private float distancia;
    [Header("Ajustes WomanWitch")]
    //Vida WomanWitch
    [Range(0, 50)]
    [SerializeField]
    private int vidaWomanWitch = 10;

    [Header("Particle Systems")]
    [SerializeField]
    private GameObject particleImpactWomanWitchPrefab;

    [Header("Contar enemigos muertos")]
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
                if (distancia < 4.0f)
                {
                    ataqueAleatorio = Random.Range(0, 2);
                    bloquearAtaque = true;
                    //print("distancia: " + distancia);
                    //ajustamos velocidad del enemigo
                    this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0.0f;

                    if (ataqueAleatorio == 0)
                    {
                        //cambiar animacion para que entre el AttackDouble
                        this.gameObject.GetComponent<Animator>().SetTrigger("AttackPetanquero");
                        Invoke("DesbloquearAtaque", 2.8f);

                        // Reproducir sonido
                        if (SoundManager.Instance != null)
                            SoundManager.Instance.PlaySound("ManKillerAtaque");
                    }
                    else
                    {
                        //cambiar animacion para que entre el Attack_ManKiller
                        this.gameObject.GetComponent<Animator>().SetTrigger("AttackKameame");
                        Invoke("DesbloquearAtaque", 2.5f);

                        // Reproducir sonido
                        if (SoundManager.Instance != null)
                            SoundManager.Instance.PlaySound("ManKillerAtaque");
                    }


                }

            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bala")
        {

            vidaWomanWitch -= 1;
            if (vidaWomanWitch <= 0)
            {
                bloquearEnemigoMuerto = true;
                //cambiar animacion para que entre el morir
                this.gameObject.GetComponent<Animator>().SetTrigger("DieWomanWitch");
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

    private void LanzamientoFireBall()
    {
        //guardar location player
        Vector3 posplayer = fpsController.transform.position;
        Quaternion rotplayerplayer = fpsController.transform.rotation;
        //guardar location woman witch
        Vector3 pos = this.gameObject.transform.position;
        Quaternion rot = this.gameObject.transform.rotation;
        //instanciar partículas
        if (particleImpactWomanWitchPrefab != null)
            Instantiate(particleImpactWomanWitchPrefab, pos, rot);
            //Lanzar con fuerza hacia la direccion de mirada del jugador
            Vector3 direccionLanzamiento = (posplayer - pos).normalized;
    }
}
