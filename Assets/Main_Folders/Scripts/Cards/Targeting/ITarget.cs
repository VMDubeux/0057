using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    IEnumerator GetTargets(List<object> targets);
}
