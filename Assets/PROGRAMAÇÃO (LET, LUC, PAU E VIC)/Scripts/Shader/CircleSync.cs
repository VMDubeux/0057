using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSync : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");

    public Material WallMaterial;
    public Camera Camera;
    public LayerMask Mask;

    void Update()
    {
        // Calcular a dire��o e criar um raycast
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);
        bool hit = Physics.Raycast(ray, Mathf.Infinity, Mask);

        // Definir o tamanho do material entre 0 e 0.8 baseado na visibilidade
        float size = hit ? 0.8f : 0f;
        WallMaterial.SetFloat(SizeID, size);

        // Obter a posi��o do player no viewport
        Vector3 viewportPosition = Camera.WorldToViewportPoint(transform.position);

        // Ajustar a posi��o para garantir que x e y sejam 0, e z seja a posi��o real
        viewportPosition.x = 0f;
        viewportPosition.y = 0.15f;

        // Configurar a posi��o no material
        WallMaterial.SetVector(PosID, viewportPosition);
    }
}
