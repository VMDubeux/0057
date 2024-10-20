using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DriplessIdle : MonoBehaviour
{
    protected Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        InvokeRepeating(nameof(HandleIdle), 5f, 5);
    }

    private void HandleIdle()
    {
        int random = Random.Range(1, 4);
        _animator.SetInteger("IdleIndex", random);
    }
}
