using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneAsset[] Levels;

    public int CurrentLevel;

    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = 0;
        SceneManager.LoadScene(Levels[CurrentLevel].name, LoadSceneMode.Additive);

        StartCoroutine(SceneChange(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene(LoadSceneMode mode)
    {
        if (Levels.Length > CurrentLevel + 1)
        {
            CurrentLevel++;
            SceneManager.LoadScene(Levels[CurrentLevel].name, LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("No Scenes to load");
        }
    }
    
    public IEnumerator SceneChange(int Delay = 0)
    {
        yield return new WaitForSeconds(Delay);
        LoadNextScene(LoadSceneMode.Additive);
    }
}