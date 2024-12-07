using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopUserInterface : MonoBehaviour
{
    public delegate void PlayerEntered();
    public static event PlayerEntered OnPlayerEntered;

    [SerializeField] internal Transform canvasStore;

    private void Awake()
    {
        OnPlayerEntered += Show;
        canvasStore = transform.parent;
        canvasStore.gameObject.SetActive(false);
        
    }

    public void Show()
    {
        canvasStore.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        canvasStore.gameObject.SetActive(false);
        //gameObject.SetActive(false);
    }

    private void Update()
    {
        if (OnPlayerEntered != null)
        {
            OnPlayerEntered.Invoke();
            OnPlayerEntered = null;
        }
    }
}