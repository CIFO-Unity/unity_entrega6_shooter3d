using UnityEngine;

/// <summary>
/// Genera un torus (donut) proceduralmente con bordes irregulares
/// </summary>
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TorusGenerator : MonoBehaviour
{
    [Header("Parámetros del Torus")]
    [Range(0.1f, 55f)]
    public float majorRadius = 2f; // Radio exterior
    
    [Range(0.1f, 2f)]
    public float minorRadius = 0.5f; // Grosor del tubo
    
    [Range(8, 64)]
    public int majorSegments = 24; // Segmentos alrededor del círculo principal
    
    [Range(8, 64)]
    public int minorSegments = 16; // Segmentos del tubo
    
    [Header("Irregularidades (Bordes arrugados)")]
    [Range(0f, 10f)]
    public float noiseStrength = 0.3f; // Qué tan arrugado
    
    [Range(0.1f, 50f)]
    public float noiseScale = 2f; // Escala del ruido
    
    [Header("Generación")]
    public bool generateOnStart = true;

    void Start()
    {
        if (generateOnStart)
        {
            GenerateTorus();
        }
    }

    [ContextMenu("Generate Torus")]
    public void GenerateTorus()
    {
        Mesh mesh = new Mesh();
        mesh.name = "ProceduralTorus";

        // Listas para vértices, normales, UVs y triángulos
        Vector3[] vertices = new Vector3[(majorSegments + 1) * (minorSegments + 1)];
        Vector3[] normals = new Vector3[vertices.Length];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[majorSegments * minorSegments * 6];

        // Generar vértices
        int vertIndex = 0;
        for (int i = 0; i <= majorSegments; i++)
        {
            float majorAngle = 2f * Mathf.PI * i / majorSegments;
            float cosMA = Mathf.Cos(majorAngle);
            float sinMA = Mathf.Sin(majorAngle);

            for (int j = 0; j <= minorSegments; j++)
            {
                float minorAngle = 2f * Mathf.PI * j / minorSegments;
                float cosMinA = Mathf.Cos(minorAngle);
                float sinMinA = Mathf.Sin(minorAngle);

                // Añadir ruido Perlin para irregularidades
                float noise = Perlin3D(i * noiseScale, j * noiseScale, 0) * noiseStrength;
                float radiusWithNoise = minorRadius + noise;

                // Calcular posición del vértice
                float x = (majorRadius + radiusWithNoise * cosMinA) * cosMA;
                float y = radiusWithNoise * sinMinA;
                float z = (majorRadius + radiusWithNoise * cosMinA) * sinMA;

                vertices[vertIndex] = new Vector3(x, y, z);

                // Calcular normal
                Vector3 center = new Vector3(majorRadius * cosMA, 0, majorRadius * sinMA);
                normals[vertIndex] = (vertices[vertIndex] - center).normalized;

                // UVs
                uvs[vertIndex] = new Vector2((float)i / majorSegments, (float)j / minorSegments);

                vertIndex++;
            }
        }

        // Generar triángulos
        int triIndex = 0;
        for (int i = 0; i < majorSegments; i++)
        {
            for (int j = 0; j < minorSegments; j++)
            {
                int current = i * (minorSegments + 1) + j;
                int next = current + minorSegments + 1;

                // Primer triángulo
                triangles[triIndex++] = current;
                triangles[triIndex++] = next;
                triangles[triIndex++] = current + 1;

                // Segundo triángulo
                triangles[triIndex++] = current + 1;
                triangles[triIndex++] = next;
                triangles[triIndex++] = next + 1;
            }
        }

        // Asignar al mesh
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();

        // Aplicar al MeshFilter
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Función de ruido Perlin 3D simplificada
    float Perlin3D(float x, float y, float z)
    {
        float xy = Mathf.PerlinNoise(x, y);
        float xz = Mathf.PerlinNoise(x, z);
        float yz = Mathf.PerlinNoise(y, z);
        return (xy + xz + yz) / 3f;
    }
}
