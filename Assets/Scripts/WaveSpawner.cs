using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject leaderBoard;
    public GameObject leaderBoardPanel;
    public GameObject youWinPanel;
    public Text enemyCountText;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject gate1;
    public GameObject gate2;
    public GameObject gate3;
    public bool start;
    public bool spawn;
    int area;
    int wave;
    public int enemyCount;
    
    public void NextArea()
    {
        area += 1;
        wave = 1;
        spawn = true;
    }

    void Start ()
    {
        start = false;
        enemyCount = 10;
        area = 1;
        wave = 1;
        spawn = true;
	}
	
	void Update ()
    {
        print(area + " " + wave);
        enemyCountText.text = enemyCount.ToString("##");
        if (start)
        {
            SpawnEnemies();
        }
        if(enemyCount == 0)
        {
            if (area == 1 && wave == 1)
            {
                gate1.GetComponent<Gate>().Collide();
            }
            if (area == 2 && wave == 1)
            {
                gate2.GetComponent<Gate>().Collide();
            }
            if (area == 3 && wave == 1)
            {
                gate3.GetComponent<Gate>().Collide();
            }
            if (area == 4 && wave == 1)
            {
                youWinPanel.SetActive(true);
                leaderBoard.GetComponent<Leaderboard>().Load();
                Time.timeScale = 0;
            }
            //wave += 1;
            //spawn = true; 
        }
	}
    
    void SpawnEnemies()
    {
        switch(area)
        {
            case 1: switch(wave)
                {
                    case 1:
                        if (spawn)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                Instantiate(enemy1, new Vector3(Random.Range(-200, -90), -4, Random.Range(80,-10)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;
                    /*case 2:
                        if (spawn)
                        {
                            enemyCount = 15;
                            for (int i = 0; i < 15; i++)
                            {
                                Instantiate(enemy1, new Vector3(Random.Range(-200, -90), -4, Random.Range(80, -10)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;
                    case 3:
                        if (spawn)
                        {
                            enemyCount = 20;
                            for (int i = 0; i < 20; i++)
                            {
                                Instantiate(enemy1, new Vector3(Random.Range(-200, -90), -4, Random.Range(80, -10)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;*/
                }
                break;
            case 2:
                switch (wave)
                {
                    case 1:
                        if (spawn)
                        {
                            enemyCount = 25;
                            for (int i = 0; i < 25; i++)
                            {
                                Instantiate(enemy2, new Vector3(Random.Range(-70, 20), -4, Random.Range(80, 0)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;
                    /*case 2:
                        if (spawn)
                        {
                            enemyCount = 30;
                            for (int i = 0; i < 30; i++)
                            {
                                Instantiate(enemy2, new Vector3(Random.Range(-70, 20), -4, Random.Range(80, 0)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;
                    case 3:
                        if (spawn)
                        {
                            enemyCount = 35;
                            for (int i = 0; i < 35; i++)
                            {
                                Instantiate(enemy2, new Vector3(Random.Range(-70, 20), -4, Random.Range(80, 0)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;*/
                }
                break;
            case 3:
                switch (wave)
                {
                    case 1:
                        if (spawn)
                        {
                            enemyCount = 40;
                            for (int i = 0; i < 40; i++)
                            {
                                Instantiate(enemy3, new Vector3(Random.Range(-60, 10), -4, Random.Range(200, 120)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;
                   /* case 2:
                        if (spawn)
                        {
                            enemyCount = 45;
                            for (int i = 0; i < 45; i++)
                            {
                                Instantiate(enemy3, new Vector3(Random.Range(-60, 10), -4, Random.Range(200, 120)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;
                    case 3:
                        if (spawn)
                        {
                            enemyCount = 50;
                            for (int i = 0; i < 50; i++)
                            {
                                Instantiate(enemy3, new Vector3(Random.Range(-60, 10), -4, Random.Range(200, 120)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;*/
                }
                break;
            case 4:
                switch (wave)
                {
                    case 1:
                        if (spawn)
                        {
                            enemyCount = 10;
                            for (int i = 0; i < 10; i++)
                            {
                                Instantiate(enemy4, new Vector3(Random.Range(-200, -100), -4, Random.Range(200, 120)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;
                    /*case 2:
                        if (spawn)
                        {
                            enemyCount = 20;
                            for (int i = 0; i < 20; i++)
                            {
                                Instantiate(enemy4, new Vector3(Random.Range(-200, -100), -4, Random.Range(200, 120)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;
                    case 3:
                        if (spawn)
                        {
                            enemyCount = 30;
                            for (int i = 0; i < 30; i++)
                            {
                                Instantiate(enemy4, new Vector3(Random.Range(-200, -100), -4, Random.Range(200, 120)), Quaternion.identity);
                            }
                            spawn = false;
                        }
                        break;*/
                }
                break;
        }
    }
}
