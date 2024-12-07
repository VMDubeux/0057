using Main_Folders.Scripts;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EncounterDefinition : MonoBehaviour
{
    private GameObject BattleVisualPrefab;
    private GameObject OverworldVisualPrefab;

    [HideInInspector] public bool EncounterIsVariable;

    [HideInInspector] public int levelMin;
    [HideInInspector] public int levelMax;

    [Header("Hidden Fields:")]
    [HideInInspector] public int numEncounters;
    [HideInInspector] public int minNumEncounters;
    [HideInInspector] public int maxNumEncounters;

    internal bool isBattleStarted = false; // Controle para evitar múltiplas execuções

    private void Update()
    {
        // Verifica se o diálogo foi finalizado, e a batalha ainda não foi iniciada
        QuestLacaio questLacaio = gameObject.GetComponent<QuestLacaio>();
        if (questLacaio.isDialogueFinished == true &&
            questLacaio != null &&
            isBattleStarted == false &&
            gameObject.GetComponent<Unit>().hasFought == false)
        {
            Debug.Log("Iniciar Sequencia de Batalha AGORA");
            isBattleStarted = true; // Marca como iniciado para evitar múltiplas execuções
            StartBattleSequence(); // Inicia o processo de batalha
        }
    }

    private void StartBattleSequence()
    {
        Debug.Log("Iniciando sequência de batalha após o diálogo.");

        References.Instance.CurrentEnemyBattle = this.gameObject;

        OverworldVisualPrefab = gameObject.GetComponent<Unit>().OverworldVisualPrefab;
        BattleVisualPrefab = gameObject.GetComponent<Unit>().BattleVisualPrefab;

        EncounterSystem encounterSystem = GameObject.Find("EncounterSystem").GetComponent<EncounterSystem>();
        StartCoroutine(encounterSystem.StartGenerateEnemiesByEncouter(
            minNumEncounters,
            maxNumEncounters,
            numEncounters,
            EncounterIsVariable,
            levelMin,
            levelMax,
            OverworldVisualPrefab,
            BattleVisualPrefab
        ));
    }

    internal void ChamarBatalha()
    {
        Debug.Log("Iniciar Sequencia de Batalha AGORA");
        isBattleStarted = true; // Marca como iniciado para evitar múltiplas execuções
        StartDungeonSequence(); // Inicia o processo de dungeon
    }

    private void StartDungeonSequence()
    {
        Debug.Log("Iniciando sequência de dungeon após o trigger.");

        References.Instance.CurrentEnemyBattle = this.gameObject;

        OverworldVisualPrefab = gameObject.GetComponent<Unit>().OverworldVisualPrefab;
        BattleVisualPrefab = gameObject.GetComponent<Unit>().BattleVisualPrefab;

        EncounterSystem encounterSystem = GameObject.Find("EncounterSystem").GetComponent<EncounterSystem>();
        StartCoroutine(encounterSystem.StartGenerateEnemiesByEncouter(
            minNumEncounters,
            maxNumEncounters,
            numEncounters,
            EncounterIsVariable,
            levelMin,
            levelMax,
            OverworldVisualPrefab,
            BattleVisualPrefab
        ));
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(EncounterDefinition))]
    public class EncounterDefinition_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            var script = (EncounterDefinition)target;

            script.EncounterIsVariable = EditorGUILayout.Toggle("Número de Encounter é variável?", script.EncounterIsVariable);

            if (!script.EncounterIsVariable)
            {
                script.numEncounters = EditorGUILayout.IntField("Número fixo de Encounters:", script.numEncounters);
                LevelControl();
                return;
            }

            script.minNumEncounters = EditorGUILayout.IntField("Número mínimo de Encounters:", script.minNumEncounters);
            script.maxNumEncounters = EditorGUILayout.IntField("Número máximo de Encounters:", script.maxNumEncounters);
            LevelControl();
        }

        private void LevelControl()
        {
            var script = (EncounterDefinition)target;

            script.levelMin = EditorGUILayout.IntField("Level mínimo:", script.levelMin);
            script.levelMax = EditorGUILayout.IntField("Level máximo:", script.levelMax);
        }
    }
#endif
}
