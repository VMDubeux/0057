using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D HoverCursorTexture; // Cursor para quando o mouse está sobre o personagem.
    public Texture2D DefaultCursorTexture; // Cursor padrão do jogo.
    private Vector2 CursorHotspot; // Ponto de ancoragem do cursor.

    void Start()
    {
        // Configura o cursor padrão no início do jogo
        if (DefaultCursorTexture != null)
        {
            CursorHotspot = new Vector2(HoverCursorTexture.width / 2, HoverCursorTexture.height / 2); // Centro da textura.
            Cursor.SetCursor(DefaultCursorTexture, CursorHotspot, CursorMode.Auto);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Altera o cursor para a textura de hover
        if (HoverCursorTexture != null)
        {
            CursorHotspot = new Vector2(HoverCursorTexture.width / 2, HoverCursorTexture.height / 2); // Centro da textura.
            Cursor.SetCursor(HoverCursorTexture, CursorHotspot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restaura o cursor para o padrão
        if (DefaultCursorTexture != null)
        {
            CursorHotspot = new Vector2(HoverCursorTexture.width / 2, HoverCursorTexture.height / 2); // Centro da textura.
            Cursor.SetCursor(DefaultCursorTexture, CursorHotspot, CursorMode.Auto);
        }
    }
}
