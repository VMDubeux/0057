using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Main_Folders.Scripts;
using Main_Folders.Scripts.StateMachine.States;
using Main_Folders.Scripts.Managers;

public class EndBattleState : State
{
    public override IEnumerator Enter()
    {
        Debug.Log("Battle ended");

        AudioManager.Instance.PlayMusic("Soundtrack", 1f);

        // Descarregar a cena de �ndice 7
        AsyncOperation unloadScene7 = SceneLoader.UnloadBattleScene(7);
        if (unloadScene7 != null)
        {
            // Aguarda at� que a cena de �ndice 7 seja descarregada
            while (!unloadScene7.isDone)
            {
                yield return null;
            }
            Debug.Log("Cena 7 descarregada.");
        }
        else
        {
            Debug.LogWarning("Cena 7 n�o est� carregada.");
        }

        // Aguarda alguns frames para garantir que a cena 7 foi totalmente descarregada
        yield return new WaitForEndOfFrame();  // Pode ajustar o n�mero de frames conforme necess�rio
        yield return new WaitForEndOfFrame();  // Espera dois frames como exemplo

        // Descarrega a cena de �ndice 4 (se necess�rio)
        AsyncOperation unloadScene4 = SceneLoader.UnloadBattleScene(4);
        if (unloadScene4 != null)
        {
            // Aguarda at� que a cena de �ndice 4 seja descarregada
            while (!unloadScene4.isDone)
            {
                yield return null;
            }
            Debug.Log("Cena 4 descarregada.");
        }
        else
        {
            Debug.LogWarning("Cena 4 n�o est� carregada.");
        }

        Debug.Log("Todas as cenas necess�rias foram descarregadas e recarregadas.");
    }
}
