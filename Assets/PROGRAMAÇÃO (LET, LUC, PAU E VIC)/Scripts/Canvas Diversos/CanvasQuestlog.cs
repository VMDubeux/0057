using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasQuestlog : MonoBehaviour
{
    [Header("Configurações do Quest Log")]
    [Tooltip("Referência para o componente TextMeshProUGUI do Quest Log.")]
    public TextMeshProUGUI questLogText;

    [Header("Configurações de Objeto")]
    [Tooltip("GameObject que será ativado ou desativado pelo botão.")]
    public GameObject targetObject;

    [Tooltip("Botão que alterna a visibilidade do GameObject.")]
    public Button button;

    private List<QuestObjects> questObjectsList = new();

    private void Start()
    {
        // Configurar o botão para chamar o método ToggleObject quando pressionado
        if (button != null)
        {
            button.onClick.AddListener(ToggleObject);
        }

        // Encontra todos os objetos no cenário que possuem o script QuestObjects
        QuestObjects[] allQuestObjects = FindObjectsByType<QuestObjects>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var quest in allQuestObjects)
        {
            if (quest.inQuestLog)
            {
                questObjectsList.Add(quest);
            }
        }

        UpdateQuestLog();

        StartCoroutine(QuestAdd());
    }

    private void OnEnable()
    {
        questObjectsList.Clear();
        QuestObjects[] allQuestObjects = FindObjectsByType<QuestObjects>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var quest in allQuestObjects)
        {
            if (quest.inQuestLog)
            {
                questObjectsList.Add(quest);
            }
        }

        UpdateQuestLog();
    }

    private IEnumerator QuestAdd() 
    {
        yield return new WaitForSeconds(2);

        questObjectsList.Clear();
        QuestObjects[] allQuestObjects = FindObjectsByType<QuestObjects>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var quest in allQuestObjects)
        {
            if (quest.inQuestLog)
            {
                questObjectsList.Add(quest);
            }
        }

        UpdateQuestLog();
    }

    private void Update()
    {
        UpdateQuestLog();
    }

    /// <summary>
    /// Atualiza o texto do Quest Log com base nos objetos e seus status.
    /// </summary>
    private void UpdateQuestLog()
    {
        if (questLogText == null) return;

        questLogText.text = "";
        List<(int number, string text)> questTexts = new();

        foreach (var quest in questObjectsList)
        {
            if (quest == null) continue;
            int questNumber = ExtractLeadingNumber(quest.transform.GetChild(0).gameObject.name);
            string formattedText = quest.isCompleted
                ? $"<s>{quest.transform.GetChild(0).gameObject.name}</s>"
                : quest.transform.GetChild(0).gameObject.name;

            questTexts.Add((questNumber, formattedText));
        }

        questTexts.Sort((a, b) => a.number.CompareTo(b.number));

        foreach (var quest in questTexts)
        {
            questLogText.text += $"{quest.text}\n\n";
        }
    }

    /// <summary>
    /// Extrai o número inicial de uma string.
    /// </summary>
    private int ExtractLeadingNumber(string text)
    {
        // Encontra o número inicial na string
        string numberStr = "";
        foreach (char c in text)
        {
            if (char.IsDigit(c))
            {
                numberStr += c;
            }
            else
            {
                break;
            }
        }

        // Converte para inteiro, retornando 0 se não encontrar número
        return int.TryParse(numberStr, out int result) ? result : 0;
    }

    /// <summary>
    /// Ativa ou desativa o GameObject quando o botão é pressionado.
    /// </summary>
    private void ToggleObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf); // Alterna o estado do GameObject
        }
    }
}