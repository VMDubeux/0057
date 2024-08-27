using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneName;
    public GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
    }
    public void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
    public void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E))
        {
            PositionFixer.fromDoor = true;
            SceneManager.LoadScene(SceneName);
        }
    }
}
