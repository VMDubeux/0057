using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMentor : QuestObjects
{
    protected override void CompletedQuest()
    {
        Destroy(possibleOutputs[0]);
        possibleOutputs[1].GetComponent<QuestLacaio>().isAvailable = true;
    }
}
