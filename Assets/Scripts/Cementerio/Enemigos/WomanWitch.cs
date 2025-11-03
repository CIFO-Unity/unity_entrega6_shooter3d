using System.Collections;
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
    private GameObject FX_Fire_06_2;

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
            /*
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

            }*/
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

            // Reproducir sonido
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("RecibirGolpeEnemigo");

            // Llamar a DestruirBala() si la bala tiene el script correspondiente
            Bala bala = other.gameObject.GetComponent<Bala>();
            if (bala != null)
                bala.DestruirBala();
        }
    }

    private void LanzamientoFireBall()//evento animacion
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

    private void LanzamientoKameame()//evento animacion
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

            FX_Fire_06_2.SetActive(true);
            //lo mantenemos en play tiempo
            StartCoroutine(StopFireAfter(1.5f, FX_Fire_06_2));
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

    private IEnumerator StopFireAfter(float time, GameObject fxFire)
    {
        // Espera el tiempo indicado
        yield return new WaitForSeconds(time);

        // Desactiva el GameObject
        fxFire.SetActive(false);
    }

    private bool brujaActiva = false; // evita lanzar múltiples coroutines

    // Llamar desde otro script para iniciar el comportamiento de ataque repetido
    public void ActivarBruja()
    {
        if (!brujaActiva)
        {
            brujaActiva = true;
            StartCoroutine(SeleccionarAtaques());
        }
    }

    // Coroutine que selecciona un ataque cada X segundos mientras la bruja esté viva
    private System.Collections.IEnumerator SeleccionarAtaques()
    {
        while (vidaWomanWitch > 0)
        {
            // Seleccionar ataque aleatorio (0,1,2)
            int ataqueSeleccionado = Random.Range(0, 3);

            switch (ataqueSeleccionado)
            {
                case 0:
                    this.gameObject.GetComponent<Animator>().SetTrigger("AttackPetanquero");
                    // Reproducir sonido de ataque (si existe)
                    if (SoundManager.Instance != null) SoundManager.Instance.PlaySound("BrujaAtaque");
                    break;
                case 1:
                    this.gameObject.GetComponent<Animator>().SetTrigger("AttackKameame");
                    if (SoundManager.Instance != null) SoundManager.Instance.PlaySound("BrujaAtaque");
                    break;
                case 2:
                    // Aquí podrías llamar a un método que haga el ataque de rayo
                    // por ejemplo: ActivarRayo();
                    break;
            }

            // Espera no bloqueante entre ataques
            yield return new WaitForSeconds(3f);
        }

        brujaActiva = false;
    }

}
