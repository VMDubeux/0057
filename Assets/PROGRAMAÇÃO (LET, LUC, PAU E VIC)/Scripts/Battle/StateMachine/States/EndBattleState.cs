using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Main_Folders.Scripts;
using Main_Folders.Scripts.StateMachine.States;

public class EndBattleState : State
{
    public override IEnumerator Enter()
    {
        Debug.Log("Battle ended");

        // Descarregar a cena de índice 7
        AsyncOperation unloadScene7 = SceneLoader.UnloadBattleScene(7);
        if (unloadScene7 != null)
        {
            // Aguarda até que a cena de índice 7 seja descarregada
            while (!unloadScene7.isDone)
            {
                yield return null;
            }
            Debug.Log("Cena 7 descarregada.");
        }
        else
        {
            Debug.LogWarning("Cena 7 não está carregada.");
        }

        // Aguarda alguns frames para garantir que a cena 7 foi totalmente descarregada
        yield return new WaitForEndOfFrame();  // Pode ajustar o número de frames conforme necessário
        yield return new WaitForEndOfFrame();  // Espera dois frames como exemplo

        // Descarrega a cena de índice 4 (se necessário)
        AsyncOperation unloadScene4 = SceneLoader.UnloadBattleScene(4);
        if (unloadScene4 != null)
        {
            // Aguarda até que a cena de índice 4 seja descarregada
            while (!unloadScene4.isDone)
            {
                yield return null;
            }
            Debug.Log("Cena 4 descarregada.");
        }
        else
        {
            Debug.LogWarning("Cena 4 não está carregada.");
        }

        Debug.Log("Todas as cenas necessárias foram descarregadas e recarregadas.");
    }
}
