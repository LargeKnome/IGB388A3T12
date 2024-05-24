using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SpotlightMesh : MonoBehaviour
{
    public Light spotlight;

    public MeshCollider meshCollider;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        if (spotlight == null || spotlight.type != LightType.Spot)
        {
            Debug.LogError("Please assign a spotlight.");
            return;
        }

        CreateSpotlightMesh();
    }

    private void CreateSpotlightMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        float angle = spotlight.spotAngle;
        float range = spotlight.range;
        int segments = 20; // Number of segments around the spotlight cone

        float halfAngle = angle * 0.5f * Mathf.Deg2Rad;
        float radius = range * Mathf.Tan(halfAngle);

        Vector3[] vertices = new Vector3[segments + 2];
        vertices[0] = Vector3.zero; // The tip of the cone

        // Calculate vertices for the base of the cone
        for (int i = 0; i <= segments; i++)
        {
            float theta = i * 2.0f * Mathf.PI / segments;
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            float z = radius * Mathf.Sin(theta);
            vertices[i + 1] = new Vector3(x, y, range);
        }

        int[] triangles = new int[segments * 3];

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;

        meshCollider.sharedMesh = mesh;
    }
}
