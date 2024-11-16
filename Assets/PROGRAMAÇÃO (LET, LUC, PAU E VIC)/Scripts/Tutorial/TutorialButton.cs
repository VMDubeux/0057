using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    private int click = 0;
    private Button button;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI banner;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick() {
        if (click == 0) {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
            textBox.text = "Walk: mouse click\nRun: SHIFT\nChange camera: C\nCard battle: drag and drop";
            banner.text = "Controls";
        } else if (click == 1) {
            Destroy(canvas.gameObject);
        } 

        click++;
    }
}
