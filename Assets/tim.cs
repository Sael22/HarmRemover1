using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSampleSceneAfterDelay(5f));
    }

    IEnumerator LoadSampleSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

