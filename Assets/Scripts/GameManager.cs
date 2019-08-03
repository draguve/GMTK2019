using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneAsset[] Levels;
    public Vector2 spawnOffset;
    public int CurrentLevel;

    public Vector2 lastSpawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        lastSpawnPosition = Vector2.zero;
        CurrentLevel = 0;
        SceneManager.LoadScene(Levels[CurrentLevel].name, LoadSceneMode.Additive);
        StartCoroutine(SceneChange(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadNextScene(LoadSceneMode mode,bool unloadPrevious = true)
    {
        if (Levels.Length > CurrentLevel + 1)
        {
            CurrentLevel++;
            var spawnPosition = lastSpawnPosition + spawnOffset;
            var asyncLoadLevel = SceneManager.LoadSceneAsync(Levels[CurrentLevel].name, LoadSceneMode.Additive);
            if (unloadPrevious)
            {
                SceneManager.UnloadSceneAsync(Levels[CurrentLevel - 1].name);
            }
            
            while (!asyncLoadLevel.isDone){
                yield return null;
            }
            
            Scene loadedScene = SceneManager.GetSceneByName(Levels[CurrentLevel].name);
            SceneManager.SetActiveScene(loadedScene);
            GameObject[] allObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var root in allObjects)
            {
                root.transform.position += (Vector3)spawnPosition;
            }

            lastSpawnPosition = spawnPosition;
        }
        else
        {
            Debug.Log("No Scenes to load");
        }
    }
    
    public IEnumerator SceneChange(int Delay = 0)
    {
        yield return new WaitForSeconds(Delay);
        StartCoroutine(LoadNextScene(LoadSceneMode.Additive, false));
    }
}