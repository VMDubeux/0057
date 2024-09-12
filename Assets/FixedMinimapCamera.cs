using UnityEngine;

public class FixedMinimapCamera : MonoBehaviour
{
    public Transform player; // Refer�ncia ao personagem que a c�mera deve seguir

    private Camera minimapCamera; // Refer�ncia � c�mera minimapa

    void Start()
    {
        // Obtenha a refer�ncia da c�mera minimapa
        minimapCamera = gameObject.GetComponent<Camera>();

        // Certifique-se de que a rota��o da c�mera est� configurada para a vis�o desejada
        minimapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        if (player != null)
        {
            // Atualize a posi��o da c�mera para seguir o personagem, mas mantenha a rota��o
            Vector3 newPosition = player.position;
            newPosition.y = minimapCamera.transform.position.y; // Mant�m a altura da c�mera constante
            minimapCamera.transform.position = newPosition;

            // Mantenha a rota��o da c�mera fixa
            minimapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}

