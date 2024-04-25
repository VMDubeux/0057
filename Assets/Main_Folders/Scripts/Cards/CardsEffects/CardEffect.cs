using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect: MonoBehaviour
{
    public abstract IEnumerator Apply(List<object> targets);
}
