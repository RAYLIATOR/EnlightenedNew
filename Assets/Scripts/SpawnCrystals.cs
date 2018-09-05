using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrystals : MonoBehaviour
{
    public GameObject flame;
    //int type;
    void Start ()
    {
        SpawnObjects(flame, 25, 210, 80, 115, 210);
	}
	
	void Update ()
    {
		
	}

    void SpawnObjects(GameObject obj, int number, float x1, float x2, float y1, float y2)
    {
        for (int i = 0; i <= number; i++)
        {
            Instantiate(obj, new Vector3(Random.Range(-x1, -x2), 0, Random.Range(y1, y2)), Quaternion.identity);
        }
    }
}
