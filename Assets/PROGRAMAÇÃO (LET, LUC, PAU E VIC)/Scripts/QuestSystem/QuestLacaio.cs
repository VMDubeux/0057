using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLacaio : QuestObjects
{
    protected override void CompletedQuest()
    {
        foreach (var o in possibleOutputs)
            Destroy(o);
    }
}
