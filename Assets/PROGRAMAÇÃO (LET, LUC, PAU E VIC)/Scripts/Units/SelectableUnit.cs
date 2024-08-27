using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SelectableUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<BodyPart> BodyParts;

    void Start()
    {
        BodyParts = new List<BodyPart>();

        foreach (SkinnedMeshRenderer skinnedMeshRenderer in gameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            BodyPart bodyPart = new BodyPart();
            bodyPart.SetValues(skinnedMeshRenderer.gameObject.name, skinnedMeshRenderer, skinnedMeshRenderer.material, skinnedMeshRenderer.material.color);
            BodyParts.Add(bodyPart);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        for (int i = 0; i < BodyParts.Count; i++)
        {
            BodyParts[i].Material.color = Color.white;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < BodyParts.Count; i++)
        {
            BodyParts[i].Material.color = BodyParts[i].RealColor;
        }
    }
}

public class BodyPart
{
    public string Name;
    public SkinnedMeshRenderer BodyRenderer;
    public Material Material;
    public Color RealColor;

    public void SetValues(string name, SkinnedMeshRenderer skinned, Material material, Color color)
    {
        Name = name;
        BodyRenderer = skinned;
        Material = material;
        RealColor = color;
    }
}