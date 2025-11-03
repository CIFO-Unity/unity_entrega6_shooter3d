using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FindMissingScripts
{
    [MenuItem("Tools/Find Missing Scripts in Project")] 
    public static void FindInProject()
    {
        int missingCount = 0;

        // Scan open scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (!scene.isLoaded) continue;
            var roots = scene.GetRootGameObjects();
            foreach (var root in roots)
            {
                missingCount += FindInGameObject(root, scene.path);
            }
        }

        // Scan all prefabs in the project
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;
            missingCount += FindInPrefab(prefab, path);
        }

        Debug.Log($"FindMissingScripts: scan finished, missing components found: {missingCount}");
    }

    private static int FindInPrefab(GameObject prefab, string assetPath)
    {
        int count = 0;
        var gos = new List<GameObject>();
        gos.Add(prefab);
        gos.AddRange(prefab.GetComponentsInChildren<Transform>(true).ConvertAll(t => t.gameObject));

        foreach (var go in gos)
        {
            var comps = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
            if (comps > 0)
            {
                Debug.LogWarning($"Missing scripts found in Prefab '{assetPath}' -> GameObject '{GetGameObjectPath(go, prefab.name)}' (missing components: {comps})");
                count += comps;
            }
        }
        return count;
    }

    private static int FindInGameObject(GameObject go, string scenePath)
    {
        int count = 0;
        var gos = go.GetComponentsInChildren<Transform>(true);
        foreach (var t in gos)
        {
            var obj = t.gameObject;
            var comps = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(obj);
            if (comps > 0)
            {
                string fullPath = string.IsNullOrEmpty(scenePath) ? obj.name : scenePath + " -> " + GetGameObjectPath(obj, obj.scene.name);
                Debug.LogWarning($"Missing scripts found in Scene '{scenePath}' -> GameObject '{GetGameObjectPath(obj, obj.name)}' (missing components: {comps})");
                count += comps;
            }
        }
        return count;
    }

    private static string GetGameObjectPath(GameObject go, string rootName)
    {
        string path = go.name;
        Transform t = go.transform.parent;
        while (t != null && t.gameObject.name != rootName)
        {
            path = t.gameObject.name + "/" + path;
            t = t.parent;
        }
        return path;
    }
}
