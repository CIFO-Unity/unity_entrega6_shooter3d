using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHighlighter : MonoBehaviour
{
    [Header("⚙️ Configuración")]
    [SerializeField] private Color activeColor = Color.yellow;  // Color del resplandor
    [SerializeField] private float interval = 1f;               // Tiempo entre cambios
    [SerializeField] private GameObject glowEffectPrefab;       // Prefab de partícula o luz (opcional)

    private List<Collider> colliders = new List<Collider>();
    private int currentIndex = -1;
    private GameObject currentGlow;

    private void Start()
    {
        // 🔎 Obtiene todos los colliders del GameObject
        Collider[] all = GetComponents<Collider>();

        // Ignora el primero y guarda el resto
        for (int i = 1; i < all.Length; i++)
            colliders.Add(all[i]);

        if (colliders.Count > 0)
            StartCoroutine(SwapActiveCollider());
        else
            Debug.LogWarning($"{name} no tiene suficientes colliders para alternar.");
    }

    private IEnumerator SwapActiveCollider()
    {
        while (true)
        {
            if (colliders.Count == 0) yield break;

            // 🧭 Apagar efecto anterior
            if (currentGlow != null)
                Destroy(currentGlow);

            // 📦 Nuevo índice aleatorio (diferente al anterior)
            int newIndex;
            do
            {
                newIndex = Random.Range(0, colliders.Count);
            } while (newIndex == currentIndex);

            currentIndex = newIndex;
            Collider selected = colliders[currentIndex];

            // 💡 Si hay un prefab de partícula, se instancia sobre el collider activo
            if (glowEffectPrefab != null)
            {
                currentGlow = Instantiate(glowEffectPrefab, selected.bounds.center, Quaternion.identity);
                currentGlow.transform.SetParent(transform); // para que se mueva con el enemigo
            }

            // 🧱 (Opcional) Dibujar un color de depuración visible en modo Scene
            Debug.DrawLine(selected.bounds.center, selected.bounds.center + Vector3.up * 0.5f, activeColor, interval);

            yield return new WaitForSeconds(interval);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (colliders == null || colliders.Count == 0) return;

        for (int i = 0; i < colliders.Count; i++)
        {
            Gizmos.color = (i == currentIndex) ? activeColor : Color.gray;
            Gizmos.DrawWireCube(colliders[i].bounds.center, colliders[i].bounds.size);
        }
    }
#endif
}

