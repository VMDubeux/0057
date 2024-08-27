using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue SO")]
public class ChatScriptableObject : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] dialogueSentences;
    public Sprite[] speakerImage;
    public string[] speakerName;
}
