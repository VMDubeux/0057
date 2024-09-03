using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessário para acessar o Canvas

public class CameraLayerController : MonoBehaviour
{
    public Camera mainCamera;            // Referência para a câmera principal
    public Canvas shopCanvas;            // Referência para o Canvas da UI da loja
    public string layerNameToToggle;     // Nome da camada a ser ativada/desativada

    private int layerToToggle;
    private int originalCullingMask;

    void Start()
    {
        if (mainCamera != null)
        {
            // Armazena a máscara de culling original da câmera
            originalCullingMask = mainCamera.cullingMask;
            // Converte o nome da camada para um número de camada
            layerToToggle = LayerMask.NameToLayer(layerNameToToggle);

            if (layerToToggle == -1)
            {
                Debug.LogError($"Layer '{layerNameToToggle}' não encontrada. Verifique se o nome da camada está correto.");
            }

            // Inicializa o estado da camada baseado na visibilidade inicial do Canvas
            if (shopCanvas != null)
            {
                bool isShopUIActive = shopCanvas.gameObject.activeInHierarchy;
                UpdateCameraCullingMask(isShopUIActive);
            }
            else
            {
                Debug.LogError("Canvas da loja não atribuído.");
            }
        }
        else
        {
            Debug.LogError("Câmera principal não atribuída.");
        }
    }

    void Update()
    {
        if (mainCamera != null && shopCanvas != null)
        {
            // Verifica se o Canvas da loja está ativo
            bool isShopUIActive = shopCanvas.gameObject.activeInHierarchy;

            // Atualiza a máscara de culling da câmera se o estado mudou
            UpdateCameraCullingMask(isShopUIActive);
        }
    }

    void UpdateCameraCullingMask(bool isShopUIActive)
    {
        if (mainCamera != null)
        {
            if (isShopUIActive)
            {
                // Ativa a camada
                mainCamera.cullingMask = originalCullingMask | (1 << layerToToggle);
            }
            else
            {
                // Desativa a camada após o final do frame atual
                StartCoroutine(DeactivateLayerAfterFrame());
            }
        }
    }

    IEnumerator DeactivateLayerAfterFrame()
    {
        // Aguarda o final do frame atual para garantir que a UI esteja completamente desativada
        yield return new WaitForEndOfFrame();

        // Atualiza a máscara de culling da câmera
        mainCamera.cullingMask = originalCullingMask & ~(1 << layerToToggle);
    }
}