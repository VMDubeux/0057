using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SelectableUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color HighlightedColor;

    [SerializeField] private GameObject Body;

    [SerializeField] private SkinnedMeshRenderer[] bodyRenderers;

    void Awake()
    {
        bodyRenderers = new SkinnedMeshRenderer[Body.transform.childCount];

        for (int i = 0; i < bodyRenderers.Length; i++)
        {
            bodyRenderers[i] = Body.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
        }

        //NormalColor = bodyRenderers[0].materials[0].color;
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