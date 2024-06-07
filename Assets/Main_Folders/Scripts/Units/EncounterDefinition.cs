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

    [Header("Hiden Fields:")]
    [HideInInspector] public int numEncouters;
    [HideInInspector] public int minNumEncouters;
    [HideInInspector] public int maxNumEncouters;

    //[Header("Prefab Battle: ")]
    //[HideInInspector] public GameObject EnemyBattlePrefab;
    //[HideInInspector] public GameObject[] EnemiesBattlePrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && gameObject.GetComponent<Unit>().hasFought == false)
        {
            OverworldVisualPrefab = gameObject.GetComponent<Unit>().OverworldVisualPrefab;
            BattleVisualPrefab = gameObject.GetComponent<Unit>().BattleVisualPrefab;
            EncounterSystem encouter = GameObject.Find("EncounterSystem").GetComponent<EncounterSystem>();
            StartCoroutine(encouter.StartGenerateEnemiesByEncouter(minNumEncouters, maxNumEncouters, numEncouters, EncounterIsVariable, levelMin, levelMax, OverworldVisualPrefab, BattleVisualPrefab));
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EncounterDefinition))]
public class EncounterDefinition_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        var script = (EncounterDefinition)target;

        script.EncounterIsVariable = EditorGUILayout.Toggle("Número de Encouter é variável?", script.EncounterIsVariable);

        if (script.EncounterIsVariable == false)
        {
            //script.EnemyBattlePrefab = EditorGUILayout.ObjectField("Enemy Battle Visual Prefab", script.EnemyBattlePrefab, typeof(GameObject), true) as GameObject;
            script.numEncouters = EditorGUILayout.IntField("Número fixo de Encouters:", script.numEncouters);
            LevelControl();
            return;
        }

        script.minNumEncouters = EditorGUILayout.IntField("Número mínimo de Encouters:", script.minNumEncouters);
        script.maxNumEncouters = EditorGUILayout.IntField("Número máximo de Encouters:", script.maxNumEncouters);
        LevelControl();

        //script.EnemiesBattlePrefab[0] = EditorGUILayout.ObjectField("Enemy Battle Visual Prefab", script.EnemiesBattlePrefab[0], typeof(GameObject), true) as GameObject;
        //script.EnemyBattlePrefab = EditorGUILayout.ObjectField("Enemy Battle Visual Prefab", script.EnemyBattlePrefab, typeof(GameObject), true) as GameObject;

    }

    private void LevelControl()
    {
        var script = (EncounterDefinition)target;

        script.levelMin = EditorGUILayout.IntField("Level mínimo:", script.levelMin);
        script.levelMax = EditorGUILayout.IntField("Level máximo:", script.levelMax);
    }
}
#endif