using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SelectableUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color HighlightedColor;
    
    [SerializeField] internal int smrCount;

    [SerializeField] internal SkinnedMeshRenderer[] bodyRenderers;

    void Awake()
    {
        foreach (SkinnedMeshRenderer smr in gameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>()) 
        {
            smrCount++;
        }

        bodyRenderers = new SkinnedMeshRenderer[smrCount];

        for (int i = 0; i < bodyRenderers.Length; i++)
        {
            bodyRenderers[i] = GetComponentsInChildren<SkinnedMeshRenderer>()[i];
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        for (int i = 0; i < bodyRenderers.Length; i++)
        {
            bodyRenderers[i].material.color = Color.white;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < bodyRenderers.Length; i++)
        {
            bodyRenderers[i].material.color = HighlightedColor;
        }
    }
}