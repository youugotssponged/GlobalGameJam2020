using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeTriggerToNextScene : MonoBehaviour
{
    public Image fadeImg;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadFirstLevel());
    }

    IEnumerator loadFirstLevel(){
        yield return new WaitForSeconds(23f);

        StartCoroutine(FadeOut());

    }


    IEnumerator FadeOut()
    {
        //fade from transparent to opaque
        // loop over 5 seconds
        for (float i = 0; i <= 5; i += Time.deltaTime)
        {
            // set color with i as alpha
            fadeImg.color = new Color(0, 0, 0, i);
            yield return null;
        }

        SceneManager.LoadScene(2);
    }

}
