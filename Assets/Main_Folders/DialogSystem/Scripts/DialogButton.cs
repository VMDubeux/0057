using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    public void ConfigureWith(DialogStepOption option, DialogManager dialogManager) {
        text.text = option.optionTitle;
        GetComponent<Button>().onClick.AddListener(() => dialogManager.SelectOption(option));
    }
}
