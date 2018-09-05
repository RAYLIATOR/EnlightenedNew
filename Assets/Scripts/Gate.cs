using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{    
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<WaveSpawner>().NextArea();
            Destroy(gameObject);
        }
    }
    void Start ()
    {

	}
	
	void Update ()
    {
		
	}

    public void Collide()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}
