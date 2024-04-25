using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogButtonMenu : MonoBehaviour
{
    [SerializeField] private DialogButton dialogButtonPrefab;
    [SerializeField] private Vector2 buttonSize = new Vector2(300.0f, 200.0f);
    [SerializeField] private Vector2 nextButtonSize = new Vector2(160.0f, 40.0f);
    private List<DialogButton> currentButtons;

    public void ConfigureWith(DialogStepOption[] options, DialogManager dialogManager)
    {
        if (currentButtons != null) {
            foreach(DialogButton b in currentButtons) {
                Destroy(b.gameObject);
            }
        }

        currentButtons = new List<DialogButton>();

        if (options == null || options.Length == 0) {
            ShowNextButton(dialogManager);
        } else {
            ShowListOfOptions(options, dialogManager);
        }
    }

    private void ShowNextButton(DialogManager dialogManager) {
        DialogButton nextButton = Instantiate(dialogButtonPrefab, dialogManager.transform);
        Button b = nextButton.GetComponent<Button>();
        b.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        
        currentButtons.Add(nextButton);
        b.onClick.AddListener(dialogManager.NextStep);

        RectTransform rect = nextButton.GetComponent<RectTransform>();
        rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 16, nextButtonSize.y);
        rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 16, nextButtonSize.x);
    }

    private void ShowListOfOptions(DialogStepOption[] options, DialogManager dialogManager) {
        int i = 0;
        foreach(DialogStepOption option in options) {
            DialogButton optionButton = Instantiate(dialogButtonPrefab, transform);
            optionButton.ConfigureWith(option, dialogManager);
            RectTransform rect = optionButton.GetComponent<RectTransform>();
            rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, buttonSize.y * i + 16, buttonSize.y);
            currentButtons.Add(optionButton);
            i++;
        }
    }
}
