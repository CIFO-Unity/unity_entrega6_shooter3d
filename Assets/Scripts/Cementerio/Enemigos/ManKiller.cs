using UnityEngine;
using UnityEngine.AI;

public class ManKiller : MonoBehaviour
{

private GameObject fpsController;

    void Start()
    {
        fpsController = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<NavMeshAgent>().SetDestination(fpsController.transform.position);
    }
}
