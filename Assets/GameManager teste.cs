using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.XR.Haptics;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (FadeInOut._instance != null)
        {
            FadeInOut._instance.Fade();
            StartCoroutine("StartStage");

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
