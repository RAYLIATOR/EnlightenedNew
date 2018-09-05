using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject leaderBoard;
    public GameObject tutorialCanvas;
    public GameObject player;
    public Text tutorialText;
    int textLevel;
    float updateRate;
    float nextTimeToUpdate;
    int levels;
    void Start ()
    {
        nextTimeToUpdate = 0f;
        updateRate = 0.2f;
        textLevel = 1;
        levels = 8;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return) && textLevel < levels)
        {
            textLevel += 1;
        }
        /*if(Time.time>=nextTimeToUpdate && textLevel < levels)
        {
            textLevel += 1;
            nextTimeToUpdate = Time.time + 1 / updateRate;
        }*/
		switch(textLevel)
        {
            case 1: tutorialText.text = "Welcome Player.";
                break;
            case 2: tutorialText.text = "Use the W, A, S and D keys to move.";
                break;
            case 3: tutorialText.text = "Use the mouse to aim and look around";
                break;
            case 4: tutorialText.text = "Use the left mouse button to shoot.";
                break;
            case 5: tutorialText.text = "The red bar in the top-left of the screen displays your character's health.";
                break;
            case 6: tutorialText.text = "Press Escape to pause the game at anytime.";
                break;
            case 7: tutorialText.text = "Fight in order to survive!";
                break;
            case 8: player.GetComponent<WaveSpawner>().start = true;
                leaderBoard.GetComponent<Leaderboard>().count = true;
                tutorialCanvas.SetActive(false);
                break;
        }
	}
}
