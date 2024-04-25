using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalManager : MonoBehaviour
{
    public string nextLevelName = "LEVEL_2";

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
