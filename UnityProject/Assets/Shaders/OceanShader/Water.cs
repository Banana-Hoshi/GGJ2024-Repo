using System;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Water : MonoBehaviour
{
    public int planeLength = 10;
    public int quadRes = 10;

    private void Awake()
    {
        Generate();
    }

    private Vector3[] vertices;
    private Mesh mesh;

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Water";
        mesh.indexFormat = IndexFormat.UInt32;

        float halfLength = planeLength * 0.5f;
        int sideVertCount = planeLength * quadRes;

        vertices = new Vector3[(sideVertCount + 1) * (sideVertCount + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int i = 0, x = 0; x <= sideVertCount; ++x)
        {
            for (int z = 0; z <= sideVertCount; ++z, ++i)
            {
                vertices[i] = new Vector3(((float)x / sideVertCount * planeLength) - halfLength, 0, ((float)z / sideVertCount * planeLength) - halfLength);
                uv[i] = new Vector2((float)x / sideVertCount, (float)z / sideVertCount);
                tangents[i] = tangent;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int[] triangles = new int[sideVertCount * sideVertCount * 6];

        for (int ti = 0, vi = 0, x = 0; x < sideVertCount; ++vi, ++x)
        {
            for (int z = 0; z < sideVertCount; ti += 6, ++vi, ++z)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = vi + 1;
                triangles[ti + 2] = vi + sideVertCount + 2;
                triangles[ti + 3] = vi;
                triangles[ti + 4] = vi + sideVertCount + 2;
                triangles[ti + 5] = vi + sideVertCount + 1;
            }
        }

        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        Vector3[] normals = mesh.normals;

        Vector3[] displacedVertices = new Vector3[vertices.Length];
        Array.Copy(vertices, 0, displacedVertices, 0, vertices.Length);
        Vector3[] displacedNormals = new Vector3[normals.Length];
        Array.Copy(normals, 0, displacedNormals, 0, normals.Length);
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
    }
}
