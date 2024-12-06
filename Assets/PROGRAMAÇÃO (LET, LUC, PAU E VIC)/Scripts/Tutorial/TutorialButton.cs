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

    void OnClick()
    {
        if (click == 0)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
            textBox.text = "Mouse click to run\nPress SHIFT to switch between running and walking\nChange camera: C\nChange minimap: Z\nCard battle: drag, drop and click on target";
            banner.text = "Controls";
        }
        else if (click == 1)
        {
            Destroy(canvas.gameObject);
        }

        click++;
    }
}
