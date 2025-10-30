using UnityEngine;

public class CalabazaDestructible : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound("DestruirCalabaza");

            Destroy(gameObject);
        }
    }
}
