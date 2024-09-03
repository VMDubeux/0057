using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necess�rio para acessar o Canvas

public class CameraLayerController : MonoBehaviour
{
    public Camera mainCamera;            // Refer�ncia para a c�mera principal
    public Canvas shopCanvas;            // Refer�ncia para o Canvas da UI da loja
    public string layerNameToToggle;     // Nome da camada a ser ativada/desativada

    private int layerToToggle;
    private int originalCullingMask;

    void Start()
    {
        if (mainCamera != null)
        {
            // Armazena a m�scara de culling original da c�mera
            originalCullingMask = mainCamera.cullingMask;
            // Converte o nome da camada para um n�mero de camada
            layerToToggle = LayerMask.NameToLayer(layerNameToToggle);

            if (layerToToggle == -1)
            {
                Debug.LogError($"Layer '{layerNameToToggle}' n�o encontrada. Verifique se o nome da camada est� correto.");
            }

            // Inicializa o estado da camada baseado na visibilidade inicial do Canvas
            if (shopCanvas != null)
            {
                bool isShopUIActive = shopCanvas.gameObject.activeInHierarchy;
                UpdateCameraCullingMask(isShopUIActive);
            }
            else
            {
                Debug.LogError("Canvas da loja n�o atribu�do.");
            }
        }
        else
        {
            Debug.LogError("C�mera principal n�o atribu�da.");
        }
    }

    void Update()
    {
        if (mainCamera != null && shopCanvas != null)
        {
            // Verifica se o Canvas da loja est� ativo
            bool isShopUIActive = shopCanvas.gameObject.activeInHierarchy;

            // Atualiza a m�scara de culling da c�mera se o estado mudou
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
                // Desativa a camada ap�s o final do frame atual
                StartCoroutine(DeactivateLayerAfterFrame());
            }
        }
    }

    IEnumerator DeactivateLayerAfterFrame()
    {
        // Aguarda o final do frame atual para garantir que a UI esteja completamente desativada
        yield return new WaitForEndOfFrame();

        // Atualiza a m�scara de culling da c�mera
        mainCamera.cullingMask = originalCullingMask & ~(1 << layerToToggle);
    }
}