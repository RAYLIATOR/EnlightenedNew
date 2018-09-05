using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Leaderboard : MonoBehaviour
{
    //leaderboard times texts
    public Text[] times;
    //timer UI
    public Text timerText;
    //triggers timer
    public bool count;
    //records elapsed time
    float timer;

    //leaderboard
    List<float> leaderBoard;

    //File IO
    string destination;

	void Start ()
    {
        //File IO
        destination = @"C:\UnityProjects\Enlightened Main\Glow\Assets\Leaderboard File\file.txt";

        //Open / Create File
        FileStream file;
        //Timer
        timer = 0;

        //leaderboard

        if (!File.Exists(destination))
        {
            file = File.Create(destination);
        }
        else
        {
            file = File.OpenWrite(destination);
            file.Close();
        }
        Load();
        //for (int i = 0; i < leaderBoard.Count; i++)
        //{
        //    print(leaderBoard[i]);
        //}
    }
	
	void Update ()
    {
        //start counting
		if(count)
        {
            timer += Time.deltaTime;
        }
        timerText.text = timer.ToString("##.##")+"s";
	}

    public void Load()
    {
        count = false;

        FileStream file;
        if(!File.Exists(destination))
        {
            return;
        }
        else
        {
            file = File.OpenRead(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();

        leaderBoard = (List<float>)bf.Deserialize(file);
        file.Close();

        if (timer > 0)
        {
            Sort(timer);
        }
    }

    void Sort(float timer)
    {
        leaderBoard.Add(timer);

        int lIndex;
        float lowest, temp;

        for (int i = 0; i < leaderBoard.Count; i++)
        {
            lowest = leaderBoard[i];
            lIndex = i;
            for(int j = i+1; j < leaderBoard.Count; j++)
            {
                if(lowest > leaderBoard[j])
                {
                    lowest = leaderBoard[j];
                    lIndex = j;
                }
            }
            temp = leaderBoard[i];
            leaderBoard[i] = leaderBoard[lIndex];
            leaderBoard[lIndex] = temp;
        }
        if (leaderBoard.Count > 5)
        {
            leaderBoard.Remove(leaderBoard[leaderBoard.Count - 1]);
        }
        for(int i = 0; i < leaderBoard.Count; i++)
        {
            times[i].text = leaderBoard[i].ToString("##.##");
            //print(leaderBoard[i]);
        }
        Save();
    }

    void Save()
    {
        FileStream file;
        if(!File.Exists(destination))
        {
            file = File.Create(destination);
        }
        else
        {
            file = File.OpenWrite(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(file, leaderBoard);
        file.Close();
    }

}
