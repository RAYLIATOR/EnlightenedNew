using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform memberPrefab;
    public int numberOfMembers;
    public List<Member> members;
    public float bounds;
    public float spawnRadius;

	void Start ()
    {
        members = new List<Member>();
        //Spawn(memberPrefab, numberOfMembers);
        //Spawn(enemyPrefab, numberOfEnemies);

        members.AddRange(FindObjectsOfType<Member>());
    }
	
    void Spawn(Transform prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
        }
    }

    public List<Member> GetNeighbours(Member member, float radius)
    {
        List<Member> neighboursFound = new List<Member>();

        foreach(var otherMember in members)
        {
            if(otherMember == member)
            {
                continue;
            }
            if(Vector3.Distance(member.transform.position, otherMember.transform.position)<=radius)
            {
                neighboursFound.Add(otherMember);
            }

        }
        return neighboursFound;
    }
}
