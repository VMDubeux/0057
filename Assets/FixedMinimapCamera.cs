using UnityEngine;

public class FixedMinimapCamera : MonoBehaviour
{
    public Transform player; // Referência ao personagem que a câmera deve seguir

    private Camera minimapCamera; // Referência à câmera minimapa

    void Start()
    {
        // Obtenha a referência da câmera minimapa
        minimapCamera = gameObject.GetComponent<Camera>();

        // Certifique-se de que a rotação da câmera está configurada para a visão desejada
        minimapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        if (player != null)
        {
            // Atualize a posição da câmera para seguir o personagem, mas mantenha a rotação
            Vector3 newPosition = player.position;
            newPosition.y = minimapCamera.transform.position.y; // Mantém a altura da câmera constante
            minimapCamera.transform.position = newPosition;

            // Mantenha a rotação da câmera fixa
            minimapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}

