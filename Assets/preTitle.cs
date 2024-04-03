using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IntroGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IntroGame()
    {
        yield return new WaitForSeconds(1.75f);
        FadeInOut._instance.Fade();
        yield return new WaitForSeconds(3);
        FadeInOut._instance.Fade();
        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => FadeInOut._instance.isFadeComplete);
        FadeInOut._instance.GoScene("Menu");
    }
}
