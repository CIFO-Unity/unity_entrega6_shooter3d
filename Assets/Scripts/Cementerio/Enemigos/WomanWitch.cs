using System.Runtime.InteropServices;
using System.Runtime.Serialization;
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

    [Header("Fireball")]
    [SerializeField]
    private Transform handFireballTransform;

    [SerializeField]
    private GameObject fireballPrefab; // prefab del proyectil (debe tener Rigidbody y partículas dentro)

    [SerializeField]
    private GameObject iceballPrefab;

    [SerializeField]
    private float fireballSpeed = 12f;

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
                            SoundManager.Instance.PlaySound("BrujaAtaque");
                    }
                    else
                    {
                        //cambiar animacion para que entre el Attack_ManKiller
                        this.gameObject.GetComponent<Animator>().SetTrigger("AttackKameame");
                        Invoke("DesbloquearAtaque", 2.5f);

                        // Reproducir sonido
                        if (SoundManager.Instance != null)
                            SoundManager.Instance.PlaySound("BrujaAtaque");
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
            if (vidaWomanWitch <= 0 && !bloquearEnemigoMuerto)
            {
                bloquearEnemigoMuerto = true;
                //cambiar animacion para que entre el morir
                this.gameObject.GetComponent<Animator>().SetTrigger("DieWomanWitch");
                //desactivamos collider para no empujar cadaver
                this.gameObject.GetComponent<CapsuleCollider>().enabled = false;

                // Reproducir sonido
                if (SoundManager.Instance != null)
                    SoundManager.Instance.PlaySound("BrujaMuerte");

                // Notificar al Player que ha ganado
                if (fpsController != null)
                {
                    Player playerScript = fpsController.GetComponent<Player>();
                    if (playerScript != null)
                    {
                        playerScript.Ganar();
                    }
                }
            }
            else
            {
                // Reproducir sonido
                if (SoundManager.Instance != null)
                    SoundManager.Instance.PlaySound("RecibirGolpeEnemigo");
            }


            // Llamar a DestruirBala() si la bala tiene el script correspondiente
            Bala bala = other.gameObject.GetComponent<Bala>();
            if (bala != null)
                bala.DestruirBala();
        }
    }

    /*private void OnTriggerEnter(Collider other)
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

                // Reproducir sonido
                if (SoundManager.Instance != null)
                    SoundManager.Instance.PlaySound("BrujaMuerte");

                // Notificar al Player que ha ganado
                if (fpsController != null)
                {
                    Player playerScript = fpsController.GetComponent<Player>();
                    if (playerScript != null)
                    {
                        playerScript.Ganar();
                    }
                }
            }
            else
            {
                // Reproducir sonido
                if (SoundManager.Instance != null)
                    SoundManager.Instance.PlaySound("RecibirGolpeEnemigo");
            }


            // Llamar a DestruirBala() si la bala tiene el script correspondiente
            Bala bala = other.gameObject.GetComponent<Bala>();
            if (bala != null)
                bala.DestruirBala();
        }
    }*/

    private void LanzamientoFireBall()
    {
        // Posición objetivo (jugador) — preferimos la cámara del jugador si existe para apuntar al torso/ojos
        Vector3 targetPos;
        Camera playerCam = Camera.main;
        if (playerCam != null)
            targetPos = playerCam.transform.position;
        else
            targetPos = fpsController.transform.position;

        // Usar la referencia de la mano si está asignada (posición exacta de la bola en la mano)
        Vector3 spawnPos = (handFireballTransform != null) ? handFireballTransform.position : (transform.position + transform.forward * 1f);
        Quaternion spawnRot = (handFireballTransform != null) ? handFireballTransform.rotation : transform.rotation;

        // Dirección normalizada hacia el jugador
        Vector3 direction = (targetPos - spawnPos).normalized;

        // Instanciar el proyectil en la posición de la mano y aplicarle velocidad
        if (fireballPrefab != null)
        {
            // Instanciamos orientado hacia el jugador para que la dirección esté alineada
            GameObject proj = Instantiate(fireballPrefab, spawnPos, Quaternion.LookRotation(direction));

            // Si el prefab tiene ParticleSystems hijos, hacemos que se reproduzcan (asumiendo que están correctamente configurados)
            var childPS = proj.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var ps in childPS)
            {
                ps.Play();
            }

            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false; // por defecto evitar que caiga; si quieres arco, cambia esto en Inspector
                rb.linearVelocity = direction * fireballSpeed;
            }
        }
    }

    private void LanzamientoKameame()
    {
        // Posición objetivo (jugador) — preferimos la cámara del jugador si existe para apuntar al torso/ojos
        Vector3 targetPos;
        Camera playerCam = Camera.main;
        if (playerCam != null)
            targetPos = playerCam.transform.position;
        else
            targetPos = fpsController.transform.position;

        // Usar la referencia de la mano si está asignada (posición exacta de la bola en la mano)
        Vector3 spawnPos = (handFireballTransform != null) ? handFireballTransform.position : (transform.position + transform.forward * 1f);
        Quaternion spawnRot = (handFireballTransform != null) ? handFireballTransform.rotation : transform.rotation;

        // Dirección normalizada hacia el jugador
        Vector3 direction = (targetPos - spawnPos).normalized;

        // Instanciar el proyectil en la posición de la mano y aplicarle velocidad
        if (iceballPrefab != null)
        {
            // Instanciamos orientado hacia el jugador para que la dirección esté alineada
            GameObject proj = Instantiate(iceballPrefab, spawnPos, Quaternion.LookRotation(direction));

            // Si el prefab tiene ParticleSystems hijos, hacemos que se reproduzcan (asumiendo que están correctamente configurados)
            var childPS = proj.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var ps in childPS)
            {
                ps.Play();
            }

            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false; // por defecto evitar que caiga; si quieres arco, cambia esto en Inspector
                rb.linearVelocity = direction * fireballSpeed;
            }
        }
    }

    public void ActivarBruja()
    {
        // Lógica para activar la bruja

        // Cuando el Player entra al ring de la bruja, ésta debe activarse y empezar a atacarle
        print("Bruja activada");
    }

}
