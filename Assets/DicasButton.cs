using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DicasButton : MonoBehaviour
{
    private int click = 0;
    private Button button;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI textBox;

    void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (click == 0)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
            textBox.text = "Walk: mouse click\nRun: SHIFT\nChange camera: C\nCard battle: drag, drop and click on target";
        }
        else if (click == 1)
        {
            Debug.Log("INVOCOU?");
            QuestMentor.questMentorEvent.Invoke();
            Destroy(canvas.gameObject);
        }

        click++;
    }
}
