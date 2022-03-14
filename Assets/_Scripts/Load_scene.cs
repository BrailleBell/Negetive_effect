using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_scene : MonoBehaviour
{
    public float load = 0;
    public Image loadingBar;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    IEnumerator LoadNextScene()
    {
        yield return null;

        AsyncOperation AsyncOp = SceneManager.LoadSceneAsync(2);
        while (!AsyncOp.isDone)
        {
            loadingBar.fillAmount = AsyncOp.progress;
            Debug.Log(AsyncOp.progress);
            yield return null;
        }
     
    }

}
