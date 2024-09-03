using UnityEngine;

public class PositionCharacter : MonoBehaviour
{
    public Camera mainCamera;         // Referência para a câmera principal
    public float distanceFromCamera = 10f; // Distância do personagem à câmera
    public float xPos;

    void Start()
    {
        if (mainCamera != null)
        {
            // Inicialmente posiciona o personagem na extremidade inferior direita
            PositionCharacterInScreenBottomRight();
        }
        else
        {
            Debug.LogError("Câmera principal não atribuída.");
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Atualiza a posição do personagem para sempre estar na extremidade inferior direita
            PositionCharacterInScreenBottomRight();
        }
    }

    void PositionCharacterInScreenBottomRight()
    {
        if (mainCamera != null)
        {
            // Calcula a posição no canto inferior direito da tela em coordenadas de viewport
            Vector3 screenPosition = new Vector3(xPos, 0, distanceFromCamera);

            // Converte as coordenadas de viewport para coordenadas de mundo
            Vector3 worldPosition = mainCamera.ViewportToWorldPoint(screenPosition);

            // Ajusta a posição do personagem
            transform.position = worldPosition;

            // Opcional: ajuste a posição para garantir que o personagem fique totalmente visível na tela
            // Isso pode depender do tamanho do personagem, então ajuste conforme necessário
        }
    }
}