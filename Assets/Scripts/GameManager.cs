using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public SceneAsset[] levels;
    public Vector2 spawnOffset;
    public int currentLevel;
    public GameObject menuCanvas;

    public Vector2 lastSpawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        lastSpawnPosition = Vector2.zero;
        Load();
        //StartCoroutine(SceneChange(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Save/Load

    public void Load()
    {
        currentLevel = 0;
    }

    #endregion
    
    #region Menu : Play

    public void OnStart()
    {
        if (currentLevel != 0)
        {
            WarnStartNewGame();
        }
        menuCanvas.SetActive(false);
        SceneManager.LoadScene(levels[currentLevel].name, LoadSceneMode.Additive);
    }

    //Function to display levels
    public void Levels()
    {
        
    }

    private void WarnStartNewGame()
    {
        bool playerConfirm = true;

        // code to confirm
        
        if (playerConfirm)
        {
            currentLevel = 0;
        }
    }

    public void OnContinue()
    {
        menuCanvas.SetActive(false);
        StartCoroutine(LoadNextScene(LoadSceneMode.Additive, false));
    }

    #endregion

    #region Flare

    public void FlarePickedUp()
    {
        StartCoroutine(LoadNextScene(LoadSceneMode.Additive));
    }

    #endregion
    
    /// <summary>
    /// Used to load hte next scene
    /// </summary>
    /// <param name="mode">Load scene mode</param>
    /// <param name="unloadPrevious">Will the previous scene be unloaded</param>
    /// <returns></returns>
    public IEnumerator LoadNextScene(LoadSceneMode mode,bool unloadPrevious = true)
    {
        if (levels.Length > currentLevel + 1)
        {
            currentLevel++;
            var spawnPosition = lastSpawnPosition + spawnOffset;
            var asyncLoadLevel = SceneManager.LoadSceneAsync(levels[currentLevel].name, mode);
            if (unloadPrevious)
            {
                SceneManager.UnloadSceneAsync(levels[currentLevel - 1].name);
            }
            
            while (!asyncLoadLevel.isDone){
                yield return null;
            }
            
            Scene loadedScene = SceneManager.GetSceneByName(levels[currentLevel].name);
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
    
    /// <summary>
    /// Not to be used in the real game, just for testing
    /// </summary>
    /// <param name="Delay"></param>
    /// <returns></returns>
    public IEnumerator SceneChange(int Delay = 0)
    {
        yield return new WaitForSeconds(Delay);
        StartCoroutine(LoadNextScene(LoadSceneMode.Additive));
    }
}