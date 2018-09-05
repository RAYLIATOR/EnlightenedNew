using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathExplosion;
    WaveSpawner waveSpawner;
    Rigidbody rb;
    public Light glow;
    public Animator enemyAs;
    GameObject target;
    float health;
    float MaxSpeed;
    float moveForce;
    bool spawn;
    float distToPlayer;
    float chaseRange;
    float attackRange;
    bool glowing;
    bool despawn;
    float attackRate;
    float nextTimeToAttack;
    List<Enemy> neighbours;

	void Start ()
    {
        neighbours = new List<Enemy>();
        //Assign stats based on enemy type
        if (gameObject.tag == "Enemy1")
        {
            moveForce = 500f;
            MaxSpeed = 20f;
            attackRate = .5f;
            attackRange = 5f;
            chaseRange = 50f;
            health = 50f;
            MaxSpeed = 0.2f;
        }
        if (gameObject.tag == "Enemy2")
        {
            moveForce = 300f;
            MaxSpeed = 1f;
            attackRate = .5f;
            attackRange = 5f;
            chaseRange = 50f;
            health = 50f;
            MaxSpeed = 0.2f;
        }
        if (gameObject.tag == "Enemy3")
        {
            moveForce = 300f;
            MaxSpeed = 1f;
            attackRate = .5f;
            attackRange = 5f;
            chaseRange = 50f;
            health = 50f;
            MaxSpeed = 0.1f;
        }
        if (gameObject.tag == "Enemy4")
        {
            moveForce = 300f;
            MaxSpeed = 1f;
            attackRate = .5f;
            attackRange = 5f;
            chaseRange = 50f;
            health = 200f;
            MaxSpeed = 0.1f;
        }
        waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
        enemyAs.SetTrigger("Roar");
        rb = GetComponent<Rigidbody>();
        nextTimeToAttack = 0;
        despawn = false;
        glowing = true;
        target = GameObject.FindGameObjectWithTag("Player");
        spawn = true;
	}
	
	void Update ()
    {
        distToPlayer = Vector3.Distance(target.transform.position, transform.position);
        Spawn();
        Chase();
        if(!glowing)
        {
            glow.intensity -= 0.03f;
        }
        if(despawn)
        {
            GetComponent<BoxCollider>().isTrigger = true;
            transform.position += Vector3.down * 0.03f;
        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
    }

    public void TakeDamage()
    {
        health -= 50;
        if(health==0)
        {
            Die();
            waveSpawner.GetComponent<WaveSpawner>().enemyCount -= 1;
        }
        else
        {
            enemyAs.SetTrigger("Hit");
        }
    }

    void Die()
    {
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        GetComponent<BoxCollider>().isTrigger = true;
        rb.isKinematic = true;
        glowing = false;
        rb.velocity = Vector3.zero;
        enemyAs.SetTrigger("Die");
        Invoke("Despawn", 2.3f);
        Destroy(gameObject, 8f);
        neighbours.Remove(this);
        neighbours.AddRange(FindObjectsOfType<Enemy>());
    }

    void Attack()
    {
        enemyAs.SetTrigger("Attack");
        if (gameObject.tag == "Enemy1")
        {
            target.GetComponent<Player>().LoseHealth(1);
        }
        if (gameObject.tag == "Enemy2")
        {
            target.GetComponent<Player>().LoseHealth(5);
        }
        if (gameObject.tag == "Enemy3")
        {
            target.GetComponent<Player>().LoseHealth(10);
        }
        if (gameObject.tag == "Enemy4")
        {
            target.GetComponent<Player>().LoseHealth(30);
        }
    }

    void Chase()
    {
        if(!spawn && distToPlayer <= chaseRange)
        {
            if (distToPlayer <= attackRange && Time.time >= nextTimeToAttack)
            {
                Attack();
                nextTimeToAttack = Time.time + 1 / attackRate;
            }
            else if(health > 0)
            {
                enemyAs.SetTrigger("Walk");
                transform.LookAt(target.transform);
                rb.AddForce(transform.forward * moveForce + Combine()* 100);
            }
        }
        else if (distToPlayer > chaseRange && !spawn)
        {
            enemyAs.SetTrigger("Idle");
        }
    }
    void Spawn()
    {
        if(transform.position.y<0 && !despawn)
        {
            spawn = true;
            transform.position += Vector3.up * 0.03f;
        }
        else
        {
            spawn = false;
            rb.useGravity = true;
        }
    }
    void Despawn()
    {
        despawn = true;
    }

    //Flocking
    virtual protected Vector3 Combine()
    {
        Vector3 finalVec = 10 * Cohesion() + 10 * Alignment() + 10 * Separation();
        return finalVec;
    }
    Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;
        if (neighbours.Count == 0)
        {
            return cohesionVector;
        }
        foreach (var member in neighbours)
        {
            cohesionVector += member.transform.position;
            countMembers++;
        }
        if (countMembers == 0)
        {
            return cohesionVector;
        }
        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - this.transform.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    Vector3 Alignment()
    {
        Vector3 alignVector = new Vector3();
        if (neighbours.Count == 0)
        {
            return alignVector;
        }
        foreach (var member in neighbours)
        {
            alignVector += member.GetComponent<Rigidbody>().velocity;
        }
        return alignVector.normalized;
    }

    Vector3 Separation()
    {
        Vector3 separateVector = new Vector3();
        if (neighbours.Count == 0)
        {
            return separateVector;
        }
        foreach (var member in neighbours)
        {
            Vector3 movingTowards = this.transform.position - member.transform.position;
            if (movingTowards.magnitude > 0)
            {
                separateVector += movingTowards.normalized / movingTowards.magnitude;
            }
        }
        return separateVector;
    }

}
