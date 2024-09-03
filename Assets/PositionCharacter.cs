using UnityEngine;

public class PositionCharacter : MonoBehaviour
{
    public Camera mainCamera;         // Refer�ncia para a c�mera principal
    public float distanceFromCamera = 10f; // Dist�ncia do personagem � c�mera
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
            Debug.LogError("C�mera principal n�o atribu�da.");
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Atualiza a posi��o do personagem para sempre estar na extremidade inferior direita
            PositionCharacterInScreenBottomRight();
        }
    }

    void PositionCharacterInScreenBottomRight()
    {
        if (mainCamera != null)
        {
            // Calcula a posi��o no canto inferior direito da tela em coordenadas de viewport
            Vector3 screenPosition = new Vector3(xPos, 0, distanceFromCamera);

            // Converte as coordenadas de viewport para coordenadas de mundo
            Vector3 worldPosition = mainCamera.ViewportToWorldPoint(screenPosition);

            // Ajusta a posi��o do personagem
            transform.position = worldPosition;

            // Opcional: ajuste a posi��o para garantir que o personagem fique totalmente vis�vel na tela
            // Isso pode depender do tamanho do personagem, ent�o ajuste conforme necess�rio
        }
    }
}