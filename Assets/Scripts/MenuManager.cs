﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject leaderBoardPanel;
    public GameObject youWinPanel;
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;
	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowLeaderBoard()
    {
        youWinPanel.SetActive(false);
        leaderBoardPanel.SetActive(true);
    }
}
