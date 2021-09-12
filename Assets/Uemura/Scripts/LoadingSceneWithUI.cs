using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneWithUI : MonoBehaviour
{
    private AsyncOperation async;
    public GameObject LoadingUi;
    public Slider Slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene(string sceneName)
    {
        LoadingUi.SetActive(true);
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            Slider.value = async.progress;
            yield return null;
        }
    }
}
