using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    private Vector2 CursorHotspot; // Ponto de ancoragem do cursor.

    private Texture2D defaultCursorTexture; // Mantém referência ao cursor padrão.

    void Start()
    {
        // Configura o cursor padrão ao iniciar o jogo.
        UpdateCursor();
    }

    void Update()
    {
        // Verifica a cada quadro se é necessário mudar o cursor.
        UpdateCursor();
    }

    void UpdateCursor()
    {
        // Obtém a cena ativa.
        Scene activeScene = SceneManager.GetActiveScene();

        // Verifica se a cena ativa é "LEVEL_BATTLE".
        if (activeScene.name != "LEVEL_BATTLE")
        {
            CursorHotspot = new Vector2(defaultCursorTexture.width / 2, defaultCursorTexture.height / 2); // Centro da textura.
            Cursor.SetCursor(defaultCursorTexture, CursorHotspot, CursorMode.Auto);
        }
    }
}

