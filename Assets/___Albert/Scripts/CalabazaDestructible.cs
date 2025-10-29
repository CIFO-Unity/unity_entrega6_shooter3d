using UnityEngine;

public class CalabazaDestructible : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            Destroy(gameObject);
        }
    }
}
