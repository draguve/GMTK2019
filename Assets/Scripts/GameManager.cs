using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using SnSMovement.Character;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelSave
{
    public int level;
    
}
public class GameManager : Singleton<GameManager>
{
    public int[] levels;
    public int currentLevel;
    public GameObject menuCanvas;

    public LevelSave Save;
    public GameObject player;
    public Vector3 spawnLocation;

    protected const string _saveFolderName = "SaveFiles/";
    protected const string _saveFileName = "Level.txt";
    
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }
    

    protected virtual void SaveLevel()
    {
        LevelSave thing = new LevelSave();
        thing.level = currentLevel-1;
        SaveLoadManager.Save(thing, _saveFileName, _saveFolderName);
    }

    /// <summary>
    /// Loads the sound settings from file (if found)
    /// </summary>
    protected virtual void LoadLevel()
    {
        LevelSave save = (LevelSave)SaveLoadManager.Load(_saveFileName, _saveFolderName);
        if (save != null)
        {
            Save = save;
        }
    }
    
    #region Save/Load

    public void Load()
    {
        LoadLevel();
        currentLevel = Save.level;
    }
    
    

    #endregion
    
    #region Menu : Play

    public void OnStart()
    {
        if (currentLevel != -1)
        {
            WarnStartNewGame();
        }
        SimpleLoadNextScene();
        menuCanvas.SetActive(false);
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
            currentLevel = -1;
        }
    }

    public void OnContinue()
    {
        SimpleLoadNextScene();
        menuCanvas.SetActive(false);
    }

    #endregion

    #region Flare

    public void FlarePickedUp()
    {
        SceneManager.LoadSceneAsync(levels[currentLevel], LoadSceneMode.Single);
        SimpleLoadNextScene();
    }

    #endregion

    public void SimpleLoadNextScene()
    {
        if (levels.Length > currentLevel + 1)
        {
            currentLevel++;
            SceneManager.LoadScene(levels[currentLevel]);
        }
        else
        {
            Debug.Log("No Scenes to load");
        }
        SaveLevel();
        
    }
    
    /// <summary>
    /// Not to be used in the real game, just for testing
    /// </summary>
    /// <param name="Delay"></param>
    /// <returns></returns>
    public IEnumerator SceneChange(int Delay = 0)
    {
        yield return new WaitForSeconds(Delay);
        SimpleLoadNextScene();
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(levels[currentLevel]);
    }
    
    public void respawnPlayer()
    {
        player.transform.position = spawnLocation;
        player.GetComponent<FlareHandler>().FlareAvailable = true;
        player.GetComponent<CharacterMotor>()._canMoveLeft = true;
        player.GetComponent<CharacterMotor>()._canMoveRight = true;
    }
}