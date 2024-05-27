using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBattleState  : State
{
    public override IEnumerator Enter(){
        yield return null;
        Debug.Log("Battle ended");
        SceneManager.UnloadSceneAsync("LEVEL_BATTLE");
    }
}