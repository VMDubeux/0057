using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour, ITarget
{
    public IEnumerator GetTargets(List<object> targets){
        GameObject playerGameObject = GameObject.Find("Units/Player");
        targets.Add(playerGameObject.GetComponentInChildren<Unit>());
        yield return null;
    }
}
