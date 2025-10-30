using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

// Herramienta simple para encontrar componentes faltantes (Missing Scripts) en la escena abierta.
// Menú: Tools -> Find Missing Scripts In Scene
public class FindMissingScriptsInScene : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts In Scene")]
    public static void ShowWindow()
    {
        FindMissing();
    }

    public static void FindMissing()
    {
        int goCount = 0;
        int componentsCount = 0;
        int missingCount = 0;

        // Recorre todos los GameObjects de las escenas cargadas
        for (int s = 0; s < UnityEngine.SceneManagement.SceneManager.sceneCount; s++)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(s);
            if (!scene.isLoaded) continue;

            var roots = scene.GetRootGameObjects();
            foreach (var root in roots)
            {
                var all = root.GetComponentsInChildren<Transform>(true);
                foreach (var t in all)
                {
                    var go = t.gameObject;
                    goCount++;
                    var comps = go.GetComponents<Component>();
                    componentsCount += comps.Length;
                    for (int i = 0; i < comps.Length; i++)
                    {
                        if (comps[i] == null)
                        {
                            missingCount++;
                            Debug.Log($"Missing script in scene '{scene.name}' on GameObject: {GetGameObjectPath(go)}", go);
                        }
                    }
                }
            }
        }

        Debug.Log($"FindMissingScripts: scanned {goCount} GameObjects, {componentsCount} components, found {missingCount} missing components.");
        if (missingCount == 0) EditorUtility.DisplayDialog("FindMissingScripts", "No missing scripts found in open scenes.", "OK");
        else EditorUtility.DisplayDialog("FindMissingScripts", $"Found {missingCount} missing components. See Console for details.", "OK");
    }

    static string GetGameObjectPath(GameObject go)
    {
        string path = go.name;
        var t = go.transform;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }
}
