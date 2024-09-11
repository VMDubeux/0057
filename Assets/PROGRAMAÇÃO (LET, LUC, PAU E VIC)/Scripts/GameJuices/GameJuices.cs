using UnityEngine;
using System.Collections;

public abstract class GameJuices : MonoBehaviour
{
    public delegate void PressedButton();
    public static event PressedButton OnPressedButton;

    public GameObject CanvasGameJuices;
    internal bool isInside = false;
    internal bool wasOpen = false;
    internal GameObject _player;
    internal Animator _animator;
    [SerializeField] internal string _assetKey;

    protected abstract void Start();
    protected abstract void HandleTriggerEnter(Collider other);
    protected abstract void HandleTriggerExit(Collider other);
    protected abstract IEnumerator IsInside();
    protected abstract void HandleButtonPress();
    protected abstract void AddRandomItemToInventory();
    protected abstract void SetupReturnToOrigin();

    protected virtual void Awake()
    {
        _assetKey = PersistentIdentifierManager.GetOrCreateIdentifier(gameObject);
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
}