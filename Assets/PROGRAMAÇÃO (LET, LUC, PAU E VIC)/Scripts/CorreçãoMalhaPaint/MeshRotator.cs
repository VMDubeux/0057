using UnityEngine;
public class MeshRotator : MonoBehaviour
{
    public MeshFilter meshFilter;
    public void ApplyRotationToVertices()
    {
        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        for (int v = 0; v < vertices.Length; v++) vertices[v] = meshFilter.transform.TransformPoint(vertices[v]);
        meshFilter.transform.localRotation = Quaternion.identity;
        meshFilter.transform.localScale = Vector3.one;
        meshFilter.transform.localPosition = Vector3.zero;
        for (int v = 0; v < vertices.Length; v++) vertices[v] = meshFilter.transform.InverseTransformPoint(vertices[v]);
        meshFilter.sharedMesh.vertices = vertices;
        meshFilter.sharedMesh.RecalculateBounds();
    }
}