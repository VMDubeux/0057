using UnityEngine;
using System.Collections;

public abstract class GameJuices : MonoBehaviour
{
    public delegate void PressedButton();
    public static event PressedButton OnPressedButton;

    [SerializeField] internal GameObject CanvasGameJuices;
    [SerializeField] internal GameObject CanvasCardDroppedMessage;
    internal bool isInside = false;
    internal bool wasOpen = false;
    internal CardInventoryManager cardInventoryManager;
    internal Animator _animator;
    [SerializeField] internal string _assetKey;

    protected abstract void Start();
    protected abstract void HandleTriggerEnter(Collider other);
    protected abstract void HandleTriggerExit(Collider other);
    protected abstract IEnumerator IsInside();
    protected abstract void HandleButtonPress();
    internal abstract void AddRandomItemToInventory();
    protected abstract void SetupReturnToOrigin();

    protected virtual void Awake()
    {
        _assetKey = gameObject.name;
        PlayerPrefs.Save(); // Ensure the data is saved immediately
        _animator = GetComponent<Animator>();
    }


    protected void PerformDelegate()
    {
        OnPressedButton?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleTriggerExit(other);
    }

    private void Update()
    {
        StartCoroutine(IsInside());
    }

    protected IEnumerator CanvasCardDropped()
    {
        if (CanvasCardDroppedMessage == null) yield break;
        CanvasCardDroppedMessage.SetActive(true);
        yield return new WaitForSeconds(3);
        CanvasCardDroppedMessage.SetActive(false);
    }
}