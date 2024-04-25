using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogStep : ScriptableObject
{
    public string[] dialogues;
    public DialogStepOption[] options;
}
