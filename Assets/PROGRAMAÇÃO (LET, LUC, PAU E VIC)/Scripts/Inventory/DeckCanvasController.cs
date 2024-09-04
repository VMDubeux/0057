using Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.Inventory;
using UnityEngine;
using static UnityEditor.Progress;

public class DeckCanvasController : MonoBehaviour
{
    [SerializeField] private Canvas deckCanvas; // O Canvas do Deck

    private void Start()
    {
        if (deckCanvas == null)
        {
            Debug.LogError("Canvas não atribuído!");
            return;
        }

        // Inicialmente desativa o Canvas
        deckCanvas.gameObject.SetActive(false);

        // Faz com que este objeto não seja destruído entre cenas
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Verifica se a tecla "i" foi pressionada
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleCanvasPosition();
        }
    }

    private void ToggleCanvasPosition()
    {
        if (!deckCanvas.gameObject.activeSelf)
        {
            deckCanvas.gameObject.SetActive(true); // Ativa o Canvas se estiver desativado
        }
        else
        {
            deckCanvas.gameObject.SetActive(false); // Desativa o Canvas se estiver ativado
        }
    }
}