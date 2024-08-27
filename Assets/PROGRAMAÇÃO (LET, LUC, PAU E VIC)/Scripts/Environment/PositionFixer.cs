using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFixer : MonoBehaviour
{
    public static bool fromDoor = false;
    public Vector3 startPosition = new Vector3(-3, 1, 3);
    public GameObject player;

    private void Start()
    {
        if (fromDoor == false)
        {
            player.transform.position = startPosition;
        }
    }
}
