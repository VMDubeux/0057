using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogStepOption : ScriptableObject
{
    public string optionTitle;
    public DialogStep nextSteps;
}
