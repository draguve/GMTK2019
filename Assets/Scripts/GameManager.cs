﻿using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int[] levels;
    public int currentLevel;
    public GameObject menuCanvas;

    public GameObject player;
    public Vector3 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    #region Save/Load

    public void Load()
    {
        currentLevel = -1;
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
    }
}