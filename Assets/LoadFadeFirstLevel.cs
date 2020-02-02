using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadFadeFirstLevel : MonoBehaviour
{
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForCrash());
    }

    IEnumerator waitForCrash()
    {
        yield return new WaitForSeconds(23.0f);

        // Fade 5 seconds
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        for (float i = 0; i <= 5; i += Time.deltaTime) {
            // set color with i as alpha
            img.color = new Color(0, 0, 0, i);
            yield return null;
        }
        SceneManager.LoadScene(2);
    }
}
